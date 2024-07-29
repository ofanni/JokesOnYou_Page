using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tags",
                newName: "JokeQuestion");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "Tags",
                newName: "JokeAnswer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JokeQuestion",
                table: "Tags",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "JokeAnswer",
                table: "Tags",
                newName: "DisplayName");
        }
    }
}
