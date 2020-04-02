using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CANAdminApi.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HasBeenDeleted = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    FileContent = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NetworkNodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HasBeenDeleted = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FileId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetworkNodes_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CanMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HasBeenDeleted = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Identity = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NetworkNodeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CanMessages_NetworkNodes_NetworkNodeId",
                        column: x => x.NetworkNodeId,
                        principalTable: "NetworkNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CanSignals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HasBeenDeleted = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StartBit = table.Column<int>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    CanMessageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanSignals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CanSignals_CanMessages_CanMessageId",
                        column: x => x.CanMessageId,
                        principalTable: "CanMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CanMessages_NetworkNodeId",
                table: "CanMessages",
                column: "NetworkNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CanSignals_CanMessageId",
                table: "CanSignals",
                column: "CanMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkNodes_FileId",
                table: "NetworkNodes",
                column: "FileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanSignals");

            migrationBuilder.DropTable(
                name: "CanMessages");

            migrationBuilder.DropTable(
                name: "NetworkNodes");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
