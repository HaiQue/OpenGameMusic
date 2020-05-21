using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenGameMusic.Migrations.AppDb
{
    public partial class AddSeedSongsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Artist", "License", "SongName" },
                values: new object[] { 1, "Faustix", 2, "Solo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
