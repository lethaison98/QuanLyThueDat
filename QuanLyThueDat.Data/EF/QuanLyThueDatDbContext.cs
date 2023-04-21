using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Configurations;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.EF
{
    public class QuanLyThueDatDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public QuanLyThueDatDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);


            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new DoanhNghiepConfiguration());
            modelBuilder.ApplyConfiguration(new FilesConfiguration());
            modelBuilder.ApplyConfiguration(new FileTaiLieuConfiguration());
            modelBuilder.ApplyConfiguration(new HopDongThueDatConfiguration());
            modelBuilder.ApplyConfiguration(new QuyetDinhDonGiaThueDatConfiguration());
            modelBuilder.ApplyConfiguration(new QuyetDinhGiaoDatConfiguration());
            modelBuilder.ApplyConfiguration(new QuyetDinhGiaoLaiDatConfiguration());
            modelBuilder.ApplyConfiguration(new QuyetDinhMienTienThueDatConfiguration());
            modelBuilder.ApplyConfiguration(new QuyetDinhThueDatConfiguration());
            modelBuilder.ApplyConfiguration(new QuyetDinhThueDatChiTietConfiguration());
            modelBuilder.ApplyConfiguration(new ThongBaoTienSuDungDatConfiguration());
            modelBuilder.ApplyConfiguration(new ThongBaoDonGiaThueDatConfiguration());
            modelBuilder.ApplyConfiguration(new ThongBaoTienThueDatConfiguration());
            modelBuilder.ApplyConfiguration(new ThongBaoTienThueDatChiTietConfiguration());
            modelBuilder.ApplyConfiguration(new ThongBaoGhiThuGhiChiConfiguration());
            modelBuilder.ApplyConfiguration(new PhuongXaConfiguration());
            modelBuilder.ApplyConfiguration(new QuanHuyenConfiguration());
            modelBuilder.ApplyConfiguration(new CanBo_QuyetDinhThueDatConfiguration());

            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<AppConfig> AppConfig { get; set; }
        public DbSet<AppRole> AppRole { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<FileTaiLieu> FileTaiLieu { get; set; }
        public DbSet<DoanhNghiep> DoanhNghiep { get; set; }
        public DbSet<QuyetDinhGiaoDat> QuyetDinhGiaoDat { get; set; }
        public DbSet<QuyetDinhGiaoLaiDat> QuyetDinhGiaoLaiDat { get; set; }
        public DbSet<QuyetDinhThueDat> QuyetDinhThueDat { get; set; }
        public DbSet<QuyetDinhThueDatChiTiet> QuyetDinhThueDatChiTiet { get; set; }
        public DbSet<QuyetDinhDonGiaThueDat> QuyetDinhDonGiaThueDat { get; set; }
        public DbSet<QuyetDinhMienTienThueDat> QuyetDinhMienTienThueDat { get; set; }
        public DbSet<HopDongThueDat> HopDongThueDat { get; set; }
        public DbSet<ThongBaoTienSuDungDat> ThongBaoTienSuDungDat { get; set; }
        public DbSet<ThongBaoDonGiaThueDat> ThongBaoDonGiaThueDat { get; set; }
        public DbSet<ThongBaoTienThueDat> ThongBaoTienThueDat { get; set; }
        public DbSet<ThongBaoTienThueDatChiTiet> ThongBaoTienThueDatChiTiet { get; set; }
        public DbSet<ThongBaoGhiThuGhiChi> ThongBaoGhiThuGhiChi { get; set; }
        public DbSet<PhuongXa> PhuongXa { get; set; }
        public DbSet<QuanHuyen> QuanHuyen { get; set; }
        public DbSet<CanBo_QuyetDinhThueDat> CanBo_QuyetDinhThueDat { get; set; }

    }
}
