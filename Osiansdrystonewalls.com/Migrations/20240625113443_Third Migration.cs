using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osiansdrystonewalls.com.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "JobRequests");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "JobRequests",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "JobRequests",
                newName: "ShortDescription");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
