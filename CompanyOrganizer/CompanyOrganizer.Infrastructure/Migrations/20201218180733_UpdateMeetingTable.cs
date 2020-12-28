using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyOrganizer.Api.Migrations
{
    public partial class UpdateMeetingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Where",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Where",
                table: "Meetings");
        }
    }
}
