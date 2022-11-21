using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppConfig",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfig", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AppRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonVi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DoanhNghiep",
                columns: table => new
                {
                    IdDoanhNghiep = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDoanhNghiep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNguoiDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoQuanQuanLyThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSoThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoiCap = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoanhNghiep", x => x.IdDoanhNghiep);
                });

            migrationBuilder.CreateTable(
                name: "HopDongThueDat",
                columns: table => new
                {
                    IdHopDongThueDat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDoanhNghiep = table.Column<int>(type: "int", nullable: false),
                    IdQuyetDinhThueDat = table.Column<int>(type: "int", nullable: true),
                    SoHopDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenHopDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayKyHopDong = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHieuLucHopDong = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHetHieuLucHopDong = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDongThueDat", x => x.IdHopDongThueDat);
                    table.ForeignKey(
                        name: "FK_HopDongThueDat_DoanhNghiep_IdDoanhNghiep",
                        column: x => x.IdDoanhNghiep,
                        principalTable: "DoanhNghiep",
                        principalColumn: "IdDoanhNghiep",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuyetDinhDonGiaThueDat",
                columns: table => new
                {
                    IdQuyetDinhDonGiaThueDat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDoanhNghiep = table.Column<int>(type: "int", nullable: false),
                    SoQuyetDinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayKy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ViTriThuaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayHieuLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHetHieuLuc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyetDinhDonGiaThueDat", x => x.IdQuyetDinhDonGiaThueDat);
                    table.ForeignKey(
                        name: "FK_QuyetDinhDonGiaThueDat_DoanhNghiep_IdDoanhNghiep",
                        column: x => x.IdDoanhNghiep,
                        principalTable: "DoanhNghiep",
                        principalColumn: "IdDoanhNghiep",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuyetDinhGiaoDat",
                columns: table => new
                {
                    IdQuyetDinhGiaoDat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoQuyetDinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayKy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ViTriThuaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayHieuLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHetHieuLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DoanhNghiepIdDoanhNghiep = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyetDinhGiaoDat", x => x.IdQuyetDinhGiaoDat);
                    table.ForeignKey(
                        name: "FK_QuyetDinhGiaoDat_DoanhNghiep_DoanhNghiepIdDoanhNghiep",
                        column: x => x.DoanhNghiepIdDoanhNghiep,
                        principalTable: "DoanhNghiep",
                        principalColumn: "IdDoanhNghiep");
                });

            migrationBuilder.CreateTable(
                name: "QuyetDinhGiaoLaiDat",
                columns: table => new
                {
                    IdQuyetDinhGiaoLaiDat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDoanhNghiep = table.Column<int>(type: "int", nullable: false),
                    SoQuyetDinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayKy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ViTriThuaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichKhongPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    NgayHieuLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHetHieuLuc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyetDinhGiaoLaiDat", x => x.IdQuyetDinhGiaoLaiDat);
                    table.ForeignKey(
                        name: "FK_QuyetDinhGiaoLaiDat_DoanhNghiep_IdDoanhNghiep",
                        column: x => x.IdDoanhNghiep,
                        principalTable: "DoanhNghiep",
                        principalColumn: "IdDoanhNghiep",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuyetDinhMienTienThueDat",
                columns: table => new
                {
                    IdQuyetDinhMienTienThueDat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDoanhNghiep = table.Column<int>(type: "int", nullable: false),
                    IdQuyetDinhThueDat = table.Column<int>(type: "int", nullable: true),
                    SoQuyetDinhMienTienThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenQuyetDinhMienTienThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayQuyetDinhMienTienThueDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DienTichMienTienThueDat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThoiHanMienTienThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayHieuLucMienTienThueDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHetHieuLucMienTienThueDat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyetDinhMienTienThueDat", x => x.IdQuyetDinhMienTienThueDat);
                    table.ForeignKey(
                        name: "FK_QuyetDinhMienTienThueDat_DoanhNghiep_IdDoanhNghiep",
                        column: x => x.IdDoanhNghiep,
                        principalTable: "DoanhNghiep",
                        principalColumn: "IdDoanhNghiep",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuyetDinhThueDat",
                columns: table => new
                {
                    IdQuyetDinhThueDat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDoanhNghiep = table.Column<int>(type: "int", nullable: false),
                    SoQuyetDinhThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenQuyetDinhThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayQuyetDinhThueDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoQuyetDinhGiaoDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenQuyetDinhGiaoDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayQuyetDinhGiaoDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThoiHanThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DenNgayThue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TuNgayThue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MucDichSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhThucThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViTriThuaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiThuaDat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyetDinhThueDat", x => x.IdQuyetDinhThueDat);
                    table.ForeignKey(
                        name: "FK_QuyetDinhThueDat_DoanhNghiep_IdDoanhNghiep",
                        column: x => x.IdDoanhNghiep,
                        principalTable: "DoanhNghiep",
                        principalColumn: "IdDoanhNghiep",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongBaoDonGiaThueDat",
                columns: table => new
                {
                    IdThongBaoDonGiaThueDat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDoanhNghiep = table.Column<int>(type: "int", nullable: false),
                    IdQuyetDinhThueDat = table.Column<int>(type: "int", nullable: true),
                    IdHopDongThueDat = table.Column<int>(type: "int", nullable: true),
                    SoQuyetDinhThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenQuyetDinhThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayQuyetDinhThueDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MucDichSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViTriThuaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiThuaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiHanThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DenNgayThue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TuNgayThue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoThongBaoDonGiaThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenThongBaoDonGiaThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanThongBaoDonGiaThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThongBaoDonGiaThueDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DienTichKhongPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThoiHanDonGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayHieuLucDonGiaThueDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHetHieuLucDonGiaThueDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HinhThucThue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoDonGiaThueDat", x => x.IdThongBaoDonGiaThueDat);
                    table.ForeignKey(
                        name: "FK_ThongBaoDonGiaThueDat_DoanhNghiep_IdDoanhNghiep",
                        column: x => x.IdDoanhNghiep,
                        principalTable: "DoanhNghiep",
                        principalColumn: "IdDoanhNghiep",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongBaoTienThueDat",
                columns: table => new
                {
                    IdThongBaoTienThueDat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDoanhNghiep = table.Column<int>(type: "int", nullable: false),
                    IdQuyetDinhThueDat = table.Column<int>(type: "int", nullable: true),
                    IdHopDongThueDat = table.Column<int>(type: "int", nullable: true),
                    IdQuyetDinhMienTienThueDat = table.Column<int>(type: "int", nullable: true),
                    IdThongBaoDonGiaThueDat = table.Column<int>(type: "int", nullable: true),
                    SoThongBaoTienThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    NgayThongBaoTienThueDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LanThongBaoTienThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoQuyetDinhThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenQuyetDinhThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayQuyetDinhThueDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MucDichSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViTriThuaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiThuaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiHanThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DenNgayThue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TuNgayThue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoThongBaoDonGiaThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenThongBaoDonGiaThueDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThongBaoDonGiaThueDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichKhongPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienMienGiam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienPhaiNop = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoTienThueDat", x => x.IdThongBaoTienThueDat);
                    table.ForeignKey(
                        name: "FK_ThongBaoTienThueDat_DoanhNghiep_IdDoanhNghiep",
                        column: x => x.IdDoanhNghiep,
                        principalTable: "DoanhNghiep",
                        principalColumn: "IdDoanhNghiep",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HopDongThueDat_IdDoanhNghiep",
                table: "HopDongThueDat",
                column: "IdDoanhNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_QuyetDinhDonGiaThueDat_IdDoanhNghiep",
                table: "QuyetDinhDonGiaThueDat",
                column: "IdDoanhNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_QuyetDinhGiaoDat_DoanhNghiepIdDoanhNghiep",
                table: "QuyetDinhGiaoDat",
                column: "DoanhNghiepIdDoanhNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_QuyetDinhGiaoLaiDat_IdDoanhNghiep",
                table: "QuyetDinhGiaoLaiDat",
                column: "IdDoanhNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_QuyetDinhMienTienThueDat_IdDoanhNghiep",
                table: "QuyetDinhMienTienThueDat",
                column: "IdDoanhNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_QuyetDinhThueDat_IdDoanhNghiep",
                table: "QuyetDinhThueDat",
                column: "IdDoanhNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoDonGiaThueDat_IdDoanhNghiep",
                table: "ThongBaoDonGiaThueDat",
                column: "IdDoanhNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoTienThueDat_IdDoanhNghiep",
                table: "ThongBaoTienThueDat",
                column: "IdDoanhNghiep");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfig");

            migrationBuilder.DropTable(
                name: "AppRole");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "HopDongThueDat");

            migrationBuilder.DropTable(
                name: "QuyetDinhDonGiaThueDat");

            migrationBuilder.DropTable(
                name: "QuyetDinhGiaoDat");

            migrationBuilder.DropTable(
                name: "QuyetDinhGiaoLaiDat");

            migrationBuilder.DropTable(
                name: "QuyetDinhMienTienThueDat");

            migrationBuilder.DropTable(
                name: "QuyetDinhThueDat");

            migrationBuilder.DropTable(
                name: "ThongBaoDonGiaThueDat");

            migrationBuilder.DropTable(
                name: "ThongBaoTienThueDat");

            migrationBuilder.DropTable(
                name: "DoanhNghiep");
        }
    }
}
