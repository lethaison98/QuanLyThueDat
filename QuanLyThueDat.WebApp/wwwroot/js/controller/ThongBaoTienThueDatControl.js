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
                        "idDoanhNghiep": idDoanhNghiep,
                        "nam": $('#namThongBao option:selected').val(),
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
                            var tables = $('#tblThongBaoTienThueDat').DataTable();
                            var info = tables.page.info();
                            return info.start + meta.row + 1;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "SoThongBaoTienThueDat",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var file = "";
                            if (row.DsFileTaiLieu != null) {
                                $.each(row.DsFileTaiLieu, function (i, item) {
                                    if (item.LoaiTaiLieu == "ThongBaoTienThueDat") {
                                        file = "<a href = '" + localStorage.getItem('API_URL').replace("api", "") + item.LinkFile + "' target='_blank'><i class = 'fas fa-paperclip' title = 'File thông báo tiền thuê đất'></i></a>";
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
                        "data": "Nam",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "SoTienPhaiNop",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "TextLoaiThongBaoTienThueDat",
                        "defaultContent": "",
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
                                    if (item.LoaiTaiLieu == "ThongBaoTienThueDat") {
                                        file = "<a href = '" + localStorage.getItem('API_URL').replace("api", "") + item.LinkFile + "' target='_blank'><i class = 'fas fa-paperclip' title = 'File thông báo tiền thuê đất'></i></a>";
                                    }
                                });
                            }
                            var show = "";
                            if (!row.QuyenDuLieu.AllowEdit) {
                                show = "style = 'display:none'";
                            }
                            if (opts == undefined) {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    file + "&nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTienThueDat-export' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-file-word' title='Xuất thông báo' ></i></a></div> ";
                                return thaotac;
                            } else {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    file + "&nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTienThueDat-export' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-file-word' title='Xuất thông báo' ></i></a> &nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTienThueDat-edit' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-edit' title='Chỉnh sửa'></i></a> &nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTienThueDat-remove text-danger' " + show + "data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                    "</div>";
                                return thaotac;
                            }

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
                                if (res.Data.LoaiThongBaoTienThueDat != "ThongBaoDieuChinh") {
                                    Get({
                                        url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                                        dataType: 'text',
                                        callback: function (popup) {
                                            $('#modalDetailThongBaoTienThueDat').html(popup);
                                            $('#popupDetailThongBaoTienThueDat').modal();
                                            $('#popupDetailThongBaoTienThueDat .modal-title').text("Chỉnh sửa thông báo tiền thuê đất - " + opts.TenDoanhNghiep);
                                            FillFormData('#FormDetailThongBaoTienThueDat', res.Data);
                                            opts.LoaiThongBaoTienThueDat = res.Data.LoaiThongBaoTienThueDat;
                                            setTimeout(function () {
                                                var checkRole = res.Data.QuyenDuLieu.AllowEdit;
                                                if (!checkRole) {
                                                    $("#popupDetailThongBaoTienThueDat").find('input').attr("disabled", true);
                                                    $("#popupDetailThongBaoTienThueDat").find('select').attr("disabled", true);
                                                    $("#popupDetailThongBaoTienThueDat").find('.fa-trash-alt').hide();
                                                    $("#popupDetailThongBaoTienThueDat").find('.fa-folder').attr("disabled", true);
                                                    $("#popupDetailThongBaoTienThueDat").find('.btn-success').hide();
                                                    $("#popupDetailThongBaoTienThueDat").find('.btn-primary').hide();
                                                } else {
                                                    self.RegisterEventsPopup(opts);
                                                }
                                            }, 200);
                                            if (res.Data.IdQuyetDinhThueDat != null) {
                                                $('#popupDetailThongBaoTienThueDat .ddQuyetDinhThueDat').append('<option value="' + res.Data.IdQuyetDinhThueDat + '">' + res.Data.SoQuyetDinhThueDat + " - " + res.Data.NgayQuyetDinhThueDat + '</option>');
                                            }
                                            if (res.Data.DsThongBaoTienThueDatChiTiet != null) {
                                                $.each(res.Data.DsThongBaoTienThueDatChiTiet, function (i, item) {
                                                    var $td = $("#tempChiTietThongBaoTienThueDat").html();
                                                    $("#tblChiTietThongBaoTienThueDat tbody").append($td);
                                                    self.LoadDanhSachThongBaoDonGiaThueDat(opts);

                                                    $("#tblChiTietThongBaoTienThueDat tbody tr").eq(i).find('[data-name="DienTichPhaiNop"]').val(item.DienTichPhaiNop);
                                                    $("#tblChiTietThongBaoTienThueDat tbody tr").eq(i).find('[data-name="SoTienPhaiNop"]').val(item.SoTienPhaiNop);
                                                    $("#tblChiTietThongBaoTienThueDat tbody tr").eq(i).find('[data-name="TuNgayTinhTien"]').val(item.TuNgayTinhTien);
                                                    $("#tblChiTietThongBaoTienThueDat tbody tr").eq(i).find('[data-name="DenNgayTinhTien"]').val(item.DenNgayTinhTien);
                                                    setTimeout(function () {
                                                        $("#tblChiTietThongBaoTienThueDat tbody tr").eq(i).find('[data-name="IdThongBaoDonGiaThueDat"]').val(item.IdThongBaoDonGiaThueDat);
                                                        $("#tblChiTietThongBaoTienThueDat tbody tr").eq(i).find('[data-name="DonGia"]').val(item.DonGia);
                                                    }, 1000)
                                                    $(".tr-remove").off('click').on('click', function () {
                                                        $(this).parents('tr:first').remove();
                                                    });

                                                });
                                            }
                                            if (res.Data.DsFileTaiLieu != null) {
                                                $.each(res.Data.DsFileTaiLieu, function (i, item) {
                                                    if (item.LoaiTaiLieu == "ThongBaoTienThueDat") {
                                                        $('[data-name="FileThongBaoTienThueDat"]').html('')
                                                        $('[data-name="FileThongBaoTienThueDat"]').append('<a href = "' + localStorage.getItem("API_URL").replace('api', '') + item.LinkFile + '" target="_blank">' + item.TenFile + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                                        $('[data-name="FileThongBaoTienThueDat"]').attr('data-idFile', item.IdFile);
                                                        $('[data-name="FileThongBaoTienThueDat"]').attr('data-id', item.IdFileTaiLieu);
                                                    }
                                                });
                                            }
                                            $('.btn-deleteFile').off('click').on('click', function () {
                                                var $y = $(this);
                                                $y.parent().removeAttr("data-idFile");
                                                $y.parent().html('');

                                            });
                                            $('#popupDetailThongBaoTienThueDat .select2').attr("disabled", true);

                                            //$("#btnTraCuu").on('click', function () {
                                            //    $('#modal-add-edit').modal('show');
                                            //});
                                        }
                                    })
                                } else {
                                    Get({
                                        url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDatDieuChinh',
                                        dataType: 'text',
                                        callback: function (popup) {
                                            $('#modalDetailThongBaoTienThueDatDieuChinh').html(popup);
                                            $('#popupDetailThongBaoTienThueDatDieuChinh').modal();
                                            $('#popupDetailThongBaoTienThueDatDieuChinh .modal-title').text("Chỉnh sửa thông báo tiền thuê đất điều chỉnh - " + opts.TenDoanhNghiep);
                                            FillFormData('#FormDetailThongBaoTienThueDatDieuChinh', res.Data);
                                            opts.LoaiThongBaoTienThueDat = res.Data.LoaiThongBaoTienThueDat;
                                            setTimeout(function () {
                                                var checkRole = res.Data.QuyenDuLieu.AllowEdit;
                                                if (!checkRole) {
                                                    $("#popupDetailThongBaoTienThueDatDieuChinh").find('input').attr("disabled", true);
                                                    $("#popupDetailThongBaoTienThueDatDieuChinh").find('select').attr("disabled", true);
                                                    $("#popupDetailThongBaoTienThueDatDieuChinh").find('.fa-trash-alt').hide();
                                                    $("#popupDetailThongBaoTienThueDatDieuChinh").find('.fa-folder').attr("disabled", true);
                                                    $("#popupDetailThongBaoTienThueDatDieuChinh").find('.btn-success').hide();
                                                    $("#popupDetailThongBaoTienThueDatDieuChinh").find('.btn-primary').hide();
                                                } else {

                                                    self.RegisterEventsPopupThongBaoDieuChinh(opts);
                                                }

                                            }, 200);
                                            if (res.Data.IdQuyetDinhThueDat != null) {
                                                $('#popupDetailThongBaoTienThueDatDieuChinh .ddQuyetDinhThueDat').append('<option value="' + res.Data.IdQuyetDinhThueDat + '">' + res.Data.SoQuyetDinhThueDat + " - " + res.Data.NgayQuyetDinhThueDat + '</option>');
                                            }

                                            if (res.Data.DsThongBaoTienThueDatChiTiet != null) {
                                                $.each(res.Data.DsThongBaoTienThueDatChiTiet, function (i, item) {
                                                    var $td = $("#tempChiTietThongBaoTienThueDatDieuChinh").html();
                                                    $("#tblChiTietThongBaoTienThueDatDieuChinh tbody").append($td);
                                                    self.LoadDanhSachThongBaoTienThueDatDeDieuChinh(opts);

                                                    $("#tblChiTietThongBaoTienThueDatDieuChinh tbody tr").eq(i).find('[data-name="DienTichPhaiNop"]').val(item.DienTichPhaiNop);
                                                    $("#tblChiTietThongBaoTienThueDatDieuChinh tbody tr").eq(i).find('[data-name="SoTienPhaiNop"]').val(item.SoTienPhaiNop);
                                                    $("#tblChiTietThongBaoTienThueDatDieuChinh tbody tr").eq(i).find('[data-name="SoTienMienGiam"]').val(item.SoTienMienGiam);
                                                    $("#tblChiTietThongBaoTienThueDatDieuChinh tbody tr").eq(i).find('[data-name="SoTien"]').val(item.SoTien);
                                                    $("#tblChiTietThongBaoTienThueDatDieuChinh tbody tr").eq(i).find('[data-name="GhiChuChiTiet"]').val(item.GhiChu);
                                                    setTimeout(function () {
                                                        $("#tblChiTietThongBaoTienThueDat tbody tr").eq(i).find('[data-name="IdThongBaoTienThueDatGoc"]').val(item.IdThongBaoTienThueDat);
                                                    }, 1000)
                                                    $(".tr-remove").off('click').on('click', function () {
                                                        $(this).parents('tr:first').remove();
                                                    });

                                                });
                                            }
                                            if (res.Data.DsFileTaiLieu != null) {
                                                $.each(res.Data.DsFileTaiLieu, function (i, item) {
                                                    if (item.LoaiTaiLieu == "ThongBaoTienThueDat") {
                                                        $('[data-name="FileThongBaoTienThueDat"]').html('')
                                                        $('[data-name="FileThongBaoTienThueDat"]').append('<a href = "' + localStorage.getItem("API_URL").replace('api', '') + item.LinkFile + '" target="_blank">' + item.TenFile + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                                        $('[data-name="FileThongBaoTienThueDat"]').attr('data-idFile', item.IdFile);
                                                        $('[data-name="FileThongBaoTienThueDat"]').attr('data-id', item.IdFileTaiLieu);
                                                    }
                                                });
                                            }
                                            $('.btn-deleteFile').off('click').on('click', function () {
                                                var $y = $(this);
                                                $y.parent().removeAttr("data-idFile");
                                                $y.parent().html('');

                                            });
                                            $('#popupDetailThongBaoTienThueDat .select2').attr("disabled", true);

                                            //$("#btnTraCuu").on('click', function () {
                                            //    $('#modal-add-edit').modal('show');
                                            //});
                                        }
                                    })
                                }

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
        var popup = $('#popupDetailThongBaoTienThueDat');
        $('.select2').select2();
        $('.datetimepicker-input').datetimepicker({
            format: 'DD/MM/YYYY'
        });
        $('#btnSelectFileThongBaoTienThueDat').click(function () {
            $('#fileThongBaoTienThueDat').trigger("click");
        });
        if ($('#fileThongBaoTienThueDat').length > 0) {
            $('#fileThongBaoTienThueDat')[0].value = "";
            $('#fileThongBaoTienThueDat').off('change').on('change', function (e) {
                var file = $('#fileThongBaoTienThueDat')[0].files.length > 0 ? $('#fileThongBaoTienThueDat')[0].files[0] : null;
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
                                $('[data-name="FileThongBaoTienThueDat"]').html('')
                                $('[data-name="FileThongBaoTienThueDat"]').append('<a href = "#">' + file.name + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                $('[data-name="FileThongBaoTienThueDat"]').attr('data-idFile', res.Data);
                                $('[data-name="FileThongBaoTienThueDat"]').attr('data-id', 0);
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
        popup.find("[data-name = 'LoaiThongBaoTienThueDat']").val(opts.LoaiThongBaoTienThueDat);
        popup.find('.ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
        $('.groupThongBaoDonGiaThueDat input').change(function () {
            self.TinhToanCongThuc(opts);
        });
        $('.groupThongBaoDonGiaThueDat input').blur(function () {
            self.TinhToanCongThuc(opts);
        });

        $("#btnSaveThongBaoTienThueDat").off('click').on('click', function () {
            self.InsertUpdate(opts);
        });
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
        $("#btnAddChiTietThongBaoTienThueDat").off('click').on('click', function () {
            var $td = $("#tempChiTietThongBaoTienThueDat").html();
            $("#tblChiTietThongBaoTienThueDat tbody").append($td);
            $(".number").change(function () {
                $(this).val(ConvertDecimalToString($(this).val()));
            });
            setTimeout(function () {
                $('.datetimepicker-input').datetimepicker({
                    format: 'DD/MM/YYYY'
                });
            }, 500)
            $(".tr-remove").off('click').on('click', function () {
                $(this).parents('tr:first').remove();
            });
            self.LoadDanhSachThongBaoDonGiaThueDat(opts);
        });
    },
    RegisterEventsPopupThongBaoDieuChinh: function (opts) {
        var self = this;
        var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
        popup.find('.ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
        popup.find("[data-name = 'LoaiThongBaoTienThueDat']").val(opts.LoaiThongBaoTienThueDat);
        $('.select2').select2();
        $('.datetimepicker-input').datetimepicker({
            format: 'DD/MM/YYYY'
        });
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
        $("#btnSaveThongBaoTienThueDatDieuChinh").off('click').on('click', function () {
            self.InsertUpdate(opts);
        });
        $("#btnAddChiTietThongBaoTienThueDatDieuChinh").off('click').on('click', function () {
            var $td = $("#tempChiTietThongBaoTienThueDatDieuChinh").html();
            $("#tblChiTietThongBaoTienThueDatDieuChinh tbody").append($td);
            $(".number").change(function () {
                $(this).val(ConvertDecimalToString($(this).val()));
            });
            $(".tr-remove").off('click').on('click', function () {
                $(this).parents('tr:first').remove();
            });
            self.LoadDanhSachThongBaoTienThueDatDeDieuChinh(opts);
        });
        $('#btnSelectFileThongBaoTienThueDat').click(function () {
            $('#fileThongBaoTienThueDat').trigger("click");
        });
        if ($('#fileThongBaoTienThueDat').length > 0) {
            $('#fileThongBaoTienThueDat')[0].value = "";
            $('#fileThongBaoTienThueDat').off('change').on('change', function (e) {
                var file = $('#fileThongBaoTienThueDat')[0].files.length > 0 ? $('#fileThongBaoTienThueDat')[0].files[0] : null;
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
                                $('[data-name="FileThongBaoTienThueDat"]').html('')
                                $('[data-name="FileThongBaoTienThueDat"]').append('<a href = "#">' + file.name + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                $('[data-name="FileThongBaoTienThueDat"]').attr('data-idFile', res.Data);
                                $('[data-name="FileThongBaoTienThueDat"]').attr('data-id', 0);
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
    },
    RegisterEvents: function (opts) {
        var self = this;
        self.LoadDatatable(opts);
        $('#btnCreateThongBaoLanDau').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTienThueDat').html(res);
                    $('#popupDetailThongBaoTienThueDat').modal();
                    $('#popupDetailThongBaoTienThueDat .modal-title').text("Thêm mới thông báo tiền thuê đất - " + opts.TenDoanhNghiep);
                    opts.LoaiThongBaoTienThueDat = "ThongBaoLanDau";
                    self.RegisterEventsPopup(opts);
                    self.LoadDanhSachQuyetDinhThueDat(opts);
                }
            })
        });
        $('#btnCreateThongBaoTuNamThuHaiTroDi').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTienThueDat').html(res);
                    $('#popupDetailThongBaoTienThueDat').modal();
                    opts.LoaiThongBaoTienThueDat = "ThongBaoTuNamThuHaiTroDi";
                    self.RegisterEventsPopup(opts);
                    self.LoadDanhSachQuyetDinhThueDat(opts);
                }
            })
        });
        $('#btnCreateThongBaoTraTienMotLan').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTienThueDat').html(res);
                    $('#popupDetailThongBaoTienThueDat').modal();
                    $('#popupDetailThongBaoTienThueDat .modal-title').text("Thêm mới thông báo trả tiền một lần - " + opts.TenDoanhNghiep);
                    opts.LoaiThongBaoTienThueDat = "ThongBaoTraTienMotLan";
                    self.RegisterEventsPopup(opts);
                    self.LoadDanhSachQuyetDinhThueDat(opts);
                }
            })
        });
        $('#btnCreateThongBaoTienThueDatDieuChinh').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDatDieuChinh',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTienThueDatDieuChinh').html(res);
                    $('#popupDetailThongBaoTienThueDatDieuChinh').modal();
                    opts.LoaiThongBaoTienThueDat = "ThongBaoDieuChinh";
                    self.RegisterEventsPopupThongBaoDieuChinh(opts);
                    self.LoadDanhSachQuyetDinhThueDat(opts);

                }
            })
        });
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchThongBaoTienThueDat").trigger('click');
            }
        });
        $("#btnSearchThongBaoTienThueDat").off('click').on('click', function () {
            self.table.ajax.reload();

        });
        $("#btnXuatBaoCaoTienThueDatHangNam").off('click').on('click', function (e) {
            if ($('#namThongBao option:selected').val() != "") {
                window.open("/ExportThongBaoTienThueDatHangNam?namThongBao=" + $('#namThongBao option:selected').val());
            } else {
                alert("Vui lòng chọn năm thông báo");
            }
        });


    },
    //TinhToanCongThuc: function (opts) {
    //    if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
    //        var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
    //        var form = '#FormDetailThongBaoTienThueDatDieuChinh';
    //    } else {
    //        var popup = $('#popupDetailThongBaoTienThueDat');
    //        var form = '#FormDetailThongBaoTienThueDat';
    //    };

    //    var tongDienTich = 0;
    //    var donGia = 0;
    //    var dienTichKhongPhaiNop = 0;

    //    if (popup.find(".groupThongBaoDonGiaThueDat [data-name='TongDienTich']").val() != '') {
    //        tongDienTich = ConvertStringToDecimal(popup.find(".groupThongBaoDonGiaThueDat [data-name='TongDienTich']").val());
    //        console.log(popup.find(".groupThongBaoDonGiaThueDat [data-name='TongDienTich']").val());
    //    };
    //    if (popup.find("[data-name='DonGia']").val() != '') {
    //        donGia = ConvertStringToDecimal(popup.find("[data-name='DonGia']").val());
    //    };
    //    if (popup.find("[data-name='DienTichKhongPhaiNop']").val() != '') {
    //        dienTichKhongPhaiNop = ConvertStringToDecimal(popup.find(".groupThongBaoDonGiaThueDat [data-name='DienTichKhongPhaiNop']").val());
    //    };

    //    var tongSoTien = donGia * tongDienTich;
    //    popup.find("[data-name='SoTien']").val(ConvertDecimalToString(tongSoTien.toFixed(0)));


    //    var soTienMienGiam = donGia * dienTichKhongPhaiNop;
    //    popup.find("[data-name='SoTienMienGiam']").val(ConvertDecimalToString(soTienMienGiam.toFixed(0)));

    //    var dienTichPhaiNop = tongDienTich - dienTichKhongPhaiNop;
    //    popup.find("[data-name='DienTichPhaiNop']").val(ConvertDecimalToString(dienTichPhaiNop.toFixed(2).toString().replace(".", ",")));

    //    var soTienPhaiNop = tongSoTien - soTienMienGiam;
    //    popup.find("[data-name='SoTienPhaiNop']").val(ConvertDecimalToString(soTienPhaiNop.toFixed(0)));

    //},
    TinhToanCongThuc: function (opts) {
        var tongDienTich = 0;
        var dienTichKhongPhaiNop = 0;
        var tongSoTien = 0
        var soTienMienGiam = 0

        if (opts.LoaiThongBaoTienThueDat == "ThongBaoTraTienMotLan") {
            $("#tblChiTietThongBaoTienThueDat tbody tr").each(function () {
                var donGia = ConvertStringToDecimal($(this).find("[data-name='DonGia']").val());
                var dienTichPhaiNop = ConvertStringToDecimal($(this).find("[data-name='DienTichPhaiNop']").val());
                var soTienPhaiNop = Math.round(donGia * dienTichPhaiNop) || 0;
                tongSoTien += soTienPhaiNop;
                $(this).find('[data-name="SoTienPhaiNop"]').val(ConvertDecimalToString(soTienPhaiNop.toFixed(0)));
            });
        } else {
            $("#tblChiTietThongBaoTienThueDat tbody tr").each(function () {
                var tuNgay = $(this).find('[data-name="TuNgayTinhTien"]').val();
                var denNgay = $(this).find('[data-name="DenNgayTinhTien"]').val();
                var donGia = ConvertStringToDecimal($(this).find("[data-name='DonGia']").val());
                var dienTichPhaiNop = ConvertStringToDecimal($(this).find("[data-name='DienTichPhaiNop']").val());

                var soThang = CalculateMonthBetweenDays(tuNgay, denNgay);
                var soTienPhaiNop = Math.round(donGia * dienTichPhaiNop * soThang / 12) || 0;
                tongSoTien += soTienPhaiNop;
                $(this).find('[data-name="SoTienPhaiNop"]').val(ConvertDecimalToString(soTienPhaiNop.toFixed(0)));
            });
        }
        tongDienTich = ConvertStringToDecimal($(".groupThongBaoTienThueDat").find("[data-name='TongDienTich']").val());
        dienTichKhongPhaiNop = ConvertStringToDecimal($(".groupThongBaoTienThueDat").find("[data-name='DienTichKhongPhaiNop']").val());

        $(".groupThongBaoTienThueDat").find("[data-name='SoTien']").val(ConvertDecimalToString(tongSoTien.toFixed(0)));
        soTienMienGiam = ConvertStringToDecimal($(".groupThongBaoTienThueDat").find("[data-name='SoTienMienGiam']").val()) || 0;
        var dienTichPhaiNop = (tongDienTich - dienTichKhongPhaiNop) || 0;
        $(".groupThongBaoTienThueDat").find("[data-name='DienTichPhaiNop']").val(ConvertDecimalToString(dienTichPhaiNop.toFixed(2).toString().replace(".", ",")));

        var soTienPhaiNop = tongSoTien - soTienMienGiam;
        $(".groupThongBaoTienThueDat").find("[data-name='SoTienPhaiNop']").val(ConvertDecimalToString(soTienPhaiNop.toFixed(0)));


    },

    InsertUpdate: function (opts) {
        var self = this;
        var data = {};
        $(".number").each(function () {
            $(this).val(ConvertStringToDecimal($(this).val()));
        });
        if (opts != undefined) {
            if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
                var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
                var data = LoadFormData("#FormDetailThongBaoTienThueDatDieuChinh");
                data.IdDoanhNghiep = popup.find(".ddDoanhNghiep option:selected").val();
                data.IdQuyetDinhThueDat = popup.find(".ddQuyetDinhThueDat option:selected").val();

                var arrayRow = $("#tblChiTietThongBaoTienThueDatDieuChinh tbody tr");
                var thongBaoChiTiet = [];
                $.each(arrayRow, function (i, item) {
                    var ct = $(item).find('[data-name="SoTien"]').val();
                    if (ct != undefined && ct != "") {
                        thongBaoChiTiet.push({
                            IdThongBaoTienThueDatGoc: $(item).find(".ddThongBaoTienThueDatGoc option:selected").val(),
                            SoTien: $(item).find('[data-name="SoTien"]').val(),
                            SoTienPhaiNop: $(item).find('[data-name="SoTienPhaiNop"]').val(),
                            SoTienMienGiam: $(item).find('[data-name="SoTienMienGiam"]').val(),
                            GhiChu: $(item).find('[data-name="GhiChuChiTiet"]').val()
                        });
                    }
                });
                data.ThongBaoTienThueDatChiTiet = thongBaoChiTiet;
            } else {
                var popup = $('#popupDetailThongBaoTienThueDat');
                var data = LoadFormData("#FormDetailThongBaoTienThueDat");
                data.IdDoanhNghiep = popup.find(".ddDoanhNghiep option:selected").val();
                data.IdQuyetDinhThueDat = popup.find(".ddQuyetDinhThueDat option:selected").val();
                var arrayRow = $("#tblChiTietThongBaoTienThueDat tbody tr");
                var thongBaoChiTiet = [];
                $.each(arrayRow, function (i, item) {
                    var ct = $(item).find('[data-name="DonGia"]').val();
                    if (ct != undefined && ct != "") {
                        thongBaoChiTiet.push({
                            IdThongBaoDonGiaThueDat: $(item).find(".ddThongBaoDonGiaThueDat option:selected").val(),
                            DonGia: $(item).find('[data-name="DonGia"]').val(),
                            DienTichPhaiNop: $(item).find('[data-name="DienTichPhaiNop"]').val(),
                            SoTienPhaiNop: $(item).find('[data-name="SoTienPhaiNop"]').val(),
                            TuNgayTinhTien: $(item).find('[data-name="TuNgayTinhTien"]').val(),
                            DenNgayTinhTien: $(item).find('[data-name="DenNgayTinhTien"]').val()
                        });
                    }
                });
                data.ThongBaoTienThueDatChiTiet = thongBaoChiTiet;
            };
        } else {
            var popup = $('#popupDetailThongBaoTienThueDat');
            var data = LoadFormData("#FormDetailThongBaoTienThueDat");
            data.IdDoanhNghiep = popup.find(".ddDoanhNghiep option:selected").val();
        }
        var fileTaiLieu = [];
        if ($('[data-name="FileThongBaoTienThueDat"]').attr("data-idFile") != undefined) {
            fileTaiLieu.push({
                IdFileTaiLieu: $('[data-name="FileThongBaoTienThueDat"]').attr("data-id"),
                IdFile: $('[data-name="FileThongBaoTienThueDat"]').attr("data-idFile"),
                LoaiTaiLieu: "ThongBaoTienThueDat"
            });
        }
        data.FileTaiLieu = fileTaiLieu;
        Post({
            "url": localStorage.getItem("API_URL") + "/ThongBaoTienThueDat/InsertUpdate",
            "data": data,
            callback: function (res) {
                if (res.IsSuccess) {
                    toastr.success('Thực hiện thành công', 'Thông báo')
                    self.table.ajax.reload();
                    $('#btnCloseThongBaoTienThueDat').trigger('click');
                }
                else {
                    toastr.error(res.Message, 'Có lỗi xảy ra')
                }
            }
        });
    },
    LoadDanhSachDoanhNghiep: function (opts) {
        var self = this;
        if (opts != undefined) {
            if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
                var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
            } else {
                var popup = $('#popupDetailThongBaoTienThueDat');
            };
        } else {
            var popup = $('#popupDetailThongBaoTienThueDat');
        }
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
                        self.LoadDanhSachQuyetDinhThueDat(opts);
                    }
                });
                //$('.ddDoanhNghiep').trigger('change');

            }
        });
    },
    LoadDanhSachQuyetDinhThueDat: function (opts) {
        var self = this;
        if (opts != undefined) {
            if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
                var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
                var form = '#FormDetailThongBaoTienThueDatDieuChinh';
            } else {
                var popup = $('#popupDetailThongBaoTienThueDat');
                var form = '#FormDetailThongBaoTienThueDat';
            };
        } else {
            var popup = $('#popupDetailThongBaoTienThueDat');
            var form = '#FormDetailThongBaoTienThueDat';
        }
        Get({
            url: localStorage.getItem("API_URL") + "/QuyetDinhThueDat/GetListQuyetDinhThueDatChiTiet",
            data: {
                idDoanhNghiep: opts.IdDoanhNghiep
            },
            showLoading: true,
            callback: function (res) {
                popup.find('.ddQuyetDinhThueDat').html('');
                popup.find('.ddQuyetDinhThueDat').append('<option value= "" selected="true" style="display: none"></option>');
                $.each(res.Data, function (i, item) {
                    var name = item.SoQuyetDinhThueDat + " - " + item.TextHinhThucThue + " - Diện tích " + item.TongDienTich + "  (m<sup>2</sup>)"
                    popup.find('.ddQuyetDinhThueDat').append('<option value=' + item.IdQuyetDinhThueDat + ' index= ' + i + '>' + name + '</option>');
                })
                popup.find('.ddQuyetDinhThueDat').on('change', function () {
                    $('.groupQuyetDinhThueDat input').val("");
                    if (popup.find(".ddQuyetDinhThueDat option:selected").val() != undefined && popup.find(".ddQuyetDinhThueDat option:selected").val() != "") {
                        var qd = res.Data[popup.find(".ddQuyetDinhThueDat option:selected").attr("index")];
                        FillFormData(form, qd);
                        //    self.LoadDanhSachThongBaoDonGiaThueDat(opts);
                    } else {
                        $('.groupQuyetDinhThueDat input').val("");
                    }
                });
                popup.find('.ddQuyetDinhThueDat').trigger('change');
            }
        });
    },
    LoadDanhSachThongBaoDonGiaThueDat: function (opts) {
        var self = this;
        var $lastrow = $('#tblChiTietThongBaoTienThueDat tbody tr:last');
        if (opts != undefined) {
            if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
                var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
                var form = '#FormDetailThongBaoTienThueDatDieuChinh';
            } else {
                var popup = $('#popupDetailThongBaoTienThueDat');
                var form = '#FormDetailThongBaoTienThueDat';
            };
        } else {
            var popup = $('#popupDetailThongBaoTienThueDat');
        }
        var Data = {
            IdQuyetDinhThueDat: popup.find(".ddQuyetDinhThueDat option:selected").val(),
            IdDoanhNghiep: popup.find(".ddDoanhNghiep option:selected").val()
        };
        Post({
            url: localStorage.getItem("API_URL") + "/ThongBaoDonGiaThueDat/GetAllByRequest",
            data: Data,
            showLoading: true,
            callback: function (res) {
                $lastrow.find('.ddThongBaoDonGiaThueDat').html('');
                $.each(res.Data, function (i, item) {
                    $lastrow.find('.ddThongBaoDonGiaThueDat').append('<option value="' + item.IdThongBaoDonGiaThueDat + '">' + item.SoThongBaoDonGiaThueDat + '</option>');
                })
                $lastrow.find('.ddThongBaoDonGiaThueDat').on('change', function () {
                    if ($lastrow.find(".ddThongBaoDonGiaThueDat option:selected").val() != 0 && $lastrow.find(".ddThongBaoDonGiaThueDat option:selected").val() != undefined) {
                        var $y = $(this).parent().parent();
                        Get({
                            url: localStorage.getItem("API_URL") + '/ThongBaoDonGiaThueDat/GetById',
                            data: {
                                idThongBaoDonGiaThueDat: $y.find(".ddThongBaoDonGiaThueDat option:selected").val()
                            },
                            callback: function (res) {
                                if (res.IsSuccess) {
                                    $y.find('[data-name="DonGia"]').val(res.Data.DonGia);
                                    $y.find('[data-name="DienTichPhaiNop"]').val(res.Data.DienTichPhaiNop);
                                    popup.find('input').blur(function () {
                                        self.TinhToanCongThuc(opts);
                                    });
                                }
                            }
                        });
                    } else {
                        $('.groupThongBaoDonGiaThueDat input').val("");
                    }
                });
                $lastrow.find('.ddThongBaoDonGiaThueDat').trigger('change');
            }
        });
    },
    LoadDanhSachThongBaoTienThueDatDeDieuChinh: function (opts) {
        var self = this;
        var $lastrow = $('#tblChiTietThongBaoTienThueDatDieuChinh tbody tr:last');
        if (opts != undefined) {
            if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
                var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
                var form = '#FormDetailThongBaoTienThueDatDieuChinh';
            } else {
                var popup = $('#popupDetailThongBaoTienThueDat');
                var form = '#FormDetailThongBaoTienThueDat';
            };
        } else {
            var popup = $('#popupDetailThongBaoTienThueDat');
        }
        var Data = {
            IdQuyetDinhThueDat: popup.find(".ddQuyetDinhThueDat option:selected").val(),
            IdDoanhNghiep: popup.find(".ddDoanhNghiep option:selected").val()
        };
        Post({
            url: localStorage.getItem("API_URL") + "/ThongBaoTienThueDat/GetAllByRequest",
            data: Data,
            showLoading: true,
            callback: function (res) {
                $lastrow.find('.ddThongBaoTienThueDatGoc').html('');
                $.each(res.Data, function (i, item) {
                    $lastrow.find('.ddThongBaoTienThueDatGoc').append('<option value="' + item.IdThongBaoTienThueDat + '">' + item.SoThongBaoTienThueDat + " ngày " + item.NgayThongBaoTienThueDat + '</option>');
                })
                $lastrow.find('.ddThongBaoTienThueDatGoc').on('change', function () {
                    if ($lastrow.find(".ddThongBaoTienThueDatGoc option:selected").val() != 0 && $lastrow.find(".ddThongBaoTienThueDatGoc option:selected").val() != undefined) {
                        var $y = $(this).parent().parent();
                        Get({
                            url: localStorage.getItem("API_URL") + '/ThongBaoTienThueDat/GetById',
                            data: {
                                idThongBaoTienThueDat: $y.find(".ddThongBaoTienThueDatGoc option:selected").val()
                            },
                            callback: function (res) {
                                if (res.IsSuccess) {
                                    $y.find('[data-name="SoTien"]').val(res.Data.SoTienPhaiNop);
                                    popup.find('input').blur(function () {
                                        self.TinhToanCongThuc(opts);
                                    });
                                }
                            }
                        });
                    } else {
                        $('.groupThongBaoDonGiaThueDat input').val("");
                    }
                });
                $lastrow.find('.ddThongBaoTienThueDatGoc').trigger('change');
            }
        });
    },

}

$(document).ready(function () {
    ThongBaoTienThueDatControl.Init();
});
