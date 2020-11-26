using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolQuizzes.Data.Migrations
{
    public partial class TakeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Takes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    QuizId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Takes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Takes_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Takes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TakedAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false),
                    AnswerId = table.Column<int>(nullable: false),
                    IsCorrect = table.Column<bool>(nullable: false),
                    TakeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakedAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TakedAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TakedAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TakedAnswers_Takes_TakeId",
                        column: x => x.TakeId,
                        principalTable: "Takes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TakedAnswers_AnswerId",
                table: "TakedAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_TakedAnswers_IsDeleted",
                table: "TakedAnswers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TakedAnswers_QuestionId",
                table: "TakedAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TakedAnswers_TakeId",
                table: "TakedAnswers",
                column: "TakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Takes_IsDeleted",
                table: "Takes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Takes_QuizId",
                table: "Takes",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Takes_UserId",
                table: "Takes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TakedAnswers");

            migrationBuilder.DropTable(
                name: "Takes");
        }
    }
}
