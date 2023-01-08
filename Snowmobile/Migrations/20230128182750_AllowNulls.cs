using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnowmobileShop.Migrations
{
    /// <inheritdoc />
    public partial class AllowNulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalHours_IdentityUser_UserId",
                table: "RentalHours");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RentalHours",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalHours_IdentityUser_UserId",
                table: "RentalHours",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalHours_IdentityUser_UserId",
                table: "RentalHours");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RentalHours",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalHours_IdentityUser_UserId",
                table: "RentalHours",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
