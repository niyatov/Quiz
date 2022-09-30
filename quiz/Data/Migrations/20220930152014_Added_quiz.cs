using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quiz.Data.Migrations
{
    public partial class Added_quiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    StartTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(64)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_PasswordHash",
                table: "Quizzes",
                column: "PasswordHash",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quizzes");
        }
    }
}
