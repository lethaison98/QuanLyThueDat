if (typeof (ThongBaoGhiThuGhiChiControl) == "undefined") ThongBaoGhiThuGhiChiControl = {};
ThongBaoGhiThuGhiChiControl = {
    Init: function () {
        ThongBaoGhiThuGhiChiControl.RegisterEvents();

    },

    LoadDatatable: function (opts) {
        var idDoanhNghiep;
        if (opts == undefined) {
            idDoanhNghiep = null;
        } else {
            idDoanhNghiep = opts.IdDoanhNghiep
        }
        console.log(23123);
        var self = this;
        self.table = SetDataTable({
            table: $('#tblThongBaoGhiThuGhiChi'),
            url: localStorage.getItem("API_URL") + "/ThongBaoGhiThuGhiChi/GetAllPaging",
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
                        "width": "5%",
                        "data": "RN",
                        "defaultContent": "1",
                        render: function (data, type, row, meta) {
                            var tables = $('#tblThongBaoGhiThuGhiChi').DataTable();
                            var info = tables.page.info();
                            return info.start + meta.row + 1;
                        }
                    },
                    {
                        "class": "name-control",
                        "width": "20%",
                        "data": "SoThongBaoGhiThuGhiChi",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var file = "";
                            if (row.DsFileTaiLieu != null) {
                                $.each(row.DsFileTaiLieu, function (i, item) {
                                    if (item.LoaiTaiLieu == "ThongBaoGhiThuGhiChi") {
                                        file = "<a href = '" + localStorage.getItem('API_URL').replace("api", "") + item.LinkFile + "' target='_blank'><i class = 'fas fa-paperclip' title = 'File thông báo ghi thu ghi chi'></i></a>";
                                    }
                                });
                            }
                            var thaotac = data + "&nbsp" + file;
                            return thaotac;

                        }
                    },
                    {
                        "class": "name-control",
                        "width": "15%",
                        "data": "NgayThongBaoGhiThuGhiChi",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "SoTienGhiThu",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "data": "SoTienGhiChi",
                        "defaultContent": ""
                    },
                    {
                        "class": "function-control",
                        "width": "10%",
                        "orderable": false,
                        //"data": "thaotac",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var show = "";
                            if (!row.QuyenDuLieu.AllowEdit) {
                                show = "style = 'display:none'";
                            }
                            if (opts == undefined) {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    "<a href='javascript:;' class='ThongBaoGhiThuGhiChi-view' data-id='" + row.IdThongBaoGhiThuGhiChi + "'><i class='fas fa-eye' title='Xem'></i></a> &nbsp" +
                                    "</div>";
                                return thaotac;
                            } else {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    "<a href='javascript:;' class='ThongBaoGhiThuGhiChi-view' data-id='" + row.IdThongBaoGhiThuGhiChi + "'><i class='fas fa-eye' title='Xem'></i></a> &nbsp" +
                                    "<a href='javascript:;' class='ThongBaoGhiThuGhiChi-edit' data-id='" + row.IdThongBaoGhiThuGhiChi + "'><i class='fas fa-edit' title='Sửa'></i></a>  &nbsp" +
                                    "<a href='javascript:;' class='ThongBaoGhiThuGhiChi-remove text-danger'"+show+" data-id='" + row.IdThongBaoGhiThuGhiChi + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                    "</div>";
                                return thaotac;
                            }
                        }
                    }
                ]
            },
            callback: function () {
                $("#tblThongBaoGhiThuGhiChi tbody .ThongBaoGhiThuGhiChi-view").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/ThongBaoGhiThuGhiChi/GetById',
                        data: {
                            idThongBaoGhiThuGhiChi: id
                        },
                        callback: function (res) {
                            Get({
                                url: '/ThongBaoGhiThuGhiChi/PopupViewThongBaoGhiThuGhiChi',
                                dataType: 'text',
                                callback: function (popup) {
                                    $('#modalViewThongBaoGhiThuGhiChi').html(popup);
                                    $('#popupViewThongBaoGhiThuGhiChi').modal();
                                    var data = [];
                                    var obj = {
                                        TenDoanhNghiep: res.Data.TenDoanhNghiep,
                                        SoHopDong: res.Data.SoHopDong,
                                        NgayKyHopDong: res.Data.NgayKyHopDong,
                                        NguoiKy: res.Data.NguoiKy,
                                        CoQuanKy: res.Data.CoQuanKy,
                                        ThoiHanHopDong: res.Data.ThoiHanHopDong,
                                        NgayHieuLucHopDong: res.Data.NgayHieuLucHopDong,
                                        NgayHetHieuLucHopDong: res.Data.NgayHetHieuLucHopDong,
                                    };
                                    data.push(obj);

                                    console.log(data);
                                    $('#tblViewThongBaoGhiThuGhiChi').DataTable({
                                        data: data,
                                        dom: 'B',
                                        buttons: [
                                            {
                                                extend: "print",
                                                title: function () {
                                                    var title = '<div class="row"><div class="col-sm-4" style="text-align:center"><h5>UBND TỈNH NGHỆ AN</h5></div></div>' +
                                                        '<div class="row"><div class="col-sm-4" style="text-align:center"><h5>BAN QUẢN LÝ KKT ĐÔNG NAM NGHỆ AN</h5></div></div>' +
                                                        '<div class="row"><div class="col-sm-12" style="text-align:center"><h5>BÁO CÁO CHI TIẾT HỢP ĐỒNG THUÊ ĐẤT</h5></div></div>';
                                                    return title;
                                                },
                                                customize: function (win) {

                                                    var last = null;
                                                    var current = null;
                                                    var bod = [];

                                                    var css = '@page { size: landscape; }',
                                                        head = win.document.head || win.document.getElementsByTagName('head')[0],
                                                        style = win.document.createElement('style');

                                                    style.type = 'text/css';
                                                    style.media = 'print';

                                                    if (style.styleSheet) {
                                                        style.styleSheet.cssText = css;
                                                    }
                                                    else {
                                                        style.appendChild(win.document.createTextNode(css));
                                                    }

                                                    head.appendChild(style);
                                                }
                                            },
                                        ],
                                        columns: [
                                            {
                                                data: "1",
                                                "defaultContent": "1",
                                            },
                                            {
                                                data: 'TenDoanhNghiep'
                                            },
                                            {
                                                data: 'SoHopDong'
                                            },
                                            {
                                                data: 'NgayKyHopDong'
                                            },
                                            {
                                                data: 'NguoiKy'
                                            },
                                            {
                                                data: 'CoQuanKy',
                                            },
                                            {
                                                data: 'ThoiHanHopDong',
                                            },
                                            {
                                                data: 'NgayHieuLucHopDong',
                                            },
                                            {
                                                data: 'NgayHetHieuLucHopDong',
                                            }
                                        ]
                                    });
                                    $('#popupViewThongBaoGhiThuGhiChi .buttons-print').trigger('click');
                                }
                            });
                        }
                    });
                });
                $('#tblThongBaoGhiThuGhiChi tbody .ThongBaoGhiThuGhiChi-edit').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/ThongBaoGhiThuGhiChi/GetById',
                        data: {
                            idThongBaoGhiThuGhiChi: id
                        },
                        callback: function (res) {
                            if (res.IsSuccess) {
                                Get({
                                    url: '/ThongBaoGhiThuGhiChi/PopupDetailThongBaoGhiThuGhiChi',
                                    dataType: 'text',
                                    callback: function (popup) {
                                        $('#modalDetailThongBaoGhiThuGhiChi').html(popup);
                                        $('#popupDetailThongBaoGhiThuGhiChi').modal();
                                        $('#popupDetailThongBaoGhiThuGhiChi .modal-title').text("Chỉnh sửa ghi thu ghi chi - " + opts.TenDoanhNghiep);

                                        FillFormData('#FormDetailThongBaoGhiThuGhiChi', res.Data);
                                        var popup = $('#popupDetailThongBaoGhiThuGhiChi');
                                        if (opts != undefined) {
                                            popup.find('.ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                                        } else {
                                            popup.find('.ddDoanhNghiep').append('<option value="' + res.Data.IdDoanhNghiep + '">' + res.Data.TenDoanhNghiep + '</option>');
                                        }
                                        if (res.Data.IdQuyetDinhThueDat != null) {
                                            console.log(111);
                                            $('#popupDetailThongBaoGhiThuGhiChi .ddQuyetDinhThueDat').append('<option value="' + res.Data.IdQuyetDinhThueDat + '">' + res.Data.SoQuyetDinhThueDat +'</option>');
                                        }
                                        setTimeout(function () {
                                            var checkRole = res.Data.QuyenDuLieu.AllowEdit;
                                            if (!checkRole) {
                                                $("#popupDetailThongBaoGhiThuGhiChi").find('input').attr("disabled", true);
                                                $("#popupDetailThongBaoGhiThuGhiChi").find('select').attr("disabled", true);
                                                $("#popupDetailThongBaoGhiThuGhiChi").find('.fa-trash-alt').hide();
                                                $("#popupDetailThongBaoGhiThuGhiChi").find('.fa-folder').attr("disabled", true);
                                                $("#popupDetailThongBaoGhiThuGhiChi").find('.btn-success').hide();
                                                $("#popupDetailThongBaoGhiThuGhiChi").find('.btn-primary').hide();                                            } else {
                                                self.RegisterEventsPopup();
                                            }
                                        }, 200)
                                        if (res.Data.DsFileTaiLieu != null) {
                                            $.each(res.Data.DsFileTaiLieu, function (i, item) {
                                                if (item.LoaiTaiLieu == "ThongBaoGhiThuGhiChi") {
                                                    $('[data-name="FileThongBaoGhiThuGhiChi"]').html('')
                                                    $('[data-name="FileThongBaoGhiThuGhiChi"]').append('<a href = "' + localStorage.getItem("API_URL").replace('api', '') + item.LinkFile + '" target="_blank">' + item.TenFile + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                                    $('[data-name="FileThongBaoGhiThuGhiChi"]').attr('data-idFile', item.IdFile);
                                                    $('[data-name="FileThongBaoGhiThuGhiChi"]').attr('data-id', item.IdFileTaiLieu);
                                                }
                                            });
                                        }
                                        $('.btn-deleteFile').off('click').on('click', function () {
                                            var $y = $(this);
                                            $y.parent().removeAttr("data-idFile");
                                            $y.parent().html('');

                                        });
                                        //$("#btnTraCuu").on('click', function () {
                                        //    $('#modal-add-edit').modal('show');
                                        //});
                                    }
                                })
                            }
                        }
                    });
                });

                $("#tblThongBaoGhiThuGhiChi tbody .ThongBaoGhiThuGhiChi-remove").off('click').on('click', function (e) {

                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/ThongBaoGhiThuGhiChi/Delete?idThongBaoGhiThuGhiChi=" + $y.attr('data-id') + "&Type=1",
                                headers: {
                                    'Authorization': 'Bearer ' + localStorage.getItem("ACCESS_TOKEN")
                                },
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
    RegisterEventsPopup: function (opts) {
        var self = this;
        $('.select2').select2();
        $('.datetimepicker-input').datetimepicker({
            format: 'DD/MM/YYYY'
        });
        $('#btnSelectFileThongBaoGhiThuGhiChi').click(function () {
            $('#fileThongBaoGhiThuGhiChi').trigger("click");
        });
        if ($('#fileThongBaoGhiThuGhiChi').length > 0) {
            $('#fileThongBaoGhiThuGhiChi')[0].value = "";
            $('#fileThongBaoGhiThuGhiChi').off('change').on('change', function (e) {
                var file = $('#fileThongBaoGhiThuGhiChi')[0].files.length > 0 ? $('#fileThongBaoGhiThuGhiChi')[0].files[0] : null;
                if (file != null) {
                    var dataFile = new FormData();
                    dataFile.append("IdDoanhNghiep", $(".ddDoanhNghiep option:selected").val());
                    dataFile.append("File", file);
                    $.ajax({
                        url: localStorage.getItem("API_URL") + "/File/UploadFile",
                        type: "POST",
                        headers: {
                            'Authorization': 'Bearer ' + localStorage.getItem("access_token")
                        },
                        cache: false,
                        contentType: false,
                        processData: false,
                        data: dataFile,
                        success: function (res) {
                            if (res.IsSuccess) {
                                $('[data-name="FileThongBaoGhiThuGhiChi"]').html('')
                                $('[data-name="FileThongBaoGhiThuGhiChi"]').append('<a href = "#">' + file.name + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                $('[data-name="FileThongBaoGhiThuGhiChi"]').attr('data-idFile', res.Data);
                                $('[data-name="FileThongBaoGhiThuGhiChi"]').attr('data-id', 0);
                                $('.btn-deleteFile').off('click').on('click', function () {
                                    var $y = $(this);
                                    $y.parent().removeAttr("data-idFile");
                                    $y.parent().html('');

                                });
                            } else {
                                alert("Upload không thành công");
                            }
                        }
                    });
                }
            });
        };
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });

        $("#btnSaveThongBaoGhiThuGhiChi").off('click').on('click', function () {
            self.InsertUpdate();
        });

    },

    RegisterEvents: function (opts) {
        var self = this;
        self.LoadDatatable(opts);
        $('#btnCreateThongBaoGhiThuGhiChi').off('click').on('click', function () {

            var $y = $(this);
            Get({
                url: '/ThongBaoGhiThuGhiChi/PopupDetailThongBaoGhiThuGhiChi',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoGhiThuGhiChi').html(res);
                    $('#popupDetailThongBaoGhiThuGhiChi').modal();
                    $('#popupDetailThongBaoGhiThuGhiChi .modal-title').text("Thêm mới ghi thu ghi chi - " + opts.TenDoanhNghiep);

                    var popup = $('#popupDetailThongBaoGhiThuGhiChi');
                    if (opts != undefined) {
                        popup.find('.ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                    } else {
                        self.LoadDanhSachDoanhNghiep();
                    }
                    self.LoadDanhSachQuyetDinhThueDat();
                    self.RegisterEventsPopup();

                }
            })
        });
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchThongBaoGhiThuGhiChi").trigger('click');
            }
        });
        $("#btnSearchThongBaoGhiThuGhiChi").off('click').on('click', function () {
            self.table.ajax.reload();

        });

    },
    InsertUpdate: function () {
        var self = this;
        var popup = $('#popupDetailThongBaoGhiThuGhiChi');
        $(".number").each(function () {
            $(this).val(ConvertStringToDecimal($(this).val()));
        });
        var data = LoadFormData("#FormDetailThongBaoGhiThuGhiChi");
        data.IdDoanhNghiep = popup.find(".ddDoanhNghiep option:selected").val();
        data.IdQuyetDinhThueDat = popup.find(".ddQuyetDinhThueDat option:selected").val();
        var fileTaiLieu = [];
        if ($('[data-name="FileThongBaoGhiThuGhiChi"]').attr("data-idFile") != undefined) {
            fileTaiLieu.push({
                IdFileTaiLieu: $('[data-name="FileThongBaoGhiThuGhiChi"]').attr("data-id"),
                IdFile: $('[data-name="FileThongBaoGhiThuGhiChi"]').attr("data-idFile"),
                LoaiTaiLieu: "ThongBaoGhiThuGhiChi"
            });
        }
        data.FileTaiLieu = fileTaiLieu;

        Post({
            "url": localStorage.getItem("API_URL") + "/ThongBaoGhiThuGhiChi/InsertUpdate",
            "data": data,
            callback: function (res) {
                if (res.IsSuccess) {
                    toastr.success('Thực hiện thành công', 'Thông báo')
                    self.table.ajax.reload();
                    $('#btnCloseThongBaoGhiThuGhiChi').trigger('click');
                }
                else {
                    toastr.error(res.Message, 'Có lỗi xảy ra')
                }

            }
        });
    },
    LoadDanhSachDoanhNghiep: function () {
        Get({
            url: localStorage.getItem("API_URL") + "/DoanhNghiep/GetAll",
            showLoading: true,
            callback: function (res) {
                $('#ddDoanhNghiep').html('');
                $.each(res.Data, function (i, item) {
                    $('#ddDoanhNghiep').append('<option value="' + item.IdDoanhNghiep + '">' + item.TenDoanhNghiep + '</option>');
                })

            }
        });
    },
    LoadDanhSachQuyetDinhThueDat: function () {
        var popup = $('#popupDetailThongBaoGhiThuGhiChi')
        Get({
            url: localStorage.getItem("API_URL") + "/QuyetDinhThueDat/GetListQuyetDinhThueDatChiTiet",
            data: {
                idDoanhNghiep: popup.find(".ddDoanhNghiep option:selected").val()
            },
            showLoading: true,
            callback: function (res) {
                popup.find('.ddQuyetDinhThueDat').html('');
                popup.find('.ddQuyetDinhThueDat').append('<option value= "" selected="true" style="display: none"></option>');
                $.each(res.Data, function (i, item) {
                    if (item.HinhThucThue == "ThueDatTraTienHangNam" || item.HinhThucThue == "HopDongThueLaiDat") {
                        var name = item.SoQuyetDinhThueDat + " - " + item.TextHinhThucThue + " - Diện tích " + item.TongDienTich + "  (m<sup>2</sup>)"
                        popup.find('.ddQuyetDinhThueDat').append('<option value=' + item.IdQuyetDinhThueDat + ' index= ' + i + '>' + name + '</option>');
                    }

                })
                popup.find('.ddQuyetDinhThueDat').on('change', function () {
                    if (popup.find(".ddQuyetDinhThueDat option:selected").val() != undefined) {
                        var qd = res.Data[popup.find(".ddQuyetDinhThueDat option:selected").attr("index")];
                    }
                });
                popup.find('.ddQuyetDinhThueDat').trigger('change');
            }
        });
    },
}

$(document).ready(function () {
    ThongBaoGhiThuGhiChiControl.Init();
});

