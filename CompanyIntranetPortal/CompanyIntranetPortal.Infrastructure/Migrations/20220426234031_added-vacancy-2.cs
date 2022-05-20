using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyIntranetPortal.Infrastructure.Migrations
{
    public partial class addedvacancy2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PossitionName",
                table: "Vacancies",
                newName: "PositionName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PositionName",
                table: "Vacancies",
                newName: "PossitionName");
        }
    }
}
