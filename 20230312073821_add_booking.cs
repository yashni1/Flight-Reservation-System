using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class add_booking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schedules_FlightScheduleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FlightScheduleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FlightScheduleId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightScheduleId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FlightScheduleId",
                table: "Users",
                column: "FlightScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Schedules_FlightScheduleId",
                table: "Users",
                column: "FlightScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }
    }
}
