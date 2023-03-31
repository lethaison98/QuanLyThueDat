using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdQuyetDinhThueDatChiTiet",
                table: "ThongBaoTienThueDat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdQuyetDinhThueDatChiTiet",
                table: "ThongBaoTienSuDungDat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdQuyetDinhThueDatChiTiet",
                table: "ThongBaoDonGiaThueDat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdPhuongXa",
                table: "QuyetDinhThueDat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdQuanHuyen",
                table: "QuyetDinhThueDat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CanBo_QuyetDinhThueDat",
                columns: table => new
                {
                    IdCanBo_QuyetDinhThueDat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdQuyetDinhThueDat = table.Column<int>(type: "int", nullable: false),
                    IdCanBoQuanLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanBo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDoanhNghiep = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanBo_QuyetDinhThueDat", x => x.IdCanBo_QuyetDinhThueDat);
                });

            migrationBuilder.CreateTable(
                name: "QuanHuyen",
                columns: table => new
                {
                    IdQuanHuyen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuanHuyen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuanHuyen", x => x.IdQuanHuyen);
                });

            migrationBuilder.CreateTable(
                name: "PhuongXa",
                columns: table => new
                {
                    IdPhuongXa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdQuanHuyen = table.Column<int>(type: "int", nullable: false),
                    TenPhuongXa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongXa", x => x.IdPhuongXa);
                    table.ForeignKey(
                        name: "FK_PhuongXa_QuanHuyen_IdQuanHuyen",
                        column: x => x.IdQuanHuyen,
                        principalTable: "QuanHuyen",
                        principalColumn: "IdQuanHuyen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhuongXa_IdQuanHuyen",
                table: "PhuongXa",
                column: "IdQuanHuyen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanBo_QuyetDinhThueDat");

            migrationBuilder.DropTable(
                name: "PhuongXa");

            migrationBuilder.DropTable(
                name: "QuanHuyen");

            migrationBuilder.DropColumn(
                name: "IdQuyetDinhThueDatChiTiet",
                table: "ThongBaoTienThueDat");

            migrationBuilder.DropColumn(
                name: "IdQuyetDinhThueDatChiTiet",
                table: "ThongBaoTienSuDungDat");

            migrationBuilder.DropColumn(
                name: "IdQuyetDinhThueDatChiTiet",
                table: "ThongBaoDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "IdPhuongXa",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "IdQuanHuyen",
                table: "QuyetDinhThueDat");
        }
    }
}
