using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShipVoyageRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voyages_Ships_ShipId",
                table: "Voyages");

            migrationBuilder.AddForeignKey(
                name: "FK_Voyages_Ships_ShipId",
                table: "Voyages",
                column: "ShipId",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voyages_Ships_ShipId",
                table: "Voyages");

            migrationBuilder.AddForeignKey(
                name: "FK_Voyages_Ships_ShipId",
                table: "Voyages",
                column: "ShipId",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
