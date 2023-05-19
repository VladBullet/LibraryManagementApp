using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Jane Austen" },
                    { 2, "Harper Lee" },
                    { 3, "George Orwell" },
                    { 4, "F. Scott Fitzgerald" },
                    { 5, "Herman Melville" },
                    { 6, "Charlotte Brontë" },
                    { 7, "J.D. Salinger" },
                    { 8, "Virginia Woolf" },
                    { 9, "Aldous Huxley" },
                    { 12, "J.K. Rowling" },
                    { 13, "J.R.R. Tolkien" },
                    { 14, "Lewis Carroll" },
                    { 15, "C.S. Lewis" },
                    { 16, "Miguel de Cervantes" },
                    { 17, "Fyodor Dostoevsky" },
                    { 18, "Homer" },
                    { 19, "Mary Shelley" },
                    { 20, "Emily Brontë" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BookLendsFlag", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, false, "1a1dc91c907325c69271ddf0c944bc72", "Admin", "admin" },
                    { 2, false, "253d405ed295f2c55d6f3ddf455f31e4", "User", "John.Snow" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Stock", "Title" },
                values: new object[,]
                {
                    { 1, 1, 4, "Pride and Prejudice" },
                    { 2, 2, 8, "To Kill a Mockingbird" },
                    { 3, 3, 6, "1984" },
                    { 4, 4, 2, "The Great Gatsby" },
                    { 5, 5, 10, "Moby-Dick" },
                    { 6, 6, 3, "Jane Eyre" },
                    { 7, 7, 5, "The Catcher in the Rye" },
                    { 8, 8, 7, "To the Lighthouse" },
                    { 9, 9, 9, "Brave New World" },
                    { 10, 13, 12, "The Lord of the Rings" },
                    { 12, 12, 15, "Harry Potter and the Sorcerer's Stone" },
                    { 13, 13, 11, "The Hobbit" },
                    { 14, 14, 6, "Alice's Adventures in Wonderland" },
                    { 15, 15, 8, "The Chronicles of Narnia" },
                    { 16, 16, 4, "Don Quixote" },
                    { 17, 17, 7, "Crime and Punishment" },
                    { 18, 18, 5, "The Odyssey" },
                    { 19, 19, 3, "Frankenstein" },
                    { 20, 20, 6, "Wuthering Heights" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
