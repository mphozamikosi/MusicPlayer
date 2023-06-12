using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicPlayerAPI.Migrations
{
    /// <inheritdoc />
    public partial class Add_PhotoLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Genres_GenresId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_GenresId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "GenresId",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "PhotoLocation",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoLocation",
                table: "Albums");

            migrationBuilder.AddColumn<int>(
                name: "GenresId",
                table: "Songs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_GenresId",
                table: "Songs",
                column: "GenresId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Genres_GenresId",
                table: "Songs",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id");
        }
    }
}
