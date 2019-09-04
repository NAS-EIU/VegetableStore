var BlogController = function () {   
    this.initialize = function () {
        loadData();
        registerEvents();
        registerControls();
    }

    function registerEvents() {
        $('#txtFromDate, #txtToDate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true },
                txtImageM: { required: true },
                txtContentM: { required: true },
                txtTagM: { required: true }
            }
        });
        $('#txtSearchKeyword').keypress(function (e) {
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
            $('#modal-detail').modal('show');
        });
        $("#ddl-show-page").on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.pageIndex = 1;
            loadData(true);
        });

        $('body').on('click', '.btn-view', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            $.ajax({
                type: "GET",
                url: "/Admin/Blog/GetById",
                data: { id: that },
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    var data = response;
                    $('#hidId').val(data.Id);
                    $('#txtNameM').val(data.Title);

                    $('#txtImageM').val(data.Image);
                    $('#txtContentM').val(data.Content);
                    $('#txtTagM').val(data.Tags);
                    $('#ckStatusM').prop('checked', data.Status == 1);
                    $('#ckHotM').prop('checked', data.HotFlag);
                    $('#ckShowHomeM').prop('checked', data.HomeFlag);

                    $('#modal-add-edit').modal('show');
                    tedu.stopLoading();
                  
                },
                error: function (e) {
                    tedu.notify('Has an error in progress', 'error');
                    tedu.stopLoading();
                }
            });
        });

        $('#btnSave').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = $('#hidId').val();
                var customerName = $('#txtCustomerName').val();
                var customerAddress = $('#txtCustomerAddress').val();
                var customerId = $('#ddlCustomerId').val();
                var customerMobile = $('#txtCustomerMobile').val();
                var customerMessage = $('#txtCustomerMessage').val();
                var paymentMethod = $('#ddlPaymentMethod').val();
                var billStatus = $('#ddlBillStatus').val();
                //bill detail

                var billDetails = [];
                $.each($('#tbl-bill-details tr'), function (i, item) {
                    billDetails.push({
                        Id: $(item).data('id'),
                        ProductId: $(item).find('select.ddlProductId').first().val(),
                        Quantity: $(item).find('input.txtQuantity').first().val(),
                        //ColorId: $(item).find('select.ddlColorId').first().val(),
                        //SizeId: $(item).find('select.ddlSizeId').first().val(),
                        BillId: id
                    });
                });

                $.ajax({
                    type: "POST",
                    url: "/Admin/Bill/SaveEntity",
                    data: {
                        Id: id,
                        BillStatus: billStatus,
                        CustomerAddress: customerAddress,
                        CustomerId: customerId,
                        CustomerMessage: customerMessage,
                        CustomerMobile: customerMobile,
                        CustomerName: customerName,
                        PaymentMethod: paymentMethod,
                        Status: 1,
                        BillDetails: billDetails
                    },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function (response) {
                        tedu.notify('Save order successful', 'success');
                        $('#modal-detail').modal('hide');
                        resetFormMaintainance();

                        tedu.stopLoading();
                        loadData(true);
                    },
                    error: function () {
                        tedu.notify('Has an error in progress', 'error');
                        tedu.stopLoading();
                    }
                });
                return false;
            }

        });

        $('#btnAddDetail').on('click', function () {
            var template = $('#template-table-bill-details').html();
            var products = getProductOptions(null);
            //var colors = getColorOptions(null);
            //var sizes = getSizeOptions(null);
            var render = Mustache.render(template,
                {
                    Id: 0,
                    Products: products,
                    //Colors: colors,
                    //Sizes: sizes,
                    Quantity: 0,
                    Total: 0
                });
            $('#tbl-bill-details').append(render);
        });

        $('body').on('click', '.btn-delete-detail', function () {
            $(this).parent().parent().remove();
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





    function loadProducts() {
        return $.ajax({
            type: "GET",
            url: "/Admin/Product/GetAll",
            dataType: "json",
            success: function (response) {
                cachedObj.products = response;
            },
            error: function () {
                tedu.notify('Has an error in progress', 'error');
            }
        });
    }



    function registerControls() {
        CKEDITOR.replace('txtContentM', {});

        //Fix: cannot click on element ck in modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () {
            $(document)
                .off('focusin.bs.modal') // guard against infinite focus loop
                .on('focusin.bs.modal', $.proxy(function (e) {
                    if (
                        this.$element[0] !== e.target && !this.$element.has(e.target).length
                        // CKEditor compatibility fix start.
                        && !$(e.target).closest('.cke_dialog, .cke').length
                        // CKEditor compatibility fix end.
                    ) {
                        this.$element.trigger('focus');
                    }
                }, this));
        };

    }

    function resetFormMaintainance() {
        $('#hidId').val(0);
        $('#txtCustomerName').val('');

        $('#txtCustomerAddress').val('');
        $('#txtCustomerMobile').val('');
        $('#txtCustomerMessage').val('');
        $('#ddlPaymentMethod').val('');
        $('#ddlCustomerId').val('');
        $('#ddlBillStatus').val('');
        $('#tbl-bill-details').html('');
    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/bill/GetAllPaging",
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
                            CustomerName: item.CustomerName,
                            Id: item.Id,
                            PaymentMethod: getPaymentMethodName(item.PaymentMethod),
                            DateCreated: tedu.dateTimeFormatJson(item.DateCreated),
                            BillStatus: getBillStatusName(item.BillStatus)
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