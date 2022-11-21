using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class QuyetDinhMienTienThueDatConfiguration : IEntityTypeConfiguration<QuyetDinhMienTienThueDat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<QuyetDinhMienTienThueDat> builder)
        {
            builder.ToTable("QuyetDinhMienTienThueDat");
            builder.HasKey(x => x.IdQuyetDinhMienTienThueDat);
            //builder.Property(x => x.IdDoanhNghiep).IsRequired();
            builder.HasOne(x => x.DoanhNghiep).WithMany(hd => hd.DsQuyetDinhMienTienThueDat).HasForeignKey(x=> x.IdDoanhNghiep);
        }
    }
}
