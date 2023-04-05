
if (typeof (PhanQuyenCanBoControl) == "undefined") PhanQuyenCanBoControl = {};
PhanQuyenCanBoControl = {
    Init: function () {
        PhanQuyenCanBoControl.RegisterEvents();
        console.log(1234);
    },



    RegisterEvents: function (opts) {
        var self = this;
        self.LoadDanhSachQuyetDinhThueDat();
        self.LoadDanhSachChuyenVienPhuTrachKV();
        //nhà đầu tư
        $("#btn-create").on('click', function () {
            $('#modal-add-edit input:checkbox[name=role]').prop('checked', false);
            $('#modal-add-edit input:text').val('');
            $('#Id').val("00000000-0000-0000-0000-000000000000");
            $('#modal-add-edit').modal('show');
            self.RegisterEventsPopup();
        });
        $("#btn-save").on('click', function () {
            self.InsertUpdate();
        });
    },
    LoadDanhSachChuyenVienPhuTrachKV: function () {
        Get({
            url: localStorage.getItem("API_URL") + "/User/GetDsChuyenVienPhuTrachKV",
            callback: function (res) {
                if (res.IsSuccess) {
                    var canBo = [];
                    $.each(res.Data, function (i, item) {
                        var html = '<div class="icheck-primary">'+
                            '<input type = "checkbox" id = "' + item.UserId + '" name = "canbo" value = "' + item.UserId +'" >'+
                            '<label for="' + item.UserId + '">' +
                            item.HoTen + " - " + item.UserName +
                                '</label></div >'

                        $("#dsCanBo").append(html);
                    });
                }
            }
        });
    },
    LoadDanhSachQuyetDinhThueDat: function () {
        Get({
            url: localStorage.getItem("API_URL") + "/QuyetDinhThueDat/GetDsQuyetDinhThueDatTheoQuanHuyen",
            callback: function (res) {
                if (res.IsSuccess) {
                    var quanHuyen = [];
                    $.each(res.Data, function (i, item) {
                        var qdThueDat = [];
                        $.each(item.DsQuyetDinhThueDat, function (i, qd) {
                            qdThueDat.push({ id: qd.IdQuyetDinhThueDat, text: qd.SoQuyetDinhThueDat + " ngày " + qd.NgayQuyetDinhThueDat + " - "+ qd.TenDoanhNghiep})
                        });
                        quanHuyen.push({ id: -1*item.IdQuanHuyen, text: item.TenQuanHuyen, items: qdThueDat, expanded: true});
                    });
                    console.log(quanHuyen);
                    var treeview = $("#treeview2");
                    var selected = $("#dsQuyetDinhThueDat");
                    CreateTreeView(quanHuyen, treeview, selected);
                    $(".dx-texteditor-input").addClass("ml-3");

                }
            }
        });
    },
    InsertUpdate: function (opts) {
        var self = this;
        var data = {};
        var dsIdCanBo = [];
        $("input:checkbox[name=canbo]:checked").each(function () {
            dsIdCanBo.push($(this).val());
        });
        data.IdCanBo = dsIdCanBo;
        data.IdQuyetDinhThueDat = $('#dsQuyetDinhThueDat').attr('data-id').split(',');
        Post({
            "url": localStorage.getItem("API_URL") + "/User/PhanQuyenChuyenVienPhuTrachKV",
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
    PhanQuyenCanBoControl.Init();
});
