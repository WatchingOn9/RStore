using Microsoft.EntityFrameworkCore.Migrations;

namespace RStore.Migrations
{
    public partial class EmailFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailFormat",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailHeader",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CartLine",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailFormat",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "EmailHeader",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Signature",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartLine");
        }
    }
}
