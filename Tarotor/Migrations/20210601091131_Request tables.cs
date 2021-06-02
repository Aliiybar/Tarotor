using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarotor.Migrations
{
    public partial class Requesttables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialMedia");

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    UserId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    TarotType = table.Column<int>(type: "int", nullable: false),
                    Reference = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Questions = table.Column<int>(type: "int", nullable: false),
                    CardsChosenBy = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CommentId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    QuestionsAsked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    QuestionsAnswered = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestProfile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    RequestId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    FirstName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Photo = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestProfile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Response",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    RequestId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ResponseDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Commentator = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Comment = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponseQuestion",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    RequestId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Question = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    QuestionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseQuestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponseQuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    RequestId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ResponseQuestionId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    AnswerDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Answer = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseQuestionAnswer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SelectedCard",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    CardName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    RequestId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedCard_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedCard_RequestId",
                table: "SelectedCard",
                column: "RequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestProfile");

            migrationBuilder.DropTable(
                name: "Response");

            migrationBuilder.DropTable(
                name: "ResponseQuestion");

            migrationBuilder.DropTable(
                name: "ResponseQuestionAnswer");

            migrationBuilder.DropTable(
                name: "SelectedCard");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Facebook = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Instagram = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Twitter = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Youtube = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedia", x => x.Id);
                });
        }
    }
}
