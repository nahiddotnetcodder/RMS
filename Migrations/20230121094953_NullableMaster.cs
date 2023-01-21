using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    public partial class NullableMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreGIssueDetails_StoreGIssueMasters_GIMId",
                table: "StoreGIssueDetails");

            migrationBuilder.AlterColumn<int>(
                name: "GIMId",
                table: "StoreGIssueDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreGIssueDetails_StoreGIssueMasters_GIMId",
                table: "StoreGIssueDetails",
                column: "GIMId",
                principalTable: "StoreGIssueMasters",
                principalColumn: "GIMId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreGIssueDetails_StoreGIssueMasters_GIMId",
                table: "StoreGIssueDetails");

            migrationBuilder.AlterColumn<int>(
                name: "GIMId",
                table: "StoreGIssueDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreGIssueDetails_StoreGIssueMasters_GIMId",
                table: "StoreGIssueDetails",
                column: "GIMId",
                principalTable: "StoreGIssueMasters",
                principalColumn: "GIMId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
