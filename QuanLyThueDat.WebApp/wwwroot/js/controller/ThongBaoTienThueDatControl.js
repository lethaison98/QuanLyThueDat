if (typeof (ThongBaoTienThueDatControl) == "undefined") ThongBaoTienThueDatControl = {};
ThongBaoTienThueDatControl = {
    Init: function () {
        ThongBaoTienThueDatControl.RegisterEvents();

    },

    LoadDatatable: function (opts) {
        var idDoanhNghiep;
        if (opts == undefined) {
            idDoanhNghiep = null;
        } else {
            idDoanhNghiep = opts.IdDoanhNghiep
        }
        var self = this;
        self.table = SetDataTable({
            table: $('#tblThongBaoTienThueDat'),
            url: localStorage.getItem("API_URL") + "/ThongBaoTienThueDat/GetAllPaging",
            dom: "rtip",
            data: {
                "requestData": function () {
                    return {
                        "keyword": $('#txtKEY_WORD').val(),
                        "idDoanhNghiep": idDoanhNghiep
                    }
                },
                "processData2": function (res) {
                    var json = jQuery.parseJSON(res);
                    json.recordsTotal = json.Data.TotalRecord;
                    json.recordsFiltered = json.Data.TotalRecord;
                    json.data = json.Data.Items;
                    return JSON.stringify(json);
                },
                "columns": [
                    {
                        "class": "stt-control",
                        "data": "RN",
                        "defaultContent": "1",
                        render: function (data, type, row, meta) {
                            return meta.row + 1;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "SoThongBaoTienThueDat",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "data": "TenDoanhNghiep",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "data": "Nam",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "SoTienPhaiNop",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        //"data": "thaotac",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                "<a href='javascript:;' class='ThongBaoTienThueDat-export' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-file-word' title='Xuất thông báo' ></i></a> &nbsp" +
                                "<a href='javascript:;' class='ThongBaoTienThueDat-edit' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-edit' title='Chỉnh sửa'></i></a> &nbsp" +
                                "<a href='javascript:;' class='ThongBaoTienThueDat-remove text-danger' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                "</div>";
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {
                $("#tblThongBaoTienThueDat tbody .ThongBaoTienThueDat-export").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        window.open("/CreateWordFile?idThongBao=" + id + "&loaiThongBao=ThongBaoTienThueDat", "_blank");
                    }
                });
                $('#tblThongBaoTienThueDat tbody .ThongBaoTienThueDat-edit').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/ThongBaoTienThueDat/GetById',
                        data: {
                            idThongBaoTienThueDat: id
                        },
                        callback: function (res) {
                            if (res.IsSuccess) {
                                Get({
                                    url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                                    dataType: 'text',
                                    callback: function (popup) {
                                        $('#modalDetailThongBaoTienThueDat').html(popup);
                                        $('#popupDetailThongBaoTienThueDat').modal();

                                        FillFormData('#FormDetailThongBaoTienThueDat', res.Data);
                                        if (opts != undefined) {
                                            $('#popupDetailThongBaoTienThueDat .ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                                        } else {
                                            $('#popupDetailThongBaoTienThueDat .ddDoanhNghiep').append('<option value="' + res.Data.IdDoanhNghiep + '">' + res.Data.TenDoanhNghiep + '</option>');
                                        }
                                        self.RegisterEventsPopup();
                                        $('#popupDetailThongBaoTienThueDat .select2').attr("disabled", true);

                                        //$("#btnTraCuu").on('click', function () {
                                        //    $('#modal-add-edit').modal('show');
                                        //});
                                    }
                                })
                            }
                        }
                    });
                });

                $("#tblThongBaoTienThueDat tbody .ThongBaoTienThueDat-remove").off('click').on('click', function (e) {

                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/ThongBaoTienThueDat/Delete?idThongBaoTienThueDat=" + $y.attr('data-id') + "&Type=1",
                                //headers: {
                                //    'Authorization': 'Bearer ' + localStorage.getItem("access_token")
                                //},
                                dataType: 'json',
                                contentType: "application/json-patch+json",
                                type: "Delete",
                                success: function (res) {
                                    if (res.IsSuccess) {
                                        alert("Thành công");
                                        self.table.ajax.reload();
                                    }
                                    else {
                                        alert("Không thành công")
                                    }
                                }
                            });
                        }

                    }
                });

            }
        });

    },
    RegisterEventsPopup: function () {
        var self = this;
        $('.select2').select2();
        $('.groupThongBaoDonGiaThueDat input').change(function () {
            self.TinhToanCongThuc();
        });
        $("#btnSaveThongBaoTienThueDat").off('click').on('click', function () {
            self.InsertUpdate();
        });
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
    },
    RegisterEvents: function (opts) {
        var self = this;
        self.LoadDatatable(opts);
        $('#btnCreateThongBaoTienThueDat').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTienThueDat').html(res);
                    $('#popupDetailThongBaoTienThueDat').modal();
                    if (opts != undefined) {
                        $('#popupDetailThongBaoTienThueDat .ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                        self.LoadDanhSachQuyetDinhThueDat();
                    } else {
                        self.LoadDanhSachDoanhNghiep();
                        self.LoadDanhSachQuyetDinhThueDat();
                    }
                    self.RegisterEventsPopup();
                }
            })
        });
        $("#btnSearchThongBaoTienThueDat").off('click').on('click', function () {
            self.table.ajax.reload();

        });



    },
    TinhToanCongThuc: function () {
        $(".groupThongBaoDonGiaThueDat [data-name='DonGia']").change(function () {
            if ($("[data-name='TongDienTich']").val() != '') {
                var tongDienTich = ConvertStringToDecimal($(".groupThongBaoDonGiaThueDat [data-name='TongDienTich']").val());
                var donGia = ConvertStringToDecimal($("[data-name='DonGia']").val());
                var tongSoTien = ConvertDecimalToString((donGia * tongDienTich).toFixed(0));
                $("[data-name='SoTien']").val(tongSoTien);
            };
        });

        $(".groupThongBaoDonGiaThueDat [data-name='TongDienTich']").change(function () {
            if ($("[data-name='DonGia']").val() != '') {
                var donGia = ConvertStringToDecimal($("[data-name='DonGia']").val());
                var tongDienTich = ConvertStringToDecimal($(".groupThongBaoDonGiaThueDat [data-name='TongDienTich']").val());
                var tongSoTien = ConvertDecimalToString((donGia * tongDienTich).toFixed(0));
                $("[data-name='SoTien']").val(tongSoTien);
            };
        });
        $("[data-name='DienTichKhongPhaiNop']").change(function () {
            if ($("[data-name='DonGia']").val() != '') {
                var donGia = ConvertStringToDecimal($("[data-name='DonGia']").val());
                var tongDienTich = ConvertStringToDecimal($(".groupThongBaoDonGiaThueDat [data-name='TongDienTich']").val());
                var dienTichKhongPhaiNop = ConvertStringToDecimal($("[data-name='DienTichKhongPhaiNop']").val());
                var soTienMienGiam = ConvertDecimalToString((donGia * dienTichKhongPhaiNop).toFixed(0));
                $("[data-name='SoTienMienGiam']").val(soTienMienGiam);
                var dienTichPhaiNop = ConvertDecimalToString((tongDienTich - dienTichKhongPhaiNop).toFixed(0));
                $("[data-name='DienTichPhaiNop']").val(dienTichPhaiNop);
            };
            if ($("[data-name='SoTien']").val() != '') {
                var soTien = ConvertStringToDecimal($("[data-name='SoTien']").val());
                var soTienMienGiam = ConvertStringToDecimal($("[data-name='SoTienMienGiam']").val());
                var soTienPhaiNop = ConvertDecimalToString((soTien - soTienMienGiam).toFixed(0));
                $("[data-name='SoTienPhaiNop']").val(soTienPhaiNop);
            };
        });
        $("[data-name='SoTienMienGiam']").change(function () {
            console.log(222);
            if ($("[data-name='SoTien']").val() != '') {
                var soTien = ConvertStringToDecimal($("[data-name='SoTien']").val());
                var soTienMienGiam = ConvertStringToDecimal($("[data-name='SoTienMienGiam']").val());
                var soTienPhaiNop = ConvertDecimalToString((soTien - soTienMienGiam).toFixed(0));
                $("[data-name='SoTienPhaiNop']").val(soTienPhaiNop);
            };
        });
    },
    InsertUpdate: function () {
        var self = this;
        var popup = $('#popupDetailThongBaoTienThueDat');
        $(".number").each(function () {
            $(this).val(ConvertStringToDecimal($(this).val()));
        });
        var data = LoadFormData("#FormDetailThongBaoTienThueDat");
        data.IdDoanhNghiep = popup.find(".ddDoanhNghiep option:selected").val();
        Post({
            "url": localStorage.getItem("API_URL") + "/ThongBaoTienThueDat/InsertUpdate",
            "data": data,
            callback: function (res) {
                self.table.ajax.reload();
                $('#btnCloseThongBaoTienThueDat').trigger('click');
            }
        });
    },
    LoadDanhSachDoanhNghiep: function () {
        var self = this;
        var popup = $('#popupDetailThongBaoTienThueDat');
        Get({
            url: localStorage.getItem("API_URL") + "/DoanhNghiep/GetAll",
            showLoading: true,
            callback: function (res) {
                popup.find('.ddDoanhNghiep').html('');
                $.each(res.Data, function (i, item) {
                    popup.find('.ddDoanhNghiep').append('<option value="' + item.IdDoanhNghiep + '">' + item.TenDoanhNghiep + '</option>');
                })
                popup.find('.ddDoanhNghiep').on('change', function () {
                    if (popup.find(".ddDoanhNghiep option:selected").val() != 0 && popup.find(".ddDoanhNghiep option:selected").val() != undefined) {
                        self.LoadDanhSachQuyetDinhThueDat();
                    }
                });
                //$('.ddDoanhNghiep').trigger('change');

            }
        });
    },
    LoadDanhSachQuyetDinhThueDat: function () {
        var self = this;
        var popup = $('#popupDetailThongBaoTienThueDat')
        Get({
            url: localStorage.getItem("API_URL") + "/QuyetDinhThueDat/GetAll",
            data: {
                idDoanhNghiep: popup.find(".ddDoanhNghiep option:selected").val()
            },
            showLoading: true,
            callback: function (res) {
                popup.find('.ddQuyetDinhThueDat').html('');
                $.each(res.Data, function (i, item) {
                    popup.find('.ddQuyetDinhThueDat').append('<option value= "" selected="true" style="display: none"></option>');
                    popup.find('.ddQuyetDinhThueDat').append('<option value="' + item.IdQuyetDinhThueDat + '">' + item.SoQuyetDinhThueDat + '</option>');
                })
                popup.find('.ddQuyetDinhThueDat').on('change', function () {
                    if (popup.find(".ddQuyetDinhThueDat option:selected").val() != 0 && popup.find(".ddQuyetDinhThueDat option:selected").val() != undefined) {
                        Get({
                            url: localStorage.getItem("API_URL") + '/QuyetDinhThueDat/GetById',
                            data: {
                                idQuyetDinhThueDat: popup.find(".ddQuyetDinhThueDat option:selected").val()
                            },
                            callback: function (res) {
                                if (res.IsSuccess) {
                                    FillFormData('#FormDetailThongBaoTienThueDat', res.Data);
                                    self.LoadDanhSachThongBaoDonGiaThueDat();
                                }
                            }
                        });
                    } else {
                        $('.groupQuyetDinhThueDat input').val("");
                    }
                });
                popup.find('.ddQuyetDinhThueDat').trigger('change');
            }
        });
    },
    LoadDanhSachThongBaoDonGiaThueDat: function () {
        var self = this;
        var popup = $('#popupDetailThongBaoTienThueDat');
        var Data = {
            IdQuyetDinhThueDat: popup.find(".ddQuyetDinhThueDat option:selected").val(),
            IdDoanhNghiep: popup.find(".ddDoanhNghiep option:selected").val()
        };
        console.log(Data);
        Post({
            url: localStorage.getItem("API_URL") + "/ThongBaoDonGiaThueDat/GetAllByRequest",
            data: Data,
            showLoading: true,
            callback: function (res) {
                popup.find('.ddThongBaoDonGiaThueDat').html('');
                $.each(res.Data, function (i, item) {
                    popup.find('.ddThongBaoDonGiaThueDat').append('<option value= "" selected="true" style="display: none"></option>');
                    popup.find('.ddThongBaoDonGiaThueDat').append('<option value="' + item.IdThongBaoDonGiaThueDat + '">' + item.SoThongBaoDonGiaThueDat + '</option>');
                })
                popup.find('.ddThongBaoDonGiaThueDat').on('change', function () {
                    if (popup.find(".ddThongBaoDonGiaThueDat option:selected").val() != 0 && popup.find(".ddThongBaoDonGiaThueDat option:selected").val() != undefined) {
                        Get({
                            url: localStorage.getItem("API_URL") + '/ThongBaoDonGiaThueDat/GetById',
                            data: {
                                idThongBaoDonGiaThueDat: popup.find(".ddThongBaoDonGiaThueDat option:selected").val()
                            },
                            callback: function (res) {
                                if (res.IsSuccess) {
                                    FillFormData('#FormDetailThongBaoTienThueDat', res.Data);
                                    $('.groupThongBaoDonGiaThueDat input').change(function () {
                                        self.TinhToanCongThuc();
                                    });
                                    setTimeout(function () { popup.find('.groupThongBaoDonGiaThueDat input').trigger('change') }, 500);
                                    
                                }
                            }
                        });
                    } else {
                        $('.groupThongBaoDonGiaThueDat input').val("");
                    }
                });
                popup.find('.ddThongBaoDonGiaThueDat').trigger('change');
            }
        });
    },

}

$(document).ready(function () {
    ThongBaoTienThueDatControl.Init();
});

