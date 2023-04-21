using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Interfaces
{
    public interface IThongBaoGhiThuGhiChiService
    {
        public Task<ApiResult<int>> InsertUpdate(ThongBaoGhiThuGhiChiRequest rq);
        public Task<ApiResult<bool>> Delete(int idHopDong);
        public Task<ApiResult<ThongBaoGhiThuGhiChiViewModel>> GetById(int idHopDong);
        public Task<ApiResult<List<ThongBaoGhiThuGhiChiViewModel>>> GetAll(int? idDoanhNghiep);
        public Task<ApiResult<PageViewModel<ThongBaoGhiThuGhiChiViewModel>>> GetAllPaging(int? idDoanhNghiep, string keyword, int pageIndex, int pageSize);
    }
}
