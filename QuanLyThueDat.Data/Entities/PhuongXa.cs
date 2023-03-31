using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class PhuongXa
    {
        public int IdPhuongXa { get; set; }
        public int IdQuanHuyen { get; set; }
        public QuanHuyen QuanHuyen { get; set; }
        public string TenPhuongXa { get; set; }
    }
}
