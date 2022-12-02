using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DienTich",
                table: "QuyetDinhThueDat",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "IdQuyetDinhThueDatChiTiet",
                table: "QuyetDinhThueDat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "QuyetDinhThueDat_DenNgayThue",
                table: "QuyetDinhThueDat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuyetDinhThueDat_HinhThucThue",
                table: "QuyetDinhThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuyetDinhThueDat_MucDichSuDung",
                table: "QuyetDinhThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuyetDinhThueDat_ThoiHanThue",
                table: "QuyetDinhThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "QuyetDinhThueDat_TuNgayThue",
                table: "QuyetDinhThueDat",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DienTich",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "IdQuyetDinhThueDatChiTiet",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "QuyetDinhThueDat_DenNgayThue",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "QuyetDinhThueDat_HinhThucThue",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "QuyetDinhThueDat_MucDichSuDung",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "QuyetDinhThueDat_ThoiHanThue",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "QuyetDinhThueDat_TuNgayThue",
                table: "QuyetDinhThueDat");
        }
    }
}
