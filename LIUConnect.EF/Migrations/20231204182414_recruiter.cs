using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LIUConnect.EF.Migrations
{
    /// <inheritdoc />
    public partial class recruiter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Recruiters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "officialFiles",
                table: "Recruiters",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Recruiters");

            migrationBuilder.DropColumn(
                name: "officialFiles",
                table: "Recruiters");
        }
    }
}
