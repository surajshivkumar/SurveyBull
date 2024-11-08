﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BullSurvey.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Questions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}