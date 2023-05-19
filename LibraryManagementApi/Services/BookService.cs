using AutoMapper;
using LibraryManagementApi;
using LibraryManagementApi.Dto;
using LibraryManagementApi.Models;
using Microsoft.EntityFrameworkCore;


namespace PostgreSQL.Demo.API.Services
{
    public interface IBookService
    {
        /// <summary>
        /// Get all book in database.
        /// </summary>
        /// <returns>All books in database</returns>
        Task<IEnumerable<Book>> GetAllBooksAsync();

        /// <summary>
        /// Get a single book by Id
        /// </summary>
        /// <param name="id">Id of book</param>
        /// <returns>A single book</returns>
        Task<Book> GetBookByIdAsync(int id);

        /// <summary>
        /// Create a new book in the database
        /// </summary>
        /// <param name="model">Create book request model</param>
        Task<int> CreateBook(BookDto model);

        /// <summary>
        /// Update a book in the database if the book already exists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        Task UpdateBook(int id, BookDto model);

        /// <summary>
        /// Delete a single book in the dabase. Will delete the book if the book exists in the database.
        /// </summary>
        /// <param name="id">Id of the book to delete</param>
        Task DeleteBook(int id);
        Task<IEnumerable<Book>> GetAvailableBooksAsync();
        Task<IEnumerable<Book>> GetBooksByIds(IEnumerable<int> bookIds);
    }

    public class BookService : IBookService
    {
        private LibraryContext _dbContext;
        private IMapper _mapper;

        public BookService(LibraryContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateBook(BookDto model)
        {
            // Validate new book
            var bookAuthor = _dbContext.Authors.FirstOrDefault(x => x.Name == model.AuthorName);
            if (bookAuthor == null)
            {
                // we need to add the author
                _dbContext.Authors.Add(new Author { Name = model.AuthorName });

            }
            if (await _dbContext.Books.AnyAsync(x => x.Title == model.Title && x.AuthorId == bookAuthor.Id))
                throw new Exception($"A book with the same name and author already exist in the database!");

            // Map model to new book object
            Book book = _mapper.Map<Book>(model);

            // Save book in database
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);

            return book.Id;
        }

        public async Task DeleteBook(int id)
        {
            Book? book = await _getBookById(id);

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _dbContext.Books
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _getBookById(id);
        }

        public async Task UpdateBook(int id, BookDto model)
        {
            Book? book = await _getBookById(id);

            // Copy model data to book object and save it in the database
            _mapper.Map(model, book);
            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);
        }

        private async Task<Book> _getBookById(int id)
        {
            Book? book = await _dbContext.Books
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync().ConfigureAwait(true);

            if (book == null)
            {
                throw new KeyNotFoundException("Book was not found in database");
            }

            return book;
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            return await _dbContext.Books.Where(book => book.Stock > 0).ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksByIds(IEnumerable<int> bookIds)
        {
            return await _dbContext.Books
                .Where(book => bookIds.Contains(book.Id))
                .ToListAsync();
        }
    }
}
