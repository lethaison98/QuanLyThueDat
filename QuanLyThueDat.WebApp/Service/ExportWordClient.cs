using DocumentFormat.OpenXml.Packaging;
using Newtonsoft.Json;
using QuanLyThueDat.Application.Common.Constant;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.ViewModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace QuanLyThueDat.WebApp.Service
{
    public class ExportWordClient : IExportWordClient
    {
        private readonly IThongBaoDonGiaThueDatService _thongBaoDonGiaThueDatService;
        private readonly IThongBaoTienThueDatService _thongBaoTienThueDatService;

        public ExportWordClient(IThongBaoDonGiaThueDatService thongBaoDonGiaThueDatService, IThongBaoTienThueDatService thongBaoTienThueDatService)
        {
            _thongBaoDonGiaThueDatService = thongBaoDonGiaThueDatService;
            _thongBaoTienThueDatService = thongBaoTienThueDatService;
        }

        private void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object toFindText, object replaceWithText)
        {
            object matchCase = true;

            object matchwholeWord = true;

            object matchwildCards = false;

            object matchSoundLike = false;

            object nmatchAllforms = false;

            object forward = true;

            object format = false;

            object matchKashida = false;

            object matchDiactitics = false;

            object matchAlefHamza = false;

            object matchControl = false;

            object read_only = false;

            object visible = true;

            object replace = 2;

            object wrap = 1;

            wordApp.Selection.Find.Execute(ref toFindText, ref matchCase,
                                            ref matchwholeWord, ref matchwildCards, ref matchSoundLike,

                                            ref nmatchAllforms, ref forward,

                                            ref wrap, ref format, ref replaceWithText,

                                            ref replace, ref matchKashida,

                                            ref matchDiactitics, ref matchAlefHamza,

                                            ref matchControl);
        }


        public async Task<ApiResult<byte[]>> CreateWordDocument1(int idThongBao, string loaiThongBaoConstant)
        {
            var pathFileTemplate = "";
            dynamic data = new System.Dynamic.ExpandoObject();
            switch (loaiThongBaoConstant)
            {
                case LoaiThongBaoConstant.ThongBaoDonGiaThueDat:
                    pathFileTemplate = "Assets/Template/MauThongBaoDonGiaThueDat.doc";
                    data = await _thongBaoDonGiaThueDatService.GetById(idThongBao);
                    if (String.IsNullOrEmpty(data.Data.TenThongBaoDonGiaThueDat))
                    {
                        data.Data.TenThongBaoDonGiaThueDat = "Về đơn giá thuê đất, thuê mặt nước";
                    }
                    if (String.IsNullOrEmpty(data.Data.CoQuanQuanLyThue))
                    {
                        data.Data.CoQuanQuanLyThue = "Chi cục thuế Bắc Vinh";
                    }
                    break;
                case LoaiThongBaoConstant.ThongBaoTienThueDat:
                    pathFileTemplate = "Assets/Template/MauThongBaoTienThueDat.doc";
                    data = await _thongBaoTienThueDatService.GetById(idThongBao);
                    if (String.IsNullOrEmpty(data.Data.TenThongBaoTienThueDat))
                    {
                        data.Data.TenThongBaoTienThueDat = "Về tiền thuê đất, thuê mặt nước theo hình thức nộp hàng năm";
                    }
                    if (String.IsNullOrEmpty(data.Data.CoQuanQuanLyThue))
                    {
                        data.Data.CoQuanQuanLyThue = "Chi cục thuế Bắc Vinh";
                    }
                    data.Data.TextSoTienPhaiNop = NumberToTextVN(data.Data.SoTienPhaiNop);
                    data.Data.TextTongDienTich = NumberToTextVN(data.Data.TongDienTich);
                    break;
            }
            //var filename = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Template", "template.docx");
            var x = Directory.GetCurrentDirectory();
            var filename = Path.Combine(Directory.GetCurrentDirectory(), pathFileTemplate);
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            object missing = Missing.Value;

            Microsoft.Office.Interop.Word.Document myWordDoc = null;

            if (File.Exists((string)filename))
            {
                object readOnly = false;

                object isvisible = false;

                wordApp.Visible = false;
                myWordDoc = wordApp.Documents.Open(filename, ref missing, ref readOnly,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing, ref missing);
                myWordDoc.Activate();
                if (data.IsSuccess)
                {
                    var xsss = new List<object>();
                    foreach (PropertyInfo propertyInfo in data.Data.GetType().GetProperties())
                    {
                        this.FindAndReplace(wordApp, "{" + propertyInfo.Name + "}", propertyInfo.GetValue(data.Data, null));
                        xsss.Add(new { name = propertyInfo.Name, value = propertyInfo.GetValue(data.Data, null) });
                    }
                }
                else
                {
                    return new ApiErrorResult<byte[]>("Lấy dữ liệu không thành công");
                }

                string docText = myWordDoc.WordOpenXML;
                byte[] bytes = Encoding.UTF8.GetBytes(docText);
                //myWordDoc.SaveAs2(fileSaveName, ref missing, ref missing, ref missing,
                //                                                ref missing, ref missing, ref missing,
                //                                                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                //                                                ref missing, ref missing, ref missing);
                object saveOption = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                object originalFormat = Microsoft.Office.Interop.Word.WdOriginalFormat.wdOriginalDocumentFormat;
                object routeDocument = false;
                myWordDoc.Close(ref saveOption, ref originalFormat, ref routeDocument);
                wordApp.Quit();
                return new ApiSuccessResult<byte[]>() { Data = bytes };
            }
            else
            {
                return new ApiErrorResult<byte[]>("Không tìm thấy file mẫu");
            }
        }
        public string NumberToTextVN(string value)
        {
            try
            {
                var total = decimal.Parse(value, new CultureInfo("vi-VN"));
                string rs = "";
                total = Math.Round(total, 0);
                string[] ch = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
                string[] rch = { "lẻ", "mốt", "", "", "", "lăm" };
                string[] u = { "", "mươi", "trăm", "ngàn", "", "", "triệu", "", "", "tỷ", "", "", "ngàn", "", "", "triệu" };
                string nstr = total.ToString();

                int[] n = new int[nstr.Length];
                int len = n.Length;
                for (int i = 0; i < len; i++)
                {
                    n[len - 1 - i] = Convert.ToInt32(nstr.Substring(i, 1));
                }

                for (int i = len - 1; i >= 0; i--)
                {
                    if (i % 3 == 2)// số 0 ở hàng trăm
                    {
                        if (n[i] == 0 && n[i - 1] == 0 && n[i - 2] == 0) continue;//nếu cả 3 số là 0 thì bỏ qua không đọc
                    }
                    else if (i % 3 == 1) // số ở hàng chục
                    {
                        if (n[i] == 0)
                        {
                            if (n[i - 1] == 0) { continue; }// nếu hàng chục và hàng đơn vị đều là 0 thì bỏ qua.
                            else
                            {
                                rs += " " + rch[n[i]]; continue;// hàng chục là 0 thì bỏ qua, đọc số hàng đơn vị
                            }
                        }
                        if (n[i] == 1)//nếu số hàng chục là 1 thì đọc là mười
                        {
                            rs += " mười"; continue;
                        }
                    }
                    else if (i != len - 1)// số ở hàng đơn vị (không phải là số đầu tiên)
                    {
                        if (n[i] == 0)// số hàng đơn vị là 0 thì chỉ đọc đơn vị
                        {
                            if (i + 2 <= len - 1 && n[i + 2] == 0 && n[i + 1] == 0) continue;
                            rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                            continue;
                        }
                        if (n[i] == 1)// nếu là 1 thì tùy vào số hàng chục mà đọc: 0,1: một / còn lại: mốt
                        {
                            rs += " " + ((n[i + 1] == 1 || n[i + 1] == 0) ? ch[n[i]] : rch[n[i]]);
                            rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                            continue;
                        }
                        if (n[i] == 5) // cách đọc số 5
                        {
                            if (n[i + 1] != 0) //nếu số hàng chục khác 0 thì đọc số 5 là lăm
                            {
                                rs += " " + rch[n[i]];// đọc số 
                                rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);// đọc đơn vị
                                continue;
                            }
                        }
                    }

                    rs += (rs == "" ? " " : ", ") + ch[n[i]];// đọc số
                    rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);// đọc đơn vị
                }
                //if (rs[rs.Length - 1] != ' ')
                //    rs += " đồng";
                //else
                //    rs += "đồng";

                if (rs.Length > 2)
                {
                    string rs1 = rs.Substring(0, 2);
                    rs1 = rs1.ToUpper();
                    rs = rs.Substring(2);
                    rs = rs1 + rs;
                }
                return rs.Trim().Replace("lẻ,", "lẻ").Replace("mươi,", "mươi").Replace("trăm,", "trăm").Replace("mười,", "mười").Replace("Mười,", "Mười");
            }
            catch
            {
                return "";
            }

        }


        public async Task<ApiResult<byte[]>> CreateWordDocument(int idThongBao, string loaiThongBaoConstant)
        {
            var pathFileTemplate = "";
            dynamic data = new System.Dynamic.ExpandoObject();
            switch (loaiThongBaoConstant)
            {
                case LoaiThongBaoConstant.ThongBaoDonGiaThueDat:
                    pathFileTemplate = "Assets/Template/MauThongBaoDonGiaThueDat.docx";
                    data = await _thongBaoDonGiaThueDatService.GetById(idThongBao);
                    if (String.IsNullOrEmpty(data.Data.TenThongBaoDonGiaThueDat))
                    {
                        data.Data.TenThongBaoDonGiaThueDat = "Về đơn giá thuê đất, thuê mặt nước";
                    }
                    if (String.IsNullOrEmpty(data.Data.CoQuanQuanLyThue))
                    {
                        data.Data.CoQuanQuanLyThue = "Chi cục thuế Bắc Vinh";
                    }
                    if (!String.IsNullOrEmpty(data.Data.LanhDaoKyThongBaoDonGiaThueDat))
                    {
                        var arr = data.Data.LanhDaoKyThongBaoDonGiaThueDat.Split('-');
                        data.Data.TextChucVuLanhDao = arr[0];
                        data.Data.TextTenLanhDao = arr[1];
                        if (data.Data.TextChucVuLanhDao != "TRƯỞNG BAN")
                        {
                            data.Data.TextKyThayLanhDao = "KT. TRƯỞNG BAN";
                        }
                    }
                    break;
                case LoaiThongBaoConstant.ThongBaoTienThueDat:
                    pathFileTemplate = "Assets/Template/MauThongBaoTienThueDat.docx";
                    data = await _thongBaoTienThueDatService.GetById(idThongBao);
                    if (String.IsNullOrEmpty(data.Data.TenThongBaoTienThueDat))
                    {
                        data.Data.TenThongBaoTienThueDat = "Về tiền thuê đất, thuê mặt nước theo hình thức nộp hàng năm";
                    }
                    if (String.IsNullOrEmpty(data.Data.CoQuanQuanLyThue))
                    {
                        data.Data.CoQuanQuanLyThue = "Chi cục thuế Bắc Vinh";
                    }
                    if (!String.IsNullOrEmpty(data.Data.LanhDaoKyThongBaoTienThueDat))
                    {
                        var arr = data.Data.LanhDaoKyThongBaoTienThueDat.Split('-');
                        data.Data.TextChucVuLanhDao = arr[0];
                        data.Data.TextTenLanhDao = arr[1];
                        if(data.Data.TextChucVuLanhDao!= "TRƯỞNG BAN")
                        {
                            data.Data.TextKyThayLanhDao = "KT. TRƯỞNG BAN";
                        }
                    }
                    data.Data.TextLoaiThongBaoTienThueDat = typeof(LoaiThongBaoTienThueDatConstant).GetField(data.Data.LoaiThongBaoTienThueDat).GetValue(null).ToString();
                    data.Data.TextSoTienPhaiNop = NumberToTextVN(data.Data.SoTienPhaiNop);
                    data.Data.TextTongDienTich = NumberToTextVN(data.Data.TongDienTich);


                    break;
            }
            var filename = Path.Combine(Directory.GetCurrentDirectory(), pathFileTemplate);
            byte[] byteArray = File.ReadAllBytes(filename);


            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(stream, true))
                {
                    string docText = null;
                    using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                    {
                        docText = sr.ReadToEnd();
                    }                 
                    if (data.IsSuccess)
                    {
                        var data1 = JsonConvert.SerializeObject(data.Data);
                        foreach (PropertyInfo propertyInfo in data.Data.GetType().GetProperties())
                        {
                            docText = docText.Replace("{" + propertyInfo.Name + "}", Convert.ToString(propertyInfo.GetValue(data.Data, null)));
                            //docText = docText.Replace("{SoThongBaoTienThueDat}", "1234");
                        }
                    }
                    else
                    {
                        return new ApiErrorResult<byte[]>("Lấy dữ liệu không thành công");
                    }

                    using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }
                }
                // Save the file with the new name
                byteArray = stream.ToArray();
            }

            return new ApiSuccessResult<byte[]>() { Data = byteArray };
        }



        //public async Task<ApiResult<byte[]>> CreateWordDocument1(int idThongBao, string loaiThongBaoConstant)
        //{
        //    var pathFileTemplate = "Assets/Template/Test.docx";
        //    var x = Directory.GetCurrentDirectory();
        //    var filename = Path.Combine(Directory.GetCurrentDirectory(), pathFileTemplate);
        //    var filetemp = Path.Combine(Directory.GetCurrentDirectory(), "Assets/Template/Test1.docx");


        //    //File.Copy(filename, filetemp);
        //    using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filename, true))
        //    {
        //        string docText = null;
        //        using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
        //        {
        //            docText = sr.ReadToEnd();
        //        }

        //        docText = docText.Replace("Thái Sơn", "Văn Lương");
        //        using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
        //        {
        //            sw.Write(docText);
        //        }
        //    }
        //    byte[] bytes = File.ReadAllBytes(filetemp);
        //    return new ApiSuccessResult<byte[]>() { Data = bytes };
        //}
    }
}
