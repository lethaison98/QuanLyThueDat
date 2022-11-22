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
                        "data": "TenQuyetDinhThueDat",
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
                            var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                "<a href='javascript:;' class='QuyetDinhThueDat-edit' data-id='" + row.IdQuyetDinhThueDat + "'><i class='fas fa-edit' title='Sửa'></i></a>" +
                                "<a href='javascript:;' class='QuyetDinhThueDat-remove text-danger' data-id='" + row.IdQuyetDinhThueDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                "</div>";
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {

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
                                //    'Authorization': 'Bearer ' + localStorage.getItem("access_token")
                                //},
                                dataType: 'json',
                                contentType: "application/json-patch+json",
                                type: "Delete",
                                success: function (res) {
                                    console.log(res);
                                    if (res.IsSuccess) {
                                        alert("Thành công");
                                        console.log(1);
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
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
        $("#btnSaveQuyetDinhThueDat").off('click').on('click', function () {
            self.InsertUpdate();
        });
        $("#btnAddChiTietQuyetDinhThueDat").off('click').on('click', function () {
            var $td = $("#tempChiTietQuyetDinhThueDat").html();
            $("#tblChiTietQuyetDinhThueDat tbody").append($td);
            $(".tr-remove").off('click').on('click', function () {
                $(this).parents('tr:first').remove();
            });
        });
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
        console.log(data);

        var arrayRow = $("#tblChiTietQuyetDinhThueDat tbody tr");
        var quyetDinhThueDatChiTiet = [];
        $.each(arrayRow, function (i, item) {
            var ct = $(item).find('[data-name="DienTich"]').val();
            if (ct != undefined) {
                quyetDinhThueDatChiTiet.push({
                    HinhThucThue: $(item).find('[data-name="HinhThucThue"] option:selected').val(),
                    MucDichSuDung: $(item).find('[data-name="MucDichSuDung"]').val(),
                    DienTich: $(item).find('[data-name="DienTich"]').val(),
                    ThoiHan: $(item).find('[data-name="ThoiHan"]').val(),
                    TuNgayThue: $(item).find('[data-name="TuNgayThue"]').val(),
                    DenNgayThue: $(item).find('[data-name="DenNgayThue"]').val()
                });
            }
        });
        console.log(quyetDinhThueDatChiTiet);
        data.QuyetDinhThueDatChiTiet = quyetDinhThueDatChiTiet;

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

