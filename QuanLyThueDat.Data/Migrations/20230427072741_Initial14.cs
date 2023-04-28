using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ThongBaoTienThueDat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ThongBaoTienSuDungDat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ThongBaoGhiThuGhiChi",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ThongBaoDonGiaThueDat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuyetDinhThueDat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuyetDinhMienTienThueDat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuyetDinhGiaoLaiDat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuyetDinhGiaoDat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuyetDinhDonGiaThueDat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HopDongThueDat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FileTaiLieu",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Files",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DoanhNghiep",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ThongBaoTienThueDat");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ThongBaoTienSuDungDat");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ThongBaoGhiThuGhiChi");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ThongBaoDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuyetDinhThueDat");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuyetDinhMienTienThueDat");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuyetDinhGiaoLaiDat");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuyetDinhGiaoDat");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuyetDinhDonGiaThueDat");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HopDongThueDat");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FileTaiLieu");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DoanhNghiep");
        }
    }
}
