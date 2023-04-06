
if (typeof (PhanQuyenCanBoControl) == "undefined") PhanQuyenCanBoControl = {};
PhanQuyenCanBoControl = {
    Init: function () {
        PhanQuyenCanBoControl.RegisterEvents();
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
        setTimeout(function () {
            $('input:checkbox[name=canbo]').change(function () {
                var treeView = $("#treeview2").dxTreeView('instance');
                var unSelect = treeView.unselectAll();
                if ($(this).is(':checked')) {
                    $('input:checkbox[name=canbo]').not(this).prop('checked', false);
                    console.log($(this).val());
                    self.LoadDanhSachQuyetDinhThueDatTheoChuyenVienPhuTrachKV($(this).val());
                } 
            });
        }, 500)
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
                            qdThueDat.push({ id: qd.IdQuyetDinhThueDat, text: qd.TenDoanhNghiep + " - " +qd.SoQuyetDinhThueDat + " ngày " + qd.NgayQuyetDinhThueDat + " - "+ qd.DiaChiThuaDat})
                        });
                        quanHuyen.push({ id: -1*item.IdQuanHuyen, text: item.TenQuanHuyen, items: qdThueDat, expanded: true});
                    });
                    var treeview = $("#treeview2");
                    var selected = $("#dsQuyetDinhThueDat");
                    CreateTreeView(quanHuyen, treeview, selected);
                    $(".dx-texteditor-input").addClass("ml-3");

                }
            }
        });
    },
    LoadDanhSachQuyetDinhThueDatTheoChuyenVienPhuTrachKV: function (userId) {
        Get({
            url: localStorage.getItem("API_URL") + "/User/LayDanhSachQuyetDinhThueDatTheoChuyenVienPhuTrachKV",
            data: {
                idCanBo: userId
            },
            callback: function (res) {
                if (res.IsSuccess) {
                    $.each(res.Data, function (i, item) {
                        var treeView = $("#treeview2").dxTreeView('instance');
                        var itemToSelect = treeView.element().find('.dx-treeview-node').find('[data-item-id="' + item.IdQuyetDinhThueDat + '"]');
                        treeView.selectItem(itemToSelect);
                    });
                    


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
        var idQuyetDinhThueDat = $('#dsQuyetDinhThueDat').attr('data-id').split(',');
        if (idQuyetDinhThueDat != "") {
            data.IdQuyetDinhThueDat = idQuyetDinhThueDat;
        } else {
            data.IdQuyetDinhThueDat = [];

        }
        Post({
            "url": localStorage.getItem("API_URL") + "/User/PhanQuyenChuyenVienPhuTrachKV",
            "data": data,
            callback: function (res) {
                if (res.IsSuccess) {
                    toastr.success('Thực hiện thành công', 'Thông báo');
                }
            }
        });
    },
}

$(document).ready(function () {
    PhanQuyenCanBoControl.Init();
});
