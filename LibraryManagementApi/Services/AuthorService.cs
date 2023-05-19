using AutoMapper;
using LibraryManagementApi;
using LibraryManagementApi.Dto;
using LibraryManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Npgsql;

namespace PostgreSQL.Demo.API.Services
{
    public interface IAuthorService
    {
        /// <summary>
        /// Get all authors in database. Set includeBooks to true if you want to include all books made by the authors.
        /// </summary>
        /// <param name="includeBooks">Optional parameter to include books</param>
        /// <returns>All authors in database</returns>
        Task<IEnumerable<Author>> GetAllAuthorsAsync(bool includeBooks = false);

        /// <summary>
        /// Get a single authors by Id and include books if requested by the includeBooks boolean.
        /// </summary>
        /// <param name="id">Id of Author</param>
        /// <param name="includeBooks">Optional parameter to include books</param>
        /// <returns>A single authors</returns>
        Task<Author> GetAuthorByIdAsync(int id, bool includeBooks = false);

        /// <summary>
        /// Create a new authors in the database
        /// </summary>
        /// <param name="model">Create Author request model</param>
        Task<int> CreateAuthor(AuthorDto model);

        /// <summary>
        /// Update an authors in the database if the authors already exists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        Task UpdateAuthor(int id, AuthorDto model);

        /// <summary>
        /// Delete a single authors in the dabase. Will delete the authors if the authors exists in the database.
        /// Cascading is enabled and will delete the authors books from the database at the same time. Use with caution.
        /// </summary>
        /// <param name="id">Id of the authors to delete</param>
        Task DeleteAuthor(int id);
        Task<IEnumerable<Author>> GetAuthorsByNameAsync(string name, bool includeBooks);
    }

    public class AuthorService : IAuthorService
    {
        private LibraryContext _dbContext;
        private readonly IConfiguration _config;
        private IMapper _mapper;

        public AuthorService(LibraryContext dbContext, IMapper mapper, IConfiguration config)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _config = config;
        }

        public async Task<int> CreateAuthor(AuthorDto model)
        {
            // Validate new authors
            if (await _dbContext.Authors.AnyAsync(x => x.Name == model.Name))
                throw new Exception($"An author with the same name {model.Name} already exists.");

            // Map model to new authors object
            Author author = _mapper.Map<Author>(model);

            // Save Author
            _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);

            if (author != null)
            {
                return author.Id; // Author got created
            }

            return 0;

        }

        public async Task DeleteAuthor(int id)
        {
            Author? author = await GetAuthorById(id);

            _dbContext.Authors.Remove(author); // Delete the authors and books (Cascading is enabled)
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync(bool includeBooks = false)
        {
            if (includeBooks)
            {
                // Get all authors and their books
                return await _dbContext.Authors
                    .Include(b => b.Books)
                    .ToListAsync().ConfigureAwait(true);
            }
            else
            {
                // Get all authors without including the books
                return await _dbContext.Authors
                    .ToListAsync().ConfigureAwait(true);
            }
        }

        public async Task<Author> GetAuthorByIdAsync(int id, bool includeBooks = false)
        {
            return await GetAuthorById(id, includeBooks).ConfigureAwait(true);
        }

        public async Task<IEnumerable<Author>> GetAuthorsByNameAsync(string name, bool includeBooks)
        {
            using (var connection = new NpgsqlConnection(_config.GetConnectionString("PostgreSQL")))
            {
                connection.Open();

                var normalizedNames = name.Split(' ').Select(n => $"%{n.ToLower()}%").ToArray();

                string sql = @"SELECT a.""Id"", a.""Name"", 
                                      b.""Id"", b.""Title"", b.""AuthorId"", b.""Stock""
                                FROM ""public"".""Authors"" a
                                LEFT JOIN ""public"".""Books"" b ON a.""Id"" = b.""AuthorId""
                                WHERE LOWER(a.""Name"") LIKE ANY(@namePatterns)";

                if (!includeBooks)
                {
                    sql = @"SELECT a.""Id"", a.""Name"" FROM ""public"".""Authors"" a
                WHERE LOWER(a.""Name"") LIKE ANY(@namePatterns)";
                    return await connection.QueryAsync<Author>(sql, new { namePatterns = normalizedNames });
                }


                var authorsDictionary = new Dictionary<int, Author>();
                var authorsFilteredByName = await connection.QueryAsync<Author, Book, Author>(
                    sql,
                    (author, book) =>
                    {
                        if (!authorsDictionary.TryGetValue(author.Id, out var authorEntry))
                        {
                            authorEntry = author;
                            authorEntry.Books = new List<Book>();
                            authorsDictionary.Add(authorEntry.Id, authorEntry);
                        }

                        if (includeBooks && book != null)
                            authorEntry.Books.Add(book);

                        return authorEntry;
                    },
                    new { namePatterns = normalizedNames },
                    splitOn: includeBooks ? "Id, Id" : "Id")
                    .ConfigureAwait(true);

                return authorsFilteredByName.Distinct().ToList();
            }

        }


        public async Task UpdateAuthor(int id, AuthorDto model)
        {
            Author? author = await GetAuthorById(id).ConfigureAwait(true);

            // copy model to authors and save
            _mapper.Map(model, author);
            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();

        }

        /// <summary>
        /// Get a single authors and the books if requested. Looks in the database for an authors and returns null, if the authors did not exist.
        /// </summary>
        /// <param name="id">Author ID</param>
        /// <param name="includeBooks">True to include books</param>
        /// <returns>A single authors</returns>
        private async Task<Author> GetAuthorById(int id, bool includeBooks = false)
        {
            if (includeBooks)
            {
                Author? author = await _dbContext.Authors
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Include(b => b.Books)
                    .FirstOrDefaultAsync().ConfigureAwait(true);

                if (author == null)
                {
                    throw new KeyNotFoundException("Author not found");
                }

                return author;
            }
            else
            {
                Author? author = await _dbContext.Authors
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync().ConfigureAwait(true);

                if (author == null)
                {
                    throw new KeyNotFoundException("Author not found");
                }

                return author;
            }
        }
    }
}
