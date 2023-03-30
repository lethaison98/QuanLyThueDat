if (typeof (TaiKhoanControl) == "undefined") TaiKhoanControl = {};
TaiKhoanControl = {
    Init: function () {
        TaiKhoanControl.RegisterEvents();
    },

    LoadDatatable: function (opts) {
        var self = this;
        self.table = SetDataTable({
            table: $('#tblTaiKhoan'),
            url: localStorage.getItem("API_URL") + "/User/GetAllPaging",
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
                        "data": "HoTen",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "UserName",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "data": null,
                        "defaultContent": "",
                        render: function (data, type, row, meta) {
                            var display = "";
                            $.each(data.DsRole, function (i, value) {
                                display += "<a href='javascript:;' class='badge bg-purple'>" + value + "</a> &nbsp";
                            });
                            return display;
                        }
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        //"data": "thaotac",
                        "defaultContent": "",
                        render: function (data, type, row) {

                            var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                "<a href='javascript:;' class='TaiKhoan-edit' data-id='" + row.UserId + "'><i class='fas fa-edit' title='Chỉnh sửa'></i></a> &nbsp" +
                                "<a href='javascript:;' class='TaiKhoan-remove text-danger' data-id='" + row.UserId + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                "</div>";
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {
                $('#tblTaiKhoan tbody .TaiKhoan-edit').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    $('#modal-add-edit input:checkbox[name=role]').prop('checked', false);
                    $('#modal-add-edit input:text').val('');
                    Get({
                        url: localStorage.getItem("API_URL") + '/User/GetById',
                        data: {
                            idTaiKhoan: id
                        },
                        callback: function (res) {
                            if (res.IsSuccess) {
                                $('#Id').val('0');
                                $('#modal-add-edit').modal('show');
                                FillFormData('#FormDetailTaiKhoan', res.Data);
                                $.each(res.Data.DsRole, function (i, value) {
                                    $('input:checkbox[name=role][value="' + value + '"]').prop('checked', true);
                                });
                                $("#btnSaveTaiKhoan").off('click').on('click', function () {
                                    self.InsertUpdate(opts);
                                });
                            }
                        }
                    });
                });

                $("#tblTaiKhoan tbody .TaiKhoan-remove").off('click').on('click', function (e) {

                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/TaiKhoan/Delete?idTaiKhoan=" + $y.attr('data-id') + "&Type=1",
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
        var popup = $('#popupDetailTaiKhoan');
        $('.groupThongBaoDonGiaThueDat input').change(function () {
            self.TinhToanCongThuc(opts);
        });
        $('.groupThongBaoDonGiaThueDat input').blur(function () {
            self.TinhToanCongThuc(opts);
        });

        $("#btnSaveTaiKhoan").off('click').on('click', function () {
            self.InsertUpdate(opts);
        });
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
    },

    RegisterEvents: function (opts) {
        var self = this;
        self.LoadDatatable(opts);
        //nhà đầu tư
        $("#btn-create").on('click', function () {
            $('#modal-add-edit input:checkbox[name=role]').prop('checked', false);
            $('#modal-add-edit input:text').val('');
            $('#Id').val("00000000-0000-0000-0000-000000000000");
            $('#modal-add-edit').modal('show');
            self.RegisterEventsPopup();
        });
    },
    InsertUpdate: function (opts) {
        var self = this;
        var data = {};
        $(".number").each(function () {
            $(this).val(ConvertStringToDecimal($(this).val()));
        });
        var data = LoadFormData("#FormDetailTaiKhoan");

        var array = [];
        $("input:checkbox[name=role]:checked").each(function () {
            array.push($(this).val());
        });
        data.DsRole = array;
        Post({
            "url": localStorage.getItem("API_URL") + "/User/InsertUpdate",
            "data": data,
            callback: function (res) {
                if (res.IsSuccess) {
                    self.table.ajax.reload();
                    $('#btnClose').trigger('click');
                    toastr.success('Thực hiện thành công', 'Thông báo');
                }
            }
        });
    },
}

$(document).ready(function () {
    TaiKhoanControl.Init();
});
