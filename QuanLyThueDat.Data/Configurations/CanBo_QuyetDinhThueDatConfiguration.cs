using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class CanBo_QuyetDinhThueDatConfiguration : IEntityTypeConfiguration<CanBo_QuyetDinhThueDat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CanBo_QuyetDinhThueDat> builder)
        {
            builder.ToTable("CanBo_QuyetDinhThueDat");
            builder.HasKey(x => x.IdCanBo_QuyetDinhThueDat);
        }
    }
}
