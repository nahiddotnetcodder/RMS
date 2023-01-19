using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    public partial class UpdateMasterDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreGIssue");

            migrationBuilder.CreateTable(
                name: "StoreGIssueMasters",
                columns: table => new
                {
                    GIMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GIMDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HRDId = table.Column<int>(type: "int", nullable: false),
                    GIMRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreGIssueMasters", x => x.GIMId);
                    table.ForeignKey(
                        name: "FK_StoreGIssueMasters_HRDepartment_HRDId",
                        column: x => x.HRDId,
                        principalTable: "HRDepartment",
                        principalColumn: "HRDId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreGIssueDetails",
                columns: table => new
                {
                    GIDId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SIGId = table.Column<int>(type: "int", nullable: false),
                    GIDUPrice = table.Column<float>(type: "real", nullable: false),
                    GIDQty = table.Column<float>(type: "real", nullable: false),
                    GIDTPrice = table.Column<int>(type: "int", nullable: false),
                    GIMId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreGIssueDetails", x => x.GIDId);
                    table.ForeignKey(
                        name: "FK_StoreGIssueDetails_StoreGIssueMasters_GIMId",
                        column: x => x.GIMId,
                        principalTable: "StoreGIssueMasters",
                        principalColumn: "GIMId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreGIssueDetails_StoreIGen_SIGId",
                        column: x => x.SIGId,
                        principalTable: "StoreIGen",
                        principalColumn: "SIGId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreGIssueDetails_GIMId",
                table: "StoreGIssueDetails",
                column: "GIMId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreGIssueDetails_SIGId",
                table: "StoreGIssueDetails",
                column: "SIGId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreGIssueMasters_HRDId",
                table: "StoreGIssueMasters",
                column: "HRDId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreGIssueDetails");

            migrationBuilder.DropTable(
                name: "StoreGIssueMasters");

            migrationBuilder.CreateTable(
                name: "StoreGIssue",
                columns: table => new
                {
                    GIId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HRDId = table.Column<int>(type: "int", nullable: false),
                    SCId = table.Column<int>(type: "int", nullable: true),
                    SIGId = table.Column<int>(type: "int", nullable: false),
                    SSCId = table.Column<int>(type: "int", nullable: true),
                    SUId = table.Column<int>(type: "int", nullable: true),
                    CUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GIDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GIQty = table.Column<float>(type: "real", nullable: false),
                    GIRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GITPrice = table.Column<int>(type: "int", nullable: false),
                    GIUPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreGIssue", x => x.GIId);
                    table.ForeignKey(
                        name: "FK_StoreGIssue_HRDepartment_HRDId",
                        column: x => x.HRDId,
                        principalTable: "HRDepartment",
                        principalColumn: "HRDId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreGIssue_StoreCategory_SCId",
                        column: x => x.SCId,
                        principalTable: "StoreCategory",
                        principalColumn: "SCId");
                    table.ForeignKey(
                        name: "FK_StoreGIssue_StoreIGen_SIGId",
                        column: x => x.SIGId,
                        principalTable: "StoreIGen",
                        principalColumn: "SIGId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreGIssue_StoreSCategory_SSCId",
                        column: x => x.SSCId,
                        principalTable: "StoreSCategory",
                        principalColumn: "SSCId");
                    table.ForeignKey(
                        name: "FK_StoreGIssue_StoreUnit_SUId",
                        column: x => x.SUId,
                        principalTable: "StoreUnit",
                        principalColumn: "SUId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreGIssue_HRDId",
                table: "StoreGIssue",
                column: "HRDId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreGIssue_SCId",
                table: "StoreGIssue",
                column: "SCId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreGIssue_SIGId",
                table: "StoreGIssue",
                column: "SIGId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreGIssue_SSCId",
                table: "StoreGIssue",
                column: "SSCId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreGIssue_SUId",
                table: "StoreGIssue",
                column: "SUId");
        }
    }
}
