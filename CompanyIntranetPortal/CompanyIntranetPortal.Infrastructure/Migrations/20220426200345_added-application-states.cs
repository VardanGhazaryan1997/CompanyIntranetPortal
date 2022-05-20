using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyIntranetPortal.Infrastructure.Migrations
{
    public partial class addedapplicationstates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationState",
                table: "VacationApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationState",
                table: "JobLeftApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationState",
                table: "DayOffApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationState",
                table: "BankAccountChangeApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationState",
                table: "VacationApplications");

            migrationBuilder.DropColumn(
                name: "ApplicationState",
                table: "JobLeftApplications");

            migrationBuilder.DropColumn(
                name: "ApplicationState",
                table: "DayOffApplications");

            migrationBuilder.DropColumn(
                name: "ApplicationState",
                table: "BankAccountChangeApplications");
        }
    }
}
