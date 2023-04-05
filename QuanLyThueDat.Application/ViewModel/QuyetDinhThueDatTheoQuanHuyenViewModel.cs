using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.ViewModel
{
    public class QuyetDinhThueDatTheoQuanHuyenViewModel
    {
        public int IdQuanHuyen { get; set; }
        public string TenQuanHuyen { get; set; }
        public List<QuyetDinhThueDatViewModel> DsQuyetDinhThueDat { get; set; }

    }
}
