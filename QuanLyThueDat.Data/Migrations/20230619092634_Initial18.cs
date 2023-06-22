using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdQuyetDinhMienTienThueDat",
                table: "ThongBaoTienThueDatChiTiet",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdQuyetDinhMienTienThueDat",
                table: "ThongBaoTienThueDatChiTiet");
        }
    }
}
