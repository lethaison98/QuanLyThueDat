using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Interfaces
{
    public interface IHopDongThueDatService
    {
        public Task<ApiResult<bool>> InsertUpdate(HopDongThueDatRequest rq);
        public Task<ApiResult<bool>> Delete(int idHopDong);
        public Task<ApiResult<HopDongThueDatViewModel>> GetById(int idHopDong);
        public Task<ApiResult<List<HopDongThueDatViewModel>>> GetAll();
        public Task<ApiResult<PageViewModel<HopDongThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep, string keyword, int pageIndex, int pageSize);
    }
}
