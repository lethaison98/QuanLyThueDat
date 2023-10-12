using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.WebApp.Service
{
    public interface IExportExcelClient
    {
        Task<ApiResult<byte[]>> ExportThongBaoTienThueDatHangNam(int namThongBao, string tuNgay, string denNgay);
        Task<ApiResult<byte[]>> ExportThongBaoDonGiaThueDat(string tuNgay, string denNgay);
        Task<ApiResult<byte[]>> ExportQuyetDinhMienTienThueDat(int? idQuyetDinhMienTienThueDat, string tuNgay, string denNgay);
        Task<ApiResult<byte[]>> ExportBaoCaoDoanhNghiepThueDat();
        Task<ApiResult<byte[]>> ExportBieuLapBo();

    }
}
