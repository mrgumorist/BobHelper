using Microsoft.EntityFrameworkCore.Migrations;

namespace CasualHub.DAL.Migrations
{
    public partial class Updawe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsComplited",
                table: "Tasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplited",
                table: "Tasks");
        }
    }
}
