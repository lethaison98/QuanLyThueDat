if (typeof (SiteControl) == "undefined") SiteControl = {};
SiteControl = {
    Init: function () {
        SiteControl.RegisterEvents();
    },
    RegisterEvents: function (opts) {
        var self = this;
        var checkRoleQuanTri = localStorage.getItem("Roles").includes("CHUYENVIENCHUYENQUAN");
        if (checkRoleQuanTri) {
            $(".roleCHUYENVIENCHUYENQUAN").show();
        }
        $("#ChangePass").on('click', function () {
            $('#modal-add-edit input:checkbox[name=role]').prop('checked', false);
            $('#modal-add-edit input:text').val('');
            $('#UserId').val("00000000-0000-0000-0000-000000000000");
            $('#modal-change-pass').modal('show');
            $("#modal-change-pass .btn-primary").off('click').on('click', function () {
                var oldPass = $("#modal-change-pass ").find('[data-name="OldPassword"]').val();
                var newPass = $("#modal-change-pass ").find('[data-name="NewPassword"]').val();
                var reNewPass = $("#modal-change-pass ").find('[data-name="ReNewPassword"]').val();
                if (reNewPass != newPass) {
                    toastr.error("Mật khẩu nhập lại không khớp", "Thông báo")
                } else {
                    var paswd = /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,16}$/;
                    if (newPass.match(paswd)) {
                        var data = {};
                        data.OldPassword = oldPass;
                        data.NewPassword = newPass;
                        Post({
                            "url": localStorage.getItem("API_URL") + "/User/ChangePassByUser",
                            "data": data,
                            callback: function (res) {
                                if (res.IsSuccess) {
                                    $('#btnClose').trigger('click');
                                    toastr.success('Thực hiện thành công', 'Thông báo');
                                } else {
                                    toastr.error(res.Message,'Thông báo' )
                                }
                            }
                        });
                    }
                    else {
                        toastr.error("Mật khẩu từ 8 đến 16 ký tự, bao gồm ít nhất 1 chữ hoa, 1 chữ thường và 1 ký tự đặc biệt", "Thông báo")
                        return false;
                    }
                }


            });
        });
    },
}

$(document).ready(function () {
    SiteControl.Init();
});

