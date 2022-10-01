using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quiz.Data.Migrations
{
    public partial class Added_McqOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "McqOptions",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false),
                    QuestionId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    IsTrue = table.Column<bool>(type: "INTEGER", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_McqOptions", x => new { x.Id, x.QuestionId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "McqOptions");
        }
    }
}
