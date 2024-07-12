using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanning.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAdminRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "AspNetRoles",
                new string[] { "Name", "NormalizedName" },
                new string[,] { { "admin", "ADMIN" }, { "user", "USER" } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("AspNetRoles", "Name", ["admin", "user"]);
        }
    }
}
