using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CompanyOrganizer.Api.Migrations
{
    public partial class AddMeetengTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Meetings",
               columns: table => new
               {
                   Id = table.Column<long>(type: "bigint", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   NameMeeting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   When = table.Column<DateTime>(type: "datetime2", nullable: false),
                   CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                   
                   UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Meetings", x => x.Id);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetings");
        }
    }
}
