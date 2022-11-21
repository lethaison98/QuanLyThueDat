using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class DoanhNghiepConfiguration : IEntityTypeConfiguration<DoanhNghiep>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DoanhNghiep> builder)
        {
            builder.ToTable("DoanhNghiep");
            builder.HasKey(x => x.IdDoanhNghiep);
            builder.Property(x => x.TenDoanhNghiep).IsRequired();

        }
    }
}
