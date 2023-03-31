﻿using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class QuanHuyenConfiguration : IEntityTypeConfiguration<QuanHuyen>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<QuanHuyen> builder)
        {
            builder.ToTable("QuanHuyen");
            builder.HasKey(x => x.IdQuanHuyen);

        }
    }
}
