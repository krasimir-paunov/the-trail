using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheTrail.Data.Migrations
{
    /// <inheritdoc />
    public partial class LegendaryCollectiblesEraLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ColorTheme",
                table: "Eras",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChapterId",
                table: "Collectibles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "EraId",
                table: "Collectibles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collectibles_EraId",
                table: "Collectibles",
                column: "EraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collectibles_Eras_EraId",
                table: "Collectibles",
                column: "EraId",
                principalTable: "Eras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collectibles_Eras_EraId",
                table: "Collectibles");

            migrationBuilder.DropIndex(
                name: "IX_Collectibles_EraId",
                table: "Collectibles");

            migrationBuilder.DropColumn(
                name: "EraId",
                table: "Collectibles");

            migrationBuilder.AlterColumn<string>(
                name: "ColorTheme",
                table: "Eras",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "ChapterId",
                table: "Collectibles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
