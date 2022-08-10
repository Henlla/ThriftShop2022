using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThriftShop.DataAccess.Migrations
{
    public partial class EditProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
