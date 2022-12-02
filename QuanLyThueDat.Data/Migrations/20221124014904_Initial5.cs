using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdNguoiCapNhat",
                table: "ThongBaoTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiTao",
                table: "ThongBaoTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "ThongBaoTienThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "ThongBaoTienThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiCapNhat",
                table: "ThongBaoTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "ThongBaoTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiCapNhat",
                table: "ThongBaoDonGiaThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiTao",
                table: "ThongBaoDonGiaThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "ThongBaoDonGiaThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "ThongBaoDonGiaThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiCapNhat",
                table: "ThongBaoDonGiaThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "ThongBaoDonGiaThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiCapNhat",
                table: "QuyetDinhThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiTao",
                table: "QuyetDinhThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "QuyetDinhThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "QuyetDinhThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiCapNhat",
                table: "QuyetDinhThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "QuyetDinhThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiCapNhat",
                table: "QuyetDinhMienTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiTao",
                table: "QuyetDinhMienTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "QuyetDinhMienTienThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "QuyetDinhMienTienThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiCapNhat",
                table: "QuyetDinhMienTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "QuyetDinhMienTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiCapNhat",
                table: "QuyetDinhGiaoLaiDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiTao",
                table: "QuyetDinhGiaoLaiDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "QuyetDinhGiaoLaiDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "QuyetDinhGiaoLaiDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiCapNhat",
                table: "QuyetDinhGiaoLaiDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "QuyetDinhGiaoLaiDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiCapNhat",
                table: "QuyetDinhGiaoDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiTao",
                table: "QuyetDinhGiaoDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "QuyetDinhGiaoDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "QuyetDinhGiaoDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiCapNhat",
                table: "QuyetDinhGiaoDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "QuyetDinhGiaoDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiCapNhat",
                table: "QuyetDinhDonGiaThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiTao",
                table: "QuyetDinhDonGiaThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "QuyetDinhDonGiaThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "QuyetDinhDonGiaThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiCapNhat",
                table: "QuyetDinhDonGiaThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "QuyetDinhDonGiaThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoQuanKy",
                table: "HopDongThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiCapNhat",
                table: "HopDongThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiTao",
                table: "HopDongThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "HopDongThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "HopDongThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiCapNhat",
                table: "HopDongThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiKy",
                table: "HopDongThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "HopDongThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiCapNhat",
                table: "DoanhNghiep",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNguoiTao",
                table: "DoanhNghiep",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "DoanhNghiep",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "DoanhNghiep",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiCapNhat",
                table: "DoanhNghiep",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "DoanhNghiep",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdNguoiCapNhat",
                table: "ThongBaoTienThueDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiTao",
                table: "ThongBaoTienThueDat");

            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "ThongBaoTienThueDat");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "ThongBaoTienThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiCapNhat",
                table: "ThongBaoTienThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "ThongBaoTienThueDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiCapNhat",
                table: "ThongBaoDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiTao",
                table: "ThongBaoDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "ThongBaoDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "ThongBaoDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiCapNhat",
                table: "ThongBaoDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "ThongBaoDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiCapNhat",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiTao",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiCapNhat",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiCapNhat",
                table: "QuyetDinhMienTienThueDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiTao",
                table: "QuyetDinhMienTienThueDat");

            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "QuyetDinhMienTienThueDat");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "QuyetDinhMienTienThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiCapNhat",
                table: "QuyetDinhMienTienThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "QuyetDinhMienTienThueDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiCapNhat",
                table: "QuyetDinhGiaoLaiDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiTao",
                table: "QuyetDinhGiaoLaiDat");

            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "QuyetDinhGiaoLaiDat");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "QuyetDinhGiaoLaiDat");

            migrationBuilder.DropColumn(
                name: "NguoiCapNhat",
                table: "QuyetDinhGiaoLaiDat");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "QuyetDinhGiaoLaiDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiCapNhat",
                table: "QuyetDinhGiaoDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiTao",
                table: "QuyetDinhGiaoDat");

            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "QuyetDinhGiaoDat");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "QuyetDinhGiaoDat");

            migrationBuilder.DropColumn(
                name: "NguoiCapNhat",
                table: "QuyetDinhGiaoDat");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "QuyetDinhGiaoDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiCapNhat",
                table: "QuyetDinhDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiTao",
                table: "QuyetDinhDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "QuyetDinhDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "QuyetDinhDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiCapNhat",
                table: "QuyetDinhDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "QuyetDinhDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "CoQuanKy",
                table: "HopDongThueDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiCapNhat",
                table: "HopDongThueDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiTao",
                table: "HopDongThueDat");

            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "HopDongThueDat");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "HopDongThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiCapNhat",
                table: "HopDongThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiKy",
                table: "HopDongThueDat");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "HopDongThueDat");

            migrationBuilder.DropColumn(
                name: "IdNguoiCapNhat",
                table: "DoanhNghiep");

            migrationBuilder.DropColumn(
                name: "IdNguoiTao",
                table: "DoanhNghiep");

            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "DoanhNghiep");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "DoanhNghiep");

            migrationBuilder.DropColumn(
                name: "NguoiCapNhat",
                table: "DoanhNghiep");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "DoanhNghiep");
        }
    }
}
