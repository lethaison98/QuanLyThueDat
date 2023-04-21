using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.ViewModel.BaoCaoViewModel
{
    public class BaoCaoDoanhNghiepThueDatViewModel
    {
        public DoanhNghiepViewModel DoanhNghiepViewModel { get; set; } = new DoanhNghiepViewModel();
        public QuyetDinhGiaoLaiDatViewModel QuyetDinhGiaoLaiDatViewModel { get; set; }= new QuyetDinhGiaoLaiDatViewModel();
        public QuyetDinhThueDatViewModel QuyetDinhThueDatViewModel { get; set; } = new QuyetDinhThueDatViewModel();
        public HopDongThueDatViewModel HopDongThueDatViewModel { get; set; } = new HopDongThueDatViewModel();
        public QuyetDinhMienTienThueDatViewModel QuyetDinhMienTienThueDatViewModel { get; set; } = new QuyetDinhMienTienThueDatViewModel();
        public ThongBaoDonGiaThueDatViewModel ThongBaoDonGiaThueDatViewModel { get; set; } = new ThongBaoDonGiaThueDatViewModel();
    }
}
