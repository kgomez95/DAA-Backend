using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAA.Database.Migrations.Migrations
{
    public partial class CreateBaseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DATATABLES_TABLES",
                columns: table => new
                {
                    DTS_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DTS_Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DTS_Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DTS_Description = table.Column<string>(type: "TEXT", nullable: false),
                    DTS_Reference = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AUT_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    AUT_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DATATABLES_TABLES", x => x.DTS_Id);
                });

            migrationBuilder.CreateTable(
                name: "PLATFORMS",
                columns: table => new
                {
                    PTS_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PTS_Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PTS_Company = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PTS_Price = table.Column<decimal>(type: "MONEY", nullable: false),
                    PTS_ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AUT_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    AUT_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLATFORMS", x => x.PTS_Id);
                });

            migrationBuilder.CreateTable(
                name: "DATATABLES_RECORDS",
                columns: table => new
                {
                    DRS_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DRS_Code = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DRS_Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DRS_Type = table.Column<int>(type: "int", nullable: false),
                    DRS_Order = table.Column<int>(type: "int", nullable: false),
                    DRS_HasFilter = table.Column<bool>(type: "bit", nullable: false),
                    DRS_IsBasic = table.Column<bool>(type: "bit", nullable: false),
                    DRS_IsRange = table.Column<bool>(type: "bit", nullable: false),
                    DRS_DefaultValue = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DRS_DefaultFrom = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DRS_DefaultTo = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DTS_Id = table.Column<int>(type: "int", nullable: false),
                    AUT_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    AUT_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DATATABLES_RECORDS", x => x.DRS_Id);
                    table.ForeignKey(
                        name: "FK_DATATABLES_RECORDS_DATATABLES_TABLES_DTS_Id",
                        column: x => x.DTS_Id,
                        principalTable: "DATATABLES_TABLES",
                        principalColumn: "DTS_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VIDEO_GAMES",
                columns: table => new
                {
                    VGS_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VGS_Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    VGS_Description = table.Column<string>(type: "TEXT", nullable: false),
                    VGS_Price = table.Column<decimal>(type: "MONEY", nullable: false),
                    VGS_Score = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    VGS_ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PTS_Id = table.Column<int>(type: "int", nullable: false),
                    AUT_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    AUT_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VIDEO_GAMES", x => x.VGS_Id);
                    table.ForeignKey(
                        name: "FK_VIDEO_GAMES_PLATFORMS_PTS_Id",
                        column: x => x.PTS_Id,
                        principalTable: "PLATFORMS",
                        principalColumn: "PTS_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DATATABLES_RECORDS_DRS_Code",
                table: "DATATABLES_RECORDS",
                column: "DRS_Code");

            migrationBuilder.CreateIndex(
                name: "IX_DATATABLES_RECORDS_DRS_Id",
                table: "DATATABLES_RECORDS",
                column: "DRS_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DATATABLES_RECORDS_DRS_Name",
                table: "DATATABLES_RECORDS",
                column: "DRS_Name");

            migrationBuilder.CreateIndex(
                name: "IX_DATATABLES_RECORDS_DTS_Id",
                table: "DATATABLES_RECORDS",
                column: "DTS_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DATATABLES_TABLES_DTS_Code",
                table: "DATATABLES_TABLES",
                column: "DTS_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DATATABLES_TABLES_DTS_Id",
                table: "DATATABLES_TABLES",
                column: "DTS_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DATATABLES_TABLES_DTS_Name",
                table: "DATATABLES_TABLES",
                column: "DTS_Name");

            migrationBuilder.CreateIndex(
                name: "IX_DATATABLES_TABLES_DTS_Reference",
                table: "DATATABLES_TABLES",
                column: "DTS_Reference");

            migrationBuilder.CreateIndex(
                name: "IX_PLATFORMS_PTS_Company",
                table: "PLATFORMS",
                column: "PTS_Company");

            migrationBuilder.CreateIndex(
                name: "IX_PLATFORMS_PTS_Id",
                table: "PLATFORMS",
                column: "PTS_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PLATFORMS_PTS_Name",
                table: "PLATFORMS",
                column: "PTS_Name");

            migrationBuilder.CreateIndex(
                name: "IX_PLATFORMS_PTS_Price",
                table: "PLATFORMS",
                column: "PTS_Price");

            migrationBuilder.CreateIndex(
                name: "IX_PLATFORMS_PTS_ReleaseDate",
                table: "PLATFORMS",
                column: "PTS_ReleaseDate");

            migrationBuilder.CreateIndex(
                name: "IX_VIDEO_GAMES_PTS_Id",
                table: "VIDEO_GAMES",
                column: "PTS_Id");

            migrationBuilder.CreateIndex(
                name: "IX_VIDEO_GAMES_VGS_Id",
                table: "VIDEO_GAMES",
                column: "VGS_Id");

            migrationBuilder.CreateIndex(
                name: "IX_VIDEO_GAMES_VGS_Name",
                table: "VIDEO_GAMES",
                column: "VGS_Name");

            migrationBuilder.CreateIndex(
                name: "IX_VIDEO_GAMES_VGS_Price",
                table: "VIDEO_GAMES",
                column: "VGS_Price");

            migrationBuilder.CreateIndex(
                name: "IX_VIDEO_GAMES_VGS_ReleaseDate",
                table: "VIDEO_GAMES",
                column: "VGS_ReleaseDate");

            migrationBuilder.CreateIndex(
                name: "IX_VIDEO_GAMES_VGS_Score",
                table: "VIDEO_GAMES",
                column: "VGS_Score");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DATATABLES_RECORDS");

            migrationBuilder.DropTable(
                name: "VIDEO_GAMES");

            migrationBuilder.DropTable(
                name: "DATATABLES_TABLES");

            migrationBuilder.DropTable(
                name: "PLATFORMS");
        }
    }
}
