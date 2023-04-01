using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Saturn.Migrations
{
    public partial class Added_CompanyProfile_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessBrief",
                table: "CompanyInfo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyLogo",
                table: "CompanyInfo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NatureOfBusinessId",
                table: "CompanyInfo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrimaryEndCustomerId",
                table: "CompanyInfo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrimaryIndustryId",
                table: "CompanyInfo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondaryIndustryId",
                table: "CompanyInfo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "CompanyInfo",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyRevenue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FinancialYear = table.Column<int>(type: "integer", nullable: false),
                    Revenue = table.Column<double>(type: "double precision", nullable: false),
                    CompanyInfoId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_CompanyRevenue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyRevenue_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTeamMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Designation = table.Column<string>(type: "text", nullable: true),
                    Department = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CompanyInfoId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_CompanyTeamMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTeamMember_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTopCompetitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompetitorName = table.Column<string>(type: "text", nullable: true),
                    CompetitorLocation = table.Column<string>(type: "text", nullable: true),
                    CompanyInfoId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_CompanyTopCompetitor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTopCompetitor_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTopCustomer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerName = table.Column<string>(type: "text", nullable: true),
                    CustomerCategory = table.Column<string>(type: "text", nullable: true),
                    CompanyInfoId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_CompanyTopCustomer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTopCustomer_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTopMarket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegionName = table.Column<string>(type: "text", nullable: true),
                    CompanyInfoId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_CompanyTopMarket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTopMarket_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTopProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "text", nullable: true),
                    ProductCategory = table.Column<string>(type: "text", nullable: true),
                    CompanyInfoId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_CompanyTopProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTopProduct_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyProblem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Problem = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_KeyProblem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NatureOfBusiness",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BusinessActivity = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_NatureOfBusiness", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryEndCustomer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Customer = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_PrimaryEndCustomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryIndustry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrimaryIndustryName = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_PrimaryIndustry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTopCompetitorRevenue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FinancialYear = table.Column<int>(type: "integer", nullable: false),
                    Revenue = table.Column<double>(type: "double precision", nullable: false),
                    CompanyTopCompetitorId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_CompanyTopCompetitorRevenue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTopCompetitorRevenue_CompanyTopCompetitor_CompanyTop~",
                        column: x => x.CompanyTopCompetitorId,
                        principalTable: "CompanyTopCompetitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTopCustomerRevenue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FinancialYear = table.Column<int>(type: "integer", nullable: false),
                    Revenue = table.Column<double>(type: "double precision", nullable: false),
                    CompanyTopCustomerId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_CompanyTopCustomerRevenue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTopCustomerRevenue_CompanyTopCustomer_CompanyTopCust~",
                        column: x => x.CompanyTopCustomerId,
                        principalTable: "CompanyTopCustomer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTopMarketRevenue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FinancialYear = table.Column<int>(type: "integer", nullable: false),
                    Revenue = table.Column<double>(type: "double precision", nullable: false),
                    CompanyTopMarketId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_CompanyTopMarketRevenue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTopMarketRevenue_CompanyTopMarket_CompanyTopMarketId",
                        column: x => x.CompanyTopMarketId,
                        principalTable: "CompanyTopMarket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTopProductRevenue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FinancialYear = table.Column<int>(type: "integer", nullable: false),
                    Revenue = table.Column<double>(type: "double precision", nullable: false),
                    CompanyTopProductId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_CompanyTopProductRevenue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTopProductRevenue_CompanyTopProduct_CompanyTopProduc~",
                        column: x => x.CompanyTopProductId,
                        principalTable: "CompanyTopProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyKeyProblem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    KeyProblemId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_CompanyKeyProblem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyKeyProblem_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyKeyProblem_KeyProblem_KeyProblemId",
                        column: x => x.KeyProblemId,
                        principalTable: "KeyProblem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecondaryIndustry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SecondaryIndustryName = table.Column<string>(type: "text", nullable: true),
                    PrimaryIndustryId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_SecondaryIndustry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondaryIndustry_PrimaryIndustry_PrimaryIndustryId",
                        column: x => x.PrimaryIndustryId,
                        principalTable: "PrimaryIndustry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfo_NatureOfBusinessId",
                table: "CompanyInfo",
                column: "NatureOfBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfo_PrimaryEndCustomerId",
                table: "CompanyInfo",
                column: "PrimaryEndCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfo_PrimaryIndustryId",
                table: "CompanyInfo",
                column: "PrimaryIndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfo_SecondaryIndustryId",
                table: "CompanyInfo",
                column: "SecondaryIndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyKeyProblem_CompanyInfoId",
                table: "CompanyKeyProblem",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyKeyProblem_KeyProblemId",
                table: "CompanyKeyProblem",
                column: "KeyProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRevenue_CompanyInfoId",
                table: "CompanyRevenue",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTeamMember_CompanyInfoId",
                table: "CompanyTeamMember",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTopCompetitor_CompanyInfoId",
                table: "CompanyTopCompetitor",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTopCompetitorRevenue_CompanyTopCompetitorId",
                table: "CompanyTopCompetitorRevenue",
                column: "CompanyTopCompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTopCustomer_CompanyInfoId",
                table: "CompanyTopCustomer",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTopCustomerRevenue_CompanyTopCustomerId",
                table: "CompanyTopCustomerRevenue",
                column: "CompanyTopCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTopMarket_CompanyInfoId",
                table: "CompanyTopMarket",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTopMarketRevenue_CompanyTopMarketId",
                table: "CompanyTopMarketRevenue",
                column: "CompanyTopMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTopProduct_CompanyInfoId",
                table: "CompanyTopProduct",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTopProductRevenue_CompanyTopProductId",
                table: "CompanyTopProductRevenue",
                column: "CompanyTopProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryIndustry_PrimaryIndustryId",
                table: "SecondaryIndustry",
                column: "PrimaryIndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInfo_NatureOfBusiness_NatureOfBusinessId",
                table: "CompanyInfo",
                column: "NatureOfBusinessId",
                principalTable: "NatureOfBusiness",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInfo_PrimaryEndCustomer_PrimaryEndCustomerId",
                table: "CompanyInfo",
                column: "PrimaryEndCustomerId",
                principalTable: "PrimaryEndCustomer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInfo_PrimaryIndustry_PrimaryIndustryId",
                table: "CompanyInfo",
                column: "PrimaryIndustryId",
                principalTable: "PrimaryIndustry",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInfo_SecondaryIndustry_SecondaryIndustryId",
                table: "CompanyInfo",
                column: "SecondaryIndustryId",
                principalTable: "SecondaryIndustry",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInfo_NatureOfBusiness_NatureOfBusinessId",
                table: "CompanyInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInfo_PrimaryEndCustomer_PrimaryEndCustomerId",
                table: "CompanyInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInfo_PrimaryIndustry_PrimaryIndustryId",
                table: "CompanyInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInfo_SecondaryIndustry_SecondaryIndustryId",
                table: "CompanyInfo");

            migrationBuilder.DropTable(
                name: "CompanyKeyProblem");

            migrationBuilder.DropTable(
                name: "CompanyRevenue");

            migrationBuilder.DropTable(
                name: "CompanyTeamMember");

            migrationBuilder.DropTable(
                name: "CompanyTopCompetitorRevenue");

            migrationBuilder.DropTable(
                name: "CompanyTopCustomerRevenue");

            migrationBuilder.DropTable(
                name: "CompanyTopMarketRevenue");

            migrationBuilder.DropTable(
                name: "CompanyTopProductRevenue");

            migrationBuilder.DropTable(
                name: "NatureOfBusiness");

            migrationBuilder.DropTable(
                name: "PrimaryEndCustomer");

            migrationBuilder.DropTable(
                name: "SecondaryIndustry");

            migrationBuilder.DropTable(
                name: "KeyProblem");

            migrationBuilder.DropTable(
                name: "CompanyTopCompetitor");

            migrationBuilder.DropTable(
                name: "CompanyTopCustomer");

            migrationBuilder.DropTable(
                name: "CompanyTopMarket");

            migrationBuilder.DropTable(
                name: "CompanyTopProduct");

            migrationBuilder.DropTable(
                name: "PrimaryIndustry");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInfo_NatureOfBusinessId",
                table: "CompanyInfo");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInfo_PrimaryEndCustomerId",
                table: "CompanyInfo");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInfo_PrimaryIndustryId",
                table: "CompanyInfo");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInfo_SecondaryIndustryId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "BusinessBrief",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "CompanyLogo",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "NatureOfBusinessId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "PrimaryEndCustomerId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "PrimaryIndustryId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "SecondaryIndustryId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "CompanyInfo");
        }
    }
}
