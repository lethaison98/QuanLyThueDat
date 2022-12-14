if (typeof (ThongBaoTienThueDatControl) == "undefined") ThongBaoTienThueDatControl = {};
ThongBaoTienThueDatControl = {
    Init: function () {
        ThongBaoTienThueDatControl.RegisterEvents();

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
                            return meta.row + 1;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "SoThongBaoTienThueDat",
                        "defaultContent": ""
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
                                    "<a href='javascript:;' class='ThongBaoTienThueDat-remove text-danger' data-id='" + row.IdThongBaoTienThueDat + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
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
                                Get({
                                    url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDat',
                                    dataType: 'text',
                                    callback: function (popup) {
                                        $('#modalDetailThongBaoTienThueDat').html(popup);
                                        $('#popupDetailThongBaoTienThueDat').modal();
                                        $('#popupDetailThongBaoTienThueDat .modal-title').text("Chỉnh sửa thông báo tiền thuê đất - " + opts.TenDoanhNghiep);

                                        FillFormData('#FormDetailThongBaoTienThueDat', res.Data);
                                        opts.LoaiThongBaoTienThueDat = res.Data.LoaiThongBaoTienThueDat;
                                        self.RegisterEventsPopup(opts);
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

                                        //$("#btnTraCuu").on('click', function () {
                                        //    $('#modal-add-edit').modal('show');
                                        //});
                                    }
                                })
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
        var self = this;
        var popup = $('#popupDetailThongBaoTienThueDat');
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
        $('.groupThongBaoDonGiaThueDat input').change(function () {
            self.TinhToanCongThuc(opts);
        });
        $('.groupThongBaoDonGiaThueDat input').blur(function () {
            self.TinhToanCongThuc(opts);
        });

        $("#btnSaveThongBaoTienThueDat").off('click').on('click', function () {
            self.InsertUpdate(opts);
        });
        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
    },
    RegisterEventsPopupThongBaoDieuChinh: function (opts) {
        var self = this;
        var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
        popup.find('.ddDoanhNghiep').append('<option value="' + opts.IdDoanhNghiep + '">' + opts.TenDoanhNghiep + '</option>');
        popup.find("[data-name = 'LoaiThongBaoTienThueDat']").val(opts.LoaiThongBaoTienThueDat);
        self.LoadDanhSachQuyetDinhThueDat(opts);
        $('.select2').select2();

        $(".number").change(function () {
            $(this).val(ConvertDecimalToString($(this).val()));
        });
        $("#btnSaveThongBaoTienThueDatDieuChinh").off('click').on('click', function () {
            self.InsertUpdate(opts);
        });
        $("#btnAddChiTietThongBaoTienThueDatDieuChinh").off('click').on('click', function () {
            var $td = $("#tempChiTietThongBaoTienThueDatDieuChinh").html();
            $("#tblChiTietThongBaoTienThueDatDieuChinh tbody").append($td);
            $(".number").change(function () {
                $(this).val(ConvertDecimalToString($(this).val()));
            });
            $(".tr-remove").off('click').on('click', function () {
                $(this).parents('tr:first').remove();
            });
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
        $('#btnCreateThongBaoTienThueDatDieuChinh').off('click').on('click', function () {
            var $y = $(this);
            Get({
                url: '/ThongBaoTienThueDat/PopupDetailThongBaoTienThueDatDieuChinh',
                dataType: 'text',
                callback: function (res) {
                    $('#modalDetailThongBaoTienThueDatDieuChinh').html(res);
                    $('#popupDetailThongBaoTienThueDatDieuChinh').modal();
                    opts.LoaiThongBaoTienThueDat = "ThongBaoDieuChinh";
                    self.RegisterEventsPopupThongBaoDieuChinh(opts);
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
        if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
            var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
            var form = '#FormDetailThongBaoTienThueDatDieuChinh';
        } else {
            var popup = $('#popupDetailThongBaoTienThueDat');
            var form = '#FormDetailThongBaoTienThueDat';
        };

        var tongDienTich = 0;
        var donGia = 0;
        var dienTichKhongPhaiNop = 0;

        if (popup.find(".groupThongBaoDonGiaThueDat [data-name='TongDienTich']").val() != '') {
            tongDienTich = ConvertStringToDecimal(popup.find(".groupThongBaoDonGiaThueDat [data-name='TongDienTich']").val());
            console.log(popup.find(".groupThongBaoDonGiaThueDat [data-name='TongDienTich']").val());
        };
        if (popup.find("[data-name='DonGia']").val() != '') {
            donGia = ConvertStringToDecimal(popup.find("[data-name='DonGia']").val());
        };
        if (popup.find("[data-name='DienTichKhongPhaiNop']").val() != '') {
            dienTichKhongPhaiNop = ConvertStringToDecimal(popup.find(".groupThongBaoDonGiaThueDat [data-name='DienTichKhongPhaiNop']").val());
        };

        var tongSoTien = donGia * tongDienTich;
        popup.find("[data-name='SoTien']").val(ConvertDecimalToString(tongSoTien.toFixed(0)));

        
        var soTienMienGiam = donGia * dienTichKhongPhaiNop;
        popup.find("[data-name='SoTienMienGiam']").val(ConvertDecimalToString(soTienMienGiam.toFixed(0)));

        var dienTichPhaiNop = tongDienTich - dienTichKhongPhaiNop;
        popup.find("[data-name='DienTichPhaiNop']").val(ConvertDecimalToString(dienTichPhaiNop.toFixed(2).toString().replace(".", ",")));

        var soTienPhaiNop = tongSoTien - soTienMienGiam;
        popup.find("[data-name='SoTienPhaiNop']").val(ConvertDecimalToString(soTienPhaiNop.toFixed(0)));

    },
    InsertUpdate: function (opts) {
        var self = this;
        var data = {};
        $(".number").each(function () {
            $(this).val(ConvertStringToDecimal($(this).val()));
        });
        if (opts != undefined) {
            if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
                var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
                var data = LoadFormData("#FormDetailThongBaoTienThueDatDieuChinh");
                data.IdDoanhNghiep = popup.find(".ddDoanhNghiep option:selected").val();
                var arrayRow = $("#tblChiTietThongBaoTienThueDatDieuChinh tbody tr");
                var thongBaoChiTiet = [];
                $.each(arrayRow, function (i, item) {
                    var ct = $(item).find('[data-name="Nam"]').val();
                    if (ct != undefined && ct != "") {
                        thongBaoChiTiet.push({
                            Nam: $(item).find('[data-name="Nam"]').val(),
                            SoTien: $(item).find('[data-name="SoTien"]').val(),
                            SoTienPhaiNop: $(item).find('[data-name="SoTienPhaiNop"]').val(),
                            SoTienMienGiam: $(item).find('[data-name="SoTienMienGiam"]').val(),
                            GhiChu: $(item).find('[data-name="GhiChu"]').val()
                        });
                    }
                });
                data.ThongBaoTienThueDatChiTiet = thongBaoChiTiet;
            } else {
                var popup = $('#popupDetailThongBaoTienThueDat');
                var data = LoadFormData("#FormDetailThongBaoTienThueDat");
                data.IdDoanhNghiep = popup.find(".ddDoanhNghiep option:selected").val();

            };
        } else {
            var popup = $('#popupDetailThongBaoTienThueDat');
            var data = LoadFormData("#FormDetailThongBaoTienThueDat");
            data.IdDoanhNghiep = popup.find(".ddDoanhNghiep option:selected").val();
        }
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
                self.table.ajax.reload();
                $('#btnCloseThongBaoTienThueDat').trigger('click');
            }
        });
    },
    LoadDanhSachDoanhNghiep: function (opts) {
        var self = this;
        if (opts != undefined) {
            if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
                var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
            } else {
                var popup = $('#popupDetailThongBaoTienThueDat');
            };
        } else {
            var popup = $('#popupDetailThongBaoTienThueDat');
        }
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
        if (opts != undefined) {
            if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
                var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
                var form = '#FormDetailThongBaoTienThueDatDieuChinh';
            } else {
                var popup = $('#popupDetailThongBaoTienThueDat');
                var form = '#FormDetailThongBaoTienThueDat';
            };
        } else {
            var popup = $('#popupDetailThongBaoTienThueDat');
            var form = '#FormDetailThongBaoTienThueDat';
        }
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
                    popup.find('.ddQuyetDinhThueDat').append('<option value=' + i + '>' + name + '</option>');
                })
                popup.find('.ddQuyetDinhThueDat').on('change', function () {
                    $('.groupQuyetDinhThueDat input').val("");
                    if (popup.find(".ddQuyetDinhThueDat option:selected").val() != undefined && popup.find(".ddQuyetDinhThueDat option:selected").val() != "") {
                        var qd = res.Data[popup.find(".ddQuyetDinhThueDat option:selected").val()];
                        FillFormData(form, qd);
                        self.LoadDanhSachThongBaoDonGiaThueDat(opts);
                        //Get({
                        //    url: localStorage.getItem("API_URL") + '/QuyetDinhThueDat/GetById',
                        //    data: {
                        //        idQuyetDinhThueDat: popup.find(".ddQuyetDinhThueDat option:selected").val()
                        //    },
                        //    callback: function (res) {
                        //        if (res.IsSuccess) {
                        //            FillFormData('#FormDetailThongBaoTienThueDat', res.Data);
                        //            self.LoadDanhSachThongBaoDonGiaThueDat();
                        //        }
                        //    }
                        //});
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
        if (opts != undefined) {
            if (opts.LoaiThongBaoTienThueDat == "ThongBaoDieuChinh") {
                var popup = $('#popupDetailThongBaoTienThueDatDieuChinh');
                var form = '#FormDetailThongBaoTienThueDatDieuChinh';
            } else {
                var popup = $('#popupDetailThongBaoTienThueDat');
                var form = '#FormDetailThongBaoTienThueDat';
            };
        } else {
            var popup = $('#popupDetailThongBaoTienThueDat');
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
                popup.find('.ddThongBaoDonGiaThueDat').html('');
                $.each(res.Data, function (i, item) {
                    popup.find('.ddThongBaoDonGiaThueDat').append('<option value= "" selected="true" style="display: none"></option>');
                    popup.find('.ddThongBaoDonGiaThueDat').append('<option value="' + item.IdThongBaoDonGiaThueDat + '">' + item.SoThongBaoDonGiaThueDat + '</option>');
                })
                popup.find('.ddThongBaoDonGiaThueDat').on('change', function () {
                    if (popup.find(".ddThongBaoDonGiaThueDat option:selected").val() != 0 && popup.find(".ddThongBaoDonGiaThueDat option:selected").val() != undefined) {
                        Get({
                            url: localStorage.getItem("API_URL") + '/ThongBaoDonGiaThueDat/GetById',
                            data: {
                                idThongBaoDonGiaThueDat: popup.find(".ddThongBaoDonGiaThueDat option:selected").val()
                            },
                            callback: function (res) {
                                if (res.IsSuccess) {
                                    FillFormData(form, res.Data);
                                    $('.groupThongBaoDonGiaThueDat input').change(function () {
                                        self.TinhToanCongThuc(opts);
                                    });
                                    $('.groupThongBaoDonGiaThueDat input').blur(function () {
                                        self.TinhToanCongThuc(opts);
                                    });
                                    setTimeout(function () { popup.find('.groupThongBaoDonGiaThueDat input').trigger('change') }, 500);

                                }
                            }
                        });
                    } else {
                        $('.groupThongBaoDonGiaThueDat input').val("");
                    }
                });
                popup.find('.ddThongBaoDonGiaThueDat').trigger('change');
            }
        });
    },

}

$(document).ready(function () {
    ThongBaoTienThueDatControl.Init();
});

