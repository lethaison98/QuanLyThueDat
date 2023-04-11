using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Inital11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "ThongBaoTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "ThongBaoTienSuDungDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "ThongBaoDonGiaThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "QuyetDinhThueDatChiTiet",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "QuyetDinhThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoQuyetDinhGiaoDatDieuChinh",
                table: "QuyetDinhThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoQuyetDinhThueDatDieuChinh",
                table: "QuyetDinhThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "QuyetDinhMienTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoQuyetDinhMienTienThueDatDieuChinh",
                table: "QuyetDinhMienTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "HopDongThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoHopDongDieuChinh",
                table: "HopDongThueDat",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "ThongBaoTienThueDat");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "ThongBaoTienSuDungDat");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "ThongBaoDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "QuyetDinhThueDatChiTiet");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "SoQuyetDinhGiaoDatDieuChinh",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "SoQuyetDinhThueDatDieuChinh",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "QuyetDinhMienTienThueDat");

            migrationBuilder.DropColumn(
                name: "SoQuyetDinhMienTienThueDatDieuChinh",
                table: "QuyetDinhMienTienThueDat");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "HopDongThueDat");

            migrationBuilder.DropColumn(
                name: "SoHopDongDieuChinh",
                table: "HopDongThueDat");
        }
    }
}
