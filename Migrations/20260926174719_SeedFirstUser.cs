using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchArchive.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedFirstUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "password_hash", "username" },
                values: new object[] { new Guid("34bd8f63-1ec7-47ec-9f7f-c5a9646369d1"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "AQAAAAIAAYagAAAAEMqEhn8w5pG58XMyEwggQhdvq8r3S2fDfulLzyclwjjs4ZF1VdJnLJ7YDj9+c67VMQ==", "taoge" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("34bd8f63-1ec7-47ec-9f7f-c5a9646369d1"));
        }
    }
}
