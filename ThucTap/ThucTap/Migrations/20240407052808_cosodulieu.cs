using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThucTap.Migrations
{
    /// <inheritdoc />
    public partial class cosodulieu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiDichVu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiDichVu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DichVu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiDichVuID = table.Column<int>(type: "int", nullable: false),
                    TenDV = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NoiDung = table.Column<string>(type: "ntext", nullable: false),
                    NguoiDungID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVu", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DichVu_LoaiDichVu_LoaiDichVuID",
                        column: x => x.LoaiDichVuID,
                        principalTable: "LoaiDichVu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DichVu_NguoiDung_NguoiDungID",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DichVu_LoaiDichVuID",
                table: "DichVu",
                column: "LoaiDichVuID");

            migrationBuilder.CreateIndex(
                name: "IX_DichVu_NguoiDungID",
                table: "DichVu",
                column: "NguoiDungID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DichVu");

            migrationBuilder.DropTable(
                name: "LoaiDichVu");
        }
    }
}
