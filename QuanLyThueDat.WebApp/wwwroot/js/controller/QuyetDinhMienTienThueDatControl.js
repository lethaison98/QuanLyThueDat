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
                    console.log(res);
                    var json = jQuery.parseJSON(res);
                    json.recordsTotal = json.Data.TotalRecord;
                    json.recordsFiltered = json.Data.TotalRecord;
                    console.log(json.recordsTotal);
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
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "data": "SoQuyetDinhThueDat",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "data": "SoQuyetDinhMienTienThueDat",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "data": "TenQuyetDinhMienTienThueDat",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "NgayQuyetDinhMienTienThueDat",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        //"data": "thaotac",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                "<a href='javascript:;' class='QuyetDinhMienTienThueDat-edit' data-id='" + row.IdQuyetDinhMienTienThueDat + "'><i class='fas fa-edit' title='Sửa'></i></a>" +
                                "<a href='javascript:;' class='QuyetDinhMienTienThueDat-remove text-danger' data-id='" + row.IdQuyetDinhMienTienThueDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                "</div>";
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {

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
                                        FillFormData('#FormDetailQuyetDinhMienTienThueDat', res.Data);
                                        if (opts != undefined) {
                                            $('#popupDetailQuyetDinhMienTienThueDat .ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                                        } else {
                                            self.LoadDanhSachDoanhNghiep();
                                        }
                                        $('.select2').select2();
                                        $("#btnSaveQuyetDinhMienTienThueDat").off('click').on('click', function () {
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

                $("#tblQuyetDinhMienTienThueDat tbody .QuyetDinhMienTienThueDat-remove").off('click').on('click', function (e) {

                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/QuyetDinhMienTienThueDat/Delete?idQuyetDinhMienTienThueDat=" + $y.attr('data-id') + "&Type=1",
                                //headers: {
                                //    'Authorization': 'Bearer ' + localStorage.getItem("ACCESS_TOKEN")
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
                    if (opts != undefined) {
                        $('#popupDetailQuyetDinhMienTienThueDat .ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                    } else {
                        self.LoadDanhSachDoanhNghiep();
                    }
                    $('.select2').select2();
                    $("#btnSaveQuyetDinhMienTienThueDat").off('click').on('click', function () {
                        self.InsertUpdate();
                    });
                }
            })
        });
        $("#btnSearchQuyetDinhMienTienThueDat").off('click').on('click', function () {
            self.table.ajax.reload();

        });

    },
    InsertUpdate: function () {
        var self = this;
        var self = this;
        var data = LoadFormData("#FormDetailQuyetDinhMienTienThueDat");
        data.IdDoanhNghiep = $(".ddDoanhNghiep option:selected").val();
        console.log(data);
        Post({
            "url": localStorage.getItem("API_URL") + "/QuyetDinhMienTienThueDat/InsertUpdate",
            "data": data,
            callback: function (res) {
                self.table.ajax.reload();
                $('#btnCloseQuyetDinhMienTienThueDat').trigger('click');
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
    QuyetDinhMienTienThueDatControl.Init();
});

