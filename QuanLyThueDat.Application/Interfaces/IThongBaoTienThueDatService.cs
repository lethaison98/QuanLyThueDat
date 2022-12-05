using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Interfaces
{
    public interface IThongBaoTienThueDatService
    {
        public Task<ApiResult<int>> InsertUpdate(ThongBaoTienThueDatRequest rq);
        public Task<ApiResult<bool>> Delete(int idThongBaoTienThueDat);
        public Task<ApiResult<ThongBaoTienThueDatViewModel>> GetById(int idThongBaoTienThueDat);
        public Task<ApiResult<List<ThongBaoTienThueDatViewModel>>> GetAll();
        public Task<ApiResult<PageViewModel<ThongBaoTienThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep, string keyword, int pageIndex, int pageSize);
    }
}
