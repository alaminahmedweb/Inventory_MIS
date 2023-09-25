//$(function() {
//    $("#Project").change(function() {
//        var projectId = $("#Project").val();
//        $("#SubProject").empty();
//        var json = { projectId: projectId };
//        $.ajax({
//            type: "POST",
//            url: '@Url.Action("GetSubDepartmentByProjectId", "CodeMaintenance")',
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify(json),
//            success: function(data) {
//                //alert(data);
//                $.each(data, function(key, value) {
//                    $("#SubProject").append('<option value=' + value.DepartmentId + '>' + value.DepartmentName + '</option>');
//                });
//            }

//        });
//    });
//});

$(function () {

    var site_url = $('.navbar-brand').attr('href');

    $('#Project').on('change', function () {
        var projectId = $(this).val();
        $('#SubProject').empty();
        var json = { projectId: projectId };
        $.ajax({
            type: "POST",
            url: site_url + "CodeMaintenance/GetSubDepartmentByProjectId/",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {
                //alert(data);
                $.each(data, function (key, value) {
                    $('#SubProject').append('<option value=' + value.DepartmentId + '>' + value.DepartmentName + '</option>');
                });
            }
        });
    });

    $("#SubProject").change(function () {
        var projectId = $("#Project").val();
        $("#SubProjectDept").empty();
        var json = { projectId: projectId };
        $.ajax({
            type: "POST",
            url: site_url + "CodeMaintenance/GetAllSubProjectDepartmentByProjectId/",
            //'@Url.Action("GetAllSubProjectDepartmentByProjectId", "CodeMaintenance")',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {
                //alert(data);
                $.each(data, function (key, value) {
                    $("#SubProjectDept").append('<option value=' + value.DepartmentId + '>' + value.DepartmentName + '</option>');
                });
            }
        });
    });

    

    $("#getItem").click(function () {
        var code = $("#Code").val();
        var json = { code: code };
        $.ajax({
            type: "POST",
            url: site_url + "CodeMaintenance/GetInvestigationListByCode/",
            //url: '@Url.Action("GetInvestigationListByCode", "CodeMaintenance")',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {
                //alert(data);
                $.each(data, function (key, value) {
                    $('#Name').val(value.Name);
                    $('#Project').val(value.Project);
                    $('#SubProject').val(value.SubProject);
                    $('#SubProjectDept').val(value.SubProjectDept);
                    $('#Price').val(value.Price);
                    $('#LessFixedAmount').val(value.LessFixedAmount);
                    $('#RefFee').val(value.RefFee);
                    $('#RefFeeType').val(value.RefFeeType);
                    $('#NormalValue').val(value.NormalValue);
                    $('#ReportFileName').val(value.ReportFileName);
                    $('#ReportDeliveryAfter').val(value.ReportDeliveryAfter);
                });
            }
        });
    });



    $('#example').DataTable({
        "scrollY": "200px",
        "scrollCollapse": true,
        "paging": false
    });

    $("#investigationChartSave").validate({
        rules:
        {
            Code: {
                required: true,
                minlength: 4
            },
            Name: "required",
            NormalValue: "required",
            ReportFileName: "required",
            Price: {
                required: true,
                number: true
            },
            LessFixedAmount: {
                required: true,
                number: true
            },
            RefFee: {
                required: true,
                number: true
            },
            ReportDeliveryAfter: {
                required: true,
                number: true
            }
        },
        message: {
            Code: {
                required: "This Field is Required",
                minlength: "Code must be at least 4 digit"
            },
            Name: "Please Fill Investigation Name ",
            NormalValue: "Please Fill Normal Value ",
            ReportFileName: "Please Fill Report File Name",
            Price: {
                required: true,
                number: true
            },
            LessFixedAmount: {
                required: "This Field is required",
                number: "Must Be a Number"
            },
            RefFee: {
                required: "This Field is required",
                number: "Must Be a Number"
            },
            ReportDeliveryAfter: {
                required: "This Field is required",
                number: "Must Be a Number"
            }
        }
    });

});
