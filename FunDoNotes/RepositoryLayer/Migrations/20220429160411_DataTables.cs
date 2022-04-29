using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class DataTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "NotesTable",
                columns: table => new
                {
                    NoteId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Reminder = table.Column<DateTime>(nullable: false),
                    IsArchive = table.Column<bool>(nullable: false),
                    IsPin = table.Column<bool>(nullable: false),
                    IsTrash = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesTable", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_NotesTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CollabTable",
                columns: table => new
                {
                    CollabId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NoteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabTable", x => x.CollabId);
                    table.ForeignKey(
                        name: "FK_CollabTable_NotesTable_NoteId",
                        column: x => x.NoteId,
                        principalTable: "NotesTable",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollabTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LabelTable",
                columns: table => new
                {
                    LabelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NoteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelTable", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_LabelTable_NotesTable_NoteId",
                        column: x => x.NoteId,
                        principalTable: "NotesTable",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LabelTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_NoteId",
                table: "CollabTable",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_UserId",
                table: "CollabTable",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelTable_NoteId",
                table: "LabelTable",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelTable_UserId",
                table: "LabelTable",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesTable_UserId",
                table: "NotesTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabTable");

            migrationBuilder.DropTable(
                name: "LabelTable");

            migrationBuilder.DropTable(
                name: "NotesTable");

            migrationBuilder.DropTable(
                name: "UserTable");
        }
    }
}
