namespace BankruptcyLaw.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddCreatorColumnToClientDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "ClientDocuments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientDocuments_AddedByUserId",
                table: "ClientDocuments",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientDocuments_AspNetUsers_AddedByUserId",
                table: "ClientDocuments",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientDocuments_AspNetUsers_AddedByUserId",
                table: "ClientDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ClientDocuments_AddedByUserId",
                table: "ClientDocuments");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "ClientDocuments");
        }
    }
}
