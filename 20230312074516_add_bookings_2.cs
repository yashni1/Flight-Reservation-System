using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class add_bookings_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_FlightId",
                table: "Schedules",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Flights_FlightId",
                table: "Schedules",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Flights_FlightId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_FlightId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Schedules");
        }
    }
}
