using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookLendsFlag",
                table: "Users",
                newName: "BookRentalOverdue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookRentalOverdue",
                table: "Users",
                newName: "BookLendsFlag");
        }
    }
}
