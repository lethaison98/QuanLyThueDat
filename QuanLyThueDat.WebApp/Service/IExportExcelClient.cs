using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.WebApp.Service
{
    public interface IExportExcelClient
    {
        Task<ApiResult<byte[]>> ExportThongBaoTienThueDatHangNam(int namThongBao, int? idQuanHuyen, string keyword, string tuNgay, string denNgay);
        Task<ApiResult<byte[]>> ExportThongBaoDonGiaThueDat(int? idQuanHuyen, string keyword, string tuNgay,  string denNgay);
        Task<ApiResult<byte[]>> ExportQuyetDinhMienTienThueDat(int? idQuyetDinhMienTienThueDat, int? idQuanHuyen, string keyword, string tuNgay, string denNgay);
        Task<ApiResult<byte[]>> ExportBaoCaoDoanhNghiepThueDat();
        Task<ApiResult<byte[]>> ExportBieuLapBo();

    }
}
