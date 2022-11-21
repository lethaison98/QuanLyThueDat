using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class QuyetDinhThueDatConfiguration : IEntityTypeConfiguration<QuyetDinhThueDat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<QuyetDinhThueDat> builder)
        {
            builder.ToTable("QuyetDinhThueDat");
            builder.HasKey(x => x.IdQuyetDinhThueDat);
            //builder.Property(x => x.IdDoanhNghiep).IsRequired();
            //builder.Property(x => x.IdQuyetDinhGiaoDat).IsRequired();
            builder.HasOne(x => x.DoanhNghiep).WithMany(hd => hd.DsQuyetDinhThueDat).HasForeignKey(x=> x.IdDoanhNghiep);

        }
    }
}
