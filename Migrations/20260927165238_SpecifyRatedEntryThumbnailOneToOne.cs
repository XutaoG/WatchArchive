using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchArchive.Server.Migrations
{
    /// <inheritdoc />
    public partial class SpecifyRatedEntryThumbnailOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_rated_entries_thumbnail_id",
                table: "rated_entries");

            migrationBuilder.CreateIndex(
                name: "IX_rated_entries_thumbnail_id",
                table: "rated_entries",
                column: "thumbnail_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_rated_entries_thumbnail_id",
                table: "rated_entries");

            migrationBuilder.CreateIndex(
                name: "IX_rated_entries_thumbnail_id",
                table: "rated_entries",
                column: "thumbnail_id");
        }
    }
}
