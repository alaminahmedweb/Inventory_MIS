$(function () {

    $(".js-example-basic-multiple").select2();

    var site_url = $('.navbar-brand').attr('href');
    var today = moment().format('YYYY-MM-DD');
    $('#txtDateFrom').val(today);
    $('#txtDateFrom').datepicker({
        format: "yyyy-mm-dd",
        todayBtn: "linked",
        orientation: "auto",
        autoclose: true,
        todayHighlight: true,
        toggleActive: true
    });

    $('#txtDateTo').val(today);
    $('#txtDateTo').datepicker({
        format: "yyyy-mm-dd",
        todayBtn: "linked",
        orientation: "auto",
        autoclose: true,
        todayHighlight: true,
        toggleActive: true,

    });

    $("#btnBalanceReport").click(function () {
        GetReport('BalanceReport');
    });

    $("#btnProductReceivingReport").click(function () {
        GetReport('StockReceive');
    });

    $("#btnProductIssueingReport").click(function () {
        GetReport('ProductIssue');
    });

    $("#btnPartyDueList").click(function () {
        GetReport('PartyDueList');
    });

    function GetReport(reportName) {
        var custId = "", itemId;
        var DateFrom = $("#txtDateFrom").val();
        var DateTo = $("#txtDateTo").val();
        if ($("#txtSupplierId").val() != 0) {
            custId = $("#txtSupplierId").val();
        } else {
            custId = "";
        }
        if ($("#txtItemId").val() != 0) {
            itemId = $("#txtItemId").val();
        } else {
            itemId = "";
        }


        var vc = {
            DateTo: $("#txtDateTo").val(),
            DateFrom: $("#txtDateFrom").val(),
            ReportFileName: reportName,
            CustId: custId,
            ItemId: itemId,
        };
        
        if (reportName == "BalanceReport") {
            var  popupWindow = window.open("/Report/ReportViewer/BalanceReport.aspx", "directories=no,height=100,width=100");
        };
        if (reportName == "StockReceive") {
            var popupWindow = window.open("/Report/ReportViewer/StockReceive.aspx?ItemId=" + itemId + "&CustId=" + custId + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "", "directories=no,height=100,width=100");
        };
        if (reportName == "ProductIssue") {
            var popupWindow = window.open("/Report/ReportViewer/ProductIssue.aspx?ItemId=" + itemId + "&CustId=" + custId + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "", "directories=no,height=100,width=100");
        };
        if (reportName == "PartyDueList") {
            var popupWindow = window.open("/Report/ReportViewer/PartyDueList.aspx?CustId=" + custId + "", "directories=no,height=100,width=100");
        };
        


//var json = { aPrint: vc };
        //alert(JSON.stringify(json));
        //return false;
        //$.ajax({
        //    type: "POST",
        //    url: site_url + "MISReport/PrintReport/",
        //    contentType: "application/json; charset=utf-8",
        //    data: JSON.stringify(vc),
        //    success: function (data) {
        //        //alert(data);
        //        if ((data == "Saved Success") || (data == "Update Success")) {
        //            //window.open("/Report/ReportViewer/ReportViewer.aspx", "_blank");
        //            //location.reload(true);
        //        }
        //    }
        //});
    }

});