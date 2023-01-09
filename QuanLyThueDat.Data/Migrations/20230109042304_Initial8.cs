using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "DoanhNghiep",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ThongBaoTienSuDungDat",
                columns: table => new
                {
                    IdThongBaoTienSuDungDat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDoanhNghiep = table.Column<int>(type: "int", nullable: false),
                    IdQuyetDinhThueDat = table.Column<int>(type: "int", nullable: true),
                    IdHopDongThueDat = table.Column<int>(type: "int", nullable: true),
                    SoQuyetDinhThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenQuyetDinhThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayQuyetDinhThueDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MucDichSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViTriThuaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiThuaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiHanThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DenNgayThue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TuNgayThue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoThongBaoTienSuDungDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenThongBaoTienSuDungDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanThongBaoTienSuDungDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThongBaoTienSuDungDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DienTichKhongPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThoiHanDonGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienMienGiam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LyDoMienGiam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoTienBoiThuongGiaiPhongMatBang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LyDoBoiThuongGiaiPhongMatBang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoTienPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayHieuLucTienSuDungDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHetHieuLucTienSuDungDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HinhThucThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanhDaoKyThongBaoTienSuDungDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoTienSuDungDat", x => x.IdThongBaoTienSuDungDat);
                    table.ForeignKey(
                        name: "FK_ThongBaoTienSuDungDat_DoanhNghiep_IdDoanhNghiep",
                        column: x => x.IdDoanhNghiep,
                        principalTable: "DoanhNghiep",
                        principalColumn: "IdDoanhNghiep",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoTienSuDungDat_IdDoanhNghiep",
                table: "ThongBaoTienSuDungDat",
                column: "IdDoanhNghiep");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongBaoTienSuDungDat");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "DoanhNghiep");
        }
    }
}
