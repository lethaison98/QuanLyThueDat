using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class ThongBaoTienThueDatConfiguration : IEntityTypeConfiguration<ThongBaoTienThueDat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ThongBaoTienThueDat> builder)
        {
            builder.ToTable("ThongBaoTienThueDat");
            builder.HasKey(x => x.IdThongBaoTienThueDat);
            //builder.Property(x => x.IdDoanhNghiep).IsRequired();
            builder.HasOne(x => x.DoanhNghiep).WithMany(hd => hd.DsThongBaoTienThueDat).HasForeignKey(x=> x.IdDoanhNghiep);
        }
    }
}
