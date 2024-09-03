using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osiansdrystonewalls.com.Migrations
{
    /// <inheritdoc />
    public partial class sixthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Customers_CustomersNameId",
                table: "JobRequests");

            migrationBuilder.RenameColumn(
                name: "CustomersNameId",
                table: "JobRequests",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_JobRequests_CustomersNameId",
                table: "JobRequests",
                newName: "IX_JobRequests_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Customers_CustomerId",
                table: "JobRequests",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Customers_CustomerId",
                table: "JobRequests");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "JobRequests",
                newName: "CustomersNameId");

            migrationBuilder.RenameIndex(
                name: "IX_JobRequests_CustomerId",
                table: "JobRequests",
                newName: "IX_JobRequests_CustomersNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Customers_CustomersNameId",
                table: "JobRequests",
                column: "CustomersNameId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
