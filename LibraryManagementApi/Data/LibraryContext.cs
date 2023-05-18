using LibraryManagementApi.Data;
using LibraryManagementApi.Data.Models;
using LibraryManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementApi
{
    public class LibraryContext : DbContext
    {
        protected readonly IConfiguration _config;
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; internal set; }
        public DbSet<BookRental> Rentals { get; internal set; }
        public LibraryContext(IConfiguration config, DbContextOptions<LibraryContext> options) : base(options) { _config = config; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasAlternateKey(x => x.Username);

            // Author
            modelBuilder.Entity<Author>().HasKey(x => x.Id);
            modelBuilder.Entity<Author>().HasAlternateKey(x => x.Name);
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .OnDelete(DeleteBehavior.Cascade);

            // Book
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Book>().HasAlternateKey(b => b.Title);

            Configuration.SeedData(modelBuilder);
        }
    }
}