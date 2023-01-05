using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnowmobileShop.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SnowmobileTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnowmobileTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Snowmobiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Horsepower = table.Column<int>(type: "INTEGER", nullable: false),
                    EngineCapacity = table.Column<string>(type: "TEXT", nullable: true),
                    YearOfProduction = table.Column<int>(type: "INTEGER", nullable: false),
                    SnowmobileTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snowmobiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Snowmobiles_SnowmobileTypes_SnowmobileTypeId",
                        column: x => x.SnowmobileTypeId,
                        principalTable: "SnowmobileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Snowmobiles_SnowmobileTypeId",
                table: "Snowmobiles",
                column: "SnowmobileTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Snowmobiles");

            migrationBuilder.DropTable(
                name: "SnowmobileTypes");
        }
    }
}
