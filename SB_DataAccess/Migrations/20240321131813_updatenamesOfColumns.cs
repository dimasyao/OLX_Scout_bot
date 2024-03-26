using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SB_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatenamesOfColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionDB",
                table: "Users",
                newName: "Subscriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subscriptions",
                table: "Users",
                newName: "SubscriptionDB");
        }
    }
}
