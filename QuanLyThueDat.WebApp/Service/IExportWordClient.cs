using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.WebApp.Service
{
    public interface IExportWordClient
    {
        Task<ApiResult<byte[]>> CreateWordDocument(int idThongBao, string loaiThongBao);
    }
}
