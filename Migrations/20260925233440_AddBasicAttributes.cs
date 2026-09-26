using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchArchive.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddBasicAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attribute_definitions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    type = table.Column<short>(type: "smallint", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attribute_definitions", x => x.id);
                    table.ForeignKey(
                        name: "FK_attribute_definitions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "boolean_attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<bool>(type: "boolean", nullable: false),
                    rated_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    attribute_definition_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boolean_attributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_boolean_attributes_attribute_definitions_attribute_definiti~",
                        column: x => x.attribute_definition_id,
                        principalTable: "attribute_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_boolean_attributes_rated_entries_rated_entry_id",
                        column: x => x.rated_entry_id,
                        principalTable: "rated_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "date_attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rated_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    attribute_definition_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_date_attributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_date_attributes_attribute_definitions_attribute_definition_~",
                        column: x => x.attribute_definition_id,
                        principalTable: "attribute_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_date_attributes_rated_entries_rated_entry_id",
                        column: x => x.rated_entry_id,
                        principalTable: "rated_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "long_text_attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(65536)", maxLength: 65536, nullable: false),
                    rated_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    attribute_definition_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_long_text_attributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_long_text_attributes_attribute_definitions_attribute_defini~",
                        column: x => x.attribute_definition_id,
                        principalTable: "attribute_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_long_text_attributes_rated_entries_rated_entry_id",
                        column: x => x.rated_entry_id,
                        principalTable: "rated_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "number_attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    rated_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    attribute_definition_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_number_attributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_number_attributes_attribute_definitions_attribute_definitio~",
                        column: x => x.attribute_definition_id,
                        principalTable: "attribute_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_number_attributes_rated_entries_rated_entry_id",
                        column: x => x.rated_entry_id,
                        principalTable: "rated_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "short_text_attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    rated_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    attribute_definition_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_short_text_attributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_short_text_attributes_attribute_definitions_attribute_defin~",
                        column: x => x.attribute_definition_id,
                        principalTable: "attribute_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_short_text_attributes_rated_entries_rated_entry_id",
                        column: x => x.rated_entry_id,
                        principalTable: "rated_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "star_rating_attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<decimal>(type: "numeric(3,1)", nullable: false),
                    rated_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    attribute_definition_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_star_rating_attributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_star_rating_attributes_attribute_definitions_attribute_defi~",
                        column: x => x.attribute_definition_id,
                        principalTable: "attribute_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_star_rating_attributes_rated_entries_rated_entry_id",
                        column: x => x.rated_entry_id,
                        principalTable: "rated_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "url_attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    rated_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    attribute_definition_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_url_attributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_url_attributes_attribute_definitions_attribute_definition_id",
                        column: x => x.attribute_definition_id,
                        principalTable: "attribute_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_url_attributes_rated_entries_rated_entry_id",
                        column: x => x.rated_entry_id,
                        principalTable: "rated_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attribute_definitions_user_id",
                table: "attribute_definitions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_boolean_attributes_attribute_definition_id",
                table: "boolean_attributes",
                column: "attribute_definition_id");

            migrationBuilder.CreateIndex(
                name: "IX_boolean_attributes_rated_entry_id",
                table: "boolean_attributes",
                column: "rated_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_date_attributes_attribute_definition_id",
                table: "date_attributes",
                column: "attribute_definition_id");

            migrationBuilder.CreateIndex(
                name: "IX_date_attributes_rated_entry_id",
                table: "date_attributes",
                column: "rated_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_long_text_attributes_attribute_definition_id",
                table: "long_text_attributes",
                column: "attribute_definition_id");

            migrationBuilder.CreateIndex(
                name: "IX_long_text_attributes_rated_entry_id",
                table: "long_text_attributes",
                column: "rated_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_number_attributes_attribute_definition_id",
                table: "number_attributes",
                column: "attribute_definition_id");

            migrationBuilder.CreateIndex(
                name: "IX_number_attributes_rated_entry_id",
                table: "number_attributes",
                column: "rated_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_short_text_attributes_attribute_definition_id",
                table: "short_text_attributes",
                column: "attribute_definition_id");

            migrationBuilder.CreateIndex(
                name: "IX_short_text_attributes_rated_entry_id",
                table: "short_text_attributes",
                column: "rated_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_star_rating_attributes_attribute_definition_id",
                table: "star_rating_attributes",
                column: "attribute_definition_id");

            migrationBuilder.CreateIndex(
                name: "IX_star_rating_attributes_rated_entry_id",
                table: "star_rating_attributes",
                column: "rated_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_url_attributes_attribute_definition_id",
                table: "url_attributes",
                column: "attribute_definition_id");

            migrationBuilder.CreateIndex(
                name: "IX_url_attributes_rated_entry_id",
                table: "url_attributes",
                column: "rated_entry_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "boolean_attributes");

            migrationBuilder.DropTable(
                name: "date_attributes");

            migrationBuilder.DropTable(
                name: "long_text_attributes");

            migrationBuilder.DropTable(
                name: "number_attributes");

            migrationBuilder.DropTable(
                name: "short_text_attributes");

            migrationBuilder.DropTable(
                name: "star_rating_attributes");

            migrationBuilder.DropTable(
                name: "url_attributes");

            migrationBuilder.DropTable(
                name: "attribute_definitions");
        }
    }
}
