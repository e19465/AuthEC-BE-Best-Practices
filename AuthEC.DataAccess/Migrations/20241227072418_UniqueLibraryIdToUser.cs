using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthEC.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UniqueLibraryIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LibraryId",
                table: "AspNetUsers",
                column: "LibraryId",
                unique: true,
                filter: "[LibraryId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LibraryId",
                table: "AspNetUsers");
        }
    }
}
