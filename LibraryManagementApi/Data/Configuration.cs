using LibraryManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Data
{
    public class Configuration
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>().HasData(
                 new User() { Id = 1, Username = "admin", Password = "1a1dc91c907325c69271ddf0c944bc72", Role = AuthorizationPolicies.Admin },
                 new User() { Id = 2, Username = "John.Snow", Password = "253d405ed295f2c55d6f3ddf455f31e4", Role = AuthorizationPolicies.User }
                 );
            // Author
            modelBuilder.Entity<Author>().HasData(
                new Author() { Id = 1, Name = "Jane Austen" },
                new Author() { Id = 2, Name = "Harper Lee" },
                new Author() { Id = 3, Name = "George Orwell" },
                new Author() { Id = 4, Name = "F. Scott Fitzgerald" },
                new Author() { Id = 5, Name = "Herman Melville" },
                new Author() { Id = 6, Name = "Charlotte Brontë" },
                new Author() { Id = 7, Name = "J.D. Salinger" },
                new Author() { Id = 8, Name = "Virginia Woolf" },
                new Author() { Id = 9, Name = "Aldous Huxley" },
                new Author() { Id = 12, Name = "J.K. Rowling" },
                new Author() { Id = 13, Name = "J.R.R. Tolkien" },
                new Author() { Id = 14, Name = "Lewis Carroll" },
                new Author() { Id = 15, Name = "C.S. Lewis" },
                new Author() { Id = 16, Name = "Miguel de Cervantes" },
                new Author() { Id = 17, Name = "Fyodor Dostoevsky" },
                new Author() { Id = 18, Name = "Homer" },
                new Author() { Id = 19, Name = "Mary Shelley" },
                new Author() { Id = 20, Name = "Emily Brontë" }
                 );
            // Book
            modelBuilder.Entity<Book>().HasData(
                new Book() { Id = 1, AuthorId = 1, Title = "Pride and Prejudice", Stock = 4 },
                new Book() { Id = 2, AuthorId = 2, Title = "To Kill a Mockingbird", Stock = 8 },
                new Book() { Id = 3, AuthorId = 3, Title = "1984", Stock = 6 },
                new Book() { Id = 4, AuthorId = 4, Title = "The Great Gatsby", Stock = 2 },
                new Book() { Id = 5, AuthorId = 5, Title = "Moby-Dick", Stock = 10 },
                new Book() { Id = 6, AuthorId = 6, Title = "Jane Eyre", Stock = 3 },
                new Book() { Id = 7, AuthorId = 7, Title = "The Catcher in the Rye", Stock = 5 },
                new Book() { Id = 8, AuthorId = 8, Title = "To the Lighthouse", Stock = 7 },
                new Book() { Id = 9, AuthorId = 9, Title = "Brave New World", Stock = 9 },
                new Book() { Id = 10, AuthorId = 13, Title = "The Lord of the Rings", Stock = 12 },
                new Book() { Id = 12, AuthorId = 12, Title = "Harry Potter and the Sorcerer's Stone", Stock = 15 },
                new Book() { Id = 13, AuthorId = 13, Title = "The Hobbit", Stock = 11 },
                new Book() { Id = 14, AuthorId = 14, Title = "Alice's Adventures in Wonderland", Stock = 6 },
                new Book() { Id = 15, AuthorId = 15, Title = "The Chronicles of Narnia", Stock = 8 },
                new Book() { Id = 16, AuthorId = 16, Title = "Don Quixote", Stock = 4 },
                new Book() { Id = 17, AuthorId = 17, Title = "Crime and Punishment", Stock = 7 },
                new Book() { Id = 18, AuthorId = 18, Title = "The Odyssey", Stock = 5 },
                new Book() { Id = 19, AuthorId = 19, Title = "Frankenstein", Stock = 3 },
                new Book() { Id = 20, AuthorId = 20, Title = "Wuthering Heights", Stock = 6 }
                 );

            // BookRentals
        }
    }
}
