using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Interfaces
{
    public interface IQuyetDinhThueDatService
    {
        public Task<ApiResult<int>> InsertUpdate(QuyetDinhThueDatRequest rq);
        public Task<ApiResult<bool>> Delete(int idQuyetDinhThueDat);
        public Task<ApiResult<QuyetDinhThueDatViewModel>> GetById(int idQuyetDinhThueDat);
        public Task<ApiResult<List<QuyetDinhThueDatViewModel>>> GetAll(int? idDoanhNghiep);
        public Task<ApiResult<List<QuyetDinhThueDatViewModel>>> GetListQuyetDinhThueDatChiTiet(int idDoanhNghiep);
        public Task<ApiResult<PageViewModel<QuyetDinhThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep,string keyword, int pageIndex, int pageSize);
    }
}
