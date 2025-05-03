using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VoyageAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voyage_Ports_ArrivalPortId",
                table: "Voyage");

            migrationBuilder.DropForeignKey(
                name: "FK_Voyage_Ports_DeparturePortId",
                table: "Voyage");

            migrationBuilder.DropForeignKey(
                name: "FK_Voyage_Ships_ShipId",
                table: "Voyage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Voyage",
                table: "Voyage");

            migrationBuilder.RenameTable(
                name: "Voyage",
                newName: "Voyages");

            migrationBuilder.RenameIndex(
                name: "IX_Voyage_ShipId",
                table: "Voyages",
                newName: "IX_Voyages_ShipId");

            migrationBuilder.RenameIndex(
                name: "IX_Voyage_DeparturePortId",
                table: "Voyages",
                newName: "IX_Voyages_DeparturePortId");

            migrationBuilder.RenameIndex(
                name: "IX_Voyage_ArrivalPortId",
                table: "Voyages",
                newName: "IX_Voyages_ArrivalPortId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Voyages",
                table: "Voyages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Voyages_Ports_ArrivalPortId",
                table: "Voyages",
                column: "ArrivalPortId",
                principalTable: "Ports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voyages_Ports_DeparturePortId",
                table: "Voyages",
                column: "DeparturePortId",
                principalTable: "Ports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voyages_Ships_ShipId",
                table: "Voyages",
                column: "ShipId",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voyages_Ports_ArrivalPortId",
                table: "Voyages");

            migrationBuilder.DropForeignKey(
                name: "FK_Voyages_Ports_DeparturePortId",
                table: "Voyages");

            migrationBuilder.DropForeignKey(
                name: "FK_Voyages_Ships_ShipId",
                table: "Voyages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Voyages",
                table: "Voyages");

            migrationBuilder.RenameTable(
                name: "Voyages",
                newName: "Voyage");

            migrationBuilder.RenameIndex(
                name: "IX_Voyages_ShipId",
                table: "Voyage",
                newName: "IX_Voyage_ShipId");

            migrationBuilder.RenameIndex(
                name: "IX_Voyages_DeparturePortId",
                table: "Voyage",
                newName: "IX_Voyage_DeparturePortId");

            migrationBuilder.RenameIndex(
                name: "IX_Voyages_ArrivalPortId",
                table: "Voyage",
                newName: "IX_Voyage_ArrivalPortId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Voyage",
                table: "Voyage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Voyage_Ports_ArrivalPortId",
                table: "Voyage",
                column: "ArrivalPortId",
                principalTable: "Ports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voyage_Ports_DeparturePortId",
                table: "Voyage",
                column: "DeparturePortId",
                principalTable: "Ports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voyage_Ships_ShipId",
                table: "Voyage",
                column: "ShipId",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
