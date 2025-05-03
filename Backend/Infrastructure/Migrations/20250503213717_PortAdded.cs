using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PortAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voyage_Port_ArrivalPortId",
                table: "Voyage");

            migrationBuilder.DropForeignKey(
                name: "FK_Voyage_Port_DeparturePortId",
                table: "Voyage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Port",
                table: "Port");

            migrationBuilder.RenameTable(
                name: "Port",
                newName: "Ports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ports",
                table: "Ports",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voyage_Ports_ArrivalPortId",
                table: "Voyage");

            migrationBuilder.DropForeignKey(
                name: "FK_Voyage_Ports_DeparturePortId",
                table: "Voyage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ports",
                table: "Ports");

            migrationBuilder.RenameTable(
                name: "Ports",
                newName: "Port");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Port",
                table: "Port",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Voyage_Port_ArrivalPortId",
                table: "Voyage",
                column: "ArrivalPortId",
                principalTable: "Port",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voyage_Port_DeparturePortId",
                table: "Voyage",
                column: "DeparturePortId",
                principalTable: "Port",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
