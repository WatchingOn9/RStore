using Microsoft.EntityFrameworkCore.Migrations;

namespace RStore.Migrations
{
    public partial class quotationno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuotationNo",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuotationNo",
                table: "Orders");
        }
    }
}
