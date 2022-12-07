using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Request
{
    public class FileUploadRequest
    {
        public int IdDoanhNghiep { get; set; }
        public IFormFile File { get; set; }

    }
}
