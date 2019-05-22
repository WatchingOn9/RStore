using Microsoft.EntityFrameworkCore.Migrations;

namespace RStore.Migrations
{
    public partial class ShowEnquiryForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowEnquiryForm",
                table: "Pages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowEnquiryForm",
                table: "Pages");
        }
    }
}
