using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenGameMusic.Migrations.AppDb
{
    public partial class AlterSeedSongsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Artist", "License", "SongName" },
                values: new object[] { 2, "Madison Mars", 2, "Back to you" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
