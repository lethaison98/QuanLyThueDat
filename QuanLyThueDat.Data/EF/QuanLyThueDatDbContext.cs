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
            modelBuilder.ApplyConfiguration(new HopDongThueDatConfiguration());
            modelBuilder.ApplyConfiguration(new QuyetDinhDonGiaThueDatConfiguration());
            modelBuilder.ApplyConfiguration(new QuyetDinhGiaoDatConfiguration());
            modelBuilder.ApplyConfiguration(new QuyetDinhGiaoLaiDatConfiguration());
            modelBuilder.ApplyConfiguration(new QuyetDinhMienTienThueDatConfiguration());
            modelBuilder.ApplyConfiguration(new QuyetDinhThueDatConfiguration());
            modelBuilder.ApplyConfiguration(new ThongBaoDonGiaThueDatConfiguration());
            modelBuilder.ApplyConfiguration(new ThongBaoTienThueDatConfiguration());

            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<AppConfig> AppConfig { get; set; }
        public DbSet<AppRole> AppRole { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<DoanhNghiep> DoanhNghiep { get; set; }
        public DbSet<QuyetDinhGiaoDat> QuyetDinhGiaoDat { get; set; }
        public DbSet<QuyetDinhGiaoLaiDat> QuyetDinhGiaoLaiDat { get; set; }
        public DbSet<QuyetDinhThueDat> QuyetDinhThueDat { get; set; }
        public DbSet<QuyetDinhDonGiaThueDat> QuyetDinhDonGiaThueDat { get; set; }
        public DbSet<QuyetDinhMienTienThueDat> QuyetDinhMienTienThueDat { get; set; }
        public DbSet<HopDongThueDat> HopDongThueDat { get; set; }
        public DbSet<ThongBaoDonGiaThueDat> ThongBaoDonGiaThueDat { get; set; }
        public DbSet<ThongBaoTienThueDat> ThongBaoTienThueDat { get; set; }
    }
}
