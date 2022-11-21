using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class HopDongThueDatConfiguration : IEntityTypeConfiguration<HopDongThueDat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<HopDongThueDat> builder)
        {
            builder.ToTable("HopDongThueDat");
            builder.HasKey(x => x.IdHopDongThueDat);
            //builder.Property(x => x.IdDoanhNghiep).IsRequired();
            //builder.Property(x => x.IdQuyetDinhThueDat).IsRequired();
            builder.HasOne(x => x.DoanhNghiep).WithMany(hd => hd.DsHopDongThueDat).HasForeignKey(x=> x.IdDoanhNghiep);
        }
    }
}
