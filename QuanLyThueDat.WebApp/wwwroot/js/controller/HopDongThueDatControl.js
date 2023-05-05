if (typeof (HopDongThueDatControl) == "undefined") HopDongThueDatControl = {};
HopDongThueDatControl = {
    Init: function () {
        HopDongThueDatControl.RegisterEvents();

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
            table: $('#tblHopDongThueDat'),
            url: localStorage.getItem("API_URL") + "/HopDongThueDat/GetAllPaging",
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
                            var tables = $('#tblHopDongThueDat').DataTable();
                            var info = tables.page.info();
                            return info.start + meta.row + 1;
                        }
                    },
                    {
                        "class": "name-control",
                        "width": "20%",
                        "data": "SoHopDong",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var file = "";
                            if (row.DsFileTaiLieu != null) {
                                $.each(row.DsFileTaiLieu, function (i, item) {
                                    if (item.LoaiTaiLieu == "HopDongThueDat") {
                                        file += "<a href = '" + localStorage.getItem('API_URL').replace("api", "") + item.LinkFile + "' target='_blank'><i class = 'fas fa-paperclip' title = 'File hợp đồng thuê đất'></i></a>";
                                    }
                                });
                            }
                            var thaotac = data + "&nbsp" + file;
                            return thaotac;

                        }

                    },
                    {
                        "class": "name-control",
                        "data": "TenDoanhNghiep",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "width": "15%",
                        "data": "NgayKyHopDong",
                        "defaultContent": "",
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
                                    "<a href='javascript:;' class='HopDongThueDat-view' data-id='" + row.IdHopDongThueDat + "'><i class='fas fa-eye' title='Xem'></i></a> &nbsp" +
                                    "</div>";
                                return thaotac;
                            } else {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    "<a href='javascript:;' class='HopDongThueDat-view' data-id='" + row.IdHopDongThueDat + "'><i class='fas fa-eye' title='Xem'></i></a> &nbsp" +
                                    "<a href='javascript:;' class='HopDongThueDat-edit' data-id='" + row.IdHopDongThueDat + "'><i class='fas fa-edit' title='Sửa'></i></a>  &nbsp" +
                                    "<a href='javascript:;' class='HopDongThueDat-remove text-danger'"+show+" data-id='" + row.IdHopDongThueDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                    "</div>";
                                return thaotac;
                            }
                        }
                    }
                ]
            },
            callback: function () {
                $("#tblHopDongThueDat tbody .HopDongThueDat-view").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/HopDongThueDat/GetById',
                        data: {
                            idHopDongThueDat: id
                        },
                        callback: function (res) {
                            Get({
                                url: '/HopDongThueDat/PopupViewHopDongThueDat',
                                dataType: 'text',
                                callback: function (popup) {
                                    $('#modalViewHopDongThueDat').html(popup);
                                    $('#popupViewHopDongThueDat').modal();
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
                                    $('#tblViewHopDongThueDat').DataTable({
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
                                    $('#popupViewHopDongThueDat .buttons-print').trigger('click');
                                }
                            });
                        }
                    });
                });
                $('#tblHopDongThueDat tbody .HopDongThueDat-edit').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/HopDongThueDat/GetById',
                        data: {
                            idHopDongThueDat: id
                        },
                        callback: function (res) {
                            if (res.IsSuccess) {
                                Get({
                                    url: '/HopDongThueDat/PopupDetailHopDongThueDat',
                                    dataType: 'text',
                                    callback: function (popup) {
                                        $('#modalDetailHopDongThueDat').html(popup);
                                        $('#popupDetailHopDongThueDat').modal();
                                        $('#popupDetailHopDongThueDat .modal-title').text("Chỉnh sửa hợp đồng thuê đất - " + opts.TenDoanhNghiep);

                                        FillFormData('#FormDetailHopDongThueDat', res.Data);
                                        var popup = $('#popupDetailHopDongThueDat');
                                        if (opts != undefined) {
                                            popup.find('.ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                                        } else {
                                            popup.find('.ddDoanhNghiep').append('<option value="' + res.Data.IdDoanhNghiep + '">' + res.Data.TenDoanhNghiep + '</option>');
                                        }
                                        if (res.Data.IdQuyetDinhThueDat != null) {
                                            console.log(111);
                                            $('#popupDetailHopDongThueDat .ddQuyetDinhThueDat').append('<option value="' + res.Data.IdQuyetDinhThueDat + '">' + res.Data.SoQuyetDinhThueDat +'</option>');
                                        }
                                        setTimeout(function () {
                                            var checkRole = res.Data.QuyenDuLieu.AllowEdit;
                                            if (!checkRole) {
                                                $("#popupDetailHopDongThueDat").find('input').attr("disabled", true);
                                                $("#popupDetailHopDongThueDat").find('select').attr("disabled", true);
                                                $("#popupDetailHopDongThueDat").find('.fa-trash-alt').hide();
                                                $("#popupDetailHopDongThueDat").find('.fa-folder').attr("disabled", true);
                                                $("#popupDetailHopDongThueDat").find('.btn-success').hide();
                                                $("#popupDetailHopDongThueDat").find('.btn-primary').hide();                                            } else {
                                                self.RegisterEventsPopup();
                                            }
                                        }, 200)
                                        if (res.Data.DsFileTaiLieu != null) {
                                            $.each(res.Data.DsFileTaiLieu, function (i, item) {
                                                if (item.LoaiTaiLieu == "HopDongThueDat") {
                                                    $('[data-name="FileHopDongThueDat"]').append('<div><a href = "' + localStorage.getItem("API_URL").replace('api', '') + item.LinkFile + '"target="_blank" data-IdFile="' + item.IdFile + '" data-id="' + item.IdFileTaiLieu + '">' + item.TenFile + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i></div>');
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

                $("#tblHopDongThueDat tbody .HopDongThueDat-remove").off('click').on('click', function (e) {

                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/HopDongThueDat/Delete?idHopDongThueDat=" + $y.attr('data-id') + "&Type=1",
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
        $('#btnSelectFileHopDongThueDat').click(function () {
            $('#fileHopDongThueDat').trigger("click");
        });
        if ($('#fileHopDongThueDat').length > 0) {
            $('#fileHopDongThueDat')[0].value = "";
            $('#fileHopDongThueDat').off('change').on('change', function (e) {
                var file = $('#fileHopDongThueDat')[0].files.length > 0 ? $('#fileHopDongThueDat')[0].files[0] : null;
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
                                $('[data-name="FileHopDongThueDat"]').append('<div><a href = "#" data-id="0" data-IdFile = "' + res.Data + '">' + file.name + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i></div>');
                                $('.btn-deleteFile').off('click').on('click', function () {
                                    var $y = $(this);
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

        $("#btnSave").off('click').on('click', function () {
            self.InsertUpdate();
        });

    },

    RegisterEvents: function (opts) {
        var self = this;
        self.LoadDatatable(opts);
        $('#btnCreateHopDongThueDat').off('click').on('click', function () {

            var $y = $(this);
            Get({
                url: '/HopDongThueDat/PopupDetailHopDongThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailHopDongThueDat').html(res);
                    $('#popupDetailHopDongThueDat').modal();
                    $('#popupDetailHopDongThueDat .modal-title').text("Thêm mới hợp đồng thuê đất - " + opts.TenDoanhNghiep);

                    var popup = $('#popupDetailHopDongThueDat');
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
                $("#btnSearchHopDongThueDat").trigger('click');
            }
        });
        $("#btnSearchHopDongThueDat").off('click').on('click', function () {
            self.table.ajax.reload();

        });

    },
    InsertUpdate: function () {
        var self = this;
        $(".number").each(function () {
            $(this).val(ConvertStringToDecimal($(this).val()));
        });
        var data = LoadFormData("#FormDetailHopDongThueDat");
        data.IdDoanhNghiep = $(".ddDoanhNghiep option:selected").val();
        data.IdQuyetDinhThueDat = $(".ddQuyetDinhThueDat option:selected").val();
        var fileTaiLieu = [];
        $('[data-name="FileHopDongThueDat"]').find("a").each(function () {
            fileTaiLieu.push({
                IdFileTaiLieu: $(this).attr("data-id"),
                IdFile: $(this).attr("data-idFile"),
                LoaiTaiLieu: "HopDongThueDat"
            });
        });
        data.FileTaiLieu = fileTaiLieu;

        Post({
            "url": localStorage.getItem("API_URL") + "/HopDongThueDat/InsertUpdate",
            "data": data,
            callback: function (res) {
                if (res.IsSuccess) {
                    toastr.success('Thực hiện thành công', 'Thông báo')
                    self.table.ajax.reload();
                    $('#btnCloseHopDongThueDat').trigger('click');
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
        var popup = $('#popupDetailHopDongThueDat')
        Get({
            url: localStorage.getItem("API_URL") + "/QuyetDinhThueDat/GetAll",
            data: {
                idDoanhNghiep: popup.find(".ddDoanhNghiep option:selected").val()
            },
            showLoading: true,
            callback: function (res) {
                popup.find('.ddQuyetDinhThueDat').html('');
                popup.find('.ddQuyetDinhThueDat').append('<option value= "" selected="true" style="display: none"></option>');
                $.each(res.Data, function (i, item) {
                        var name = item.SoQuyetDinhThueDat + " - " + " - Diện tích " + item.TongDienTich + "  (m<sup>2</sup>)"
                        popup.find('.ddQuyetDinhThueDat').append('<option value=' + item.IdQuyetDinhThueDat + ' index= ' + i + '>' + name + '</option>');

                })
                popup.find('.ddQuyetDinhThueDat').on('change', function () {
                    if (popup.find(".ddQuyetDinhThueDat option:selected").val() != undefined) {
                        var qd = res.Data[popup.find(".ddQuyetDinhThueDat option:selected").attr("index")];
                        popup.find('[data-name="DienTich"]').val(qd.TongDienTich);
                    }
                });
                popup.find('.ddQuyetDinhThueDat').trigger('change');
            }
        });
    },
}

$(document).ready(function () {
    HopDongThueDatControl.Init();
});

