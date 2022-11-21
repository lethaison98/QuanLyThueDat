using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class QuyetDinhDonGiaThueDatConfiguration : IEntityTypeConfiguration<QuyetDinhDonGiaThueDat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<QuyetDinhDonGiaThueDat> builder)
        {
            builder.ToTable("QuyetDinhDonGiaThueDat");
            builder.HasKey(x => x.IdQuyetDinhDonGiaThueDat);
            //builder.Property(x => x.IdDoanhNghiep).IsRequired();
            builder.HasOne(x => x.DoanhNghiep).WithMany(hd => hd.DsQuyetDinhDonGiaThueDat).HasForeignKey(x=> x.IdDoanhNghiep);

        }
    }
}
