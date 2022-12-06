$(document).ready(function () {
    // Default: application/x-www-form-urlencoded; charset=UTF-8 //// application/x-www-form-urlencoded, multipart/form-data, or text/plain
    Post = function (opts) {
        var settings = {
            url: '',
            data: {

            },
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("access_token")
            },
            dataType: 'json',
            showLoading: true,
            isUploadFile: false,
            isJson: true,
            contentType: "application/json-patch+json",
            type: "Post",
            callback: function () { }
        }
        settings = $.extend(true, {}, settings, opts);
        if (settings.isJson) {
            settings.data = JSON.stringify(settings.data);
            if (typeof (settings.contentType) == "undefined") settings.contentType = 'application/json-patch+json; charset=utf-8';
        }

        if (settings.isUploadFile) {
            settings.contentType = false;
            settings.processData = false;
            settings.cache = false;
        }
        if (settings.showLoading) {
            $('#loading').show();
        }
        settings.success = function (res) {
            settings.callback(res);

            if (settings.showLoading) {
                $('#loading').hide();
            }
        }
        settings.error = function (jqXHR, error, errorThrown) {
            console.log(jqXHR, error, errorThrown)
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 401) {
                //window.location.href = "/User/Login";
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (error === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (error === 'timeout') {
                msg = 'Time out error.';
            } else if (error === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            if (settings.showLoading) {
                $('#loading').hide();
            }
        }

        $.ajax(settings);
    };

    Get = function (opts) {
        var settings = {
            url: '',
            data: {

            },
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("access_token")
            },
            dataType: 'json',
            showLoading: true,
            isJson: false,
            type: "Get",
            callback: function () { }
        }

        settings = $.extend(true, {}, settings, opts);
        if (settings.showLoading) {
            $('#loading').show();
        }
        settings.success = function (data) {
            settings.callback(data);
            if (settings.showLoading) {
                $('#loading').hide();
            }
        }
        settings.error = function (jqXHR, error, errorThrown) {
            console.log(jqXHR, error, errorThrown)
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 401) {
                //window.location.href = "/User/Login";
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (error === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (error === 'timeout') {
                msg = 'Time out error.';
            } else if (error === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }

            settings.callback(jqXHR.responseJSON);
            if (settings.showLoading) {
                $('#loading').hide();
            }
        }
        $.ajax(settings);
    };
    Delete = function (opts) {
        var settings = {
            url: '',
            data: {

            },
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("access_token")
            },
            dataType: 'json',
            showLoading: true,
            isJson: false,
            type: "Delete",
            callback: function () { }
        }

        settings = $.extend(true, {}, settings, opts);
        if (settings.showLoading) {
            $('#loading').show();
        }
        settings.success = function (data) {
            settings.callback(data);
            if (settings.showLoading) {
                $('#loading').hide();
            }
        }
        settings.error = function (jqXHR, error, errorThrown) {
            console.log(jqXHR, error, errorThrown)
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 401) {
                window.location.href = "/User/Login";
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (error === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (error === 'timeout') {
                msg = 'Time out error.';
            } else if (error === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }

            settings.callback(jqXHR.responseJSON);
            if (settings.showLoading) {
                $('#loading').hide();
            }
        }
        $.ajax(settings);
    };
    FillFormData = function (formId, data) {
        if (typeof data == 'undefined' || data == null) data = {};
        var $form = $(formId);
        $.each($('[data-name]', $form), function (i, item) {
            if (data[$(item).attr('data-name')] == null) {
                if ($(item).hasClass('date-picker')) $(item).datepicker("update", "");
                return;
            };
            var type = $(item).getType();
            switch (type) {
                case 'p':
                case 'span':
                case 'strong':
                    $(item).text(data[$(item).attr('data-name')]);
                    break;
                case 'text':
                case 'textarea':
                case 'password':
                case 'hidden':
                case 'select':
                    $(item).val(data[$(item).attr('data-name')] + "");
                    break;
                case 'checkbox':
                    $(item).prop('checked', Boolean(data[$(item).attr('data-name')]));
                    break;
                default:

                    break;
            }
            if ($(item).hasClass('date-picker')) {
                $(item).datepicker("update", new Date(data[$(item).attr('data-name')].replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3")));
            }
        });
    };

    LoadFormData = function (formId, data) {
        if (typeof data == 'undefined') data = {};
        var $form = $(formId);

        $.each($('[data-name]', $form), function (i, item) {
            var key = $(item).attr('data-name');
            var value = '';

            var type = $(item).getType();
            var key2;
            var value2;

            switch (type) {
                case 'text':
                case 'span':
                    if ($(item).hasClass('maskMoney')) {
                        key2 = key + '_maskMoney';
                        value2 = $(item).val();
                        data[key2] = value2;
                        value = $(item).val().replace(/\./g, "").replace("VNĐ", "").trim();
                    } else if ($(item).hasClass('maskPercent')) {

                        value = (parseInt($(item).val().replace(/\ %/g, "").trim()) || 0) + '';
                    } else if ($(item).hasClass('maskYear')) {

                        value = (parseFloat($(item).val().replace(/\,/g, ".").replace(/\ năm/g, "").trim()) || 0) + '';
                    } else if ($(item).hasClass('selectboxit')) {
                        value = $(item).parent().prev().val();
                    } else {
                        value = $(item).val().trim();
                    }

                    break;
                case 'textarea':
                case 'password':
                case 'hidden':
                    value = $(item).val().trim();
                    break;
                case 'checkbox':
                    value = $(item).prop('checked');
                    break;
                case 'radio':
                    var name = $(item).attr('name');
                    if (name && $('input[name="' + name + '"]:checked').length > 0) {
                        value = $('input[name="' + name + '"]:checked').val();
                    }
                    else {
                        value = false;
                    }

                    break;
                case 'select':
                    key2 = key + '_fieldName';
                    value2 = $(item).find('option:selected').text().trim();
                    data[key2] = value2;
                    $(item).val() ? value = $(item).val().trim() : value = "";

                    break;
                default:

                    break;
            }

            if (type === "radio") {
                if ($(item).prop('checked')) data[key] = value.trim();
            }
            else if (type === "checkbox") data[key] = value;
            else data[key] = value.trim();
        });
        return data;
    };
    $.fn.getType = function () {
        return this[0].tagName === "INPUT" ? this[0].type.toLowerCase() : this[0].tagName.toLowerCase();
        //return this[0].tagName == "INPUT" || this[0].tagName == "DIV" ? this[0].type.toLowerCase() : this[0].tagName.toLowerCase();
    }

    SetDataTable = function (opts) {

        if (typeof (opts) == "undefined") opts = {};
        if (typeof (opts.url) == "undefined") opts.url = "";
        if (typeof (opts.dom) == "undefined") opts.dom = "Blrtip";
        if (typeof (opts.ordering) == "undefined") opts.ordering = false;
        if (typeof (opts.data) == "undefined") opts.data = {};
        if (typeof (opts.data.requestData) != "function") opts.data.requestData = function () { return {} };
        if (typeof (opts.data.columns) == "undefined") opts.data.columns = [];

        if (typeof (opts.table) == "undefined") opts.table = $('<table></table>');
        if (typeof (opts.callback) != "function") opts.callback = function () { };
        if (typeof (opts.DrawCallback) != "function") opts.DrawCallback = function () { };
        if (typeof (opts.OnDraw) != "function") opts.OnDraw = function () { };
        if (typeof (opts.InfoCallback) != "function") opts.InfoCallback = function () { };
        if (typeof (opts.CreatedRow) != "function") opts.CreatedRow = function () { };
        if (typeof (opts.InitComplete) != "function") opts.InitComplete = function () { };
        if (typeof (opts.FooterCallback) != "function") opts.FooterCallback = function () { };
        if (typeof (opts.BeforeSend) != "function") opts.BeforeSend = function () { };
        var $tbl;
        var $table = $(opts.table);

        var options = {
            "ajax": {
                "url": opts.url,
                "data": function (d) {
                    var _d = {
                        "start": d.start,
                        "length": d.length,
                        "pageSize": d.length,
                        "pageNumber": parseInt(((d.start + d.length) + 1) / d.length),
                        "order": d.order,
                        "search": d.search.value,
                        "keyword": d.search.value
                    }
                    _d = $.extend({}, _d, opts.data.requestData());
                    return _d;
                },
                dataType: 'json',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            },
            dom: opts.dom,
            ordering: opts.ordering,
            "columns": opts.data.columns,
            "initComplete": function (settings, json) {
                if (json.sessionend) {
                    window.location.href = '/';
                } else {
                    opts.InitComplete(json);
                }
            },
            "fnInfoCallback": function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                opts.InfoCallback(iTotal);
                return "Đang xem " + iStart + " đến " + iEnd + " trong tổng số " + iTotal + " mục";
            },
            "fnDrawCallback": function (oSettings) {
                opts.callback();
                opts.DrawCallback(oSettings);
                if (typeof (opts.drawDetail) == "function") {
                    $('tbody tr td.details-control', $table).on('click', function () {
                        $('tr', $table).removeClass('selected');
                        var td = $(this);
                        var tr = td.closest('tr');
                        var row = $tbl.row(tr);


                        if (row.child.isShown()) {
                            $('div.slider', row.child()).slideUp(function () {
                                tr.find('.expand-icon').removeClass('fa-minus-circle').addClass('fa-plus-circle');
                                tr.removeClass('details selected');
                                row.child.hide();
                            });
                        }
                        else {
                            row.child(opts.drawDetail(row.data()), 'no-padding').show();
                            row.child().css('position', 'relative');
                            tr.addClass('details selected');
                            tr.find('.expand-icon').removeClass('fa-plus-circle').addClass('fa-minus-circle');
                            $('div.slider', row.child()).slideDown();
                        }
                    });
                }

                $('#loading').hide();
            },
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                opts.CreatedRow(nRow, aData, iDataIndex);
            },
            "footerCallback": function (row, data, start, end, display) {
                opts.FooterCallback(row, data, start, end, display);
            },
            "preXhr": function (e, settings, data) {
                console.log(e, settings, data)
            }
        }

        if (typeof (opts.data.processData) == "function") options.ajax.dataSrc = function (json) { return opts.data.processData(json); }; //tra ve mang data object
        if (typeof (opts.data.processData2) == "function") options.ajax.dataFilter = function (json) { return opts.data.processData2(json); }; //tra ve chuoi data string, dung de phan trang 

        var defaultOption = {
            "lengthMenu": [[10, 25, 50, 100, 1000], [10, 25, 50, 100, 1000]],
            "processing": true,
            autoFill: true,
            //responsive: true,
            searchHighlight: true,
            dom: "Blrtip",
            buttons: [
                {
                    extend: 'colvis',
                    text: '<i class="fas fa-cog"></i>'
                },
                'copy',
                'excel',
                'pdf',
                'print'
            ],
            "serverSide": true,
            "ordering": false,
            "deferRender": true,
            stateSave: true,
            "language": {
                "url": "/lib/DataTables/Vietnamese.json"
            },
            "ajax": {
                "url": "",
                "contentType": "application/json",
                "dataType": 'json',
                "type": "GET",
                "data": function (d) {
                    return d;
                }
            },
            "columns": [

            ],
            "initComplete": function (settings, json) {

            },
            "fnCreatedRow": function (nRow, aData, iDataIndex) {

            },
            "fnInfoCallback": function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                return "Đang xem " + iStart + " đến " + iEnd + " trong tổng số " + iTotal + " mục";
            },
            "fnDrawCallback": function (oSettings) {

            },
            "footerCallback": function (row, data, start, end, display) {
                console.log(row, data, start, end, display);
            }
        }

        options = $.extend({}, options, opts.data);
        defaultOption = $.extend({}, defaultOption, options);
        defaultOption.ajax.error = function (jqXHR, error, code) {
            console.log(jqXHR);
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 401) {
                window.location.href =  "/User/Login";
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (error === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (error === 'timeout') {
                msg = 'Time out error.';
            } else if (error === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            if (options.showLoading) {
                $('#loading').hide();
            }
        }

        $tbl = $table.on('preXhr.dt', function (e, settings, data) {
            $('#loading').show();
        }).on('error.dt', function (e, settings, techNote, message) {

        }).DataTable(defaultOption);

        if (typeof $tbl.table == 'undefined') $tbl.table = $table;

        return $tbl;
    }

    ConvertDecimalToString = function (value) {
        console.log(value);
        return  value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
    }
    ConvertStringToDecimal = function (value) {
        return parseFloat(value.replaceAll('.', '').replaceAll(',', '.'))
    }
});


// Sửa trong file appsettings.json cả 2 project (4file)
// Sửa base address
// Sửa trong file support