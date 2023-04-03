using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class change_credit_card : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CreditCards_CreditCardId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CreditCardId",
                table: "Users",
                newName: "credit_card");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CreditCardId",
                table: "Users",
                newName: "IX_Users_credit_card");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CreditCards_credit_card",
                table: "Users",
                column: "credit_card",
                principalTable: "CreditCards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CreditCards_credit_card",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "credit_card",
                table: "Users",
                newName: "CreditCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_credit_card",
                table: "Users",
                newName: "IX_Users_CreditCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CreditCards_CreditCardId",
                table: "Users",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "Id");
        }
    }
}
