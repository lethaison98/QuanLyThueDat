using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdThongBaoDonGiaThueDat",
                table: "ThongBaoTienThueDatChiTiet",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdThongBaoDonGiaThueDat",
                table: "ThongBaoTienThueDatChiTiet");
        }
    }
}
