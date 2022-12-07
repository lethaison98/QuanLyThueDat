using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class FileTaiLieuConfiguration : IEntityTypeConfiguration<FileTaiLieu>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FileTaiLieu> builder)
        {
            builder.ToTable("FileTaiLieu");
            builder.HasKey(x => x.IdFileTaiLieu);
            builder.HasOne(x => x.File).WithOne(hd => hd.FileTaiLieu).HasForeignKey<FileTaiLieu>(x=> x.IdFile);

        }
    }
}
