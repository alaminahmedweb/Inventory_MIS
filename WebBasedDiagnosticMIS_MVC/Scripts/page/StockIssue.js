$(function () {

    $('#Unit').prop('disabled', true);
    $('#UnitPrice').prop('disabled', true);
    $('#Balance').prop('disabled', true);
    $('#TotalPrice').prop('disabled', true);

    $('#txtReceiptNo').keydown(function (e) {
        if (e.which === 13) {
            $('#txtSupplierId').focus();
        }
    });

    $('#txtReceiptNo').keydown(function (e) { if (e.which === 13) { $('#ProductId').focus(); } });
    $("#ProductId").on("select2:close", function () { $("#txtQuantity").focus(); });
    $('#txtQuantity').keydown(function (e) {
        if (e.which === 13) {
            $('#btnAdd').focus();
        }
    });
    //loadData();

    var site_url = $('.navbar-brand').attr('href');
    var today = moment().format('YYYY-MM-DD');
    $('#txtInvoiceDate').val(today);
    $('#txtInvoiceDate').datepicker({
        format: "yyyy-mm-dd",
        todayBtn: "linked",
        orientation: "top left",
        autoclose: true,
        todayHighlight: true,
        toggleActive: true
    });

    $('#txtReceiptDate').val(today);
    $('#txtReceiptDate').datepicker({
        format: "yyyy-mm-dd",
        todayBtn: "linked",
        orientation: "top left",
        autoclose: true,
        todayHighlight: true,
        toggleActive: true
    });

    $("#btnSave").click(function () {
        save();
    });

    $("#btnAdd").click(function () {
        add();
    });

    function add() {
        var res = validate();
        if (res == false) {
            return false;
        }
        
        var itemId = $('#ProductId').val();
        var pdName = $("#ProductId option:selected").text();
        var unit = $('#Unit').val();
        var unitPrice = $('#UnitPrice').val();
        var quantity = $('#txtQuantity').val();
        var totalPrice = unitPrice * quantity;
        var balance = $("#Balance").val();
        if (quantity > balance) {
            alert('More than balance qty.');
            return false;
        }
        var html = '';
        if ($('#tbody').html != '') {
            html = $('#tbody').html();
        } else {
            html = '';
        }
        html += '<tr>';
        html += '<td>' + itemId + '</td>';
        html += '<td>' + pdName + '</td>';
        html += '<td>' + unit + '</td>';
        html += '<td>' + unitPrice + '</td>';
        html += '<td>' + quantity + '</td>';
        html += '<td>' + totalPrice + '</td>';
        html += '<td><a href="#" class="deleteRow"><span class="glyphicon glyphicon-trash"></span</a></td>';
        html += '</tr>';
        $('#tbody').html(html);
        $('.entry-form input').val("");
        $("#TotalPrice").val(sumColumn(6));
        $("#txtTotalProduct").val(sumOfProduct(1));
        return false;
    }

    function sumColumn(index) {
        var total = 0;
        $("table #tbody td:nth-child(" + index + ")").each(function () {
            total += parseInt($(this).text(), 10) || 0;
        });
        return total;
    }

    function sumOfProduct(index) {
        var total = 0;
        $("table #tbody td:nth-child(" + index + ")").each(function () {
            total += 1;
        });
        return total;
    }


    function validate() {
        var isValid = true;
        var table = $("table #tbody");
        var itemId = $("#ProductId").val();
        table.find('tr').each(function (i) {
            var $tds = $(this).find('td'),
                tableValue = $tds.eq(0).text();
            if (tableValue == itemId) {
                alert("Already Exist");
                isValid = false;
            }
        });
        if ($('#txtQuantity').val() == 0) { isValid = false; }
        return isValid;
    }



    $('#ProductId').on('change', function () {
        var productId = $(this).val();
        $('#Unit').empty();
        $('#UnitPrice').empty();
        var json = { productId: productId };
        $.ajax({
            type: "POST",
            url: site_url + "StockIssue/GetProductListByIdOnlyAvailable/",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {
                //alert(data);
                $.each(data, function (key, value) {
                    $('#Unit').val(value.Unit);
                    $('#UnitPrice').val(value.UnitPrice);
                    $('#Balance').val(value.Balance);
                });
                $("#txtQuantity").focus();
            }
        });
    });


    ////Form Validation
    $("#drInfoSave").validate({
        rules: {
            DrCode: "required",
            DrName: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            PresentAddress: "required",
            RegistrationDate: {
                required: true,
                date: true
            },
            DrDOB: {
                required: true,
                date: true
            },
            PermanentAddress: "required",
            TelephoneNo: "required"
        },
        messages: {
            DrCode: "Please enter Dr Code",
            DrName: {
                required: "Dr Name is required"
            },
            Email: {
                required: "Email is required",
                email: "Email is not valid."
            },
            PresentAddress: "Present Address is required",
            RegistrationDate: {
                required: "Registration Date is required",
                date: "Date is not valid"
            },
            DrDOB: {
                required: "Dr DOB Date is required",
                date: "Date is not valid"
            },
            PermanentAddress: "Permanent Address is required",
            TelephoneNo: "Telephone No is Required"
        },
    });


    function loadData() {
        $("#table-example").DataTable({
            searching: true,
            destroy: true,
            bAutoWidth: false,
            aaSorting: [],
            "ajax": {
                //url: '@Url.Action("GetStockReceiveInvoiceList", "StockReceiveNewItem")',
                type: "GET",
                datatype: "json",
            },
            "columns": [
                { "data": "Id", "name": "Id", visible: false },
                { "data": "SupplierName", "name": "SupplierName" },
                { "data": "InvoiceDate", "name": "InvoiceDate", "render": function (data) { return (window.ToJavaScriptDate(data)); } },
                { "data": "InvoiceNo", "name": "InvoiceNo" },
                { "data": "InvoiceValue", "name": "InvoiceValue" },
                { "data": "TotalProduct", "name": "TotalProduct" },
                { "data": "BrandName", "name": "BrandName" },
                {
                    "data": "Id",
                    width: "100px",
                    "render": function (data) {
                        return "<a href='javascript:;' class='md-trigger' onclick='return GetInvoicePrint(" + data + ")'><span class='glyphicon glyphicon-edit'></span></a>";
                    }
                }
            ]
        });
    }


    function GetInvoicePrint(parameter) {
        $.ajax({
            url: '@Url.Action("GetStockReceiveInvoicePrint", "StockReceiveNewItem")',
            type: "GET",
            datatype: "json",
            data: { 'id': parameter },
            success: function (data) {
                window.open("/Report/ReportViewer/ReportViewer.aspx", "_blank");
            },
        });
    }



    $("table #tbody").on("click", "a.deleteRow", function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
        $(function () {
            $("#TotalPrice").val(sumColumn(7));
        });
        return false;
    });


    function save() {
        var res = validation();
        if (res == true) {
            var vouchers = [];
            var table = $('table #tbody');
            table.find('tr').each(function () {
                var $tds = $(this).find('td'),
                    titemId = $tds.eq(0).text(),
                    titemName = $tds.eq(1).text(),
                    tunit = $tds.eq(2).text(),
                    tunitPrice = $tds.eq(3).text(),
                    ttxtQuantity = $tds.eq(4).text(),
                    ttotalPrice = $tds.eq(5).text();
                var voucher = {
                    ReceiptNo: $("#txtReceiptNo").val(),
                    ReceiptDate: $("#txtReceiptDate").val(),
                    PartyId: $("#txtSupplierId").val(),
                    InvoiceValue: $("#TotalPrice").val(),
                    Remarks: $("#txtRemarks").val(),
                    TotalProduct: $("#txtTotalProduct").val(),
                    ProductId: titemId,
                    ProductName: titemName,
                    Unit: tunit,
                    UnitPrice: tunitPrice,
                    Quantity: ttxtQuantity,
                    TotalPrice: ttotalPrice
                };
                vouchers.push(voucher);
            });

            var json = { aModel: vouchers };
            //alert(JSON.stringify(json));
            //return false;
            $.ajax({
                type: "POST",
                url: site_url + "StockIssue/Save/",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(json),
                success: function (data) {
                    alert(data);
                    if ((data == "Saved Success") || (data == "Update Success")) {
                        //window.open("/Report/ReportViewer/ReportViewer.aspx", "_blank");
                        location.reload(true);
                    }
                }
            });

            return false;
        }
        else {
            alert("Please fill required field");
        }
        return false;
    }

    function validation() {
        var isValid = true;
        
        if ($('#tbody').html() == '') {
            alert('Need To add a product.');
            isValid = false;
        }
        
        if ($('#txtReceiptNo').val() == '') { $('#txtReceiptNo').val('N/A'); }
        if ($('#txtRemarks').val() == '') { $('#txtRemarks').val('N/A'); }

        return isValid;
    }

}
);