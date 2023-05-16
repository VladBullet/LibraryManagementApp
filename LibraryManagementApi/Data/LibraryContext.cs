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
        public LibraryContext(IConfiguration config, DbContextOptions<LibraryContext> options) : base(options) { _config = config; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasKey(x => x.Id);
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<User>().HasKey(b => b.Id);

        }
    }
}