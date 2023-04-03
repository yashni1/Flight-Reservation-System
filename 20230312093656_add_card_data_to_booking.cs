using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class add_card_data_to_booking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CreditCards_credit_card",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_Users_credit_card",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "credit_card",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "CardHolderName",
                table: "Bookings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "CardNumber",
                table: "Bookings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ExpiryDate",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NoOfSeats",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardHolderName",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "NoOfSeats",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "credit_card",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_credit_card",
                table: "Users",
                column: "credit_card");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CreditCards_credit_card",
                table: "Users",
                column: "credit_card",
                principalTable: "CreditCards",
                principalColumn: "Id");
        }
    }
}
