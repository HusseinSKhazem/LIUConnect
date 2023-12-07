using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LIUConnect.EF.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resume_StudentID",
                table: "Resume");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_StudentID",
                table: "Resume",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resume_StudentID",
                table: "Resume");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_StudentID",
                table: "Resume",
                column: "StudentID",
                unique: true);
        }
    }
}
