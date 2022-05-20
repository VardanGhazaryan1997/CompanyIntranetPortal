using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyIntranetPortal.Infrastructure.Migrations
{
    public partial class addedapplicationstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationState",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationState",
                table: "Applications");
        }
    }
}
