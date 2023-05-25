using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class Initial17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaChuong",
                table: "DoanhNghiep",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaCoQuanQuanLyThu",
                table: "DoanhNghiep",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenChuong",
                table: "DoanhNghiep",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaChuong",
                table: "DoanhNghiep");

            migrationBuilder.DropColumn(
                name: "MaCoQuanQuanLyThu",
                table: "DoanhNghiep");

            migrationBuilder.DropColumn(
                name: "TenChuong",
                table: "DoanhNghiep");
        }
    }
}
