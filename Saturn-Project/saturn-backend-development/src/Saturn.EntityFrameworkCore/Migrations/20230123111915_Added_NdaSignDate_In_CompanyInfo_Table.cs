using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saturn.Migrations
{
    public partial class Added_NdaSignDate_In_CompanyInfo_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NdaSignDate",
                table: "CompanyInfo",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NdaSignDate",
                table: "CompanyInfo");
        }
    }
}
