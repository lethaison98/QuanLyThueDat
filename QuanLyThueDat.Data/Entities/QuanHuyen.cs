using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class QuanHuyen
    {
        public int IdQuanHuyen { get; set; }
        public string TenQuanHuyen { get; set; }
        public List<PhuongXa> DsPhuongXa { get; set; }

    }
}
