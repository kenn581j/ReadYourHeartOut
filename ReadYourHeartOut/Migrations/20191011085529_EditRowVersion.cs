using Microsoft.EntityFrameworkCore.Migrations;

namespace ReadYourHeartOut.Migrations
{
    public partial class EditRowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "Users",
                newName: "RowVersion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "Users",
                newName: "RowVersion2");
        }
    }
}
