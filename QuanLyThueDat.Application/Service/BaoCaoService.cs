using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using QuanLyThueDat.Application.Common.Constant;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using QuanLyThueDat.Application.ViewModel.BaoCaoViewModel;
using QuanLyThueDat.Data.EF;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Service
{
    public class BaoCaoService : IBaoCaoService
    {
        private readonly QuanLyThueDatDbContext _context;
        private readonly IDoanhNghiepService _DoanhNghiepService;
        private readonly IQuyetDinhThueDatService _QuyetDinhThueDatService;
        private readonly IHopDongThueDatService _HopDongThueDatService;
        private readonly IQuyetDinhMienTienThueDatService _QuyetDinhMienTienThueDatService;
        private readonly IThongBaoDonGiaThueDatService _ThongBaoDonGiaThueDatService;
        private readonly IThongBaoTienThueDatService _ThongBaoTienThueDatService;

        public BaoCaoService(QuanLyThueDatDbContext context,
            IDoanhNghiepService DoanhNghiepService,
            IQuyetDinhThueDatService QuyetDinhThueDatService,
            IHopDongThueDatService HopDongThueDatService,
            IQuyetDinhMienTienThueDatService QuyetDinhMienTienThueDatService,
            IThongBaoDonGiaThueDatService ThongBaoDonGiaThueDatService,
            IThongBaoTienThueDatService ThongBaoTienThueDatService)
        {
            _context = context;
            _DoanhNghiepService = DoanhNghiepService;
            _QuyetDinhThueDatService = QuyetDinhThueDatService;
            _HopDongThueDatService = HopDongThueDatService;
            _QuyetDinhMienTienThueDatService = QuyetDinhMienTienThueDatService;
            _ThongBaoDonGiaThueDatService = ThongBaoDonGiaThueDatService;
            _ThongBaoTienThueDatService = ThongBaoTienThueDatService;
        }
        public async Task<ApiResult<List<BaoCaoDoanhNghiepThueDatViewModel>>> BaoCaoDoanhNghiepThueDat()
        {
            var result = new List<BaoCaoDoanhNghiepThueDatViewModel>();
            var dsDoanhNghiep = await _DoanhNghiepService.GetAll();
            if(dsDoanhNghiep.Data.Count != 0)
            {
                foreach (var doanhNghiep in dsDoanhNghiep.Data)
                {
                    var dsQuyetDinhThueDat = await _QuyetDinhThueDatService.GetListQuyetDinhThueDatChiTiet(doanhNghiep.IdDoanhNghiep);
                    var dsHopDong = await _HopDongThueDatService.GetAll(doanhNghiep.IdDoanhNghiep);
                    if(dsQuyetDinhThueDat.Data.Count != 0)
                    {
                        var dsQuyetDinhThueDatTraTienHangNam = dsQuyetDinhThueDat.Data.Where(x => x.HinhThucThue == "ThueDatTraTienHangNam");
                        foreach (var quyetDinhThueDat in dsQuyetDinhThueDatTraTienHangNam)
                        {
                            if (!String.IsNullOrEmpty(quyetDinhThueDat.SoQuyetDinhThueDat))
                            {
                                var rq = new ThongBaoDonGiaThueDatRequest();
                                rq.SoQuyetDinhThueDat = quyetDinhThueDat.SoQuyetDinhThueDat;
                                rq.NgayQuyetDinhThueDat = quyetDinhThueDat.NgayQuyetDinhThueDat;
                                var dsThongBaoDonGiaThueDat = await _ThongBaoDonGiaThueDatService.GetAllByRequest(rq);
                                if (dsThongBaoDonGiaThueDat.Data.Count != 0)
                                {
                                    foreach (var thongBaoDonGia in dsThongBaoDonGiaThueDat.Data)
                                    {
                                        var item = new BaoCaoDoanhNghiepThueDatViewModel();
                                        item.DoanhNghiepViewModel = doanhNghiep;
                                        item.QuyetDinhThueDatViewModel = quyetDinhThueDat;
                                        item.ThongBaoDonGiaThueDatViewModel = thongBaoDonGia;
                                        foreach (var hopDong in dsHopDong.Data)
                                        {
                                            if (quyetDinhThueDat.IdQuyetDinhThueDat == hopDong.IdQuyetDinhThueDat)
                                            {
                                                item.HopDongThueDatViewModel = hopDong;
                                            }
                                        }
                                        result.Add(item);
                                    }
                                }
                                else
                                {
                                    var item = new BaoCaoDoanhNghiepThueDatViewModel();
                                    item.DoanhNghiepViewModel = doanhNghiep;
                                    item.QuyetDinhThueDatViewModel = quyetDinhThueDat;
                                    foreach (var hopDong in dsHopDong.Data)
                                    {
                                        if (quyetDinhThueDat.IdQuyetDinhThueDat == hopDong.IdQuyetDinhThueDat)
                                        {
                                            item.HopDongThueDatViewModel = hopDong;
                                        }
                                    }
                                    result.Add(item);
                                }
                            }                        
                            else
                            {
                                var item = new BaoCaoDoanhNghiepThueDatViewModel();
                                item.DoanhNghiepViewModel = doanhNghiep;
                                item.QuyetDinhThueDatViewModel = quyetDinhThueDat;
                                foreach (var hopDong in dsHopDong.Data)
                                {
                                    if (quyetDinhThueDat.IdQuyetDinhThueDat == hopDong.IdQuyetDinhThueDat)
                                    {
                                        item.HopDongThueDatViewModel = hopDong;
                                    }
                                }
                                result.Add(item);
                            }
                        }
                    }
                    else
                    {
                        var item = new BaoCaoDoanhNghiepThueDatViewModel();
                        item.DoanhNghiepViewModel = doanhNghiep;
                        result.Add(item);
                    }

                }
            }
            return new ApiSuccessResult<List<BaoCaoDoanhNghiepThueDatViewModel>>() { Data = result };
        }
    }
}
