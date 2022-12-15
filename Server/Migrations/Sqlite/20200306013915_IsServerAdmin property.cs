using Microsoft.EntityFrameworkCore.Migrations;

namespace Rimot.Server.Migrations.Sqlite
{
    public partial class IsServerAdminproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsServerAdmin",
                table: "RimotUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsServerAdmin",
                table: "RimotUsers");
        }
    }
}
