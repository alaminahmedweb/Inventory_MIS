$(function () {

    var site_url = $('.navbar-brand').attr('href');
    $('#txtDueAmount').prop('disabled', true);
    $('#txtBalanceAmount').prop('disabled', true);
    $('#txtLessAmount').keydown(function (e) { if (e.which === 13) { $('#txtPaidAmount').focus(); } });
    $('#txtPaidAmount').keydown(function (e) { if (e.which === 13) { $('#txtRemarks').focus(); } });
    $('#txtRemarks').keydown(function (e) { if (e.which === 13) { $('#btnSave').focus(); } });

    $('#txtCustomerId').on('change', function () {
        var supplierId = $(this).val();
        $('#txtBalanceAmount').empty();
        var json = { supplierId: supplierId };
        $.ajax({
            type: "POST",
            url: site_url + "PartyPayment/GetSupplierList/",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {
                //alert(data);
                $.each(data, function (key, value) {
                    $('#txtBalanceAmount').val(value.TotalDueAmt);
                });
            }
        });
    });


    $('#txtLessAmount').keyup(function () {
        $("#txtDueAmount").val($("#txtBalanceAmount").val() - $("#txtLessAmount").val());
        if ($('#txtDueAmount').val() < 0) {
            $('#txtLessAmount').val(0);
            $('#txtLessAmount').focus().select();
            $("#txtDueAmount").val($("#txtBalanceAmount").val());
        }
    });

    $('#txtPaidAmount').keyup(function () {
        $("#txtDueAmount").val($("#txtBalanceAmount").val() - $("#txtLessAmount").val() - $("#txtPaidAmount").val());
        if ($('#txtDueAmount').val() < 0) {
            $('#txtPaidAmount').val(0);
            $('#txtPaidAmount').focus().select();
            $("#txtDueAmount").val($("#txtBalanceAmount").val() - $("#txtLessAmount").val());
        }
    });

    $("#btnSave").click(function () {
        save();
    });

    function save() {
        var res = validation();
        if (res == true) {
            var object = {
                PartyId: $('#txtCustomerId').val(),
                PaymentAmt: $('#txtPaidAmount').val(),
                LessAmt: $("#txtLessAmount").val(),
                Remarks: $("#txtRemarks").val(),
            };
            //var json = { aModel: object };
            $.ajax({
                type: "POST",
                url: site_url + "PartyPayment/Save/",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(object),
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
        if ($("#txtCustomerId").val() == 0) { $('.CustomerId .select2-selection').css('border-color', 'red'); isValid = false; }
        else { $('.CustomerId .select2-selection').css('border-color', 'lightgrey'); }

        if ($("#txtPaidAmount").val() == '') { $('#txtPaidAmount').css('border-color', 'red'); isValid = false; }
        else { $('#txtPaidAmount').css('border-color', 'lightgrey'); }
        if ($('#txtRemarks').val() == '') { $('#txtRemarks').val('N/A'); }
        if ($("#txtDueAmount").val() < 0) { $('#txtDueAmount').css('border-color', 'red'); isValid = false; }
        return isValid;
    }

});