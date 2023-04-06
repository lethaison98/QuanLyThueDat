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
    },
}

$(document).ready(function () {
    SiteControl.Init();
});

