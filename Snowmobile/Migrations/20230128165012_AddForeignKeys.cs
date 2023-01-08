using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnowmobileShop.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalHours_RentalDays_RentalDayId",
                table: "RentalHours");

            migrationBuilder.AlterColumn<int>(
                name: "RentalDayId",
                table: "RentalHours",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "RentalHours",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsReserved",
                table: "RentalHours",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

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
                name: "FK_RentalHours_RentalDays_RentalDayId",
                table: "RentalHours",
                column: "RentalDayId",
                principalTable: "RentalDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalDays_Snowmobiles_SnowmobileId",
                table: "RentalDays");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalHours_RentalDays_RentalDayId",
                table: "RentalHours");

            migrationBuilder.DropIndex(
                name: "IX_RentalDays_SnowmobileId",
                table: "RentalDays");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "RentalHours");

            migrationBuilder.DropColumn(
                name: "IsReserved",
                table: "RentalHours");

            migrationBuilder.DropColumn(
                name: "SnowmobileId",
                table: "RentalDays");

            migrationBuilder.AlterColumn<int>(
                name: "RentalDayId",
                table: "RentalHours",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalHours_RentalDays_RentalDayId",
                table: "RentalHours",
                column: "RentalDayId",
                principalTable: "RentalDays",
                principalColumn: "Id");
        }
    }
}
