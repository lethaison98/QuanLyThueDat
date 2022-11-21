using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Interfaces
{
    public interface IQuyetDinhMienTienThueDatService
    {
        public Task<ApiResult<bool>> InsertUpdate(QuyetDinhMienTienThueDatRequest rq);
        public Task<ApiResult<bool>> Delete(int idQuyetDinhMienTienThueDat);
        public Task<ApiResult<QuyetDinhMienTienThueDatViewModel>> GetById(int idQuyetDinhMienTienThueDat);
        public Task<ApiResult<List<QuyetDinhMienTienThueDatViewModel>>> GetAll(int? idDoanhNghiep);
        public Task<ApiResult<PageViewModel<QuyetDinhMienTienThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep,string keyword, int pageIndex, int pageSize);
        public Task<ApiResult<List<QuyetDinhMienTienThueDatViewModel>>> CanhBaoQuyetDinhMienTienThueDatSapHetHan();
    }
}
