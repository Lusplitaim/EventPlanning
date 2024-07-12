using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanning.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxMembersCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxMembersCount",
                table: "Events",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxMembersCount",
                table: "Events");
        }
    }
}
