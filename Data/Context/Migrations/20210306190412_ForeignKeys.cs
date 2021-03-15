using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class ForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Flights_FlightUniquePlaneNumber",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "UniquePlaneNumber",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "FlightUniquePlaneNumber",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Flights_FlightUniquePlaneNumber",
                table: "Reservations",
                column: "FlightUniquePlaneNumber",
                principalTable: "Flights",
                principalColumn: "UniquePlaneNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Flights_FlightUniquePlaneNumber",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "FlightUniquePlaneNumber",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UniquePlaneNumber",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Flights_FlightUniquePlaneNumber",
                table: "Reservations",
                column: "FlightUniquePlaneNumber",
                principalTable: "Flights",
                principalColumn: "UniquePlaneNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
