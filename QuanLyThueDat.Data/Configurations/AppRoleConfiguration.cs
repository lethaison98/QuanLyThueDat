﻿using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Configurations
{
    internal class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("AppRole");
          
        }
    }
}
