using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WishFolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProfilePropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Profile_Name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Profile_Age",
                table: "Users",
                newName: "Age");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Profile_Name");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Users",
                newName: "Profile_Age");
        }
    }
}
