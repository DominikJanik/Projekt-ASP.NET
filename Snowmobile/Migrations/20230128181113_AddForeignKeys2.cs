using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnowmobileShop.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeys2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalDays_Snowmobiles_SnowmobileId",
                table: "RentalDays");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_IdentityUser_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_RentalDays_SnowmobileId",
                table: "RentalDays");

            migrationBuilder.DropColumn(
                name: "SnowmobileId",
                table: "RentalDays");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ShoppingCarts",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RentalTimeId",
                table: "ShoppingCartLines",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RentalHours",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "RentalDays",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartLines_RentalTimeId",
                table: "ShoppingCartLines",
                column: "RentalTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalHours_UserId",
                table: "RentalHours",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalDays_ProductId",
                table: "RentalDays",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalDays_Products_ProductId",
                table: "RentalDays",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalHours_IdentityUser_UserId",
                table: "RentalHours",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartLines_RentalHours_RentalTimeId",
                table: "ShoppingCartLines",
                column: "RentalTimeId",
                principalTable: "RentalHours",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_IdentityUser_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalDays_Products_ProductId",
                table: "RentalDays");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalHours_IdentityUser_UserId",
                table: "RentalHours");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartLines_RentalHours_RentalTimeId",
                table: "ShoppingCartLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_IdentityUser_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartLines_RentalTimeId",
                table: "ShoppingCartLines");

            migrationBuilder.DropIndex(
                name: "IX_RentalHours_UserId",
                table: "RentalHours");

            migrationBuilder.DropIndex(
                name: "IX_RentalDays_ProductId",
                table: "RentalDays");

            migrationBuilder.DropColumn(
                name: "RentalTimeId",
                table: "ShoppingCartLines");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RentalHours");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "RentalDays");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ShoppingCarts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "SnowmobileId",
                table: "RentalDays",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalDays_SnowmobileId",
                table: "RentalDays",
                column: "SnowmobileId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalDays_Snowmobiles_SnowmobileId",
                table: "RentalDays",
                column: "SnowmobileId",
                principalTable: "Snowmobiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_IdentityUser_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }
    }
}
