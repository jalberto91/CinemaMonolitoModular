using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CP.Portal.Movies.Module.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "movies",
                table: "genres",
                columns: new[] { "genre_id", "name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Action" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Drama" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Comedy" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Sci-Fi" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Thriller" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Fantasy" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Horror" }
                });

            migrationBuilder.InsertData(
                schema: "movies",
                table: "persons",
                columns: new[] { "person_id", "bio", "birth_date", "name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Bio for John Doe", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "John Doe" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Bio for Jane Smith", new DateTime(1985, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Jane Smith" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Bio for Michael Johnson", new DateTime(1990, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Michael Johnson" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), null, new DateTime(1995, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Emily Davis" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Bio for David Brown", new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), "David Brown" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Bio for Sarah Wilson", new DateTime(1988, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Sarah Wilson" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Bio for Chris Lee", new DateTime(1992, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Chris Lee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "movies",
                table: "genres",
                keyColumn: "genre_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "genres",
                keyColumn: "genre_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "genres",
                keyColumn: "genre_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "genres",
                keyColumn: "genre_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "genres",
                keyColumn: "genre_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "genres",
                keyColumn: "genre_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "genres",
                keyColumn: "genre_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "persons",
                keyColumn: "person_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "persons",
                keyColumn: "person_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "persons",
                keyColumn: "person_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "persons",
                keyColumn: "person_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "persons",
                keyColumn: "person_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "persons",
                keyColumn: "person_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                schema: "movies",
                table: "persons",
                keyColumn: "person_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));
        }
    }
}
