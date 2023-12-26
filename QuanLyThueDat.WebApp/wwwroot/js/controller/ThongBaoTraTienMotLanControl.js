if (typeof (ThongBaoTraTienMotLanControl) == "undefined") ThongBaoTraTienMotLanControl = {};
ThongBaoTraTienMotLanControl = {
    Init: function () {
        ThongBaoTraTienMotLanControl.RegisterEvents();

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
            table: $('#tblThongBaoTraTienMotLan'),
            url: localStorage.getItem("API_URL") + "/ThongBaoTienThueDat/GetAllPaging",
            dom: "rtip",
            data: {
                "requestData": function () {
                    return {
                        "keyword": $('#txtKEY_WORD').val(),
                        "idDoanhNghiep": idDoanhNghiep,
                        "thueDatTraTienMotLan": 1,
                        "nam": $('#namThongBao option:selected').val(),
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
                            var tables = $('#tblThongBaoTraTienMotLan').DataTable();
                            var info = tables.page.info();
                            return info.start + meta.row + 1;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "SoThongBaoTienThueDat",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var file = "";
                            if (row.DsFileTaiLieu != null) {
                                $.each(row.DsFileTaiLieu, function (i, item) {
                                    if (item.LoaiTaiLieu == "ThongBaoTraTienMotLan") {
                                        file = "<a href = '" + localStorage.getItem('API_URL').replace("api", "") + item.LinkFile + "' target='_blank'><i class = 'fas fa-paperclip' title = 'File thông báo tiền thuê đất'></i></a>";
                                    }
                                });
                            }
                            var thaotac = data + "&nbsp" + file;
                            return thaotac;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "TenDoanhNghiep",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "data": "Nam",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "SoTienPhaiNop",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "TextLoaiThongBaoTienThueDat",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "GhiChu",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        //"data": "thaotac",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var file = "";
                            if (row.DsFileTaiLieu != null) {
                                $.each(row.DsFileTaiLieu, function (i, item) {
                                    if (item.LoaiTaiLieu == "ThongBaoTraTienMotLan") {
                                        file = "<a href = '" + localStorage.getItem('API_URL').replace("api", "") + item.LinkFile + "' target='_blank'><i class = 'fas fa-paperclip' title = 'File thông báo tiền thuê đất'></i></a>";
                                    }
                                });
                            }
                            var show = "";
                            if (!row.QuyenDuLieu.AllowEdit) {
                                show = "style = 'display:none'";
                            }
                            if (opts == undefined) {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    file + "&nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTraTienMotLan-export' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-file-word' title='Xuất thông báo' ></i></a></div> ";
                                return thaotac;
                            } else {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    file + "&nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTraTienMotLan-export' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-file-word' title='Xuất thông báo' ></i></a> &nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTraTienMotLan-edit' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-edit' title='Chỉnh sửa'></i></a> &nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTraTienMotLan-remove text-danger' " + show + "data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                    "</div>";
                                return thaotac;
                            }

                        }
                    }
                ]
            },
            callback: function () {
                $("#tblThongBaoTraTienMotLan tbody .ThongBaoTraTienMotLan-export").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        window.open("/CreateWordFile?idThongBao=" + id + "&loaiThongBao=ThongBaoTienThueDat", "_blank");
                    }
                });

                $('#tblThongBaoTraTienMotLan tbody .ThongBaoTraTienMotLan-edit').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/ThongBaoTienThueDat/GetById',
                        data: {
                            idThongBaoTienThueDat: id
                        },
                        callback: function (res) {
                            if (res.IsSuccess) {
                                Get({
                                    url: '/ThongBaoTienThueDat/PopupDetailThongBaoTraTienMotLan',
                                    dataType: 'text',
                                    callback: function (popup) {
                                        $('#modalDetailThongBaoTraTienMotLan').html(popup);
                                        $('#popupDetailThongBaoTraTienMotLan').modal();
                                        $('#popupDetailThongBaoTraTienMotLan .modal-title').text("Chỉnh sửa thông báo tiền thuê đất - " + opts.TenDoanhNghiep);
                                        FillFormData('#FormDetailThongBaoTraTienMotLan', res.Data);
                                        opts.LoaiThongBaoTienThueDat = res.Data.LoaiThongBaoTienThueDat;
                                        setTimeout(function () {
                                            var checkRole = res.Data.QuyenDuLieu.AllowEdit;
                                            if (!checkRole) {
                                                $("#popupDetailThongBaoTraTienMotLan").find('input').attr("disabled", true);
                                                $("#popupDetailThongBaoTraTienMotLan").find('select').attr("disabled", true);
                                                $("#popupDetailThongBaoTraTienMotLan").find('.fa-trash-alt').hide();
                                                $("#popupDetailThongBaoTraTienMotLan").find('.fa-folder').attr("disabled", true);
                                                $("#popupDetailThongBaoTraTienMotLan").find('.btn-success').hide();
                                                $("#popupDetailThongBaoTraTienMotLan").find('.btn-primary').hide();
                                            } else {
                                                self.RegisterEventsPopup(opts);
                                            }
                                        }, 200);
                                        if (res.Data.IdQuyetDinhThueDat != null) {
                                            $('#popupDetailThongBaoTraTienMotLan .ddQuyetDinhThueDat').append('<option value="' + res.Data.IdQuyetDinhThueDat + '">' + res.Data.SoQuyetDinhThueDat + " - " + res.Data.NgayQuyetDinhThueDat + '</option>');
                                        }
                                        if (res.Data.DsThongBaoTienThueDatChiTiet != null) {
                                            var j = 0;
                                            var k = 0;
                                            $.each(res.Data.DsThongBaoTienThueDatChiTiet, function (i, item) {
                                                if (item.IdThongBaoDonGiaThueDat > 0) {
                                                    var $td = $("#tempChiTietThongBaoTraTienMotLan").html();
                                                    $("#tblChiTietThongBaoTraTienMotLan tbody").append($td);
                                                    self.LoadDanhSachThongBaoDonGiaThueDat(opts);
                                                    setTimeout(function () {
                                                        $("#tblChiTietThongBaoTraTienMotLan tbody tr").eq(j).find('[data-name="IdThongBaoDonGiaThueDat"]').val(item.IdThongBaoDonGiaThueDat);
                                                        $("#tblChiTietThongBaoTraTienMotLan tbody tr").eq(j).find('[data-name="DonGia"]').val(item.DonGia);
                                                        $("#tblChiTietThongBaoTraTienMotLan tbody tr").eq(j).find('[data-name="DienTichPhaiNop"]').val(item.DienTichPhaiNop);
                                                        $("#tblChiTietThongBaoTraTienMotLan tbody tr").eq(j).find('[data-name="SoTienPhaiNop"]').val(item.SoTienPhaiNop);
                                                        $("#tblChiTietThongBaoTraTienMotLan tbody tr").eq(j).find('[data-name="TuNgayTinhTien"]').val(item.TuNgayTinhTien);
                                                        $("#tblChiTietThongBaoTraTienMotLan tbody tr").eq(j).find('[data-name="DenNgayTinhTien"]').val(item.DenNgayTinhTien);
                                                        $("#tblChiTietThongBaoTraTienMotLan tbody tr").eq(j).find('[data-name="IdThongBaoDonGiaThueDat"]').val(item.IdThongBaoDonGiaThueDat);
                                                        j++;
                                                    }, 500)

                                                    $(".tr-remove").off('click').on('click', function () {
                                                        $(this).parents('tr:first').remove();
                                                    });

                                                } else if (item.IdQuyetDinhMienTienThueDat > 0) {
                                                    var $td = $("#tempChiTietMienGiamTienThueDat").html();
                                                    $("#tblChiTietMienGiamTienThueDat tbody").append($td);
                                                    self.LoadDanhSachQuyetDinhMienTienThueDat(opts);
                                                    setTimeout(function () {
                                                        $("#tblChiTietMienGiamTienThueDat tbody tr").eq(k).find('[data-name="DienTichMienGiam"]').val(item.DienTichKhongPhaiNop);
                                                        $("#tblChiTietMienGiamTienThueDat tbody tr").eq(k).find('[data-name="SoTienMienGiam"]').val(item.SoTienMienGiam);
                                                        $("#tblChiTietMienGiamTienThueDat tbody tr").eq(k).find('[data-name="TuNgayTinhTien"]').val(item.TuNgayTinhTien);
                                                        $("#tblChiTietMienGiamTienThueDat tbody tr").eq(k).find('[data-name="DenNgayTinhTien"]').val(item.DenNgayTinhTien);
                                                        $("#tblChiTietMienGiamTienThueDat tbody tr").eq(k).find('[data-name="DonGia"]').val(item.DonGia);
                                                        $("#tblChiTietMienGiamTienThueDat tbody tr").eq(k).find('[data-name="IdQuyetDinhMienTienThueDat"]').val(item.IdQuyetDinhMienTienThueDat);
                                                        k++;
                                                    }, 500)
                                                    $(".tr-remove").off('click').on('click', function () {
                                                        $(this).parents('tr:first').remove();
                                                    });

                                                }


                                            });
                                        }
                                        if (res.Data.DsFileTaiLieu != null) {
                                            $.each(res.Data.DsFileTaiLieu, function (i, item) {
                                                if (item.LoaiTaiLieu == "ThongBaoTraTienMotLan") {
                                                    $('[data-name="FileThongBaoTraTienMotLan"]').html('')
                                                    $('[data-name="FileThongBaoTraTienMotLan"]').append('<a href = "' + localStorage.getItem("API_URL").replace('api', '') + item.LinkFile + '" target="_blank">' + item.TenFile + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                                    $('[data-name="FileThongBaoTraTienMotLan"]').attr('data-idFile', item.IdFile);
                                                    $('[data-name="FileThongBaoTraTienMotLan"]').attr('data-id', item.IdFileTaiLieu);
                                                }
                                            });
                                        }
                                        $('.btn-deleteFile').off('click').on('click', function () {
                                            var $y = $(this);
                                            $y.parent().removeAttr("data-idFile");
                                            $y.parent().html('');

                                        });
                                        $('#popupDetailThongBaoTraTienMotLan .select2').attr("disabled", true);
                                    }
                                })
                            }
                        }
                    });
                });

                $("#tblThongBaoTraTienMotLan tbody .ThongBaoTraTienMotLan-remove").off('click').on('click', function (e) {

                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/ThongBaoTienThueDat/Delete?idThongBaoTienThueDat=" + $y.attr('data-id') + "&Type=1",
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
        var popup = $('#popupDetailThongBaoTraTienMotLan');
        if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
            var $table = popup.find("#temptable-ThongBaoTraTienMotLanDieuChinh").html();
            popup.find("#temptable").append($table);
        } else {
            var $table = popup.find("#temptable-ThongBaoTraTienMotLan").html();
            popup.find("#temptable").append($table);
        }
        $('.select2').select2();
        $('.datetimepicker-input').datetimepicker({
            format: 'DD/MM/YYYY'
        });
        $('#btnSelectFileThongBaoTraTienMotLan').click(function () {
            $('#fileThongBaoTraTienMotLan').trigger("click");
        });
        if ($('#fileThongBaoTraTienMotLan').length > 0) {
            $('#fileThongBaoTraTienMotLan')[0].value = "";
            $('#fileThongBaoTraTienMotLan').off('change').on('change', function (e) {
                var file = $('#fileThongBaoTraTienMotLan')[0].files.length > 0 ? $('#fileThongBaoTraTienMotLan')[0].files[0] : null;
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
                                $('[data-name="FileThongBaoTraTienMotLan"]').html('')
                                $('[data-name="FileThongBaoTraTienMotLan"]').append('<a href = "#">' + file.name + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                $('[data-name="FileThongBaoTraTienMotLan"]').attr('data-idFile', res.Data);
                                $('[data-name="FileThongBaoTraTienMotLan"]').attr('data-id', 0);
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
        popup.find("[data-name = 'LoaiThongBaoTienThueDat']").val(opts.LoaiThongBaoTienThueDat);
        popup.find('.ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
        popup.find('.calc').change(function () {
            self.TinhToanCongThuc(opts);
        });
        popup.find('.calc').blur(function () {
            self.TinhToanCongThuc(opts);
        });

        $("#btnSaveThongBaoTraTienMotLan").off('click').on('click', function () {
            self.InsertUpdate(opts);
        });
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
        $("#btnAddChiTietThongBaoTraTienMotLan").off('click').on('click', function () {
            var $td = $("#tempChiTietThongBaoTraTienMotLan").html();
            $("#tblChiTietThongBaoTraTienMotLan tbody").append($td);
            $(".number").change(function () {
                $(this).val(ConvertDecimalToString($(this).val()));
            });
            setTimeout(function () {
                $('.datetimepicker-input').datetimepicker({
                    format: 'DD/MM/YYYY'
                });
            }, 500)
            $(".tr-remove").off('click').on('click', function () {
                $(this).parents('tr:first').remove();
            });
            self.LoadDanhSachThongBaoDonGiaThueDat(opts);
        });
        $("#btnAddChiTietMienGiamTienThueDat").off('click').on('click', function () {
            var $td = $("#tempChiTietMienGiamTienThueDat").html();
            $("#tblChiTietMienGiamTienThueDat tbody").append($td);
            $(".number").change(function () {
                $(this).val(ConvertDecimalToString($(this).val()));
            });
            $(".tr-remove").off('click').on('click', function () {
                $(this).parents('tr:first').remove();
            });
            var $lastrow = $('#tblChiTietMienGiamTienThueDat tbody tr:last');
            var donGia = $('#tblChiTietThongBaoTraTienMotLan tbody tr:last').find('[data-name="DonGia"]').val();
            var dienTich = $('#tblChiTietThongBaoTraTienMotLan tbody tr:last').find('[data-name="DienTichPhaiNop"]').val();
            $lastrow.find('[data-name="DonGia"]').val(donGia);
            $lastrow.find('[data-name="DienTichMienGiam"]').val(dienTich);
            self.LoadDanhSachQuyetDinhMienTienThueDat(opts);
        });
    },
    RegisterEvents: function (opts) {
        var self = this;
        self.LoadDatatable(opts);
        $('#btnCreateThongBaoLanDau').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTraTienMotLan',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTraTienMotLan').html(res);
                    $('#popupDetailThongBaoTraTienMotLan').modal();
                    $('#popupDetailThongBaoTraTienMotLan .modal-title').text("Thêm mới thông báo tiền thuê đất - " + opts.TenDoanhNghiep);
                    opts.LoaiThongBaoTienThueDat = "ThongBaoLanDau";
                    self.RegisterEventsPopup(opts);
                    self.LoadDanhSachQuyetDinhThueDat(opts);
                }
            })
        });
        $('#btnCreateThongBaoTuNamThuHaiTroDi').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTraTienMotLan',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTraTienMotLan').html(res);
                    $('#popupDetailThongBaoTraTienMotLan').modal();
                    opts.LoaiThongBaoTienThueDat = "ThongBaoTuNamThuHaiTroDi";
                    self.RegisterEventsPopup(opts);
                    self.LoadDanhSachQuyetDinhThueDat(opts);
                }
            })
        });
        $('#btnCreateThongBaoTraTienMotLan').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTraTienMotLan',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTraTienMotLan').html(res);
                    $('#popupDetailThongBaoTraTienMotLan').modal();
                    $('#popupDetailThongBaoTraTienMotLan .modal-title').text("Thêm mới thông báo trả tiền một lần - " + opts.TenDoanhNghiep);
                    opts.LoaiThongBaoTienThueDat = "ThongBaoTraTienMotLan";
                    self.RegisterEventsPopup(opts);
                    self.LoadDanhSachQuyetDinhThueDat(opts);
                }
            })
        });
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchThongBaoTraTienMotLan").trigger('click');
            }
        });
        $("#btnSearchThongBaoTraTienMotLan").off('click').on('click', function () {
            self.table.ajax.reload();

        });
        $("#btnXuatBaoCaoTienThueDatHangNam").off('click').on('click', function (e) {
            if ($('#namThongBao option:selected').val() != "") {
                window.open("/ExportThongBaoTienThueDatHangNam?namThongBao=" + $('#namThongBao option:selected').val());
            } else {
                alert("Vui lòng chọn năm thông báo");
            }
        });


    },

    TinhToanCongThuc: function (opts) {
        var tongDienTich = 0;
        var dienTichKhongPhaiNop = 0;
        var tongSoTien = 0
        var tongSoTienMienGiam = 0
        console.log(12345);
        $("#tblChiTietThongBaoTraTienMotLan tbody tr").each(function () {
            var tuNgay = $(this).find('[data-name="TuNgayTinhTien"]').val();
            var denNgay = $(this).find('[data-name="DenNgayTinhTien"]').val();
            var donGia = ConvertStringToDecimal($(this).find("[data-name='DonGia']").val());
            var dienTichPhaiNop = ConvertStringToDecimal($(this).find("[data-name='DienTichPhaiNop']").val());

            var soThang = CalculateMonthBetweenDays(tuNgay, denNgay);
            var soTienPhaiNop = Math.round(donGia * dienTichPhaiNop * soThang) || 0;
            tongSoTien += soTienPhaiNop;
            $(this).find('[data-name="SoTienPhaiNop"]').val(ConvertDecimalToString(soTienPhaiNop.toFixed(0)));
        });

        $("#tblChiTietMienGiamTienThueDat tbody tr").each(function () {
            var tuNgayMienGiam = $(this).find('[data-name="TuNgayTinhTien"]').val();
            var denNgayMienGiam = $(this).find('[data-name="DenNgayTinhTien"]').val();
            var donGiaMienGiam = ConvertStringToDecimal($(this).find("[data-name='DonGia']").val());
            var dienTichMienGiam = ConvertStringToDecimal($(this).find("[data-name='DienTichMienGiam']").val());
            var soThangMienGiam = CalculateMonthBetweenDays(tuNgayMienGiam, denNgayMienGiam);
            var soTienMienGiam = Math.round(donGiaMienGiam * dienTichMienGiam * soThangMienGiam) || 0;
            console.log(soTienMienGiam);
            tongSoTienMienGiam += soTienMienGiam;
            $(this).find('[data-name="SoTienMienGiam"]').val(ConvertDecimalToString(soTienMienGiam.toFixed(0)));
        });
        $(".groupThongBaoTraTienMotLan").find("[data-name='SoTienMienGiam']").val(ConvertDecimalToString(tongSoTienMienGiam.toFixed(0)));

        tongDienTich = ConvertStringToDecimal($(".groupThongBaoTraTienMotLan").find("[data-name='TongDienTich']").val());
        dienTichKhongPhaiNop = ConvertStringToDecimal($(".groupThongBaoTraTienMotLan").find("[data-name='DienTichKhongPhaiNop']").val());

        $(".groupThongBaoTraTienMotLan").find("[data-name='SoTien']").val(ConvertDecimalToString(tongSoTien.toFixed(0)));
        soTienMienGiam = ConvertStringToDecimal($(".groupThongBaoTraTienMotLan").find("[data-name='SoTienMienGiam']").val()) || 0;
        var dienTichPhaiNop = (tongDienTich - dienTichKhongPhaiNop) || 0;
        $(".groupThongBaoTraTienMotLan").find("[data-name='DienTichPhaiNop']").val(ConvertDecimalToString(dienTichPhaiNop.toFixed(2).toString().replace(".", ",")));

        var soTienPhaiNop = tongSoTien - soTienMienGiam;
        $(".groupThongBaoTraTienMotLan").find("[data-name='SoTienPhaiNop']").val(ConvertDecimalToString(soTienPhaiNop.toFixed(0)));


    },

    InsertUpdate: function (opts) {
        var self = this;
        var data = {};
        $(".number").each(function () {
            $(this).val(ConvertStringToDecimal($(this).val()));
        });
        var popup = $('#popupDetailThongBaoTraTienMotLan');
        var data = LoadFormData("#FormDetailThongBaoTraTienMotLan");
        data.IdDoanhNghiep = popup.find(".ddDoanhNghiep option:selected").val();
        data.IdQuyetDinhThueDat = popup.find(".ddQuyetDinhThueDat option:selected").val();
        var arrayRowChiTiet = popup.find("#FormDetailThongBaoTraTienMotLan #tblChiTietThongBaoTraTienMotLan tbody tr");
        var arrayRowMienGiam = popup.find("#FormDetailThongBaoTraTienMotLan #tblChiTietMienGiamTienThueDat tbody tr");
        var thongBaoChiTiet = [];
        $.each(arrayRowChiTiet, function (i, item) {
            var ct = $(item).find('[data-name="DonGia"]').val();
            if (ct != undefined && ct != "") {
                thongBaoChiTiet.push({
                    IdThongBaoTienThueDatGoc: $(item).find(".ddThongBaoTienThueDatGoc option:selected").val(),
                    IdThongBaoDonGiaThueDat: $(item).find(".ddThongBaoDonGiaThueDat option:selected").val(),
                    IdQuyetDinhMienTienThueDat: 0,
                    DonGia: $(item).find('[data-name="DonGia"]').val(),
                    DienTichPhaiNop: $(item).find('[data-name="DienTichPhaiNop"]').val(),
                    SoTienPhaiNop: $(item).find('[data-name="SoTienPhaiNop"]').val(),
                    TuNgayTinhTien: $(item).find('[data-name="TuNgayTinhTien"]').val(),
                    DenNgayTinhTien: $(item).find('[data-name="DenNgayTinhTien"]').val()
                });
            }
        });
        $.each(arrayRowMienGiam, function (i, item) {
            var ct = $(item).find('[data-name="DonGia"]').val();
            if (ct != undefined && ct != "") {
                thongBaoChiTiet.push({
                    IdThongBaoDonGiaThueDat: 0,
                    IdQuyetDinhMienTienThueDat: $(item).find(".ddQuyetDinhMienTienThueDat option:selected").val(),
                    DonGia: $(item).find('[data-name="DonGia"]').val(),
                    DienTichKhongPhaiNop: $(item).find('[data-name="DienTichMienGiam"]').val(),
                    SoTienMienGiam: $(item).find('[data-name="SoTienMienGiam"]').val(),
                    TuNgayTinhTien: $(item).find('[data-name="TuNgayTinhTien"]').val(),
                    DenNgayTinhTien: $(item).find('[data-name="DenNgayTinhTien"]').val()
                });
            }
        });
        data.ThongBaoTienThueDatChiTiet = thongBaoChiTiet;


        if (data.Nam == "") data.Nam = new Date().getFullYear()
        var fileTaiLieu = [];
        if ($('[data-name="FileThongBaoTraTienMotLan"]').attr("data-idFile") != undefined) {
            fileTaiLieu.push({
                IdFileTaiLieu: $('[data-name="FileThongBaoTraTienMotLan"]').attr("data-id"),
                IdFile: $('[data-name="FileThongBaoTraTienMotLan"]').attr("data-idFile"),
                LoaiTaiLieu: "ThongBaoTraTienMotLan"
            });
        }
        data.FileTaiLieu = fileTaiLieu;
        Post({
            "url": localStorage.getItem("API_URL") + "/ThongBaoTienThueDat/InsertUpdate",
            "data": data,
            callback: function (res) {
                if (res.IsSuccess) {
                    toastr.success('Thực hiện thành công', 'Thông báo')
                    self.table.ajax.reload();
                    $('#btnCloseThongBaoTraTienMotLan').trigger('click');
                }
                else {
                    toastr.error(res.Message, 'Có lỗi xảy ra')
                }
            }
        });
    },
    LoadDanhSachDoanhNghiep: function (opts) {
        var self = this;
        var popup = $('#popupDetailThongBaoTraTienMotLan');
        Get({
            url: localStorage.getItem("API_URL") + "/DoanhNghiep/GetAll",
            showLoading: true,
            callback: function (res) {
                popup.find('.ddDoanhNghiep').html('');
                $.each(res.Data, function (i, item) {
                    popup.find('.ddDoanhNghiep').append('<option value="' + item.IdDoanhNghiep + '">' + item.TenDoanhNghiep + '</option>');
                })
                popup.find('.ddDoanhNghiep').on('change', function () {
                    if (popup.find(".ddDoanhNghiep option:selected").val() != 0 && popup.find(".ddDoanhNghiep option:selected").val() != undefined) {
                        self.LoadDanhSachQuyetDinhThueDat(opts);
                    }
                });
                //$('.ddDoanhNghiep').trigger('change');

            }
        });
    },
    LoadDanhSachQuyetDinhThueDat: function (opts) {
        var self = this;
        var popup = $('#popupDetailThongBaoTraTienMotLan');
        var form = '#FormDetailThongBaoTraTienMotLan';

        Get({
            url: localStorage.getItem("API_URL") + "/QuyetDinhThueDat/GetListQuyetDinhThueDatChiTiet",
            data: {
                idDoanhNghiep: opts.IdDoanhNghiep
            },
            showLoading: true,
            callback: function (res) {
                popup.find('.ddQuyetDinhThueDat').html('');
                popup.find('.ddQuyetDinhThueDat').append('<option value= "" selected="true" style="display: none"></option>');
                $.each(res.Data, function (i, item) {
                    var name = item.SoQuyetDinhThueDat + " - " + item.TextHinhThucThue + " - Diện tích " + item.TongDienTich + "  (m<sup>2</sup>)"
                    popup.find('.ddQuyetDinhThueDat').append('<option value=' + item.IdQuyetDinhThueDat + ' index= ' + i + '>' + name + '</option>');
                })
                popup.find('.ddQuyetDinhThueDat').on('change', function () {
                    $('.groupQuyetDinhThueDat input').val("");
                    if (popup.find(".ddQuyetDinhThueDat option:selected").val() != undefined && popup.find(".ddQuyetDinhThueDat option:selected").val() != "") {
                        var qd = res.Data[popup.find(".ddQuyetDinhThueDat option:selected").attr("index")];
                        FillFormData(form, qd);
                        //    self.LoadDanhSachThongBaoDonGiaThueDat(opts);
                    } else {
                        $('.groupQuyetDinhThueDat input').val("");
                    }
                });
                popup.find('.ddQuyetDinhThueDat').trigger('change');
            }
        });
    },
    LoadDanhSachThongBaoDonGiaThueDat: function (opts) {
        var self = this;
        var popup = $('#popupDetailThongBaoTraTienMotLan');
        var form = $('#FormDetailThongBaoTraTienMotLan');
        if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
            var $lastrow = $('#tblChiTietThongBaoTraTienMotLan tbody tr:last');

        } else {
            var $lastrow = $('#tblChiTietThongBaoTraTienMotLan tbody tr:last');

        }


        var Data = {
            IdQuyetDinhThueDat: popup.find(".ddQuyetDinhThueDat option:selected").val(),
            IdDoanhNghiep: popup.find(".ddDoanhNghiep option:selected").val()
        };
        Post({
            url: localStorage.getItem("API_URL") + "/ThongBaoDonGiaThueDat/GetAllByRequest",
            data: Data,
            showLoading: true,
            callback: function (res) {
                $lastrow.find('.ddThongBaoDonGiaThueDat').html('');
                $.each(res.Data, function (i, item) {
                    $lastrow.find('.ddThongBaoDonGiaThueDat').append('<option value="' + item.IdThongBaoDonGiaThueDat + '">' + item.SoThongBaoDonGiaThueDat + '</option>');
                })
                $lastrow.find('.ddThongBaoDonGiaThueDat').on('change', function () {
                    if ($lastrow.find(".ddThongBaoDonGiaThueDat option:selected").val() != 0 && $lastrow.find(".ddThongBaoDonGiaThueDat option:selected").val() != undefined) {
                        var $y = $(this).parent().parent();
                        Get({
                            url: localStorage.getItem("API_URL") + '/ThongBaoDonGiaThueDat/GetById',
                            data: {
                                idThongBaoDonGiaThueDat: $y.find(".ddThongBaoDonGiaThueDat option:selected").val()
                            },
                            callback: function (res) {
                                if (res.IsSuccess) {
                                    var donGiaMotLan = ConvertDecimalToString((ConvertStringToDecimal(res.Data.DonGia) / 50 / 12).toFixed(0))
                                    $y.find('[data-name="DonGia"]').val(donGiaMotLan);
                                    $y.find('[data-name="DienTichPhaiNop"]').val(res.Data.DienTichPhaiNop);
                                    popup.find('.calc').blur(function () {
                                        self.TinhToanCongThuc(opts);
                                    });
                                }
                            }
                        });
                    } else {
                        $('.groupThongBaoDonGiaThueDat input').val("");
                    }
                });
                $lastrow.find('.ddThongBaoDonGiaThueDat').trigger('change');
            }
        });
    },


    LoadDanhSachQuyetDinhMienTienThueDat: function (opts) {
        var self = this;
        var $lastrow = $('#tblChiTietMienGiamTienThueDat tbody tr:last');

        var popup = $('#popupDetailThongBaoTraTienMotLan');
        var form = '#FormDetailThongBaoTraTienMotLan';


        Get({
            url: localStorage.getItem("API_URL") + "/QuyetDinhMienTienThueDat/GetAll?idDoanhNghiep=" + opts.IdDoanhNghiep,
            showLoading: true,
            callback: function (res) {
                $lastrow.find('.ddQuyetDinhMienTienThueDat').html('');
                $.each(res.Data, function (i, item) {
                    $lastrow.find('.ddQuyetDinhMienTienThueDat').append('<option value="' + item.IdQuyetDinhMienTienThueDat + '">' + item.SoQuyetDinhMienTienThueDat + " từ " + item.NgayHieuLucMienTienThueDat + " đến " + item.NgayHetHieuLucMienTienThueDat + '</option>');
                })

                popup.find('.calc').blur(function () {
                    self.TinhToanCongThuc(opts);
                });
            }
        });
    },

}

$(document).ready(function () {
    ThongBaoTraTienMotLanControl.Init();
});
