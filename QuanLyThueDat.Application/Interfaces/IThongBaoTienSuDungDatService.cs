using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Interfaces
{
    public interface IThongBaoTienSuDungDatService
    {
        public Task<ApiResult<int>> InsertUpdate(ThongBaoTienSuDungDatRequest rq);
        public Task<ApiResult<bool>> Delete(int idThongBaoTienSuDungDat);
        public Task<ApiResult<ThongBaoTienSuDungDatViewModel>> GetById(int idThongBaoTienSuDungDat);
        public Task<ApiResult<List<ThongBaoTienSuDungDatViewModel>>> GetAll();
        public Task<ApiResult<PageViewModel<ThongBaoTienSuDungDatViewModel>>> GetAllPaging(int? idDoanhNghiep, string keyword, int pageIndex, int pageSize);
        public Task<ApiResult<List<ThongBaoTienSuDungDatViewModel>>> GetAllByRequest(ThongBaoTienSuDungDatRequest rq);

    }
}
