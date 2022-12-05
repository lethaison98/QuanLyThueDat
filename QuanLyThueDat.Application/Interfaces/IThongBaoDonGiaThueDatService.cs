using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Interfaces
{
    public interface IThongBaoDonGiaThueDatService
    {
        public Task<ApiResult<int>> InsertUpdate(ThongBaoDonGiaThueDatRequest rq);
        public Task<ApiResult<bool>> Delete(int idThongBaoDonGiaThueDat);
        public Task<ApiResult<ThongBaoDonGiaThueDatViewModel>> GetById(int idThongBaoDonGiaThueDat);
        public Task<ApiResult<List<ThongBaoDonGiaThueDatViewModel>>> GetAll();
        public Task<ApiResult<PageViewModel<ThongBaoDonGiaThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep, string keyword, int pageIndex, int pageSize);
        public Task<ApiResult<List<ThongBaoDonGiaThueDatViewModel>>> GetAllByRequest(ThongBaoDonGiaThueDatRequest rq);
        public Task<ApiResult<List<ThongBaoDonGiaThueDatViewModel>>> CanhBaoThongBaoDonGiaThueDatSapHetHan();

    }
}
