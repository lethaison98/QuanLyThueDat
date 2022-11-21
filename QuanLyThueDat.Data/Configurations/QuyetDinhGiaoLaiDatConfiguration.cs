using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class QuyetDinhGiaoLaiDatConfiguration : IEntityTypeConfiguration<QuyetDinhGiaoLaiDat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<QuyetDinhGiaoLaiDat> builder)
        {
            builder.ToTable("QuyetDinhGiaoLaiDat");
            builder.HasKey(x => x.IdQuyetDinhGiaoLaiDat);
            //builder.Property(x => x.IdDoanhNghiep).IsRequired();
            builder.HasOne(x => x.DoanhNghiep).WithMany(hd => hd.DsQuyetDinhGiaoLaiDat).HasForeignKey(x=> x.IdDoanhNghiep);

        }
    }
}
