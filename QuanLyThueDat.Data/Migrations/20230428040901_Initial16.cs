using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoTienMienGiam",
                table: "QuyetDinhMienTienThueDat",
                newName: "TongSoTienMienGiam");

            migrationBuilder.AddColumn<decimal>(
                name: "SoTienMienGiamTrongMotNam",
                table: "QuyetDinhMienTienThueDat",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoTienMienGiamTrongMotNam",
                table: "QuyetDinhMienTienThueDat");

            migrationBuilder.RenameColumn(
                name: "TongSoTienMienGiam",
                table: "QuyetDinhMienTienThueDat",
                newName: "SoTienMienGiam");
        }
    }
}
