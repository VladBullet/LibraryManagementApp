using LibraryManagementApi;
using LibraryManagementApi.Dto;
using LibraryManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Demo.API.Services;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostgreSQL.Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/<AuthorController>
        [HttpGet("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            IEnumerable<Author> authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("getAuthorById/{id}")]
        public async Task<IActionResult> GetAuthorById(int id, bool includeBooks = false)
        {
            Author author = await _authorService.GetAuthorByIdAsync(id, includeBooks);
            return Ok(author);
        }


        [HttpGet("getAuthorsByName/{name}")]
        public async Task<IActionResult> GetAuthorsByName(string name, bool includeBooks = false)
        {
            try
            {
                var authors = await _authorService.GetAuthorsByNameAsync(name, includeBooks);
                return Ok(authors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<AuthorController>
        [Authorize(Policy = AuthorizationPolicies.Admin)]
        [HttpPost("createAuthor")]
        public async Task<IActionResult> CreateAuthor(AuthorDto model)
        {
            int authorId = await _authorService.CreateAuthor(model);

            if (authorId != 0)
            {
                return Ok(new { message = $"Author was successfully created in database with the id {authorId}" });
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "The author was not created in the database.");
        }

        // PUT api/<AuthorController>/5
        [Authorize(Policy = AuthorizationPolicies.Admin)]
        [HttpPut("updateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorDto model)
        {
            await _authorService.UpdateAuthor(id, model);
            return Ok(new { message = "Author was successfully updated in database" });

        }

        // DELETE api/<AuthorController>/5
        [Authorize(Policy = AuthorizationPolicies.Admin)]
        [HttpDelete("deleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthor(id);
            return Ok(new { message = "Author was successfully deleted in database" });

        }
    }
}
