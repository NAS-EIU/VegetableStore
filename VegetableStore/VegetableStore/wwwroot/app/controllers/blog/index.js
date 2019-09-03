var blogController = function () {


    this.initialize = function () {
        loadData();
        registerEvents();
    }

    function registerEvents() {
        //$('#txtFromDate, #txtToDate').datepicker({
        //    autoclose: true,
        //    format: 'dd/mm/yyyy'
        //});
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true },
                txtContent: { required: true },
                ckStatusM: { required: true }
            }
        });
        $('#txt-search-keyword').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                loadData();
            }
        });
        $("#btn-search").on('click', function () {
            loadData();
        });

        $("#btn-create").on('click', function () {
            resetFormMaintainance();
            $('#modal-add-edit').modal('show');
        });

        $("#ddl-show-page").on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.pageIndex = 1;
            loadData(true);
        });

        //$('body').on('click', '.btn-view', function (e) {
        //    e.preventDefault();
        //    var that = $(this).data('id');
        //    $.ajax({
        //        type: "GET",
        //        url: "/Admin/Bill/GetById",
        //        data: { id: that },
        //        beforeSend: function () {
        //            tedu.startLoading();
        //        },
        //        success: function (response) {
        //            var data = response;
        //            $('#hidId').val(data.Id);
        //            $('#txtCustomerName').val(data.CustomerName);
        //            $('#txtCustomerAddress').val(data.CustomerAddress);
        //            $('#txtCustomerMobile').val(data.CustomerMobile);
        //            $('#txtCustomerMessage').val(data.CustomerMessage);
        //            $('#ddlPaymentMethod').val(data.PaymentMethod);
        //            $('#ddlCustomerId').val(data.CustomerId);
        //            $('#ddlBillStatus').val(data.BillStatus);

        //            var billDetails = data.BillDetails;
        //            if (data.BillDetails != null && data.BillDetails.length > 0) {
        //                var render = '';
        //                var templateDetails = $('#template-table-bill-details').html();

        //                $.each(billDetails, function (i, item) {
        //                    var products = getProductOptions(item.ProductId);

        //                    render += Mustache.render(templateDetails,
        //                        {
        //                            Id: item.Id,
        //                            Products: products,
        //                            Quantity: item.Quantity
        //                        });
        //                });
        //                $('#tbl-bill-details').html(render);
        //            }
        //            $('#modal-detail').modal('show');
        //            tedu.stopLoading();

        //        },
        //        error: function (e) {
        //            tedu.notify('Has an error in progress', 'error');
        //            tedu.stopLoading();
        //        }
        //    });
        //});

        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });

        $("#fileInputImage").on('change', function () {
            var fileUpload = $(this).get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            $.ajax({
                type: "POST",
                url: "/Admin/Upload/UploadImage",
                contentType: false,
                processData: false,
                data: data,
                success: function (path) {
                    $('#txtImageM').val(path);
                    tedu.notify('Tải ảnh thành công', 'success');

                },
                error: function () {
                    tedu.notify('Có lỗi khi tải ảnh', 'error');
                }
            });
        });

        $('#btnSave').on('click', function (e) {
            saveBlog(e);
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            loadDetails(that);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            deleteProduct(that);
        });

        $("#btnExport").on('click', function () {
            var that = $('#hidId').val();
            $.ajax({
                type: "POST",
                url: "/Admin/Bill/ExportExcel",
                data: { billId: that },
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    window.location.href = response;

                    tedu.stopLoading();

                }
            });
        });
    };


    function deleteProduct(id) {
        tedu.confirm('Bạn có muôn xóa ?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Blog/Delete",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    tedu.notify('Xóa thành công', 'success');
                    tedu.stopLoading();
                    loadData();
                },
                error: function (status) {
                    tedu.notify('Has an error in delete progress', 'error');
                    tedu.stopLoading();
                }
            });
        });
    }

    function loadDetails(that) {
        $.ajax({
            type: "GET",
            url: "/Admin/Blog/GetById",
            data: { id: that },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var data = response;
                $('#hidIdM').val(data.Id);
                $('#txtNameM').val(data.Title);
                $('#txtImageM').val(data.Image);
                $('#txtContent').val(data.Content);
                $('#txtTagM').val(data.Tags);
                $('#ckStatusM').prop('checked', data.Status == 1);
                $('#ckShowHomeM').prop('checked', data.HomeFlag);
                $('#modal-add-edit').modal('show');
                tedu.stopLoading();

            },
            error: function (status) {
                tedu.notify('Có lỗi xảy ra', 'error');
                tedu.stopLoading();
            }
        });
    }

    function saveBlog(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidIdM').val();
            var title = $('#txtNameM').val();
            var image = $('#txtImageM').val();
            var content = $('#txtContent').val('');
            var tags = $('#txtTagM').val();
            var status = $('#ckStatusM').prop('checked') == true ? 1 : 0;
            var showHome = $('#ckShowHomeM').prop('checked');

            $.ajax({
                type: "POST",
                url: "/Admin/Blog/SaveEntity",
                data: {
                    Id: id,
                    Title   : title,
                    Image: image,
                    HomeFlag: showHome,
                    Tags: tags,
                    Status: status,
                    Content: content
                },
                dataType: "json",
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    tedu.notify('Cập nhật thành công', 'success');
                    $('#modal-add-edit').modal('hide');
                    resetFormMaintainance();

                    tedu.stopLoading();
                    loadData(true);
                },
                error: function () {
                    tedu.notify('Có lỗi trong quá trình lưu', 'error');
                    tedu.stopLoading();
                }
            });
            return false;
        }
    }

    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtNameM').val('');
        $('#txtImageM').val('');
        $('#txtContent').val('');
        $('#txtTagM').val('');
        $('#ckStatusM').prop('checked', true);
        $('#ckShowHomeM').prop('checked', false);
    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/blog/GetAllPaging",
            data: {
                startDate: $('#txtFromDate').val(),
                endDate: $('#txtToDate').val(),
                keyword: $('#txtSearchKeyword').val(),
                page: tedu.configs.pageIndex,
                pageSize: tedu.configs.pageSize
            },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var template = $('#table-template').html();
                var render = "";
                if (response.RowCount > 0) {
                    $.each(response.Results, function (i, item) {
                        render += Mustache.render(template, {
                            Title: item.Title,
                            Id: item.Id,
                            DateModified: item.DateModified,
                            Image: item.Image == null ? '<img src="/admin-side/images/user.png" width=25' : '<img src="' + item.Image + '" width=25 />',
                            Status: tedu.getStatus(item.Status)
                            //BillStatus: getBillStatusName(item.BillStatus)
                        });
                    });
                    $("#lbl-total-records").text(response.RowCount);
                    if (render != undefined) {
                        $('#tbl-content').html(render);

                    }
                    wrapPaging(response.RowCount, function () {
                        loadData();
                    }, isPageChanged);


                }
                else {
                    $("#lbl-total-records").text('0');
                    $('#tbl-content').html('');
                }
                tedu.stopLoading();
            },
            error: function (status) {
                console.log(status);
            }
        });
    };

    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / tedu.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, p) {
                tedu.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }
}