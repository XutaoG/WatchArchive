using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WatchArchive.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name", "user_id" },
                values: new object[,]
                {
                    { new Guid("777f65af-997b-4aff-bf13-dc15738e3efe"), "TV Show", new Guid("34bd8f63-1ec7-47ec-9f7f-c5a9646369d1") },
                    { new Guid("81668a4a-412d-4aa0-8ec1-4c11509b51bc"), "Documentary", new Guid("34bd8f63-1ec7-47ec-9f7f-c5a9646369d1") },
                    { new Guid("db9b5c46-2f5f-4fd4-8e86-8a7cf34458ed"), "Movie", new Guid("34bd8f63-1ec7-47ec-9f7f-c5a9646369d1") },
                    { new Guid("ff336c33-fcd6-48c6-b0ee-aef243cff897"), "Anime", new Guid("34bd8f63-1ec7-47ec-9f7f-c5a9646369d1") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("777f65af-997b-4aff-bf13-dc15738e3efe"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("81668a4a-412d-4aa0-8ec1-4c11509b51bc"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("db9b5c46-2f5f-4fd4-8e86-8a7cf34458ed"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("ff336c33-fcd6-48c6-b0ee-aef243cff897"));
        }
    }
}
