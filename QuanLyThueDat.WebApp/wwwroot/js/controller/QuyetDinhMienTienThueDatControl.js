if (typeof (QuyetDinhMienTienThueDatControl) == "undefined") QuyetDinhMienTienThueDatControl = {};
QuyetDinhMienTienThueDatControl = {
    Init: function () {
        QuyetDinhMienTienThueDatControl.RegisterEvents();

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
            table: $('#tblQuyetDinhMienTienThueDat'),
            url: localStorage.getItem("API_URL") + "/QuyetDinhMienTienThueDat/GetAllPaging",
            dom: "rtip",
            data: {
                "requestData": function () {
                    return {
                        "keyword": $('#txtKEY_WORD').val(),
                        idDoanhNghiep: idDoanhNghiep
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
                            var tables = $('#tblQuyetDinhMienTienThueDat').DataTable();
                            var info = tables.page.info();
                            return info.start + meta.row + 1;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "TenDoanhNghiep",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "data": "SoQuyetDinhMienTienThueDat",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var file = "";
                            if (row.DsFileTaiLieu != null) {
                                $.each(row.DsFileTaiLieu, function (i, item) {
                                    if (item.LoaiTaiLieu == "QuyetDinhMienTienThueDat") {
                                        file += "<a href = '" + localStorage.getItem('API_URL').replace("api", "") + item.LinkFile + "' target='_blank'><i class = 'fas fa-paperclip' title = 'File quyết định miễn tiền thuê đất'></i></a>";
                                    }
                                });
                            }
                            var thaotac = data + "&nbsp" + file;
                            return thaotac;
                        }
                    },

                    {
                        "class": "name-control",
                        "data": "NgayQuyetDinhMienTienThueDat",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "NgayHieuLucMienTienThueDat",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "NgayHetHieuLucMienTienThueDat",
                        "defaultContent": ""
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        //"data": "thaotac",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var file = "";
                            if (row.DsFileTaiLieu != null) {
                                $.each(row.DsFileTaiLieu, function (i, item) {
                                    if (item.LoaiTaiLieu == "QuyetDinhMienTienThueDat") {
                                        file += "<a href = '" + localStorage.getItem('API_URL').replace("api", "") + item.LinkFile + "' target='_blank'><i class = 'fas fa-paperclip' title = 'File quyết định miễn tiền thuê đất'></i></a>";
                                    }
                                });
                            }
                            console.log(opts);
                            if (opts == undefined) {
                                console.log(111);
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    file + "&nbsp" +
                                    "<a href='javascript:;' class='QuyetDinhMienTienThueDat-view' data-id='" + row.IdQuyetDinhMienTienThueDat + "'><i class='fas fa-eye' title='Xem'></i></a> &nbsp" +
                                    "<a href='javascript:;' class='QuyetDinhMienTienThueDat-export' data-id='" + row.IdQuyetDinhMienTienThueDat + "'><i class='fas fa-file-excel' title='Xuất thông báo' ></i></a></div> ";

                                    "</div>";
                                return thaotac;
                            } else {
                                var show = "";
                                if (!row.QuyenDuLieu.AllowEdit) {
                                    show = "style = 'display:none'";
                                }
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    file + "&nbsp" +
                                    "<a href='javascript:;' class='QuyetDinhMienTienThueDat-view' data-id='" + row.IdQuyetDinhMienTienThueDat + "'><i class='fas fa-eye' title='Xem'></i></a> &nbsp" +
                                    "<a href='javascript:;' class='QuyetDinhMienTienThueDat-export' data-id='" + row.IdQuyetDinhMienTienThueDat + "'><i class='fas fa-file-excel' title='Xuất thông báo' ></i></a> &nbsp" +
                                    "<a href='javascript:;' class='QuyetDinhMienTienThueDat-edit' data-id='" + row.IdQuyetDinhMienTienThueDat + "'><i class='fas fa-edit' title='Sửa'></i></a>  &nbsp" +
                                    "<a href='javascript:;' class='QuyetDinhMienTienThueDat-remove text-danger'" + show +" data-id='" + row.IdQuyetDinhMienTienThueDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                    "</div>";
                                return thaotac;
                            }
                        }
                    }
                ]
            },
            callback: function () {
                $("#tblQuyetDinhMienTienThueDat tbody .QuyetDinhMienTienThueDat-export").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        var w = window.open("BaoCao/ExportQuyetDinhMienTienThueDat?idQuyetDinhMienTienThueDat=" + id);
                        console.log(w);
                        w.print();
                    }
                });
                $("#tblQuyetDinhMienTienThueDat tbody .QuyetDinhMienTienThueDat-view").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/QuyetDinhMienTienThueDat/GetById',
                        data: {
                            idQuyetDinhMienTienThueDat: id
                        },
                        callback: function (res) {
                            Get({
                                url: '/QuyetDinhMienTienThueDat/PopupViewQuyetDinhMienTienThueDat',
                                dataType: 'text',
                                callback: function (popup) {
                                    $('#modalViewQuyetDinhMienTienThueDat').html(popup);
                                    $('#popupViewQuyetDinhMienTienThueDat').modal();

                                    var data = [];
                                    var obj = {
                                        TenDoanhNghiep: res.Data.TenDoanhNghiep,
                                        SoQuyetDinhMienTienThueDat: res.Data.SoQuyetDinhMienTienThueDat,
                                        NgayQuyetDinhMienTienThueDat: res.Data.NgayQuyetDinhMienTienThueDat,
                                        DienTichMienTienThueDat: res.Data.DienTichMienTienThueDat,
                                        ThoiHanMienTienThueDat: res.Data.ThoiHanMienTienThueDat,
                                        NgayHieuLucMienTienThueDat: res.Data.NgayHieuLucMienTienThueDat,
                                        NgayHetHieuLucMienTienThueDat: res.Data.NgayHetHieuLucMienTienThueDat,
                                    };
                                    data.push(obj);
                                    $('#tblViewQuyetDinhMienTienThueDat').DataTable({
                                        data: data,
                                        dom: 'B',
                                        buttons: [
                                            {
                                                extend: "print",
                                                title: function () {
                                                    var title = '<div class="row"><div class="col-sm-4" style="text-align:center"><h5>UBND TỈNH NGHỆ AN</h5></div></div>' +
                                                        '<div class="row"><div class="col-sm-4" style="text-align:center"><h5>BAN QUẢN LÝ KKT ĐÔNG NAM NGHỆ AN</h5></div></div>' +
                                                        '<div class="row"><div class="col-sm-12" style="text-align:center"><h5>BÁO CÁO CHI TIẾT QUYẾT ĐỊNH MIỄN TIỀN THUÊ ĐẤT</h5></div></div>';
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
                                                data: 'SoQuyetDinhMienTienThueDat'
                                            },
                                            {
                                                data: 'NgayQuyetDinhMienTienThueDat'
                                            },
                                            {
                                                data: 'DienTichMienTienThueDat'
                                            },
                                            {
                                                data: 'ThoiHanMienTienThueDat',
                                            },
                                            {
                                                data: 'NgayHieuLucMienTienThueDat',
                                            },
                                            {
                                                data: 'NgayHetHieuLucMienTienThueDat',
                                            }
                                        ]
                                    });
                                    $('#popupViewQuyetDinhMienTienThueDat .buttons-print').trigger('click');

                                }
                            });
                        }
                    });
                });
                $('#tblQuyetDinhMienTienThueDat tbody .QuyetDinhMienTienThueDat-edit').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/QuyetDinhMienTienThueDat/GetById',
                        data: {
                            idQuyetDinhMienTienThueDat: id
                        },
                        callback: function (res) {
                            if (res.IsSuccess) {
                                Get({
                                    url: '/QuyetDinhMienTienThueDat/PopupDetailQuyetDinhMienTienThueDat',
                                    dataType: 'text',
                                    callback: function (popup) {
                                        $('#modalDetailQuyetDinhMienTienThueDat').html(popup);
                                        $('#popupDetailQuyetDinhMienTienThueDat').modal();
                                        $('#popupViewQuyetDinhMienTienThueDat .modal-title').text("Chỉnh sửa quyết định miễn tiền thuê đất - " + opts.TenDoanhNghiep);

                                        FillFormData('#FormDetailQuyetDinhMienTienThueDat', res.Data);
                                        if (opts != undefined) {
                                            $('#popupDetailQuyetDinhMienTienThueDat .ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                                        } else {
                                            self.LoadDanhSachDoanhNghiep();
                                        }
                                        if (res.Data.IdQuyetDinhThueDat != null) {
                                            $('#popupDetailQuyetDinhMienTienThueDat .ddQuyetDinhThueDat').append('<option value="' + res.Data.IdQuyetDinhThueDat + '">' + res.Data.SoQuyetDinhThueDat +'</option>');
                                        }
                                        if (res.Data.DsFileTaiLieu != null) {
                                            $.each(res.Data.DsFileTaiLieu, function (i, item) {
                                                if (item.LoaiTaiLieu == "QuyetDinhMienTienThueDat") {
                                                    $('[data-name="FileQuyetDinhMienTienThueDat"]').append('<div><a href = "' + localStorage.getItem("API_URL").replace('api', '') + item.LinkFile + '"target="_blank" data-IdFile="' + item.IdFile + '" data-id="' + item.IdFileTaiLieu + '">' + item.TenFile + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i></div>');
                                                }
                                            });
                                            $('.btn-deleteFile').off('click').on('click', function () {
                                                var $y = $(this);
                                                $y.parent().html('');
                                            });
                                        }
                                        setTimeout(function () {
                                            var checkRole = res.Data.QuyenDuLieu.AllowEdit;
                                            if (!checkRole) {
                                                $("#popupDetailQuyetDinhMienTienThueDat").find('input').attr("disabled", true);
                                                $("#popupDetailQuyetDinhMienTienThueDat").find('select').attr("disabled", true);
                                                $("#popupDetailQuyetDinhMienTienThueDat").find('.fa-trash-alt').hide();
                                                $("#popupDetailQuyetDinhMienTienThueDat").find('.fa-folder').attr("disabled", true);
                                                $("#popupDetailQuyetDinhMienTienThueDat").find('.btn-success').hide();
                                                $("#popupDetailQuyetDinhMienTienThueDat").find('.btn-primary').hide();
                                            } else {
                                                self.RegisterEventsPopup();
                                            }
                                        }, 200)
                                    }
                                })
                            }
                        }
                    });
                });

                $("#tblQuyetDinhMienTienThueDat tbody .QuyetDinhMienTienThueDat-remove").off('click').on('click', function (e) {

                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/QuyetDinhMienTienThueDat/Delete?idQuyetDinhMienTienThueDat=" + $y.attr('data-id') + "&Type=1",
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
        $('#btnSelectFileQuyetDinhMienTienThueDat').click(function () {
            $('#fileQuyetDinhMienTienThueDat').trigger("click");
        });
        if ($('#fileQuyetDinhMienTienThueDat').length > 0) {
            $('#fileQuyetDinhMienTienThueDat')[0].value = "";
            $('#fileQuyetDinhMienTienThueDat').off('change').on('change', function (e) {

                var file = $('#fileQuyetDinhMienTienThueDat')[0].files.length > 0 ? $('#fileQuyetDinhMienTienThueDat')[0].files[0] : null;
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
                                $('[data-name="FileQuyetDinhMienTienThueDat"]').append('<div><a href = "#" data-id="0" data-IdFile = "' + res.Data + '">' + file.name + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i></div>');
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
        }
        $("#btnSaveQuyetDinhMienTienThueDat").off('click').on('click', function () {
            self.InsertUpdate();
        });
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
    },

    RegisterEvents: function (opts) {
        var self = this;
        self.LoadDatatable(opts);
        $('#btnCreateQuyetDinhMienTienThueDat').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/QuyetDinhMienTienThueDat/PopupDetailQuyetDinhMienTienThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailQuyetDinhMienTienThueDat').html(res);
                    $('#popupDetailQuyetDinhMienTienThueDat').modal();
                    $('#popupDetailQuyetDinhMienTienThueDat .modal-title').text("Thêm mới quyết định miễn tiền thuê đất - " + opts.TenDoanhNghiep);

                    if (opts != undefined) {
                        $('#popupDetailQuyetDinhMienTienThueDat .ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                        self.LoadDanhSachQuyetDinhThueDat();
                    } else {
                        self.LoadDanhSachDoanhNghiep();
                    }
                    self.RegisterEventsPopup();
                }
            })
        });
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchQuyetDinhMienTienThueDat").trigger('click');
            }
        });
        $("#btnSearchQuyetDinhMienTienThueDat").off('click').on('click', function () {
            self.table.ajax.reload();

        });

        $("#btnXuatBaoCaoQuyetDinhMienTienThueDat").off('click').on('click', function (e) {
            window.open("/ExportQuyetDinhMienTienThueDat");
        });

    },
    InsertUpdate: function () {
        var self = this;
        $(".number").each(function () {
            $(this).val(ConvertStringToDecimal($(this).val()));
        });
        var data = LoadFormData("#FormDetailQuyetDinhMienTienThueDat");
        data.IdDoanhNghiep = $(".ddDoanhNghiep option:selected").val();
        data.IdQuyetDinhThueDat = $(".ddQuyetDinhThueDat option:selected").val();

        var fileTaiLieu = [];
        $('[data-name="FileQuyetDinhMienTienThueDat"]').find("a").each(function () {
            fileTaiLieu.push({
                IdFileTaiLieu: $(this).attr("data-id"),
                IdFile: $(this).attr("data-idFile"),
                LoaiTaiLieu: "QuyetDinhMienTienThueDat"
            });
        });

        data.FileTaiLieu = fileTaiLieu;
        Post({
            "url": localStorage.getItem("API_URL") + "/QuyetDinhMienTienThueDat/InsertUpdate",
            "data": data,
            callback: function (res) {
                if (res.IsSuccess) {
                    toastr.success('Thực hiện thành công', 'Thông báo')
                    self.table.ajax.reload();
                    $('#btnCloseQuyetDinhMienTienThueDat').trigger('click');
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
                $('.ddDoanhNghiep').html('');
                $.each(res.Data, function (i, item) {
                    $('.ddDoanhNghiep').append('<option value="' + item.IdDoanhNghiep + '">' + item.TenDoanhNghiep + '</option>');
                })

            }
        });
    },
    LoadDanhSachQuyetDinhThueDat: function () {
        var popup = $('#popupDetailQuyetDinhMienTienThueDat')
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
    QuyetDinhMienTienThueDatControl.Init();
});

