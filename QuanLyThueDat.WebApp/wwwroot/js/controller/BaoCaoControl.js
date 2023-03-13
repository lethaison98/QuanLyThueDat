if (typeof (BaoCaoControl) == "undefined") BaoCaoControl = {};
BaoCaoControl = {
    Init: function () {
        BaoCaoControl.RegisterEvents();
    },
    RegisterEvents: function (opts) {
        var self = this;
        $('[data-name="LoaiBaoCao"]').on('change', function () {
            if ($('[data-name="LoaiBaoCao"] option:selected').val() != 0 && $('[data-name="LoaiBaoCao"] option:selected').val() != undefined) {
                var loaiBaoCao = $('[data-name="LoaiBaoCao"] option:selected').val();
                console.log(loaiBaoCao);
                switch (loaiBaoCao) {
                    case "BaoCaoDoanhNghiepThueDat":
                        $('#namThongBao').hide();
                        $("#btnXuatBaoCao").off('click').on('click', function (e) {
                            window.open("BaoCao/ExportBaoCaoDoanhNghiepThueDat");
                        });
                        break;
                    case "BaoCaoTienThueDat":
                        $('#namThongBao').show();
                        $("#btnXuatBaoCao").off('click').on('click', function (e) {
                            if ($('#namThongBao option:selected').val() != "") {
                                window.open("BaoCao/ExportThongBaoTienThueDatHangNam?namThongBao=" + $('#namThongBao option:selected').val());
                            } else {
                                alert("Vui lòng chọn năm thông báo");
                            }
                        });
                        break;
                    case "BaoCaoMienGiamTienThueDat":
                        $('#namThongBao').hide();
                        $("#btnXuatBaoCao").off('click').on('click', function (e) {
                            window.open("BaoCao/ExportQuyetDinhMienTienThueDat");
                        });
                        break;
                    default:
                    // code block
                }
            }
        });
        $("#btnXuatBaoCao").off('click').on('click', function (e) {
            console.log(111);
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

