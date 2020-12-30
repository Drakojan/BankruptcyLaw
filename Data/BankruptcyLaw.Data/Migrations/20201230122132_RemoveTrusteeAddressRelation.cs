using Microsoft.EntityFrameworkCore.Migrations;

namespace BankruptcyLaw.Data.Migrations
{
    public partial class RemoveTrusteeAddressRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trustees_Addresses_AddressId",
                table: "Trustees");

            migrationBuilder.DropIndex(
                name: "IX_Trustees_AddressId",
                table: "Trustees");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Trustees");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Trustees",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Hearings",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HearingAddress",
                table: "Hearings",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Trustees");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Trustees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Hearings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HearingAddress",
                table: "Hearings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_Trustees_AddressId",
                table: "Trustees",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trustees_Addresses_AddressId",
                table: "Trustees",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
