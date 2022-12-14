using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.WebApp.Service
{
    public interface IExportExcelClient
    {
        Task<ApiResult<byte[]>> ExportThongBaoTienThueDatHangNam(int namThongBao);
        Task<ApiResult<byte[]>> ExportQuyetDinhMienTienThueDat(int? idQuyetDinhMienTienThueDat);

    }
}
