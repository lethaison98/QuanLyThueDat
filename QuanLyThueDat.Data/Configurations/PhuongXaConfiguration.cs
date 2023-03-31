using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class PhuongXaConfiguration : IEntityTypeConfiguration<PhuongXa>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PhuongXa> builder)
        {
            builder.ToTable("PhuongXa");
            builder.HasKey(x => x.IdPhuongXa);
            builder.HasOne(x => x.QuanHuyen).WithMany(hd => hd.DsPhuongXa).HasForeignKey(x => x.IdQuanHuyen);

        }
    }
}
