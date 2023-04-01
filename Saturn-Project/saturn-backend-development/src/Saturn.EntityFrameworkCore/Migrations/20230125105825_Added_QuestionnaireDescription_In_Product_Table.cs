using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saturn.Migrations
{
    public partial class Added_QuestionnaireDescription_In_Product_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionnaireDescription",
                table: "Product",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionnaireDescription",
                table: "Product");
        }
    }
}
