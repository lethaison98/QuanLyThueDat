using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class QuyetDinhGiaoDatConfiguration : IEntityTypeConfiguration<QuyetDinhGiaoDat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<QuyetDinhGiaoDat> builder)
        {
            builder.ToTable("QuyetDinhGiaoDat");
            builder.HasKey(x => x.IdQuyetDinhGiaoDat);
            //builder.Property(x => x.IdDoanhNghiep).IsRequired();

        }
    }
}
