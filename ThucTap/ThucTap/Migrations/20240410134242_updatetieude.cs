using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThucTap.Migrations
{
    /// <inheritdoc />
    public partial class updatetieude : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TieuDe",
                table: "DichVu",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TieuDe",
                table: "DichVu");
        }
    }
}
