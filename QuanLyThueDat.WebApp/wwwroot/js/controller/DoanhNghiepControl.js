if (typeof (DoanhNghiepControl) == "undefined") DoanhNghiepControl = {};
DoanhNghiepControl = {
    Init: function () {
        DoanhNghiepControl.RegisterEvents();

    },

    LoadDatatable: function (opts) {
        var self = this;
        self.table = SetDataTable({
            table: $('#tbl'),
            url: localStorage.getItem("API_URL") + "/DoanhNghiep/GetAllPaging",
            dom: "rtip",
            data: {
                "requestData": function () {
                    return {
                        "keyword": $('#txtKEY_WORD').val(),
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
                        "data": "TenDoanhNghiep",
                        "defaultContent": "",
                        render: function (data, type, row, meta) {
                            return "<a href='javascript:;' class='doanhnghiep-thongbao' data-id='" + row.IdDoanhNghiep + "'>" + data + "</a>"

                        }
                    },
                    {
                        "class": "name-control",
                        "data": "MaSoThue",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "data": "DiaChi",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        //"data": "thaotac",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                "<a href='javascript:;' class='doanhnghiep-edit' data-id='" + row.IdDoanhNghiep + "'><i class='fas fa-edit' title='Sửa'></i></a>&nbsp" +
                                //"<a href='javascript:;' class='doanhnghiep-thongbao' data-id='" + row.IdDoanhNghiep + "'><i class='fas fa-plus' title='Sửa'></i></a>" +
                                "<a href='javascript:;' class='doanhnghiep-remove text-danger' data-id='" + row.IdDoanhNghiep + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                "</div>";
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {

                $('#tbl tbody .doanhnghiep-edit').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/DoanhNghiep/GetById',
                        data: {
                            idDoanhNghiep: id
                        },
                        callback: function (res) {
                            if (res.IsSuccess) {
                                Get({
                                    url: '/DoanhNghiep/PopupDetailDoanhNghiep',
                                    dataType: 'text',
                                    callback: function (popup) {
                                        $('#modalDetailDoanhNghiep').html(popup);
                                        $('#popupDetailDoanhNghiep').modal();
                                        $('#popupDetailDoanhNghiep .modal-title').text("Chỉnh sửa thông tin doanh nghiệp");
                                        FillFormData('#FormDetailDoanhNghiep', res.Data);
                                        $('.datetimepicker-input').datetimepicker({
                                            format: 'DD/MM/YYYY'
                                        });
                                        $("#btnSaveDoanhNghiep").off('click').on('click', function () {
                                            self.InsertUpdate();
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

                $('#tbl tbody .doanhnghiep-thongbao').off('click').on('click', function (e) {
                    var idDoanhNghiep = $(this).attr('data-id');
                    var tenDoanhNghiep = $(this).parents('tr:first').find("td:nth(1)").text();
                    var opts = {};
                    opts.IdDoanhNghiep = idDoanhNghiep;
                    opts.TenDoanhNghiep = tenDoanhNghiep;
                    Get({
                        url: localStorage.getItem("API_URL") + '/DoanhNghiep/GetById',
                        data: {
                            idDoanhNghiep: idDoanhNghiep
                        },
                        callback: function (res) {
                            if (res.IsSuccess) {
                                Get({
                                    url: '/DoanhNghiep/PopupThongBaoDoanhNghiep',
                                    dataType: 'text',
                                    callback: function (popup) {
                                        $('#modalThongBaoDoanhNghiep').html(popup);
                                        $('#popupThongBaoDoanhNghiep').modal();
                                        $('#popupThongBaoDoanhNghiep .modal-title').text(tenDoanhNghiep);
                                        $("#selectTabQuyetDinhThueDat").off('click').on('click', function () {
                                            $('#tblQuyetDinhThueDat').DataTable().destroy();
                                            self.RegisterEventsQuyetDinhThueDat(opts);
                                        });
                                        $("#selectTabQuyetDinhMienTienThueDat").off('click').on('click', function () {
                                            $('#tblQuyetDinhMienTienThueDat').DataTable().destroy();
                                            self.RegisterEventsQuyetDinhMienTienThueDat(opts);
                                        });
                                        $("#selectTabHopDongThueDat").off('click').on('click', function () {
                                            $('#tblHopDongThueDat').DataTable().destroy();
                                            self.RegisterEventsHopDongThueDat(opts);
                                        });
                                        $("#selectTabThongBaoTienSuDungDat").off('click').on('click', function () {
                                            $('#tblThongBaoTienSuDungDat').DataTable().destroy();
                                            self.RegisterEventsThongBaoTienSuDungDat(opts);
                                        });
                                        $("#selectTabThongBaoDonGiaThueDat").off('click').on('click', function () {
                                            $('#tblThongBaoDonGiaThueDat').DataTable().destroy();
                                            self.RegisterEventsThongBaoDonGiaThueDat(opts);
                                        });
                                        $("#selectTabThongBaoTienThueDat").off('click').on('click', function () {
                                            $('#tblThongBaoTienThueDat').DataTable().destroy();
                                            self.RegisterEventsThongBaoTienThueDat(opts);
                                        });
                                        $('#selectTabQuyetDinhThueDat').trigger('click');
                                    }
                                })
                            }
                        }
                    });
                });

                $("#tbl tbody .doanhnghiep-remove").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/DoanhNghiep/Delete?idDoanhNghiep=" + $y.attr('data-id') + "&Type=1",
                                headers: {
                                    'Authorization': 'Bearer ' + localStorage.getItem("ACCESS_TOKEN")
                                },
                                dataType: 'json',
                                contentType: "application/json-patch+json",
                                type: "Delete",
                                success: function (res) {
                                    if (res.IsSuccess) {
                                        toastr.success('Thực hiện thành công', 'Thông báo')
                                        self.table.ajax.reload();
                                    }
                                    else {
                                        toastr.error(res.Message, 'Có lỗi xảy ra')
                                    }
                                }
                            });
                        }

                    }
                });

            }
        });

    },

    InsertUpdate: function () {
        var self = this;
        var self = this;
        var data = LoadFormData("#FormDetailDoanhNghiep");
        Post({
            "url": localStorage.getItem("API_URL") + "/DoanhNghiep/InsertUpdate",
            "data": data,
            callback: function (res) {
                if (res.IsSuccess) {
                    toastr.success('Thực hiện thành công', 'Thông báo')
                    self.table.ajax.reload();
                    $('#btnCloseDoanhNghiep').trigger('click');
                }
                else {
                    toastr.error(res.Message, 'Có lỗi xảy ra')
                }
            }
        });
    },

    RegisterEvents: function () {
        var self = this;
        self.LoadDatatable();
        $('#btnCreateDoanhNghiep').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/DoanhNghiep/PopupDetailDoanhNghiep',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailDoanhNghiep').html(res);
                    $('#popupDetailDoanhNghiep').modal();
                    $('.datetimepicker-input').datetimepicker({
                        format: 'DD/MM/YYYY'
                    });
                    $("#btnSaveDoanhNghiep").off('click').on('click', function () {
                        self.InsertUpdate();
                    });
                }
            })
        });
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchDoanhNghiep").trigger('click');
            }
        });
        $("#btnSearchDoanhNghiep").off('click').on('click', function () {
            self.table.ajax.reload();

        });
        $('#btnThemMoiHangLoat').off('click').on('click', function () {
            Get({
                url: 'DoanhNghiep/PopupImportDoanhNghiep',
                dataType: 'text',
                callback: function (res) {
                    $('#modalImportDoanhNghiep').html(res);
                    $('#popupImportDoanhNghiep').modal();
                    $('#btnSelectFile').click(function () {
                        $('#fileUpload').trigger("click");
                    });

                    if ($('#fileUpload').length > 0) {
                        $('#fileUpload')[0].value = "";

                        $("#btnSaveFileDoanhNghiep").off('click').on('click', function () {
                            var file = $('#fileUpload')[0].files.length > 0 ? $('#fileUpload')[0].files[0] : null;
                            var data = new FormData();
                            data.append("files", file);
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/DoanhNghiep/ImportDoanhNghiep",
                                type: "POST",
                                headers: {
                                    'Authorization': 'Bearer ' + localStorage.getItem("ACCESS_TOKEN")
                                },
                                cache: false,
                                contentType: false,
                                processData: false,
                                data: data,
                                success: function (res) {
                                    if (res.IsSuccess) {
                                        self.table.ajax.reload();
                                        $('#btnCloseFileDoanhNghiep').trigger('click');
                                        alert(res.Data[0]);
                                    } else {
                                        alert("Có lỗi xảy ra");
                                    }
                                }
                            });
                        });
                        $('#fileUpload').off('change').on('change', function (e) {
                            var file = $('#fileUpload')[0].files.length > 0 ? $('#fileUpload')[0].files[0] : null;
                            var $div = $('#rowFile').html();
                            $('.fileDinhKem').append($div);
                            $('.fileDinhKem').find('.inputFileUpload').last().val(file.name);
                            $('.btn-deleteFile').off('click').on('click', function () {
                                var $y = $(this);
                                $y.parents('.rowFile:first').remove();
                            });
                        });
                    }
                }
            })
        });
    },

    RegisterEventsQuyetDinhThueDat: function (opts) {
        var self = this;
        QuyetDinhThueDatControl.RegisterEvents(opts);
    },
    RegisterEventsQuyetDinhMienTienThueDat: function (opts) {
        var self = this;
        QuyetDinhMienTienThueDatControl.RegisterEvents(opts);
    },
    RegisterEventsHopDongThueDat: function (opts) {
        var self = this;
        HopDongThueDatControl.RegisterEvents(opts);
    },
    RegisterEventsThongBaoTienSuDungDat: function (opts) {
        var self = this;
        ThongBaoTienSuDungDatControl.RegisterEvents(opts);
    },
    RegisterEventsThongBaoDonGiaThueDat: function (opts) {
        var self = this;
        ThongBaoDonGiaThueDatControl.RegisterEvents(opts);
    },
    RegisterEventsThongBaoTienThueDat: function (opts) {
        var self = this;
        ThongBaoTienThueDatControl.RegisterEvents(opts);
    },
}

$(document).ready(function () {
    DoanhNghiepControl.Init();
});

