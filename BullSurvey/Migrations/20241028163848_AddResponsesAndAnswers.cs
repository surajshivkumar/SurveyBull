using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BullSurvey.Migrations
{
    public partial class AddResponsesAndAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Responses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Responses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Responses");
        }
    }
}
