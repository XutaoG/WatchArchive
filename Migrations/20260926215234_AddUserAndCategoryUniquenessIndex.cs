using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchArchive.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndCategoryUniquenessIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_categories_user_id",
                table: "categories");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "users",
                type: "citext",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(24)",
                oldMaxLength: 24);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "categories",
                type: "citext",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_user_id_name",
                table: "categories",
                columns: new[] { "user_id", "name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_username",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_categories_user_id_name",
                table: "categories");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "users",
                type: "character varying(24)",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "citext",
                oldMaxLength: 24);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "categories",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "citext",
                oldMaxLength: 32);

            migrationBuilder.CreateIndex(
                name: "IX_categories_user_id",
                table: "categories",
                column: "user_id");
        }
    }
}
