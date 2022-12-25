if (typeof (QuyetDinhThueDatControl) == "undefined") QuyetDinhThueDatControl = {};
QuyetDinhThueDatControl = {
    Init: function () {
        QuyetDinhThueDatControl.RegisterEvents();

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
            table: $('#tblQuyetDinhThueDat'),
            url: localStorage.getItem("API_URL") + "/QuyetDinhThueDat/GetAllPaging",
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
                        "width": "5%",
                        "data": "RN",
                        "defaultContent": "1",
                        render: function (data, type, row, meta) {
                            return meta.row + 1;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "TenDoanhNghiep",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "width": "10%",
                        "data": "SoQuyetDinhGiaoDat",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "width": "10%",
                        "data": "SoQuyetDinhThueDat",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "width": "30%",
                        "data": "ViTriThuaDat",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "width": "10%",
                        "data": "NgayQuyetDinhThueDat",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "width": "10%",
                        "orderable": false,
                        //"data": "thaotac",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var fileQDThueDat = "";
                            var fileQDGiaoDat = "";
                            if (row.DsFileTaiLieu != null) {
                                $.each(row.DsFileTaiLieu, function (i, item) {
                                    if (item.LoaiTaiLieu == "QuyetDinhThueDat") {
                                        fileQDThueDat = "<a href = '" + localStorage.getItem('API_URL').replace("api", "") + item.LinkFile + "' target='_blank'><i class = 'fas fa-file-pdf' title = 'File quyết định thuê đất'></i></a>";                                    }
                                    if (item.LoaiTaiLieu == "QuyetDinhGiaoDat") {
                                        fileQDGiaoDat = "<a href = '" + localStorage.getItem('API_URL').replace("api", "") + item.LinkFile + "' target='_blank'><i class = 'fas fa-file-pdf' style = 'color: gold' title = 'File quyết định giao đất'></i></a>";                                       }
                                });
                            }
                            if (opts == undefined) {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    fileQDGiaoDat + "&nbsp" + fileQDThueDat + "&nbsp" +
                                    "<a href='javascript:;' class='QuyetDinhThueDat-view' data-id='" + row.IdQuyetDinhThueDat + "'><i class='fas fa-eye' title='Xem'></i></a> &nbsp" +
                                    "</div>";
                                return thaotac;
                            } else {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    fileQDGiaoDat + "&nbsp" + fileQDThueDat + "&nbsp" +
                                    "<a href='javascript:;' class='QuyetDinhThueDat-view' data-id='" + row.IdQuyetDinhThueDat + "'><i class='fas fa-eye' title='Xem'></i></a> &nbsp" +
                                    "<a href='javascript:;' class='QuyetDinhThueDat-edit' data-id='" + row.IdQuyetDinhThueDat + "'><i class='fas fa-edit' title='Sửa'></i></a> &nbsp" +
                                    "<a href='javascript:;' class='QuyetDinhThueDat-remove text-danger' data-id='" + row.IdQuyetDinhThueDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                    "</div>";
                                return thaotac;
                            }
                        }
                    }
                ]
            },
            callback: function () {
                $("#tblQuyetDinhThueDat tbody .QuyetDinhThueDat-view").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/QuyetDinhThueDat/GetById',
                        data: {
                            idQuyetDinhThueDat: id
                        },
                        callback: function (res) {
                            Get({
                                url: '/QuyetDinhThueDat/PopupViewQuyetDinhThueDat',
                                dataType: 'text',
                                callback: function (popup) {
                                    $('#modalViewQuyetDinhThueDat').html(popup);
                                    $('#popupViewQuyetDinhThueDat').modal();
                                    var data = [];
                                    for (var i = 0; i < res.Data.DsQuyetDinhThueDatChiTiet.length; i++) {
                                        var obj = {
                                            TenDoanhNghiep : res.Data.TenDoanhNghiep,
                                            SoQuyetDinhThueDat : res.Data.SoQuyetDinhThueDat,
                                            NgayQuyetDinhThueDat : res.Data.NgayQuyetDinhThueDat,
                                            SoQuyetDinhGiaoDat : res.Data.SoQuyetDinhGiaoDat,
                                            ViTriThuaDat : res.Data.ViTriThuaDat,
                                            DiaChiThuaDat : res.Data.DiaChiThuaDat,
                                            TextHinhThucThue: res.Data.DsQuyetDinhThueDatChiTiet[i].TextHinhThucThue,
                                            DienTich : res.Data.DsQuyetDinhThueDatChiTiet[i].DienTich,
                                            MucDichSuDung : res.Data.DsQuyetDinhThueDatChiTiet[i].MucDichSuDung,
                                            ThoiHanThue : res.Data.DsQuyetDinhThueDatChiTiet[i].ThoiHanThue,
                                            TuNgayThue : res.Data.DsQuyetDinhThueDatChiTiet[i].TuNgayThue,
                                            DenNgayThue : res.Data.DsQuyetDinhThueDatChiTiet[i].DenNgayThue,
                                        };
                                        data.push(obj);
                                    }
                                    $('#tblViewQuyetDinhThueDat').DataTable({
                                        data: data,
                                        dom: 'B',
                                        buttons: [
                                            'print'
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
                                                data: 'SoQuyetDinhThueDat'
                                            },
                                            {
                                                data: 'NgayQuyetDinhThueDat'
                                            },
                                            {
                                                data: 'SoQuyetDinhGiaoDat'
                                            },
                                            {
                                                data: 'ViTriThuaDat',
                                            },
                                            {
                                                data: 'DiaChiThuaDat',
                                            },
                                            {
                                                data: 'TextHinhThucThue',
                                            },
                                            {
                                                data: 'DienTich',
                                            },
                                            {
                                                data: 'MucDichSuDung',
                                            },
                                            {
                                                data: 'ThoiHanThue',
                                            },
                                            {
                                                data: 'TuNgayThue',
                                            },
                                            {
                                                data: 'DenNgayThue',
                                            },

                                        ]
                                    });
                                }
                            });
                        }
                    });
                });
                $('#tblQuyetDinhThueDat tbody .QuyetDinhThueDat-edit').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/QuyetDinhThueDat/GetById',
                        data: {
                            idQuyetDinhThueDat: id
                        },
                        callback: function (res) {
                            if (res.IsSuccess) {
                                Get({
                                    url: '/QuyetDinhThueDat/PopupDetailQuyetDinhThueDat',
                                    dataType: 'text',
                                    callback: function (popup) {
                                        $('#modalDetailQuyetDinhThueDat').html(popup);
                                        $('#popupDetailQuyetDinhThueDat').modal();
                                        FillFormData('#FormDetailQuyetDinhThueDat', res.Data);
                                        if (opts != undefined) {
                                            $('#popupDetailQuyetDinhThueDat .ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                                        } else {
                                            $('#popupDetailQuyetDinhThueDat .ddDoanhNghiep').append('<option value="' + res.Data.IdDoanhNghiep + '">' + res.Data.TenDoanhNghiep + '</option>');
                                        }
                                        self.RegisterEventsPopup();
                                        if (res.Data.DsQuyetDinhThueDatChiTiet != null) {
                                            $.each(res.Data.DsQuyetDinhThueDatChiTiet, function (i, item) {
                                                var $td = $("#tempChiTietQuyetDinhThueDat").html();
                                                $("#tblChiTietQuyetDinhThueDat tbody").append($td);

                                                $("#tblChiTietQuyetDinhThueDat tbody tr").eq(i).find('[data-name="IdQuyetDinhThueDatChiTiet"]').val(item.IdQuyetDinhThueDatChiTiet);
                                                $("#tblChiTietQuyetDinhThueDat tbody tr").eq(i).find('[data-name="MucDichSuDung"]').val(item.MucDichSuDung);
                                                $("#tblChiTietQuyetDinhThueDat tbody tr").eq(i).find('[data-name="ThoiHanThue"]').val(item.ThoiHanThue);
                                                $("#tblChiTietQuyetDinhThueDat tbody tr").eq(i).find('[data-name="TuNgayThue"]').val(item.TuNgayThue);
                                                $("#tblChiTietQuyetDinhThueDat tbody tr").eq(i).find('[data-name="DenNgayThue"]').val(item.DenNgayThue);
                                                $("#tblChiTietQuyetDinhThueDat tbody tr").eq(i).find('[data-name="HinhThucThue"]').val(item.HinhThucThue);
                                                $("#tblChiTietQuyetDinhThueDat tbody tr").eq(i).find('[data-name="DienTich"]').val(item.DienTich);


                                                $(".tr-remove").off('click').on('click', function () {
                                                    $(this).parents('tr:first').remove();
                                                });

                                            });
                                        }
                                        if (res.Data.DsFileTaiLieu != null) {
                                            $.each(res.Data.DsFileTaiLieu, function (i, item) {
                                                if (item.LoaiTaiLieu == "QuyetDinhThueDat") {
                                                    $('[data-name="FileQuyetDinhThueDat"]').html('')
                                                    $('[data-name="FileQuyetDinhThueDat"]').append('<a href = "' + localStorage.getItem("API_URL").replace('api', '') + item.LinkFile + '" target="_blank">' + item.TenFile + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                                    $('[data-name="FileQuyetDinhThueDat"]').attr('data-idFile', item.IdFile);
                                                    $('[data-name="FileQuyetDinhThueDat"]').attr('data-id', item.IdFileTaiLieu);
                                                }
                                                if (item.LoaiTaiLieu == "QuyetDinhGiaoDat") {
                                                    $('[data-name="FileQuyetDinhGiaoDat"]').html('')
                                                    $('[data-name="FileQuyetDinhGiaoDat"]').append('<a href = "' + localStorage.getItem("API_URL").replace('api', '') + item.LinkFile + '" target="_blank">' + item.TenFile + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                                    $('[data-name="FileQuyetDinhGiaoDat"]').attr('data-idFile', item.IdFile);
                                                    $('[data-name="FileQuyetDinhGiaoDat"]').attr('data-id', item.IdFileTaiLieu);
                                                }
                                            });
                                            $('.btn-deleteFile').off('click').on('click', function () {
                                                var $y = $(this);
                                                $y.parent().removeAttr("data-idFile");
                                                $y.parent().html('');

                                            });
                                        }
                                    }
                                })
                            }
                        }
                    });
                });

                $("#tblQuyetDinhThueDat tbody .QuyetDinhThueDat-remove").off('click').on('click', function (e) {

                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/QuyetDinhThueDat/Delete?idQuyetDinhThueDat=" + $y.attr('data-id') + "&Type=1",
                                //headers: {
                                //    'Authorization': 'Bearer ' + localStorage.getItem("ACCESS_TOKEN")
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
    RegisterEventsPopup: function (opts) {
        var self = this;
        $('.select2').select2();
        $('.datetimepicker-input').datetimepicker({
            format: 'DD/MM/YYYY'
        });
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
        $("#btnSaveQuyetDinhThueDat").off('click').on('click', function () {
            self.InsertUpdate();
        });
        $("#btnAddChiTietQuyetDinhThueDat").off('click').on('click', function () {
            var $td = $("#tempChiTietQuyetDinhThueDat").html();
            $("#tblChiTietQuyetDinhThueDat tbody").append($td);
            $(".number").change(function () {
                $(this).val(ConvertDecimalToString($(this).val()));
            });
            $(".tr-remove").off('click').on('click', function () {
                $(this).parents('tr:first').remove();
            });
        });

        $('#btnSelectFileQuyetDinhThueDat').click(function () {
            $('#fileQuyetDinhThueDat').trigger("click");
        });


        if ($('#fileQuyetDinhThueDat').length > 0) {
            $('#fileQuyetDinhThueDat')[0].value = "";
            $('#fileQuyetDinhThueDat').off('change').on('change', function (e) {
                var $this = this;
                var file = $('#fileQuyetDinhThueDat')[0].files.length > 0 ? $('#fileQuyetDinhThueDat')[0].files[0] : null;
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
                                $('[data-name="FileQuyetDinhThueDat"]').html('')
                                $('[data-name="FileQuyetDinhThueDat"]').append('<a href = "#">' + file.name + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                $('[data-name="FileQuyetDinhThueDat"]').attr('data-idFile', res.Data);
                                $('[data-name="FileQuyetDinhThueDat"]').attr('data-id', 0);
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
        $('#btnSelectFileQuyetDinhGiaoDat').click(function () {
            $('#fileQuyetDinhGiaoDat').trigger("click");
        });


        if ($('#fileQuyetDinhGiaoDat').length > 0) {
            $('#fileQuyetDinhGiaoDat')[0].value = "";
            $('#fileQuyetDinhGiaoDat').off('change').on('change', function (e) {
                var $this = this;
                var file = $('#fileQuyetDinhGiaoDat')[0].files.length > 0 ? $('#fileQuyetDinhGiaoDat')[0].files[0] : null;
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
                                $('[data-name="FileQuyetDinhGiaoDat"]').html('')
                                $('[data-name="FileQuyetDinhGiaoDat"]').append('<a href = "#">' + file.name + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                $('[data-name="FileQuyetDinhGiaoDat"]').attr('data-idFile', res.Data);
                                $('[data-name="FileQuyetDinhGiaoDat"]').attr('data-id', 0);
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
        $('#btnCreateQuyetDinhThueDat').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/QuyetDinhThueDat/PopupDetailQuyetDinhThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailQuyetDinhThueDat').html(res);
                    $('#popupDetailQuyetDinhThueDat').modal();
                    if (opts != undefined) {
                        $('#popupDetailQuyetDinhThueDat .ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                    } else {
                        self.LoadDanhSachDoanhNghiep();
                    }
                    self.RegisterEventsPopup();
                }
            })
        });
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchQuyetDinhThueDat").trigger('click');
            }
        });
        $("#btnSearchQuyetDinhThueDat").off('click').on('click', function () {
            self.table.ajax.reload();

        });

    },
    InsertUpdate: function () {
        var self = this;
        $(".number").each(function () {
            $(this).val(ConvertStringToDecimal($(this).val()));
        });
        var data = LoadFormData("#FormDetailQuyetDinhThueDat");
        data.IdDoanhNghiep = $(".ddDoanhNghiep option:selected").val();

        var arrayRow = $("#tblChiTietQuyetDinhThueDat tbody tr");
        var quyetDinhThueDatChiTiet = [];
        $.each(arrayRow, function (i, item) {
            var ct = $(item).find('[data-name="DienTich"]').val();
            if (ct != undefined) {
                quyetDinhThueDatChiTiet.push({
                    IdQuyetDinhThueDatChiTiet: $(item).find('[data-name="IdQuyetDinhThueDatChiTiet"]').val(),
                    IdQuyetDinhThueDat: $('[data-name="IdQuyetDinhThueDat"]').val(),
                    HinhThucThue: $(item).find('[data-name="HinhThucThue"] option:selected').val(),
                    MucDichSuDung: $(item).find('[data-name="MucDichSuDung"]').val(),
                    DienTich: $(item).find('[data-name="DienTich"]').val(),
                    ThoiHanThue: $(item).find('[data-name="ThoiHanThue"]').val(),
                    TuNgayThue: $(item).find('[data-name="TuNgayThue"]').val(),
                    DenNgayThue: $(item).find('[data-name="DenNgayThue"]').val()
                });
            }
        });
        var fileTaiLieu = [];
        if ($('[data-name="FileQuyetDinhThueDat"]').attr("data-idFile") != undefined) {
            fileTaiLieu.push({
                IdFileTaiLieu: $('[data-name="FileQuyetDinhThueDat"]').attr("data-id"),
                IdFile: $('[data-name="FileQuyetDinhThueDat"]').attr("data-idFile"),
                LoaiTaiLieu: "QuyetDinhThueDat"
            });
        }
        if ($('[data-name="FileQuyetDinhGiaoDat"]').attr("data-idFile") != undefined) {
            fileTaiLieu.push({
                IdFileTaiLieu: $('[data-name="FileQuyetDinhGiaoDat"]').attr("data-id"),
                IdFile: $('[data-name="FileQuyetDinhGiaoDat"]').attr("data-idFile"),
                LoaiTaiLieu: "QuyetDinhGiaoDat"
            });
        }
        data.QuyetDinhThueDatChiTiet = quyetDinhThueDatChiTiet;
        data.FileTaiLieu = fileTaiLieu;

        Post({
            "url": localStorage.getItem("API_URL") + "/QuyetDinhThueDat/InsertUpdate",
            "data": data,
            callback: function (res) {
                self.table.ajax.reload();
                $('#btnCloseQuyetDinhThueDat').trigger('click');
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

}

$(document).ready(function () {
    QuyetDinhThueDatControl.Init();
});

