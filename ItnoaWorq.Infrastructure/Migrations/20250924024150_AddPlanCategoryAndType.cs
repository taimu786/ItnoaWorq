using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItnoaWorq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanCategoryAndType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Category",
                table: "Plans",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "Plans",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Plans");
        }
    }
}
