using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchArchive.Server.Migrations
{
    /// <inheritdoc />
    public partial class RatedEntryMultipleCategoryEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rated_entries_categories_category_id",
                table: "rated_entries");

            migrationBuilder.DropIndex(
                name: "IX_rated_entries_category_id",
                table: "rated_entries");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "rated_entries");

            migrationBuilder.CreateTable(
                name: "category_entries",
                columns: table => new
                {
                    rated_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category_entries", x => new { x.rated_entry_id, x.category_id });
                    table.ForeignKey(
                        name: "FK_category_entries_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_category_entries_rated_entries_rated_entry_id",
                        column: x => x.rated_entry_id,
                        principalTable: "rated_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_entries_category_id",
                table: "category_entries",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category_entries");

            migrationBuilder.AddColumn<Guid>(
                name: "category_id",
                table: "rated_entries",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_rated_entries_category_id",
                table: "rated_entries",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_rated_entries_categories_category_id",
                table: "rated_entries",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id");
        }
    }
}
