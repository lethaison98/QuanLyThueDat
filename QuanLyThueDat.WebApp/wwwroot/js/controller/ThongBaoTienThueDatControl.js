if (typeof (ThongBaoTienThueDatControl) == "undefined") ThongBaoTienThueDatControl = {};
ThongBaoTienThueDatControl = {
    Init: function () {
        ThongBaoTienThueDatControl.RegisterEvents();

    },

    LoadDatatable: function (opts) {
        console.log(113);
        var idDoanhNghiep;
        if (opts == undefined) {
            idDoanhNghiep = null;
        } else {
            idDoanhNghiep = opts.IdDoanhNghiep
        }
        var self = this;
        self.table = SetDataTable({
            table: $('#tblThongBaoTienThueDat'),
            url: localStorage.getItem("API_URL") + "/ThongBaoTienThueDat/GetAllPaging",
            dom: "rtip",
            data: {
                "requestData": function () {
                    return {
                        "keyword": $('#txtKEY_WORD').val(),
                        "idDoanhNghiep": idDoanhNghiep,
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
                            var tables = $('#tblThongBaoTienThueDat').DataTable();
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
                                    if (item.LoaiTaiLieu == "ThongBaoTienThueDat") {
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
                                    if (item.LoaiTaiLieu == "ThongBaoTienThueDat") {
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
                                    "<a href='javascript:;' class='ThongBaoTienThueDat-export' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-file-word' title='Xuất thông báo' ></i></a></div> ";
                                return thaotac;
                            } else {
                                var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                    file + "&nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTienThueDat-export' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-file-word' title='Xuất thông báo' ></i></a> &nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTienThueDat-edit' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-edit' title='Chỉnh sửa'></i></a> &nbsp" +
                                    "<a href='javascript:;' class='ThongBaoTienThueDat-remove text-danger' " + show + "data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                    "</div>";
                                return thaotac;
                            }

                        }
                    }
                ]
            },
            callback: function () {
                $("#tblThongBaoTienThueDat tbody .ThongBaoTienThueDat-export").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        window.open("/CreateWordFile?idThongBao=" + id + "&loaiThongBao=ThongBaoTienThueDat", "_blank");
                    }
                });

                $('#tblThongBaoTienThueDat tbody .ThongBaoTienThueDat-edit').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/ThongBaoTienThueDat/GetById',
                        data: {
                            idThongBaoTienThueDat: id
                        },
                        callback: function (res) {
                            if (res.IsSuccess) {
                                if (res.Data.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
                                    Get({
                                        url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                                        dataType: 'text',
                                        callback: function (popup) {
                                            $('#modalDetailThongBaoTienThueDat').html(popup);
                                            $('#popupDetailThongBaoTienThueDat').modal();
                                            $('#popupDetailThongBaoTienThueDat .modal-title').text("Chỉnh sửa thông báo tiền thuê đất - " + opts.TenDoanhNghiep);
                                            FillFormData('#FormDetailThongBaoTienThueDat', res.Data);
                                            opts.LoaiThongBaoTienThueDat = res.Data.LoaiThongBaoTienThueDat;
                                            setTimeout(function () {
                                                var checkRole = res.Data.QuyenDuLieu.AllowEdit;
                                                if (!checkRole) {
                                                    $("#popupDetailThongBaoTienThueDat").find('input').attr("disabled", true);
                                                    $("#popupDetailThongBaoTienThueDat").find('select').attr("disabled", true);
                                                    $("#popupDetailThongBaoTienThueDat").find('.fa-trash-alt').hide();
                                                    $("#popupDetailThongBaoTienThueDat").find('.fa-folder').attr("disabled", true);
                                                    $("#popupDetailThongBaoTienThueDat").find('.btn-success').hide();
                                                    $("#popupDetailThongBaoTienThueDat").find('.btn-primary').hide();
                                                } else {
                                                    self.RegisterEventsPopup(opts);
                                                }
                                            }, 200);
                                            if (res.Data.IdQuyetDinhThueDat != null) {
                                                $('#popupDetailThongBaoTienThueDat .ddQuyetDinhThueDat').append('<option value="' + res.Data.IdQuyetDinhThueDat + '">' + res.Data.SoQuyetDinhThueDat + " - " + res.Data.NgayQuyetDinhThueDat + '</option>');
                                            }
                                            if (res.Data.DsThongBaoTienThueDatChiTiet != null) {
                                                var j = 0;
                                                var k = 0;
                                                $.each(res.Data.DsThongBaoTienThueDatChiTiet, function (i, item) {
                                                    if (item.IdThongBaoDonGiaThueDat > 0) {
                                                        setTimeout(function () {
                                                            var $td = $("#tempChiTietThongBaoTienThueDatDieuChinh").html();
                                                            $("#tblChiTietThongBaoTienThueDat tbody").append($td);
                                                            self.LoadDanhSachThongBaoDonGiaThueDat(opts);
                                                            self.LoadDanhSachThongBaoTienThueDatDeDieuChinh(opts);
                                                        },300)
                                                        setTimeout(function () {
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="IdThongBaoTienThueDatGoc"]').val(item.IdThongBaoTienThueDatGoc);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="IdThongBaoDonGiaThueDat"]').val(item.IdThongBaoDonGiaThueDat);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="DonGia"]').val(item.DonGia);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="DienTichPhaiNop"]').val(item.DienTichPhaiNop);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="SoTienPhaiNop"]').val(item.SoTienPhaiNop);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="TuNgayTinhTien"]').val(item.TuNgayTinhTien);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="DenNgayTinhTien"]').val(item.DenNgayTinhTien);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="IdThongBaoDonGiaThueDat"]').val(item.IdThongBaoDonGiaThueDat);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="DonGia"]').val(item.DonGia);
                                                            j++;
                                                        },500)

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
                                                    if (item.LoaiTaiLieu == "ThongBaoTienThueDat") {
                                                        $('[data-name="FileThongBaoTienThueDat"]').html('')
                                                        $('[data-name="FileThongBaoTienThueDat"]').append('<a href = "' + localStorage.getItem("API_URL").replace('api', '') + item.LinkFile + '" target="_blank">' + item.TenFile + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                                        $('[data-name="FileThongBaoTienThueDat"]').attr('data-idFile', item.IdFile);
                                                        $('[data-name="FileThongBaoTienThueDat"]').attr('data-id', item.IdFileTaiLieu);
                                                    }
                                                });
                                            }
                                            $('.btn-deleteFile').off('click').on('click', function () {
                                                var $y = $(this);
                                                $y.parent().removeAttr("data-idFile");
                                                $y.parent().html('');

                                            });
                                            $('#popupDetailThongBaoTienThueDat .select2').attr("disabled", true);
                                        }
                                    })
                                } else {
                                    Get({
                                        url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                                        dataType: 'text',
                                        callback: function (popup) {
                                            $('#modalDetailThongBaoTienThueDat').html(popup);
                                            $('#popupDetailThongBaoTienThueDat').modal();
                                            $('#popupDetailThongBaoTienThueDat .modal-title').text("Chỉnh sửa thông báo tiền thuê đất - " + opts.TenDoanhNghiep);
                                            FillFormData('#FormDetailThongBaoTienThueDat', res.Data);
                                            opts.LoaiThongBaoTienThueDat = res.Data.LoaiThongBaoTienThueDat;
                                            setTimeout(function () {
                                                var checkRole = res.Data.QuyenDuLieu.AllowEdit;
                                                if (!checkRole) {
                                                    $("#popupDetailThongBaoTienThueDat").find('input').attr("disabled", true);
                                                    $("#popupDetailThongBaoTienThueDat").find('select').attr("disabled", true);
                                                    $("#popupDetailThongBaoTienThueDat").find('.fa-trash-alt').hide();
                                                    $("#popupDetailThongBaoTienThueDat").find('.fa-folder').attr("disabled", true);
                                                    $("#popupDetailThongBaoTienThueDat").find('.btn-success').hide();
                                                    $("#popupDetailThongBaoTienThueDat").find('.btn-primary').hide();
                                                } else {
                                                    self.RegisterEventsPopup(opts);
                                                }
                                            }, 200);
                                            if (res.Data.IdQuyetDinhThueDat != null) {
                                                $('#popupDetailThongBaoTienThueDat .ddQuyetDinhThueDat').append('<option value="' + res.Data.IdQuyetDinhThueDat + '">' + res.Data.SoQuyetDinhThueDat + " - " + res.Data.NgayQuyetDinhThueDat + '</option>');
                                            }
                                            if (res.Data.DsThongBaoTienThueDatChiTiet != null) {
                                                var j = 0;
                                                var k = 0;
                                                $.each(res.Data.DsThongBaoTienThueDatChiTiet, function (i, item) {
                                                    if (item.IdThongBaoDonGiaThueDat > 0) {
                                                        var $td = $("#tempChiTietThongBaoTienThueDat").html();
                                                        $("#tblChiTietThongBaoTienThueDat tbody").append($td);
                                                        self.LoadDanhSachThongBaoDonGiaThueDat(opts);
                                                        setTimeout(function () {
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="IdThongBaoDonGiaThueDat"]').val(item.IdThongBaoDonGiaThueDat);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="DonGia"]').val(item.DonGia);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="DienTichPhaiNop"]').val(item.DienTichPhaiNop);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="SoTienPhaiNop"]').val(item.SoTienPhaiNop);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="TuNgayTinhTien"]').val(item.TuNgayTinhTien);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="DenNgayTinhTien"]').val(item.DenNgayTinhTien);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="IdThongBaoDonGiaThueDat"]').val(item.IdThongBaoDonGiaThueDat);
                                                            $("#tblChiTietThongBaoTienThueDat tbody tr").eq(j).find('[data-name="DonGia"]').val(item.DonGia);
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
                                                    if (item.LoaiTaiLieu == "ThongBaoTienThueDat") {
                                                        $('[data-name="FileThongBaoTienThueDat"]').html('')
                                                        $('[data-name="FileThongBaoTienThueDat"]').append('<a href = "' + localStorage.getItem("API_URL").replace('api', '') + item.LinkFile + '" target="_blank">' + item.TenFile + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                                        $('[data-name="FileThongBaoTienThueDat"]').attr('data-idFile', item.IdFile);
                                                        $('[data-name="FileThongBaoTienThueDat"]').attr('data-id', item.IdFileTaiLieu);
                                                    }
                                                });
                                            }
                                            $('.btn-deleteFile').off('click').on('click', function () {
                                                var $y = $(this);
                                                $y.parent().removeAttr("data-idFile");
                                                $y.parent().html('');

                                            });
                                            $('#popupDetailThongBaoTienThueDat .select2').attr("disabled", true);
                                        }
                                    })
                                }
                               
                            }
                        }
                    });
                });

                $("#tblThongBaoTienThueDat tbody .ThongBaoTienThueDat-remove").off('click').on('click', function (e) {

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
        console.log(opts)
        var self = this;
        var popup = $('#popupDetailThongBaoTienThueDat');
        if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
            var $table = popup.find("#temptable-ThongBaoTienThueDatDieuChinh").html();
            popup.find("#temptable").append($table);
        } else {
            var $table = popup.find("#temptable-ThongBaoTienThueDat").html();
            popup.find("#temptable").append($table);
        }
        $('.select2').select2();
        $('.datetimepicker-input').datetimepicker({
            format: 'DD/MM/YYYY'
        });
        $('#btnSelectFileThongBaoTienThueDat').click(function () {
            $('#fileThongBaoTienThueDat').trigger("click");
        });
        if ($('#fileThongBaoTienThueDat').length > 0) {
            $('#fileThongBaoTienThueDat')[0].value = "";
            $('#fileThongBaoTienThueDat').off('change').on('change', function (e) {
                var file = $('#fileThongBaoTienThueDat')[0].files.length > 0 ? $('#fileThongBaoTienThueDat')[0].files[0] : null;
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
                                $('[data-name="FileThongBaoTienThueDat"]').html('')
                                $('[data-name="FileThongBaoTienThueDat"]').append('<a href = "#">' + file.name + '</a>&nbsp;<i class="fas fa-trash-alt btn-deleteFile" title="Xóa"></i>');
                                $('[data-name="FileThongBaoTienThueDat"]').attr('data-idFile', res.Data);
                                $('[data-name="FileThongBaoTienThueDat"]').attr('data-id', 0);
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
            console.log(123)
            self.TinhToanCongThuc(opts);
        });
        popup.find('.calc').blur(function () {
            console.log(123)

            self.TinhToanCongThuc(opts);
        });

        $("#btnSaveThongBaoTienThueDat").off('click').on('click', function () {
            self.InsertUpdate(opts);
        });
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
        $("#btnAddChiTietThongBaoTienThueDat").off('click').on('click', function () {
            var $td = $("#tempChiTietThongBaoTienThueDat").html();
            $("#tblChiTietThongBaoTienThueDat tbody").append($td);
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
        $("#btnAddChiTietThongBaoTienThueDatDieuChinh").off('click').on('click', function () {
            var $td = $("#tempChiTietThongBaoTienThueDatDieuChinh").html();
            $("#tblChiTietThongBaoTienThueDat tbody").append($td);
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
            self.LoadDanhSachThongBaoTienThueDatDeDieuChinh(opts);
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
            var donGia = $('#tblChiTietThongBaoTienThueDat tbody tr:last').find('[data-name="DonGia"]').val();
            var dienTich = $('#tblChiTietThongBaoTienThueDat tbody tr:last').find('[data-name="DienTichPhaiNop"]').val();
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
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTienThueDat').html(res);
                    $('#popupDetailThongBaoTienThueDat').modal();
                    $('#popupDetailThongBaoTienThueDat .modal-title').text("Thêm mới thông báo tiền thuê đất - " + opts.TenDoanhNghiep);
                    opts.LoaiThongBaoTienThueDat = "ThongBaoLanDau";
                    self.RegisterEventsPopup(opts);
                    self.LoadDanhSachQuyetDinhThueDat(opts);
                }
            })
        });
        $('#btnCreateThongBaoTuNamThuHaiTroDi').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTienThueDat').html(res);
                    $('#popupDetailThongBaoTienThueDat').modal();
                    opts.LoaiThongBaoTienThueDat = "ThongBaoTuNamThuHaiTroDi";
                    self.RegisterEventsPopup(opts);
                    self.LoadDanhSachQuyetDinhThueDat(opts);
                }
            })
        });
        $('#btnCreateThongBaoTraTienMotLan').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTienThueDat').html(res);
                    $('#popupDetailThongBaoTienThueDat').modal();
                    $('#popupDetailThongBaoTienThueDat .modal-title').text("Thêm mới thông báo trả tiền một lần - " + opts.TenDoanhNghiep);
                    opts.LoaiThongBaoTienThueDat = "ThongBaoTraTienMotLan";
                    self.RegisterEventsPopup(opts);
                    self.LoadDanhSachQuyetDinhThueDat(opts);
                }
            })
        });
        $('#btnCreateThongBaoTienThueDatDieuChinh').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTienThueDat').html(res);
                    $('#popupDetailThongBaoTienThueDat').modal();
                    opts.LoaiThongBaoTienThueDat = "ThongBaoDieuChinh";
                    self.RegisterEventsPopup(opts);
                    self.LoadDanhSachQuyetDinhThueDat(opts);

                }
            })
        });
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchThongBaoTienThueDat").trigger('click');
            }
        });
        $("#btnSearchThongBaoTienThueDat").off('click').on('click', function () {
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
        console.log(1111);
        var tongDienTich = 0;
        var dienTichKhongPhaiNop = 0;
        var tongSoTien = 0
        var tongSoTienMienGiam = 0

        if (opts.LoaiThongBaoTienThueDat == "ThongBaoTraTienMotLan") {
            $("#tblChiTietThongBaoTienThueDat tbody tr").each(function () {
                var donGia = ConvertStringToDecimal($(this).find("[data-name='DonGia']").val());
                var dienTichPhaiNop = ConvertStringToDecimal($(this).find("[data-name='DienTichPhaiNop']").val());
                var soTienPhaiNop = Math.round(donGia * dienTichPhaiNop) || 0;
                tongSoTien += soTienPhaiNop;
                $(this).find('[data-name="SoTienPhaiNop"]').val(ConvertDecimalToString(soTienPhaiNop.toFixed(0)));
            });
        } else {
            $("#tblChiTietThongBaoTienThueDat tbody tr").each(function () {
                var tuNgay = $(this).find('[data-name="TuNgayTinhTien"]').val();
                var denNgay = $(this).find('[data-name="DenNgayTinhTien"]').val();
                var donGia = ConvertStringToDecimal($(this).find("[data-name='DonGia']").val());
                var dienTichPhaiNop = ConvertStringToDecimal($(this).find("[data-name='DienTichPhaiNop']").val());

                var soThang = CalculateMonthBetweenDays(tuNgay, denNgay);
                var soTienPhaiNop = Math.round(donGia * dienTichPhaiNop * soThang / 12) || 0;
                tongSoTien += soTienPhaiNop;
                $(this).find('[data-name="SoTienPhaiNop"]').val(ConvertDecimalToString(soTienPhaiNop.toFixed(0)));
            });

            $("#tblChiTietMienGiamTienThueDat tbody tr").each(function () {
                var tuNgayMienGiam = $(this).find('[data-name="TuNgayTinhTien"]').val();
                var denNgayMienGiam = $(this).find('[data-name="DenNgayTinhTien"]').val();
                var donGiaMienGiam = ConvertStringToDecimal($(this).find("[data-name='DonGia']").val());
                var dienTichMienGiam = ConvertStringToDecimal($(this).find("[data-name='DienTichMienGiam']").val());
                var soThangMienGiam = CalculateMonthBetweenDays(tuNgayMienGiam, denNgayMienGiam);
                var soTienMienGiam = Math.round(donGiaMienGiam * dienTichMienGiam * soThangMienGiam / 12) || 0;
                tongSoTienMienGiam += soTienMienGiam;
                $(this).find('[data-name="SoTienMienGiam"]').val(ConvertDecimalToString(soTienMienGiam.toFixed(0)));
            });
            $(".groupThongBaoTienThueDat").find("[data-name='SoTienMienGiam']").val(ConvertDecimalToString(tongSoTienMienGiam.toFixed(0)));

        }
        tongDienTich = ConvertStringToDecimal($(".groupThongBaoTienThueDat").find("[data-name='TongDienTich']").val());
        dienTichKhongPhaiNop = ConvertStringToDecimal($(".groupThongBaoTienThueDat").find("[data-name='DienTichKhongPhaiNop']").val());

        $(".groupThongBaoTienThueDat").find("[data-name='SoTien']").val(ConvertDecimalToString(tongSoTien.toFixed(0)));
        soTienMienGiam = ConvertStringToDecimal($(".groupThongBaoTienThueDat").find("[data-name='SoTienMienGiam']").val()) || 0;
        var dienTichPhaiNop = (tongDienTich - dienTichKhongPhaiNop) || 0;
        $(".groupThongBaoTienThueDat").find("[data-name='DienTichPhaiNop']").val(ConvertDecimalToString(dienTichPhaiNop.toFixed(2).toString().replace(".", ",")));

        var soTienPhaiNop = tongSoTien - soTienMienGiam;
        $(".groupThongBaoTienThueDat").find("[data-name='SoTienPhaiNop']").val(ConvertDecimalToString(soTienPhaiNop.toFixed(0)));


    },

    InsertUpdate: function (opts) {
        var self = this;
        var data = {};
        $(".number").each(function () {
            $(this).val(ConvertStringToDecimal($(this).val()));
        });
        var popup = $('#popupDetailThongBaoTienThueDat');
        var data = LoadFormData("#FormDetailThongBaoTienThueDat");
        data.IdDoanhNghiep = popup.find(".ddDoanhNghiep option:selected").val();
        data.IdQuyetDinhThueDat = popup.find(".ddQuyetDinhThueDat option:selected").val();
        var arrayRowChiTiet = popup.find("#FormDetailThongBaoTienThueDat #tblChiTietThongBaoTienThueDat tbody tr");
        var arrayRowMienGiam = popup.find("#FormDetailThongBaoTienThueDat #tblChiTietMienGiamTienThueDat tbody tr");
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
        if ($('[data-name="FileThongBaoTienThueDat"]').attr("data-idFile") != undefined) {
            fileTaiLieu.push({
                IdFileTaiLieu: $('[data-name="FileThongBaoTienThueDat"]').attr("data-id"),
                IdFile: $('[data-name="FileThongBaoTienThueDat"]').attr("data-idFile"),
                LoaiTaiLieu: "ThongBaoTienThueDat"
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
                    $('#btnCloseThongBaoTienThueDat').trigger('click');
                }
                else {
                    toastr.error(res.Message, 'Có lỗi xảy ra')
                }
            }
        });
    },
    LoadDanhSachDoanhNghiep: function (opts) {
        var self = this;
        var popup = $('#popupDetailThongBaoTienThueDat');
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
        var popup = $('#popupDetailThongBaoTienThueDat');
        var form = '#FormDetailThongBaoTienThueDat';

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
        console.log(opts);

        var popup = $('#popupDetailThongBaoTienThueDat');
        var form = $('#FormDetailThongBaoTienThueDat');
        if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
            var $lastrow = $('#tblChiTietThongBaoTienThueDat tbody tr:last');

        } else {
            var $lastrow = $('#tblChiTietThongBaoTienThueDat tbody tr:last');

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
                                    $y.find('[data-name="DonGia"]').val(res.Data.DonGia);
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

        var popup = $('#popupDetailThongBaoTienThueDat');
        var form = '#FormDetailThongBaoTienThueDat';


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

    LoadDanhSachThongBaoTienThueDatDeDieuChinh: function (opts) {
        var self = this;
        var $lastrow = $('#tblChiTietThongBaoTienThueDat tbody tr:last');
        if (opts != undefined) {
            var popup = $('#popupDetailThongBaoTienThueDat');
            var form = '#FormDetailThongBaoTienThueDat';
            var Data = {
                IdQuyetDinhThueDat: popup.find(".ddQuyetDinhThueDat option:selected").val(),
                IdDoanhNghiep: popup.find(".ddDoanhNghiep option:selected").val()
            };
            Post({
                url: localStorage.getItem("API_URL") + "/ThongBaoTienThueDat/GetAllByRequest",
                data: Data,
                showLoading: true,
                callback: function (res) {
                    $lastrow.find('.ddThongBaoTienThueDatGoc').html('');
                    $.each(res.Data, function (i, item) {
                        $lastrow.find('.ddThongBaoTienThueDatGoc').append('<option value="' + item.IdThongBaoTienThueDat + '">' + item.SoThongBaoTienThueDat + " ngày " + item.NgayThongBaoTienThueDat + '</option>');
                    })
                    $lastrow.find('.ddThongBaoTienThueDatGoc').on('change', function () {
                        if ($lastrow.find(".ddThongBaoTienThueDatGoc option:selected").val() != 0 && $lastrow.find(".ddThongBaoTienThueDatGoc option:selected").val() != undefined) {
                            var $y = $(this).parent().parent();
                            Get({
                                url: localStorage.getItem("API_URL") + '/ThongBaoTienThueDat/GetById',
                                data: {
                                    idThongBaoTienThueDat: $y.find(".ddThongBaoTienThueDatGoc option:selected").val()
                                },
                                callback: function (res) {
                                    if (res.IsSuccess) {
                                        $y.find('[data-name="SoTien"]').val(res.Data.SoTienPhaiNop);
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
                    $lastrow.find('.ddThongBaoTienThueDatGoc').trigger('change');
                }
            });
        }
    },
}

$(document).ready(function () {
    ThongBaoTienThueDatControl.Init();
});
