using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnowmobileShop.Migrations
{
    /// <inheritdoc />
    public partial class AddCalendar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentalDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    From = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    To = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    RentalDayId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalHours_RentalDays_RentalDayId",
                        column: x => x.RentalDayId,
                        principalTable: "RentalDays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalHours_RentalDayId",
                table: "RentalHours",
                column: "RentalDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalHours");

            migrationBuilder.DropTable(
                name: "RentalDays");
        }
    }
}
