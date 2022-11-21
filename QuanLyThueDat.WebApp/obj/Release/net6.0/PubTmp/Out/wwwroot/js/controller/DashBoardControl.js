if (typeof (DashBoardControl) == "undefined") DashBoardControl = {};
var countDenHan = 0;
DashBoardControl = {
    Init: function () {
        DashBoardControl.RegisterEvents();
    },

    LoadDatatableDonGiaThueDat: function (opts) {
        var self = this;
        self.table = SetDataTable({
            table: $('#tblThongBaoDonGiaThueDatSapHetHan'),
            url: localStorage.getItem("API_URL") + "/ThongBaoDonGiaThueDat/CanhBaoThongBaoDonGiaThueDatSapHetHan",
            dom: "t",
            data: {
                //"requestData": function () {
                //    return {
                //        "keyword": $('#txtKEY_WORD').val(),
                //    }
                //},
                "processData2": function (res) {

                    var json = jQuery.parseJSON(res);
                    countDenHan += json.Data.length;
                    json.data = json.Data;
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
                        "data": "SoThongBaoDonGiaThueDat",
                        "defaultContent": ""
                    },
                    
                    {
                        "class": "name-control",
                        "data": "NgayHetHieuLucDonGiaThueDat",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        "data": "ThoiHanDonGia",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var percent = parseInt(data) / 90 * 100;
                            var bg = "bg-warning";
                            if (percent < 50) {
                                bg = "bg-danger";
                            }
                            var thaotac = '<div class="progress progress-xs"><div class="progress-bar ' + bg + '" style = "width:' + percent + '%" ></div></div>' + data + ' ngày'
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {

                
                   

            }
        });

    },
    LoadDatatableQuyetDinhMienTienThueDat: function (opts) {
        var self = this;
        self.table = SetDataTable({
            table: $('#tblQuyetDinhMienTienThueDatSapHetHan'),
            url: localStorage.getItem("API_URL") + "/QuyetDinhMienTienThueDat/CanhBaoQuyetDinhMienTienThueDatSapHetHan",
            dom: "t",
            data: {
                //"requestData": function () {
                //    return {
                //        "keyword": $('#txtKEY_WORD').val(),
                //    }
                //},
                "processData2": function (res) {
                    var json = jQuery.parseJSON(res);
                    countDenHan += json.Data.length;
                    json.data = json.Data;
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
                        "data": "SoQuyetDinhMienTienThueDat",
                        "defaultContent": ""
                    },

                    {
                        "class": "name-control",
                        "data": "NgayHetHieuLucMienTienThueDat",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        "data": "ThoiHanMienTienThueDat",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var percent = parseInt(data) / 90 * 100;
                            var bg = "bg-warning";
                            if (percent < 50) {
                                bg = "bg-danger";
                            }
                            var thaotac = '<div class="progress progress-xs"><div class="progress-bar ' + bg + '" style = "width:' + percent + '%" ></div></div>'+data+' ngày'
                            return thaotac;
                        }
                    }                                              
                ]
            },
            callback: function () {

            }
        });
    },
    CountDoanhNghiep: function () {
        Get({
            url: localStorage.getItem("API_URL") + "/DoanhNghiep/GetAll",
            showLoading: true,
            callback: function (res) {
                $('.bg-info h3').text(res.Data.length);
            }
        });
    },
    CountThongBaoDonGiaThueDat: function () {
        Get({
            url: localStorage.getItem("API_URL") + "/ThongBaoDonGiaThueDat/GetAll",
            showLoading: true,
            callback: function (res) {
                $('.bg-success h3').text(res.Data.length);
            }
        });
    },
    CountThongBaoTienThueDat: function () {
        Get({
            url: localStorage.getItem("API_URL") + "/ThongBaoTienThueDat/GetAll",
            showLoading: true,
            callback: function (res) {
                $('.bg-warning h3').text(res.Data.length);
            }
        });
    },
    RegisterEvents: function () {
        var self = this;
        self.CountDoanhNghiep();
        self.CountThongBaoDonGiaThueDat();
        self.CountThongBaoTienThueDat();
        //self.CountDoanhNghiep();
        self.LoadDatatableDonGiaThueDat();
        self.LoadDatatableQuyetDinhMienTienThueDat();
        setTimeout(() => {
            $('.bg-danger h3').text(countDenHan);
            console.log(countDenHan);
        }, "1000");
               
    },
}

$(document).ready(function () {
    DashBoardControl.Init();
});

