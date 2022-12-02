using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LanThongBaoTienThueDat",
                table: "ThongBaoTienThueDat",
                newName: "LoaiThongBaoTienThueDat");

            migrationBuilder.AddColumn<string>(
                name: "LanhDaoKyThongBaoTienThueDat",
                table: "ThongBaoTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanhDaoKyThongBaoDonGiaThueDat",
                table: "ThongBaoDonGiaThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ThongBaoTienThueDatChiTiet",
                columns: table => new
                {
                    IdThongBaoTienThueDatChiTiet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdThongBaoTienThueDat = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    DienTichKhongPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienMienGiam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TuNgayTinhTien = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DenNgayTinhTien = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoTienThueDatChiTiet", x => x.IdThongBaoTienThueDatChiTiet);
                    table.ForeignKey(
                        name: "FK_ThongBaoTienThueDatChiTiet_ThongBaoTienThueDat_IdThongBaoTienThueDat",
                        column: x => x.IdThongBaoTienThueDat,
                        principalTable: "ThongBaoTienThueDat",
                        principalColumn: "IdThongBaoTienThueDat",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoTienThueDatChiTiet_IdThongBaoTienThueDat",
                table: "ThongBaoTienThueDatChiTiet",
                column: "IdThongBaoTienThueDat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongBaoTienThueDatChiTiet");

            migrationBuilder.DropColumn(
                name: "LanhDaoKyThongBaoTienThueDat",
                table: "ThongBaoTienThueDat");

            migrationBuilder.DropColumn(
                name: "LanhDaoKyThongBaoDonGiaThueDat",
                table: "ThongBaoDonGiaThueDat");

            migrationBuilder.RenameColumn(
                name: "LoaiThongBaoTienThueDat",
                table: "ThongBaoTienThueDat",
                newName: "LanThongBaoTienThueDat");
        }
    }
}
