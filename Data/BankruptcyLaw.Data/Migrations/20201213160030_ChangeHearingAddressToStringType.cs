using Microsoft.EntityFrameworkCore.Migrations;

namespace BankruptcyLaw.Data.Migrations
{
    public partial class ChangeHearingAddressToStringType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hearings_Addresses_AddressId",
                table: "Hearings");

            migrationBuilder.DropIndex(
                name: "IX_Hearings_AddressId",
                table: "Hearings");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Hearings");

            migrationBuilder.AddColumn<string>(
                name: "HearingAddress",
                table: "Hearings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HearingAddress",
                table: "Hearings");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Hearings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Hearings_AddressId",
                table: "Hearings",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hearings_Addresses_AddressId",
                table: "Hearings",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
