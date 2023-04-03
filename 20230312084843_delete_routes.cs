using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class delete_routes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Routes_FlightRouteId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Flights_FlightRouteId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FlightRouteId",
                table: "Flights");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "FlightRouteId",
                table: "Flights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_FlightRouteId",
                table: "Flights",
                column: "FlightRouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Routes_FlightRouteId",
                table: "Flights",
                column: "FlightRouteId",
                principalTable: "Routes",
                principalColumn: "Id");
        }
    }
}
