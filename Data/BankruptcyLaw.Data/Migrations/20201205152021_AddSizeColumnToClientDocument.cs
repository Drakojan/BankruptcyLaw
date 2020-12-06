using Microsoft.EntityFrameworkCore.Migrations;

namespace BankruptcyLaw.Data.Migrations
{
    public partial class AddSizeColumnToClientDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ClientDocuments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "ClientDocuments");
        }
    }
}
