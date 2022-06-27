using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Data.Migrations
{
    public partial class InStockAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isInStock",
                table: "Products",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isInStock",
                table: "Products");
        }
    }
}
