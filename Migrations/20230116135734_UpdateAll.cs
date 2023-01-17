using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    public partial class UpdateAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessLevelId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserLogin",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentStatusId",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AccBankAccounts",
                columns: table => new
                {
                    ABAId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACMAccCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ABAAType = table.Column<int>(type: "int", nullable: false),
                    ABABAName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ABABANumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ABABName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ABABAdd = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ABABCCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ABALRDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ABAERBal = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccBankAccounts", x => x.ABAId);
                });

            migrationBuilder.CreateTable(
                name: "AccChartClass",
                columns: table => new
                {
                    ACCId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACCName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ACCCType = table.Column<int>(type: "int", nullable: false),
                    CUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccChartClass", x => x.ACCId);
                });

            migrationBuilder.CreateTable(
                name: "AccJournal",
                columns: table => new
                {
                    AJId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AJType = table.Column<int>(type: "int", nullable: false),
                    AJTrNo = table.Column<int>(type: "int", nullable: false),
                    AJTrDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AJRef = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AJSoRef = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AJEDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AJDDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AJAmount = table.Column<double>(type: "float", nullable: false),
                    AJMemo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccJournal", x => x.AJId);
                });

            migrationBuilder.CreateTable(
                name: "AccChartType",
                columns: table => new
                {
                    ACTId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACTName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ACCId = table.Column<int>(type: "int", nullable: false),
                    ACTParent = table.Column<int>(type: "int", nullable: false),
                    CUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccChartType", x => x.ACTId);
                    table.ForeignKey(
                        name: "FK_AccChartType_AccChartClass_ACCId",
                        column: x => x.ACCId,
                        principalTable: "AccChartClass",
                        principalColumn: "ACCId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccGlTrans",
                columns: table => new
                {
                    AGTId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AJType = table.Column<int>(type: "int", nullable: false),
                    AJTrNo = table.Column<int>(type: "int", nullable: false),
                    AJTrDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AGTAccount = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AGTMemo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AGTAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccGlTrans", x => x.AGTId);
                    table.ForeignKey(
                        name: "FK_AccGlTrans_AccJournal_AJTrNo",
                        column: x => x.AJTrNo,
                        principalTable: "AccJournal",
                        principalColumn: "AJId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccChartMaster",
                columns: table => new
                {
                    ACMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACMAccCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ACMAccName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ACTId = table.Column<int>(type: "int", nullable: false),
                    ACMAI = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccChartMaster", x => x.ACMId);
                    table.ForeignKey(
                        name: "FK_AccChartMaster_AccChartType_ACTId",
                        column: x => x.ACTId,
                        principalTable: "AccChartType",
                        principalColumn: "ACTId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccChartMaster_ACTId",
                table: "AccChartMaster",
                column: "ACTId");

            migrationBuilder.CreateIndex(
                name: "IX_AccChartType_ACCId",
                table: "AccChartType",
                column: "ACCId");

            migrationBuilder.CreateIndex(
                name: "IX_AccGlTrans_AJTrNo",
                table: "AccGlTrans",
                column: "AJTrNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccBankAccounts");

            migrationBuilder.DropTable(
                name: "AccChartMaster");

            migrationBuilder.DropTable(
                name: "AccGlTrans");

            migrationBuilder.DropTable(
                name: "AccChartType");

            migrationBuilder.DropTable(
                name: "AccJournal");

            migrationBuilder.DropTable(
                name: "AccChartClass");

            migrationBuilder.DropColumn(
                name: "AccessLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserLogin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentStatusId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetRoles");
        }
    }
}
