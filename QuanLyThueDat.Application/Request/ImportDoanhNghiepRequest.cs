using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Request
{
    public class ImportDoanhNghiepRequest
    {
        public DoanhNghiepRequest DoanhNghiepRequest { get; private set; } = new DoanhNghiepRequest();
        public QuyetDinhThueDatRequest QuyetDinhThueDatRequest { get; set; } = new QuyetDinhThueDatRequest();
        public HopDongThueDatRequest HopDongThueDatRequest { get; set; } = new HopDongThueDatRequest();
        public QuyetDinhMienTienThueDatRequest QuyetDinhMienTienThueDatRequest { get; set; } = new QuyetDinhMienTienThueDatRequest();
        public ThongBaoDonGiaThueDatRequest ThongBaoDonGiaThueDatRequest { get; set; } = new ThongBaoDonGiaThueDatRequest();
        public ThongBaoTienThueDatRequest ThongBaoTienThueDat2018Request { get; set; } = new ThongBaoTienThueDatRequest();
        public ThongBaoTienThueDatRequest ThongBaoTienThueDat2019Request { get; set; } = new ThongBaoTienThueDatRequest();
        public ThongBaoTienThueDatRequest ThongBaoTienThueDat2020Request { get; set; } = new ThongBaoTienThueDatRequest();
        public ThongBaoTienThueDatRequest ThongBaoTienThueDat2021Request { get; set; } = new ThongBaoTienThueDatRequest();
        public ThongBaoTienThueDatRequest ThongBaoTienThueDat2022Request { get; set; } = new ThongBaoTienThueDatRequest();
    }
}
