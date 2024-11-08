using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BullSurvey.Migrations
{
    public partial class AddPublishFieldsToSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Surveys",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Surveys");
        }
    }
}
