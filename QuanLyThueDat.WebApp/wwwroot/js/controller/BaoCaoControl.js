if (typeof (BaoCaoControl) == "undefined") BaoCaoControl = {};
BaoCaoControl = {
    Init: function () {
        BaoCaoControl.RegisterEvents();
    },
    RegisterEvents: function (opts) {
        $('.datetimepicker-input').datetimepicker({
            format: 'DD/MM/YYYY'
        });
        var self = this;
        $('[data-name="LoaiBaoCao"]').on('change', function () {
            if ($('[data-name="LoaiBaoCao"] option:selected').val() != 0 && $('[data-name="LoaiBaoCao"] option:selected').val() != undefined) {
                var loaiBaoCao = $('[data-name="LoaiBaoCao"] option:selected').val();
                switch (loaiBaoCao) {
                    case "BaoCaoDoanhNghiepThueDat":
                        $('#namThongBao').parent().parent().hide();
                        $('#keyword').parent().parent().hide();
                        $('#ddQuanHuyen').parent().parent().hide();
                        $('#tuNgay').parent().parent().hide();
                        $('#denNgay').parent().parent().hide();
                        $("#btnXuatBaoCao").off('click').on('click', function (e) {
                            window.open("BaoCao/ExportBaoCaoDoanhNghiepThueDat");
                        });
                        break;
                    case "BaoCaoTienThueDat":
                        $('#namThongBao').parent().parent().show();
                        $('#tuNgay').parent().parent().show();
                        $('#denNgay').parent().parent().show();
                        $('#keyword').parent().parent().show();
                        $('#ddQuanHuyen').parent().parent().show();
                        $("#btnXuatBaoCao").off('click').on('click', function (e) {
                            window.open("BaoCao/ExportThongBaoTienThueDatHangNam?namThongBao=" + $('#namThongBao option:selected').val() + "&idQuanHuyen=" + $('#ddQuanHuyen option:selected').val() + "&keyword=" + $('#keyword').val() + "&tuNgay=" + $('#tuNgay').val() + "&denNgay=" + $('#denNgay').val());
                        });
                        break;
                    case "BaoCaoDonGiaThueDat":
                        $('#namThongBao').parent().parent().hide();
                        $('#tuNgay').parent().parent().show();
                        $('#denNgay').parent().parent().show();
                        $('#keyword').parent().parent().show();
                        $('#ddQuanHuyen').parent().parent().show();
                        $("#btnXuatBaoCao").off('click').on('click', function (e) {
                            window.open("BaoCao/ExportThongBaoDonGiaThueDat?idQuanHuyen=" + $('#ddQuanHuyen option:selected').val() + "&keyword=" + $('#keyword').val() + "&tuNgay=" + $('#tuNgay').val() + "&denNgay=" + $('#denNgay').val());
                        });
                        break;
                    case "BaoCaoMienGiamTienThueDat":
                        $('#namThongBao').parent().parent().hide();
                        $('#tuNgay').parent().parent().show();
                        $('#denNgay').parent().parent().show();
                        $('#keyword').parent().parent().show();
                        $('#ddQuanHuyen').parent().parent().show();
                        $("#btnXuatBaoCao").off('click').on('click', function (e) {
                            window.open("BaoCao/ExportQuyetDinhMienTienThueDat?idQuanHuyen=" + $('#ddQuanHuyen option:selected').val() + "&keyword=" + $('#keyword').val() + "&tuNgay=" + $('#tuNgay').val() + "&denNgay=" + $('#denNgay').val());
                        });
                        break;
                    case "BieuLapBo":
                        $('#namThongBao').parent().parent().hide();
                        $('#keyword').parent().parent().hide();
                        $('#ddQuanHuyen').parent().parent().hide();
                        $('#tuNgay').parent().parent().hide();
                        $('#denNgay').parent().parent().hide();
                        $("#btnXuatBaoCao").off('click').on('click', function (e) {
                            window.open("BaoCao/ExportBieuLapBo");
                        });
                        break;
                    default:
                    // code block
                }
            }
        });
        $("#btnXuatBaoCao").off('click').on('click', function (e) {
            if ($('[data-name="LoaiBaoCao"] option:selected').val() == "") {
                alert("Vui lòng chọn loại báo cáo");
            }
        });
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchBaoCao").trigger('click');
            }
        });
        $("#btnSearchBaoCao").off('click').on('click', function () {
            self.table.ajax.reload();

        });

    },
}

$(document).ready(function () {
    BaoCaoControl.Init();
});

