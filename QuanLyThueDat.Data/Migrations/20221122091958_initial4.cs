using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "QuyetDinhThueDatChiTiet",
                columns: table => new
                {
                    IdQuyetDinhThueDatChiTiet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdQuyetDinhThueDat = table.Column<int>(type: "int", nullable: false),
                    HinhThucThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThoiHanThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DenNgayThue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TuNgayThue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MucDichSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyetDinhThueDatChiTiet", x => x.IdQuyetDinhThueDatChiTiet);
                    table.ForeignKey(
                        name: "FK_QuyetDinhThueDatChiTiet_QuyetDinhThueDat_IdQuyetDinhThueDat",
                        column: x => x.IdQuyetDinhThueDat,
                        principalTable: "QuyetDinhThueDat",
                        principalColumn: "IdQuyetDinhThueDat",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuyetDinhThueDatChiTiet_IdQuyetDinhThueDat",
                table: "QuyetDinhThueDatChiTiet",
                column: "IdQuyetDinhThueDat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuyetDinhThueDatChiTiet");

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
    }
}
