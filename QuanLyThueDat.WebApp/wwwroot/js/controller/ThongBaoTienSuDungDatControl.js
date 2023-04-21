if (typeof (ThongBaoTienSuDungDatControl) == "undefined") ThongBaoTienSuDungDatControl = {};
ThongBaoTienSuDungDatControl = {
    Init: function () {
        ThongBaoTienSuDungDatControl.RegisterEvents();

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
            table: $('#tblThongBaoTienSuDungDat'),
            url: localStorage.getItem("API_URL") + "/ThongBaoTienSuDungDat/GetAllPaging",
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
                            var tables = $('#tblThongBaoTienSuDungDat').DataTable();
                            var info = tables.page.info();
                            return info.start + meta.row + 1;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "SoThongBaoTienSuDungDat",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var file = "";
                            if (row.DsFileTaiLieu != null) {
                                $.each(row.DsFileTaiLieu, function (i, item) {
                                    if (item.LoaiTaiLieu == "ThongBaoTienSuDungDat") {
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
                        "data": "NgayThongBaoTienSuDungDat",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "DienTichPhaiNop",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "DonGia",
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
                            var file = "";
                            if (row.DsFileTaiLieu != null) {
                                $.each(row.DsFileTaiLieu, function (i, item) {
                                    if (item.LoaiTaiLieu == "ThongBaoTienSuDungDat") {
                                        file = "<a href = '" + localStorage.getItem('API_URL').replace("api", "") + item.LinkFile + "' target='_blank'><i class = 'fas fa-paperclip' title = 'File thông báo đơn giá thuê đất'></i></a>";
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
                                    "<a href='javascript:;' class='ThongBaoTienSuDungDat-export' data-id='" + row.IdThongBaoTienSuDungDat + "'><i class='fas fa-file-word' title='Xuất thông báo' ></i></a> &nbsp" +
                                    "</div>";
                                return thaotac;
                            } else {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    file + "&nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTienSuDungDat-export' data-id='" + row.IdThongBaoTienSuDungDat + "'><i class='fas fa-file-word' title='Xuất thông báo' ></i></a> &nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTienSuDungDat-edit' data-id='" + row.IdThongBaoTienSuDungDat + "'><i class='fas fa-edit' title='Chỉnh sửa'></i></a>" +
                                    "<a href='javascript:;' class='ThongBaoTienSuDungDat-remove text-danger' " + show + "data-id='" + row.IdThongBaoTienSuDungDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                    "</div>";
                                return thaotac;
                            }
                        }
                    }
                ]
            },
            callback: function () {
                $("#tblThongBaoTienSuDungDat tbody .ThongBaoTienSuDungDat-export").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        window.open("/CreateWordFile?idThongBao=" + id + "&loaiThongBao=ThongBaoTienSuDungDatLanDau", "_blank");
                    }
                });
                $('#tblThongBaoTienSuDungDat tbody .ThongBaoTienSuDungDat-edit').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/ThongBaoTienSuDungDat/GetById',
                        data: {
                            idThongBaoTienSuDungDat: id
                        },
                        callback: function (res) {
                            if (res.IsSuccess) {
                                Get({
                                    url: '/ThongBaoTienSuDungDat/PopupDetailThongBaoTienSuDungDat',
                                    dataType: 'text',
                                    callback: function (popup) {
                                        $('#modalDetailThongBaoTienSuDungDat').html(popup);
                                        $('#popupDetailThongBaoTienSuDungDat').modal();
                                        $('#popupDetailThongBaoTienSuDungDat .modal-title').text("Chỉnh sửa thông báo đơn giá thuê đất - " + opts.TenDoanhNghiep);

                                        FillFormData('#FormDetailThongBaoTienSuDungDat', res.Data);
                                        var popup = $('#popupDetailThongBaoTienSuDungDat');

                                        if (opts != undefined) {
                                            popup.find('.ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                                        } else {
                                            $('#popupDetailThongBaoTienSuDungDat .ddDoanhNghiep').append('<option value="' + res.Data.IdDoanhNghiep + '">' + res.Data.TenDoanhNghiep + '</option>');
                                        }
                                        setTimeout(function () {
                                            var checkRole = res.Data.QuyenDuLieu.AllowEdit;
                                            if (!checkRole) {
                                                $("#popupDetailThongBaoTienSuDungDat").find('input').attr("disabled", true);
                                                $("#popupDetailThongBaoTienSuDungDat").find('select').attr("disabled", true);
                                                $("#popupDetailThongBaoTienSuDungDat").find('.fa-trash-alt').hide();
                                                $("#popupDetailThongBaoTienSuDungDat").find('.fa-folder').attr("disabled", true);
                                                $("#popupDetailThongBaoTienSuDungDat").find('.btn-success').hide();
                                                $("#popupDetailThongBaoTienSuDungDat").find('.btn-primary').hide();
                                            } else {
                                                self.RegisterEventsPopup();
                                            }
                                        }, 200)
                                        if (res.Data.DsFileTaiLieu != null) {
                                            $.each(res.Data.DsFileTaiLieu, function (i, item) {
                                                if (item.LoaiTaiLieu == "ThongBaoTienSuDungDat") {
                                                    $('[data-name="FileThongBaoTienSuDungDat"]').html('')
                                                    $('[data-name="FileThongBaoTienSuDungDat"]').append('<a href = "' + localStorage.getItem("API_URL").replace('api', '') + item.LinkFile + '" target="_blank">' + item.TenFile + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                                    $('[data-name="FileThongBaoTienSuDungDat"]').attr('data-idFile', item.IdFile);
                                                    $('[data-name="FileThongBaoTienSuDungDat"]').attr('data-id', item.IdFileTaiLieu);
                                                }
                                            });
                                        }
                                        $('.btn-deleteFile').off('click').on('click', function () {
                                            var $y = $(this);
                                            $y.parent().removeAttr("data-idFile");
                                            $y.parent().html('');

                                        });

                                        $('#popupDetailThongBaoTienSuDungDat .select2').attr("disabled", true);

                                    }
                                })
                            }
                        }
                    });
                });

                $("#tblThongBaoTienSuDungDat tbody .ThongBaoTienSuDungDat-remove").off('click').on('click', function (e) {

                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/ThongBaoTienSuDungDat/Delete?idThongBaoTienSuDungDat=" + $y.attr('data-id') + "&Type=1",
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
        $('.select2').select2();
        $('.datetimepicker-input').datetimepicker({
            format: 'DD/MM/YYYY'
        });
        $('#btnSelectFileThongBaoTienSuDungDat').click(function () {
            $('#fileThongBaoTienSuDungDat').trigger("click");
        });
        if ($('#fileThongBaoTienSuDungDat').length > 0) {
            $('#fileThongBaoTienSuDungDat')[0].value = "";
            $('#fileThongBaoTienSuDungDat').off('change').on('change', function (e) {
                var file = $('#fileThongBaoTienSuDungDat')[0].files.length > 0 ? $('#fileThongBaoTienSuDungDat')[0].files[0] : null;
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
                                $('[data-name="FileThongBaoTienSuDungDat"]').html('')
                                $('[data-name="FileThongBaoTienSuDungDat"]').append('<a href = "#">' + file.name + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                $('[data-name="FileThongBaoTienSuDungDat"]').attr('data-idFile', res.Data);
                                $('[data-name="FileThongBaoTienSuDungDat"]').attr('data-id', 0);
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
        $('.groupThongBaoTienSuDungDat input').change(function () {
            self.TinhToanCongThuc();
        });
        $('.groupThongBaoTienSuDungDat input').blur(function () {
            self.TinhToanCongThuc();
        });
        $("#btnSaveThongBaoTienSuDungDat").off('click').on('click', function () {
            self.InsertUpdate();
        });
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
    },

    RegisterEvents: function (opts) {
        var self = this;
        self.LoadDatatable(opts);
        $('#btnCreateThongBaoTienSuDungDat').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienSuDungDat/PopupDetailThongBaoTienSuDungDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTienSuDungDat').html(res);
                    $('#popupDetailThongBaoTienSuDungDat').modal();
                    $('#popupDetailThongBaoTienSuDungDat .modal-title').text("Thêm mới thông báo đơn giá thuê đất - " + opts.TenDoanhNghiep);

                    var popup = $('#popupDetailThongBaoTienSuDungDat');
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
                $("#btnSearchThongBaoTienSuDungDat").trigger('click');
            }
        });
        $("#btnSearchThongBaoTienSuDungDat").off('click').on('click', function () {
            self.table.ajax.reload();

        });

    },
    InsertUpdate: function () {
        var self = this;
        var popup = $('#popupDetailThongBaoTienSuDungDat');
        $(".number").each(function () {
            $(this).val(ConvertStringToDecimal($(this).val()));
        });
        var data = LoadFormData("#FormDetailThongBaoTienSuDungDat");
        data.IdDoanhNghiep = popup.find(".ddDoanhNghiep option:selected").val();
        var fileTaiLieu = [];
        if ($('[data-name="FileThongBaoTienSuDungDat"]').attr("data-idFile") != undefined) {
            fileTaiLieu.push({
                IdFileTaiLieu: $('[data-name="FileThongBaoTienSuDungDat"]').attr("data-id"),
                IdFile: $('[data-name="FileThongBaoTienSuDungDat"]').attr("data-idFile"),
                LoaiTaiLieu: "ThongBaoTienSuDungDat"
            });
        }
        data.FileTaiLieu = fileTaiLieu;
        Post({
            "url": localStorage.getItem("API_URL") + "/ThongBaoTienSuDungDat/InsertUpdate",
            "data": data,
            callback: function (res) {
                if (res.IsSuccess) {
                    toastr.success('Thực hiện thành công', 'Thông báo')
                    self.table.ajax.reload();
                    $('#btnCloseThongBaoTienSuDungDat').trigger('click');
                }
                else {
                    toastr.error(res.Message, 'Có lỗi xảy ra')
                }

            }
        });
    },
    LoadDanhSachDoanhNghiep: function () {
        var self = this;
        var popup = $('#popupDetailThongBaoTienSuDungDat')
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
        var popup = $('#popupDetailThongBaoTienSuDungDat')
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
                    var name = item.SoQuyetDinhThueDat + " - " + item.TextHinhThucThue + " - Diện tích " + item.TongDienTich + "  (m<sup>2</sup>)"
                    popup.find('.ddQuyetDinhThueDat').append('<option value=' + i + '>' + name + '</option>');
                })
                popup.find('.ddQuyetDinhThueDat').on('change', function () {
                    $('.groupQuyetDinhThueDat input').val("");
                    if (popup.find(".ddQuyetDinhThueDat option:selected").val() != undefined) {
                        var qd = res.Data[popup.find(".ddQuyetDinhThueDat option:selected").val()];
                        FillFormData('#FormDetailThongBaoTienSuDungDat', qd);
                        //Get({
                        //    url: localStorage.getItem("API_URL") + '/QuyetDinhThueDat/GetById',
                        //    data: {
                        //        idQuyetDinhThueDat: popup.find(".ddQuyetDinhThueDat option:selected").val()
                        //    },
                        //    callback: function (res) {
                        //        if (res.IsSuccess) {
                        //            FillFormData('#FormDetailThongBaoTienSuDungDat', res.Data);
                        //        }
                        //    }
                        //});
                    }
                });
                popup.find('.ddQuyetDinhThueDat').trigger('change');
            }
        });
    },
    TinhToanCongThuc: function () {
        var popup = $('#popupDetailThongBaoTienSuDungDat');
        var tongDienTich = 0;
        var donGia = 0;
        var soTienMienGiam = 0;
        var soTienBoiThuongGiaiPhongMatBang = 0

        if (popup.find("[data-name='DienTichPhaiNop']").val() != '') {
            tongDienTich = ConvertStringToDecimal(popup.find(".groupThongBaoTienSuDungDat [data-name='DienTichPhaiNop']").val());
        };
        if (popup.find("[data-name='DonGia']").val() != '') {
            donGia = ConvertStringToDecimal(popup.find("[data-name='DonGia']").val());
        };
        if (popup.find("[data-name='SoTienMienGiam']").val() != '') {
            soTienMienGiam = ConvertStringToDecimal(popup.find("[data-name='SoTienMienGiam']").val());
        };
        if (popup.find("[data-name='SoTienBoiThuongGiaiPhongMatBang']").val() != '') {
            soTienBoiThuongGiaiPhongMatBang = ConvertStringToDecimal(popup.find("[data-name='SoTienBoiThuongGiaiPhongMatBang']").val());
        };

        soTien = donGia * tongDienTich;
        popup.find("[data-name='SoTien']").val(ConvertDecimalToString(soTien.toFixed(0)));

        soTienPhaiNop = soTien - soTienMienGiam - soTienBoiThuongGiaiPhongMatBang;
        popup.find("[data-name='SoTienPhaiNop']").val(ConvertDecimalToString(soTienPhaiNop.toFixed(0)));

    },
}

$(document).ready(function () {
    ThongBaoTienSuDungDatControl.Init();
});

