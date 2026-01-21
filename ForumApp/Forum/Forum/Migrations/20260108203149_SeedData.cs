using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[,]
                {
                    { "9a7e2c55-4f0b-4b8e-8c2a-1d4f7b3e6a22", "Please be respectful to others and follow the community guidelines.", "Forum Guidelines" },
                    { "f3c0b4f1-8c7e-4c3d-9e3a-2b6f4e2a9d11", "This is the first post in the forum. Feel free to share your thoughts!", "Welcome to the Forum" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "9a7e2c55-4f0b-4b8e-8c2a-1d4f7b3e6a22");

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "f3c0b4f1-8c7e-4c3d-9e3a-2b6f4e2a9d11");
        }
    }
}
