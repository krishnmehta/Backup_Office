using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Saturn.Migrations
{
    public partial class Added_PersonalInfoDetails_In_CompanyInfo_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CompanyInfo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyUserType",
                table: "CompanyInfo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfessionalPhoto",
                table: "CompanyInfo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfessionalSummary",
                table: "CompanyInfo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "CompanyInfo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetLine",
                table: "CompanyInfo",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Competency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCompetencyMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetencyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCompetencyMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyCompetencyMappings_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyCompetencyMappings_Competency_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCompetencyMappings_CompanyInfoId",
                table: "CompanyCompetencyMappings",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCompetencyMappings_CompetencyId",
                table: "CompanyCompetencyMappings",
                column: "CompetencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyCompetencyMappings");

            migrationBuilder.DropTable(
                name: "Competency");

            migrationBuilder.DropColumn(
                name: "City",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "CompanyUserType",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "ProfessionalPhoto",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "ProfessionalSummary",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "State",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "StreetLine",
                table: "CompanyInfo");
        }
    }
}
