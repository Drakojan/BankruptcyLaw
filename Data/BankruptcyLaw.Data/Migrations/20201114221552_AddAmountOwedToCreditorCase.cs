using Microsoft.EntityFrameworkCore.Migrations;

namespace BankruptcyLaw.Data.Migrations
{
    public partial class AddAmountOwedToCreditorCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AmountOwed",
                table: "CreditorCases",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AmountPaid",
                table: "CreditorCases",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOwed",
                table: "CreditorCases");

            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "CreditorCases");
        }
    }
}
