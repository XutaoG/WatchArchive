using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchArchive.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddTagsAndSingleSelectAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "select_options",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    attribute_definition_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_select_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_select_options_attribute_definitions_attribute_definition_id",
                        column: x => x.attribute_definition_id,
                        principalTable: "attribute_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_tags_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "single_select_attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_option_id = table.Column<Guid>(type: "uuid", nullable: true),
                    rated_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    attribute_definition_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_single_select_attributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_single_select_attributes_attribute_definitions_attribute_de~",
                        column: x => x.attribute_definition_id,
                        principalTable: "attribute_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_single_select_attributes_rated_entries_rated_entry_id",
                        column: x => x.rated_entry_id,
                        principalTable: "rated_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_single_select_attributes_select_options_select_option_id",
                        column: x => x.select_option_id,
                        principalTable: "select_options",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tag_entries",
                columns: table => new
                {
                    tag_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rated_entry_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag_entries", x => new { x.tag_id, x.rated_entry_id });
                    table.ForeignKey(
                        name: "FK_tag_entries_rated_entries_rated_entry_id",
                        column: x => x.rated_entry_id,
                        principalTable: "rated_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tag_entries_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_select_options_attribute_definition_id",
                table: "select_options",
                column: "attribute_definition_id");

            migrationBuilder.CreateIndex(
                name: "IX_single_select_attributes_attribute_definition_id",
                table: "single_select_attributes",
                column: "attribute_definition_id");

            migrationBuilder.CreateIndex(
                name: "IX_single_select_attributes_rated_entry_id",
                table: "single_select_attributes",
                column: "rated_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_single_select_attributes_select_option_id",
                table: "single_select_attributes",
                column: "select_option_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_entries_rated_entry_id",
                table: "tag_entries",
                column: "rated_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_tags_user_id",
                table: "tags",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "single_select_attributes");

            migrationBuilder.DropTable(
                name: "tag_entries");

            migrationBuilder.DropTable(
                name: "select_options");

            migrationBuilder.DropTable(
                name: "tags");
        }
    }
}
