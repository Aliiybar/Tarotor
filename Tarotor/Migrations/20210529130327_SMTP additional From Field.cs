using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarotor.Migrations
{
    public partial class SMTPadditionalFromField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromEmail",
                table: "Smtp",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromEmail",
                table: "Smtp");
        }
    }
}
