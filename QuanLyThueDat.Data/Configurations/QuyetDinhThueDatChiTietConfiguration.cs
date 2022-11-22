using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class QuyetDinhThueDatChiTietConfiguration : IEntityTypeConfiguration<QuyetDinhThueDatChiTiet>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<QuyetDinhThueDatChiTiet> builder)
        {
            builder.ToTable("QuyetDinhThueDatChiTiet");
            builder.HasKey(x => x.IdQuyetDinhThueDatChiTiet);
            builder.HasOne(x => x.QuyetDinhThueDat).WithMany(hd => hd.DsQuyetDinhThueDatChiTiet).HasForeignKey(x=> x.IdQuyetDinhThueDat);

        }
    }
}
