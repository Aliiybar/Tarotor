using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarotor.Migrations
{
    public partial class SocialMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Twitter = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Facebook = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Instagram = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Youtube = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedia", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialMedia");
        }
    }
}
