using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoaiQuyetDinhMienTienThueDat",
                table: "QuyetDinhMienTienThueDat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SoTienMienGiam",
                table: "QuyetDinhMienTienThueDat",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoaiQuyetDinhMienTienThueDat",
                table: "QuyetDinhMienTienThueDat");

            migrationBuilder.DropColumn(
                name: "SoTienMienGiam",
                table: "QuyetDinhMienTienThueDat");
        }
    }
}
