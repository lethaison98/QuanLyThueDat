using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class ThongBaoTienThueDatChiTietConfiguration : IEntityTypeConfiguration<ThongBaoTienThueDatChiTiet>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ThongBaoTienThueDatChiTiet> builder)
        {
            builder.ToTable("ThongBaoTienThueDatChiTiet");
            builder.HasKey(x => x.IdThongBaoTienThueDatChiTiet);
            builder.HasOne(x => x.ThongBaoTienThueDat).WithMany(hd => hd.DsThongBaoTienThueDatChiTiet).HasForeignKey(x=> x.IdThongBaoTienThueDat);

        }
    }
}
