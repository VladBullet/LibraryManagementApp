using LibraryManagementApi.Data.Models;
using LibraryManagementApi.Dto;
using LibraryManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using PostgreSQL.Demo.API.Services;

namespace LibraryManagementApi.Services
{
    public interface IRentService
    {
        Task<Result<IEnumerable<int>>> RentBooks(int userId, IEnumerable<int> bookIds);

        Task<Result<IEnumerable<int>>> ReturnBooks(int userId, IEnumerable<int> bookIds);
        Task<bool> HasRentalOverdue(int userId);
        Task<IEnumerable<Book>> GetCurrentlyRentedBooks(int userId);
    }

    public class RentService : IRentService
    {
        private LibraryContext _dbContext;
        private IBookService _bookService;
        public RentService(LibraryContext db, IBookService bookService)
        {
            _dbContext = db;
            _bookService = bookService;
        }

        public async Task<Result<IEnumerable<int>>> RentBooks(int userId, IEnumerable<int> bookIds)
        {
            DateTime currentDate = DateTime.Now;

            // Check if the person is eligible for renting
            if (!IsEligibleForRent(userId, bookIds.Count()))
                return Result<IEnumerable<int>>.Failure("User is not eligible for renting books.");

            // Check if all the requested books are available
            var availableBooks = await _bookService.GetAvailableBooksAsync();
            var requestedBooks = _dbContext.Books.Where(book => bookIds.Contains(book.Id)).ToList();
            if (requestedBooks.Count != bookIds.Count() || !AreBooksAvailable(requestedBooks, availableBooks))
            {
                var notAvailableBooks = requestedBooks.Except(availableBooks).ToList();
                return Result<IEnumerable<int>>.Failure($"Some books are not available for rent: {string.Join(", ", notAvailableBooks.Select(x => x.Title))}");
            }

            foreach (var bookId in bookIds)
            {
                var book = requestedBooks.FirstOrDefault(b => b.Id == bookId);
                if (book != null)
                {
                    book.Stock--;
                    var rental = new BookRental
                    {
                        UserId = userId,
                        DateStart = currentDate,
                        ReturnedDate = null,
                        BookId = book.Id
                    };
                    _dbContext.Rentals.Add(rental);
                }
            }

            _dbContext.SaveChanges();

            return Result<IEnumerable<int>>.Success(bookIds);
        }

        private bool IsEligibleForRent(int userId, int bookCount)
        {
            // Check if the person has already rented the maximum number of books
            var rentedBooksCount = _dbContext.Rentals.Count(rental => rental.UserId == userId && rental.ReturnedDate == null);
            if (rentedBooksCount + bookCount > 5)
                return false;

            // Check if the person has any overdue rentals
            var overdueRentalsCount = _dbContext.Rentals.Count(rental => rental.UserId == userId && rental.DateStart.AddDays(30).Date < DateTime.Now.Date && rental.ReturnedDate == null);
            if (overdueRentalsCount > 0)
                return false;

            return true;
        }

        private bool AreBooksAvailable(IEnumerable<Book> requestedBooks, IEnumerable<Book> availableBooks)
        {
            foreach (var book in requestedBooks)
            {
                var availableBook = availableBooks.FirstOrDefault(b => b.Id == book.Id);
                if (availableBook == null || availableBook.Stock <= 0)
                    return false;
            }

            return true;
        }

        public async Task<Result<IEnumerable<int>>> ReturnBooks(int userId, IEnumerable<int> bookIds)
        {
            // find all users rentals where the bookIds match rentals.bookId
            var rentals = await _dbContext.Rentals
                .Where(r => r.UserId == userId && r.ReturnedDate == null && bookIds.Contains(r.BookId)).ToListAsync();

            if (rentals == null)
            {
                // No active rentals found for the user
                return Result<IEnumerable<int>>.Failure("No active rentals found for the user!");
            }

            var booksFromRentals = _dbContext.Books.Where(b => rentals.Select(x => x.BookId).Contains(b.Id)).ToList();
            var returnedBooks = _dbContext.Books
                .Where(b => bookIds.Contains(b.Id) && booksFromRentals.Contains(b))
                .ToList();

            if (returnedBooks.Count != bookIds.Count())
            {
                // TODO: test this logic - RentService - ReturnBooks
                // Not all requested books are part of the rentals
                return Result<IEnumerable<int>>.Failure("Not all requested books are part of the rentals.");
            }

            // Update the stock of returned books
            foreach (var book in returnedBooks)
            {
                book.Stock++;
                var rental = rentals.FirstOrDefault(r => r.BookId == book.Id);
                rental.ReturnedDate = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
            }

            await _dbContext.SaveChangesAsync();

            return Result<IEnumerable<int>>.Success(bookIds);
        }

        public async Task<bool> HasRentalOverdue(int userId)
        {
            var overdueRentalsCount = await _dbContext.Rentals.CountAsync(rental => rental.UserId == userId && rental.DateStart.AddDays(30).Date < DateTime.Now.Date && rental.ReturnedDate == null);
            if (overdueRentalsCount == 0)
                return true;
            return false;
        }

        public async Task<IEnumerable<Book>> GetCurrentlyRentedBooks(int userId)
        {
            var rentedBookIds = await _dbContext.Rentals
                .Where(r => r.UserId == userId && r.ReturnedDate == null)
                .Select(r => r.BookId)
                .ToListAsync();

            var rentedBooks = await _bookService.GetBooksByIds(rentedBookIds);

            return rentedBooks;
        }
    }
}
