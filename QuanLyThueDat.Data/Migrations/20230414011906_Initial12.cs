using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdThongBaoTienThueDatGoc",
                table: "ThongBaoTienThueDatChiTiet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BiDieuChinh",
                table: "ThongBaoTienThueDat",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdThongBaoTienThueDatGoc",
                table: "ThongBaoTienThueDatChiTiet");

            migrationBuilder.DropColumn(
                name: "BiDieuChinh",
                table: "ThongBaoTienThueDat");
        }
    }
}
