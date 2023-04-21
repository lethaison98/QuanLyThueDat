using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DienTich",
                table: "HopDongThueDat",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ThongBaoGhiThuGhiChi",
                columns: table => new
                {
                    IdThongBaoGhiThuGhiChi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDoanhNghiep = table.Column<int>(type: "int", nullable: false),
                    IdQuyetDinhThueDat = table.Column<int>(type: "int", nullable: true),
                    SoThongBaoGhiThuGhiChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThongBaoGhiThuGhiChi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoTienGhiThu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoiDungGhiThu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoTienGhiChi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoiDungGhiChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoGhiThuGhiChi", x => x.IdThongBaoGhiThuGhiChi);
                    table.ForeignKey(
                        name: "FK_ThongBaoGhiThuGhiChi_DoanhNghiep_IdDoanhNghiep",
                        column: x => x.IdDoanhNghiep,
                        principalTable: "DoanhNghiep",
                        principalColumn: "IdDoanhNghiep",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoGhiThuGhiChi_IdDoanhNghiep",
                table: "ThongBaoGhiThuGhiChi",
                column: "IdDoanhNghiep");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongBaoGhiThuGhiChi");

            migrationBuilder.DropColumn(
                name: "DienTich",
                table: "HopDongThueDat");
        }
    }
}
