using Microsoft.EntityFrameworkCore.Migrations;

namespace WitLoginWebAPI.Migrations
{
    public partial class FixedCreateAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificationMessage",
                table: "UsersCreateNotifications",
                newName: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "UsersCreateNotifications",
                newName: "NotificationMessage");
        }
    }
}
