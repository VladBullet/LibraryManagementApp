using AutoMapper;
using LibraryManagementApi;
using LibraryManagementApi.Dto;
using LibraryManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Demo.API.Services;

namespace PostgreSQL.Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        // GET: api/<BooksController>
        [HttpGet("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            IEnumerable<Book> books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("getFilteredBooks/{title}")]
        public async Task<IActionResult> GetBooksFilteredByTitle(string title, bool includeAuthor = true)
        {
            var books = await _bookService.GetBooksFilteredByTitle(title, includeAuthor);
            return Ok(books);
        }

        // GET api/<BooksController>/5
        [HttpGet("getBookById/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            Book book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);
        }

        // POST api/<BooksController>
        [Authorize(Policy = AuthorizationPolicies.Admin)]
        [HttpPost("createBook")]
        public async Task<IActionResult> CreateBook(BookDto model)
        {
            int book = await _bookService.CreateBook(model);

            if (book != 0)
            {
                return Ok("The book was successfully added to the database");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "The book was successfully added to the database");
        }

        // PUT api/<BooksController>/5
        [Authorize(Policy = AuthorizationPolicies.Admin)]
        [HttpPut("updateBook/{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookDto model)
        {
            await _bookService.UpdateBook(id, model);
            return Ok("The book was successfully updated in the database");
        }

        // DELETE api/<BooksController>/5
        [Authorize(Policy = AuthorizationPolicies.Admin)]
        [HttpDelete("deleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);
            return Ok("The book was successfully deleted in the database");
        }
    }
}
