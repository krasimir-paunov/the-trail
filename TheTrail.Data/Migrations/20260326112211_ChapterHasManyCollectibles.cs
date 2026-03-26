using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheTrail.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChapterHasManyCollectibles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Collectibles_ChapterId",
                table: "Collectibles");

            migrationBuilder.CreateIndex(
                name: "IX_Collectibles_ChapterId",
                table: "Collectibles",
                column: "ChapterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Collectibles_ChapterId",
                table: "Collectibles");

            migrationBuilder.CreateIndex(
                name: "IX_Collectibles_ChapterId",
                table: "Collectibles",
                column: "ChapterId",
                unique: true);
        }
    }
}
