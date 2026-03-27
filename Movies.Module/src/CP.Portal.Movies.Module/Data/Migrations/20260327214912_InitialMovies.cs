using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CP.Portal.Movies.Module.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "movies");

            migrationBuilder.CreateTable(
                name: "genres",
                schema: "movies",
                columns: table => new
                {
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genres", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                schema: "movies",
                columns: table => new
                {
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    original_title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    synopsis = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    release_year = table.Column<DateOnly>(type: "date", nullable: false),
                    duration_minutes = table.Column<int>(type: "integer", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    rental_price = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movies", x => x.movie_id);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                schema: "movies",
                columns: table => new
                {
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    bio = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_persons", x => x.person_id);
                });

            migrationBuilder.CreateTable(
                name: "movie_genres",
                schema: "movies",
                columns: table => new
                {
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movie_genres", x => new { x.movie_id, x.genre_id });
                    table.ForeignKey(
                        name: "fk_movie_genres_genres_genre_id",
                        column: x => x.genre_id,
                        principalSchema: "movies",
                        principalTable: "genres",
                        principalColumn: "genre_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_movie_genres_movies_movie_id",
                        column: x => x.movie_id,
                        principalSchema: "movies",
                        principalTable: "movies",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_casts",
                schema: "movies",
                columns: table => new
                {
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    character_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cast_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movie_casts", x => new { x.movie_id, x.person_id });
                    table.ForeignKey(
                        name: "fk_movie_casts_movies_movie_id",
                        column: x => x.movie_id,
                        principalSchema: "movies",
                        principalTable: "movies",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_movie_casts_persons_person_id",
                        column: x => x.person_id,
                        principalSchema: "movies",
                        principalTable: "persons",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movies_crews",
                schema: "movies",
                columns: table => new
                {
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movies_crews", x => new { x.movie_id, x.person_id });
                    table.ForeignKey(
                        name: "fk_movies_crews_movies_movie_id",
                        column: x => x.movie_id,
                        principalSchema: "movies",
                        principalTable: "movies",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_movies_crews_peoples_person_id",
                        column: x => x.person_id,
                        principalSchema: "movies",
                        principalTable: "persons",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_movie_casts_person_id",
                schema: "movies",
                table: "movie_casts",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_movie_genres_genre_id",
                schema: "movies",
                table: "movie_genres",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "ix_movie_genres_movie_id",
                schema: "movies",
                table: "movie_genres",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "ix_movies_crews_person_id",
                schema: "movies",
                table: "movies_crews",
                column: "person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movie_casts",
                schema: "movies");

            migrationBuilder.DropTable(
                name: "movie_genres",
                schema: "movies");

            migrationBuilder.DropTable(
                name: "movies_crews",
                schema: "movies");

            migrationBuilder.DropTable(
                name: "genres",
                schema: "movies");

            migrationBuilder.DropTable(
                name: "movies",
                schema: "movies");

            migrationBuilder.DropTable(
                name: "persons",
                schema: "movies");
        }
    }
}
