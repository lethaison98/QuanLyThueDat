﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLyThueDat.Data.EF;

#nullable disable

namespace QuanLyThueDat.Data.Migrations
{
    [DbContext(typeof(QuanLyThueDatDbContext))]
    partial class QuanLyThueDatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.AppConfig", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.ToTable("AppConfig", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRole", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonVi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("HoTen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUser", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.DoanhNghiep", b =>
                {
                    b.Property<int>("IdDoanhNghiep")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDoanhNghiep"), 1L, 1);

                    b.Property<string>("CoQuanQuanLyThue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaSoThue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCap")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiCap")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenDoanhNghiep")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenNguoiDaiDien")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDoanhNghiep");

                    b.ToTable("DoanhNghiep", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.HopDongThueDat", b =>
                {
                    b.Property<int>("IdHopDongThueDat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHopDongThueDat"), 1L, 1);

                    b.Property<string>("CoQuanKy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdDoanhNghiep")
                        .HasColumnType("int");

                    b.Property<string>("IdNguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdQuyetDinhThueDat")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHetHieuLucHopDong")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHieuLucHopDong")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKyHopDong")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiKy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoHopDong")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenHopDong")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdHopDongThueDat");

                    b.HasIndex("IdDoanhNghiep");

                    b.ToTable("HopDongThueDat", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhDonGiaThueDat", b =>
                {
                    b.Property<int>("IdQuyetDinhDonGiaThueDat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdQuyetDinhDonGiaThueDat"), 1L, 1);

                    b.Property<decimal>("DienTich")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdDoanhNghiep")
                        .HasColumnType("int");

                    b.Property<string>("IdNguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHetHieuLuc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHieuLuc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKy")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoQuyetDinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ViTriThuaDat")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdQuyetDinhDonGiaThueDat");

                    b.HasIndex("IdDoanhNghiep");

                    b.ToTable("QuyetDinhDonGiaThueDat", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhGiaoDat", b =>
                {
                    b.Property<int>("IdQuyetDinhGiaoDat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdQuyetDinhGiaoDat"), 1L, 1);

                    b.Property<decimal>("DienTich")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("DoanhNghiepIdDoanhNghiep")
                        .HasColumnType("int");

                    b.Property<string>("IdNguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHetHieuLuc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHieuLuc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKy")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoQuyetDinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ViTriThuaDat")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdQuyetDinhGiaoDat");

                    b.HasIndex("DoanhNghiepIdDoanhNghiep");

                    b.ToTable("QuyetDinhGiaoDat", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhGiaoLaiDat", b =>
                {
                    b.Property<int>("IdQuyetDinhGiaoLaiDat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdQuyetDinhGiaoLaiDat"), 1L, 1);

                    b.Property<decimal>("DienTichKhongPhaiNop")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DienTichPhaiNop")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdDoanhNghiep")
                        .HasColumnType("int");

                    b.Property<string>("IdNguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHetHieuLuc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHieuLuc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKy")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoQuyetDinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TongDienTich")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.Property<string>("ViTriThuaDat")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdQuyetDinhGiaoLaiDat");

                    b.HasIndex("IdDoanhNghiep");

                    b.ToTable("QuyetDinhGiaoLaiDat", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhMienTienThueDat", b =>
                {
                    b.Property<int>("IdQuyetDinhMienTienThueDat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdQuyetDinhMienTienThueDat"), 1L, 1);

                    b.Property<decimal>("DienTichMienTienThueDat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdDoanhNghiep")
                        .HasColumnType("int");

                    b.Property<string>("IdNguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdQuyetDinhThueDat")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHetHieuLucMienTienThueDat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHieuLucMienTienThueDat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayQuyetDinhMienTienThueDat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoQuyetDinhMienTienThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenQuyetDinhMienTienThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThoiHanMienTienThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdQuyetDinhMienTienThueDat");

                    b.HasIndex("IdDoanhNghiep");

                    b.ToTable("QuyetDinhMienTienThueDat", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhThueDat", b =>
                {
                    b.Property<int>("IdQuyetDinhThueDat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdQuyetDinhThueDat"), 1L, 1);

                    b.Property<DateTime?>("DenNgayThue")
                        .HasColumnType("datetime2");

                    b.Property<string>("DiaChiThuaDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhThucThue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdDoanhNghiep")
                        .HasColumnType("int");

                    b.Property<string>("IdNguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MucDichSuDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayQuyetDinhGiaoDat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayQuyetDinhThueDat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoQuyetDinhGiaoDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoQuyetDinhThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenQuyetDinhGiaoDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenQuyetDinhThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThoiHanThue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TongDienTich")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("TuNgayThue")
                        .HasColumnType("datetime2");

                    b.Property<string>("ViTriThuaDat")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdQuyetDinhThueDat");

                    b.HasIndex("IdDoanhNghiep");

                    b.ToTable("QuyetDinhThueDat", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhThueDatChiTiet", b =>
                {
                    b.Property<int>("IdQuyetDinhThueDatChiTiet")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdQuyetDinhThueDatChiTiet"), 1L, 1);

                    b.Property<DateTime?>("DenNgayThue")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DienTich")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("HinhThucThue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdQuyetDinhThueDat")
                        .HasColumnType("int");

                    b.Property<string>("MucDichSuDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThoiHanThue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TuNgayThue")
                        .HasColumnType("datetime2");

                    b.HasKey("IdQuyetDinhThueDatChiTiet");

                    b.HasIndex("IdQuyetDinhThueDat");

                    b.ToTable("QuyetDinhThueDatChiTiet", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.ThongBaoDonGiaThueDat", b =>
                {
                    b.Property<int>("IdThongBaoDonGiaThueDat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdThongBaoDonGiaThueDat"), 1L, 1);

                    b.Property<DateTime?>("DenNgayThue")
                        .HasColumnType("datetime2");

                    b.Property<string>("DiaChiThuaDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DienTichKhongPhaiNop")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DienTichPhaiNop")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DonGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("HinhThucThue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdDoanhNghiep")
                        .HasColumnType("int");

                    b.Property<int?>("IdHopDongThueDat")
                        .HasColumnType("int");

                    b.Property<string>("IdNguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdQuyetDinhThueDat")
                        .HasColumnType("int");

                    b.Property<string>("LanThongBaoDonGiaThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MucDichSuDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHetHieuLucDonGiaThueDat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHieuLucDonGiaThueDat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayQuyetDinhThueDat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayThongBaoDonGiaThueDat")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoQuyetDinhThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoThongBaoDonGiaThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenQuyetDinhThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenThongBaoDonGiaThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThoiHanDonGia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThoiHanThue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TongDienTich")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("TuNgayThue")
                        .HasColumnType("datetime2");

                    b.Property<string>("ViTriThuaDat")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdThongBaoDonGiaThueDat");

                    b.HasIndex("IdDoanhNghiep");

                    b.ToTable("ThongBaoDonGiaThueDat", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.ThongBaoTienThueDat", b =>
                {
                    b.Property<int>("IdThongBaoTienThueDat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdThongBaoTienThueDat"), 1L, 1);

                    b.Property<DateTime?>("DenNgayThue")
                        .HasColumnType("datetime2");

                    b.Property<string>("DiaChiThuaDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DienTichKhongPhaiNop")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DienTichPhaiNop")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DonGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdDoanhNghiep")
                        .HasColumnType("int");

                    b.Property<int?>("IdHopDongThueDat")
                        .HasColumnType("int");

                    b.Property<string>("IdNguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdQuyetDinhMienTienThueDat")
                        .HasColumnType("int");

                    b.Property<int?>("IdQuyetDinhThueDat")
                        .HasColumnType("int");

                    b.Property<int?>("IdThongBaoDonGiaThueDat")
                        .HasColumnType("int");

                    b.Property<string>("LanThongBaoTienThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MucDichSuDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nam")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayQuyetDinhThueDat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayThongBaoDonGiaThueDat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayThongBaoTienThueDat")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoQuyetDinhThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoThongBaoDonGiaThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoThongBaoTienThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SoTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SoTienMienGiam")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SoTienPhaiNop")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TenQuyetDinhThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenThongBaoDonGiaThueDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThoiHanThue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TongDienTich")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("TuNgayThue")
                        .HasColumnType("datetime2");

                    b.Property<string>("ViTriThuaDat")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdThongBaoTienThueDat");

                    b.HasIndex("IdDoanhNghiep");

                    b.ToTable("ThongBaoTienThueDat", (string)null);
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.HopDongThueDat", b =>
                {
                    b.HasOne("QuanLyThueDat.Data.Entities.DoanhNghiep", "DoanhNghiep")
                        .WithMany("DsHopDongThueDat")
                        .HasForeignKey("IdDoanhNghiep")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DoanhNghiep");
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhDonGiaThueDat", b =>
                {
                    b.HasOne("QuanLyThueDat.Data.Entities.DoanhNghiep", "DoanhNghiep")
                        .WithMany("DsQuyetDinhDonGiaThueDat")
                        .HasForeignKey("IdDoanhNghiep")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DoanhNghiep");
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhGiaoDat", b =>
                {
                    b.HasOne("QuanLyThueDat.Data.Entities.DoanhNghiep", null)
                        .WithMany("DsQuyetDinhGiaoDat")
                        .HasForeignKey("DoanhNghiepIdDoanhNghiep");
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhGiaoLaiDat", b =>
                {
                    b.HasOne("QuanLyThueDat.Data.Entities.DoanhNghiep", "DoanhNghiep")
                        .WithMany("DsQuyetDinhGiaoLaiDat")
                        .HasForeignKey("IdDoanhNghiep")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DoanhNghiep");
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhMienTienThueDat", b =>
                {
                    b.HasOne("QuanLyThueDat.Data.Entities.DoanhNghiep", "DoanhNghiep")
                        .WithMany("DsQuyetDinhMienTienThueDat")
                        .HasForeignKey("IdDoanhNghiep")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DoanhNghiep");
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhThueDat", b =>
                {
                    b.HasOne("QuanLyThueDat.Data.Entities.DoanhNghiep", "DoanhNghiep")
                        .WithMany("DsQuyetDinhThueDat")
                        .HasForeignKey("IdDoanhNghiep")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DoanhNghiep");
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhThueDatChiTiet", b =>
                {
                    b.HasOne("QuanLyThueDat.Data.Entities.QuyetDinhThueDat", "QuyetDinhThueDat")
                        .WithMany("DsQuyetDinhThueDatChiTiet")
                        .HasForeignKey("IdQuyetDinhThueDat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuyetDinhThueDat");
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.ThongBaoDonGiaThueDat", b =>
                {
                    b.HasOne("QuanLyThueDat.Data.Entities.DoanhNghiep", "DoanhNghiep")
                        .WithMany("DsThongBaoDonGiaThueDat")
                        .HasForeignKey("IdDoanhNghiep")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DoanhNghiep");
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.ThongBaoTienThueDat", b =>
                {
                    b.HasOne("QuanLyThueDat.Data.Entities.DoanhNghiep", "DoanhNghiep")
                        .WithMany("DsThongBaoTienThueDat")
                        .HasForeignKey("IdDoanhNghiep")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DoanhNghiep");
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.DoanhNghiep", b =>
                {
                    b.Navigation("DsHopDongThueDat");

                    b.Navigation("DsQuyetDinhDonGiaThueDat");

                    b.Navigation("DsQuyetDinhGiaoDat");

                    b.Navigation("DsQuyetDinhGiaoLaiDat");

                    b.Navigation("DsQuyetDinhMienTienThueDat");

                    b.Navigation("DsQuyetDinhThueDat");

                    b.Navigation("DsThongBaoDonGiaThueDat");

                    b.Navigation("DsThongBaoTienThueDat");
                });

            modelBuilder.Entity("QuanLyThueDat.Data.Entities.QuyetDinhThueDat", b =>
                {
                    b.Navigation("DsQuyetDinhThueDatChiTiet");
                });
#pragma warning restore 612, 618
        }
    }
}
