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
                        "width": "5%",
                        "data": "RN",
                        "defaultContent": "1",
                        render: function (data, type, row, meta) {
                            return meta.row + 1;
                        }
                    },
                    {
                        "class": "name-control",
                        "width": "20%",
                        "data": "SoHopDong",
                        "defaultContent": ""
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
                            var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                "<a href='javascript:;' class='HopDongThueDat-edit' data-id='" + row.IdHopDongThueDat + "'><i class='fas fa-edit' title='Sửa'></i></a>" +
                                "<a href='javascript:;' class='HopDongThueDat-remove text-danger' data-id='" + row.IdHopDongThueDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                "</div>";
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {

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
                                        FillFormData('#FormDetailHopDongThueDat', res.Data);
                                        var popup = $('#popupDetailHopDongThueDat');
                                        $('.select2').select2();
                                        if (opts != undefined) {
                                            popup.find('.ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                                        } else {
                                            popup.find('.ddDoanhNghiep').append('<option value="' + res.Data.IdDoanhNghiep + '">' + res.Data.TenDoanhNghiep + '</option>');
                                        }
                                        $("#btnSave").off('click').on('click', function () {
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

                $("#tblHopDongThueDat tbody .HopDongThueDat-remove").off('click').on('click', function (e) {

                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/HopDongThueDat/Delete?idHopDongThueDat=" + $y.attr('data-id') + "&Type=1",
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
        $('#btnCreateHopDongThueDat').off('click').on('click', function () {

            var $y = $(this);
            Get({
                url: '/HopDongThueDat/PopupDetailHopDongThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailHopDongThueDat').html(res);
                    $('#popupDetailHopDongThueDat').modal();
                    var popup = $('#popupDetailHopDongThueDat');
                    if (opts != undefined) {
                        console.log(1);
                        popup.find('.ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
                    } else {
                        self.LoadDanhSachDoanhNghiep();
                        console.log(2);
                    }
                    $('.select2').select2();
                    $("#btnSave").off('click').on('click', function () {
                        self.InsertUpdate();
                    });
                }
            })
        });
        $("#btnSearchHopDongThueDat").off('click').on('click', function () {
            self.table.ajax.reload();

        });

    },
    InsertUpdate: function () {
        var self = this;
        var self = this;
        var data = LoadFormData("#FormDetailHopDongThueDat");
        data.IdDoanhNghiep = $(".ddDoanhNghiep option:selected").val();
        console.log(data);
        Post({
            "url": localStorage.getItem("API_URL") + "/HopDongThueDat/InsertUpdate",
            "data": data,
            callback: function (res) {
                self.table.ajax.reload();
                $('#btnCloseHopDongThueDat').trigger('click');
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

}

$(document).ready(function () {
    HopDongThueDatControl.Init();
});

