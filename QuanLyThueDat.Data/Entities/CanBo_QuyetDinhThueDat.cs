using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class CanBo_QuyetDinhThueDat
    {
        public int IdCanBo_QuyetDinhThueDat { get; set; }
        public int IdQuyetDinhThueDat { get; set; }
        public string IdCanBoQuanLy { get; set; }  
        public string CanBo { get; set; }
        public string TenDoanhNghiep { get; set; }
    }
}
