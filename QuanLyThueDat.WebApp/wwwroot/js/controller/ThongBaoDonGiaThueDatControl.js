if (typeof (ThongBaoDonGiaThueDatControl) == "undefined") ThongBaoDonGiaThueDatControl = {};
ThongBaoDonGiaThueDatControl = {
    Init: function () {
        ThongBaoDonGiaThueDatControl.RegisterEvents();

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
            table: $('#tblThongBaoDonGiaThueDat'),
            url: localStorage.getItem("API_URL") + "/ThongBaoDonGiaThueDat/GetAllPaging",
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
                        "data": "SoThongBaoDonGiaThueDat",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var file = "";
                            if (row.DsFileTaiLieu != null) {
                                $.each(row.DsFileTaiLieu, function (i, item) {
                                    if (item.LoaiTaiLieu == "ThongBaoDonGiaThueDat") {
                                        file = "<a href = '" + localStorage.getItem('API_URL').replace("api", "") + item.LinkFile + "' target='_blank'><i class = 'fas fa-paperclip' title = 'File thông báo đơn giá thuê đất'></i></a>";
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
                        "data": "NgayThongBaoDonGiaThueDat",
                        "defaultContent": "",
                    },
                    //{
                    //    "class": "name-control",
                    //    "data": "DienTichPhaiNop",
                    //    "defaultContent": "",
                    //},
                    //{
                    //    "class": "name-control",
                    //    "data": "DienTichKhongPhaiNop",
                    //    "defaultContent": "",
                    //},
                    {
                        "class": "name-control",
                        "data": "DonGia",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "NgayHieuLucDonGiaThueDat",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "NgayHetHieuLucDonGiaThueDat",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        //"data": "thaotac",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            if (opts == undefined) {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    "<a href='javascript:;' class='ThongBaoDonGiaThueDat-export' data-id='" + row.IdThongBaoDonGiaThueDat + "'><i class='fas fa-file-word' title='Xuất thông báo' ></i></a> &nbsp" +
                                    "</div>";
                                return thaotac;
                            } else {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    "<a href='javascript:;' class='ThongBaoDonGiaThueDat-export' data-id='" + row.IdThongBaoDonGiaThueDat + "'><i class='fas fa-file-word' title='Xuất thông báo' ></i></a> &nbsp" +
                                    "<a href='javascript:;' class='ThongBaoDonGiaThueDat-edit' data-id='" + row.IdThongBaoDonGiaThueDat + "'><i class='fas fa-edit' title='Chỉnh sửa'></i></a>" +
                                    "<a href='javascript:;' class='ThongBaoDonGiaThueDat-remove text-danger' data-id='" + row.IdThongBaoDonGiaThueDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                    "</div>";
                                return thaotac;
                            }
                        }
                    }
                ]
            },
            callback: function () {
                $("#tblThongBaoDonGiaThueDat tbody .ThongBaoDonGiaThueDat-export").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        window.open("/CreateWordFile?idThongBao=" + id + "&loaiThongBao=ThongBaoDonGiaThueDat", "_blank");
                    }
                });
                $('#tblThongBaoDonGiaThueDat tbody .ThongBaoDonGiaThueDat-edit').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/ThongBaoDonGiaThueDat/GetById',
                        data: {
                            idThongBaoDonGiaThueDat: id
                        },
                        callback: function (res) {
                            if (res.IsSuccess) {
                                Get({
                                    url: '/ThongBaoDonGiaThueDat/PopupDetailThongBaoDonGiaThueDat',
                                    dataType: 'text',
                                    callback: function (popup) {
                                        $('#modalDetailThongBaoDonGiaThueDat').html(popup);
                                        $('#popupDetailThongBaoDonGiaThueDat').modal();
                                        $('#popupDetailThongBaoDonGiaThueDat .modal-title').text("Chỉnh sửa thông báo đơn giá thuê đất - " + opts.TenDoanhNghiep);

                                        FillFormData('#FormDetailThongBaoDonGiaThueDat', res.Data);
                                        var popup = $('#popupDetailThongBaoDonGiaThueDat');

                                        if (opts != undefined) {
                                            popup.find('.ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                                        } else {
                                            $('#popupDetailThongBaoDonGiaThueDat .ddDoanhNghiep').append('<option value="' + res.Data.IdDoanhNghiep + '">' + res.Data.TenDoanhNghiep + '</option>');
                                        }
                                        if (res.Data.IdQuyetDinhThueDat != null) {
                                            $('#popupDetailThongBaoDonGiaThueDat .ddQuyetDinhThueDat').append('<option value="' + res.Data.IdQuyetDinhThueDat + '">' + res.Data.SoQuyetDinhThueDat + " - " + res.Data.NgayQuyetDinhThueDat + '</option>');
                                        }

                                        self.RegisterEventsPopup();
                                        if (res.Data.DsFileTaiLieu != null) {
                                            $.each(res.Data.DsFileTaiLieu, function (i, item) {
                                                if (item.LoaiTaiLieu == "ThongBaoDonGiaThueDat") {
                                                    $('[data-name="FileThongBaoDonGiaThueDat"]').html('')
                                                    $('[data-name="FileThongBaoDonGiaThueDat"]').append('<a href = "' + localStorage.getItem("API_URL").replace('api', '') + item.LinkFile + '" target="_blank">' + item.TenFile + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                                    $('[data-name="FileThongBaoDonGiaThueDat"]').attr('data-idFile', item.IdFile);
                                                    $('[data-name="FileThongBaoDonGiaThueDat"]').attr('data-id', item.IdFileTaiLieu);
                                                }
                                            });
                                        }
                                        $('.btn-deleteFile').off('click').on('click', function () {
                                            var $y = $(this);
                                            $y.parent().removeAttr("data-idFile");
                                            $y.parent().html('');

                                        });

                                        $('#popupDetailThongBaoDonGiaThueDat .select2').attr("disabled", true);

                                    }
                                })
                            }
                        }
                    });
                });

                $("#tblThongBaoDonGiaThueDat tbody .ThongBaoDonGiaThueDat-remove").off('click').on('click', function (e) {

                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/ThongBaoDonGiaThueDat/Delete?idThongBaoDonGiaThueDat=" + $y.attr('data-id') + "&Type=1",
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
    RegisterEventsPopup: function () {
        var self = this;
        setTimeout(function () {
            var checkRole = localStorage.getItem("Roles").includes("ThongBaoDonGiaThueDat");
            if (!checkRole) {
                $("#popupDetailThongBaoDonGiaThueDat").find('input').attr("disabled", true);
                $("#popupDetailThongBaoDonGiaThueDat").find('select').attr("disabled", true);
                $("#popupDetailThongBaoDonGiaThueDat").find('.fa-trash-alt').hide();
                $("#popupDetailThongBaoDonGiaThueDat").find('.fa-folder').attr("disabled", true);
                $("#popupDetailThongBaoDonGiaThueDat").find('.btn-success').hide();
                $("#popupDetailThongBaoDonGiaThueDat").find('.btn-primary').hide();
            }
        }, 200)
        $('.select2').select2();
        $('.datetimepicker-input').datetimepicker({
            format: 'DD/MM/YYYY'
        });
        $('#btnSelectFileThongBaoDonGiaThueDat').click(function () {
            $('#fileThongBaoDonGiaThueDat').trigger("click");
        });
        if ($('#fileThongBaoDonGiaThueDat').length > 0) {
            $('#fileThongBaoDonGiaThueDat')[0].value = "";
            $('#fileThongBaoDonGiaThueDat').off('change').on('change', function (e) {
                var file = $('#fileThongBaoDonGiaThueDat')[0].files.length > 0 ? $('#fileThongBaoDonGiaThueDat')[0].files[0] : null;
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
                                $('[data-name="FileThongBaoDonGiaThueDat"]').html('')
                                $('[data-name="FileThongBaoDonGiaThueDat"]').append('<a href = "#">' + file.name + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                $('[data-name="FileThongBaoDonGiaThueDat"]').attr('data-idFile', res.Data);
                                $('[data-name="FileThongBaoDonGiaThueDat"]').attr('data-id', 0);
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
        $("#btnSaveThongBaoDonGiaThueDat").off('click').on('click', function () {
            self.InsertUpdate();
        });
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
    },

    RegisterEvents: function (opts) {
        var self = this;
        self.LoadDatatable(opts);
        $('#btnCreateThongBaoDonGiaThueDat').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoDonGiaThueDat/PopupDetailThongBaoDonGiaThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoDonGiaThueDat').html(res);
                    $('#popupDetailThongBaoDonGiaThueDat').modal();
                    $('#popupDetailThongBaoDonGiaThueDat .modal-title').text("Thêm mới thông báo đơn giá thuê đất - " + opts.TenDoanhNghiep);

                    var popup = $('#popupDetailThongBaoDonGiaThueDat');
                    if (opts != undefined) {
                        popup.find('.ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
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
                $("#btnSearchThongBaoDonGiaThueDat").trigger('click');
            }
        });
        $("#btnSearchThongBaoDonGiaThueDat").off('click').on('click', function () {
            self.table.ajax.reload();

        });

    },
    InsertUpdate: function () {
        var self = this;
        var popup = $('#popupDetailThongBaoDonGiaThueDat');
        $(".number").each(function () {
            $(this).val(ConvertStringToDecimal($(this).val()));
        });
        var data = LoadFormData("#FormDetailThongBaoDonGiaThueDat");
        data.IdDoanhNghiep = popup.find(".ddDoanhNghiep option:selected").val();
        data.IdQuyetDinhThueDat = popup.find(".ddQuyetDinhThueDat option:selected").val();

        var fileTaiLieu = [];
        if ($('[data-name="FileThongBaoDonGiaThueDat"]').attr("data-idFile") != undefined) {
            fileTaiLieu.push({
                IdFileTaiLieu: $('[data-name="FileThongBaoDonGiaThueDat"]').attr("data-id"),
                IdFile: $('[data-name="FileThongBaoDonGiaThueDat"]').attr("data-idFile"),
                LoaiTaiLieu: "ThongBaoDonGiaThueDat"
            });
        }
        data.FileTaiLieu = fileTaiLieu;
        Post({
            "url": localStorage.getItem("API_URL") + "/ThongBaoDonGiaThueDat/InsertUpdate",
            "data": data,
            callback: function (res) {
                self.table.ajax.reload();
                $('#btnCloseThongBaoDonGiaThueDat').trigger('click');
            }
        });
    },
    LoadDanhSachDoanhNghiep: function () {
        var self = this;
        var popup = $('#popupDetailThongBaoDonGiaThueDat')
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
        var popup = $('#popupDetailThongBaoDonGiaThueDat')
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
                    $('.groupQuyetDinhThueDat input').val("");
                    if (popup.find(".ddQuyetDinhThueDat option:selected").val() != undefined) {
                        var qd = res.Data[popup.find(".ddQuyetDinhThueDat option:selected").attr("index")];
                        FillFormData('#FormDetailThongBaoDonGiaThueDat', qd);
                        //Get({
                        //    url: localStorage.getItem("API_URL") + '/QuyetDinhThueDat/GetById',
                        //    data: {
                        //        idQuyetDinhThueDat: popup.find(".ddQuyetDinhThueDat option:selected").val()
                        //    },
                        //    callback: function (res) {
                        //        if (res.IsSuccess) {
                        //            FillFormData('#FormDetailThongBaoDonGiaThueDat', res.Data);
                        //        }
                        //    }
                        //});
                    }
                });
                popup.find('.ddQuyetDinhThueDat').trigger('change');
            }
        });
    },

}

$(document).ready(function () {
    ThongBaoDonGiaThueDatControl.Init();
});

