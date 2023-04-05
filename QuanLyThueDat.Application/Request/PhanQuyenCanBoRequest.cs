using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Request
{
    public class PhanQuyenCanBoRequest
    {
        public List<string> IdCanBo { get; set; }
        public List<int> IdQuyetDinhThueDat { get; set; }
    }
}
