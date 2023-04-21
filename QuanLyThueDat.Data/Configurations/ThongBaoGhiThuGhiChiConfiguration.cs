using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class ThongBaoGhiThuGhiChiConfiguration : IEntityTypeConfiguration<ThongBaoGhiThuGhiChi>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ThongBaoGhiThuGhiChi> builder)
        {
            builder.ToTable("ThongBaoGhiThuGhiChi");
            builder.HasKey(x => x.IdThongBaoGhiThuGhiChi);
            //builder.Property(x => x.IdDoanhNghiep).IsRequired();
            //builder.Property(x => x.IdQuyetDinhThueDat).IsRequired();
            builder.HasOne(x => x.DoanhNghiep).WithMany(hd => hd.DsThongBaoGhiThuGhiChi).HasForeignKey(x=> x.IdDoanhNghiep);
        }
    }
}
