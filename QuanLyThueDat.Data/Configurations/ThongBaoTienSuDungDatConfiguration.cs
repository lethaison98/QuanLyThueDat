using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class ThongBaoTienSuDungDatConfiguration : IEntityTypeConfiguration<ThongBaoTienSuDungDat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ThongBaoTienSuDungDat> builder)
        {
            builder.ToTable("ThongBaoTienSuDungDat");
            builder.HasKey(x => x.IdThongBaoTienSuDungDat);
            builder.HasOne(x => x.DoanhNghiep).WithMany(hd => hd.DsThongBaoTienSuDungDat).HasForeignKey(x=> x.IdDoanhNghiep);
        }
    }
}
