using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class ThongBaoDonGiaThueDatConfiguration : IEntityTypeConfiguration<ThongBaoDonGiaThueDat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ThongBaoDonGiaThueDat> builder)
        {
            builder.ToTable("ThongBaoDonGiaThueDat");
            builder.HasKey(x => x.IdThongBaoDonGiaThueDat);
            builder.HasOne(x => x.DoanhNghiep).WithMany(hd => hd.DsThongBaoDonGiaThueDat).HasForeignKey(x=> x.IdDoanhNghiep);
        }
    }
}
