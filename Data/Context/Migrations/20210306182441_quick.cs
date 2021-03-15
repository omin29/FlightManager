using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class quick : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Flights_UniquePlaneNumber",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_UniquePlaneNumber",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "UniquePlaneNumber",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlightUniquePlaneNumber",
                table: "Reservations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FlightUniquePlaneNumber",
                table: "Reservations",
                column: "FlightUniquePlaneNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Flights_FlightUniquePlaneNumber",
                table: "Reservations",
                column: "FlightUniquePlaneNumber",
                principalTable: "Flights",
                principalColumn: "UniquePlaneNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Flights_FlightUniquePlaneNumber",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_FlightUniquePlaneNumber",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "FlightUniquePlaneNumber",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "UniquePlaneNumber",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UniquePlaneNumber",
                table: "Reservations",
                column: "UniquePlaneNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Flights_UniquePlaneNumber",
                table: "Reservations",
                column: "UniquePlaneNumber",
                principalTable: "Flights",
                principalColumn: "UniquePlaneNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
