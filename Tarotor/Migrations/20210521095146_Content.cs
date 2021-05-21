using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarotor.Migrations
{
    public partial class Content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TemplateSubject",
                table: "Template",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ContentName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ContentTitle = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Language = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ContentBody = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.DropColumn(
                name: "TemplateSubject",
                table: "Template");
        }
    }
}
