$(function () {
    var site_url = $('.navbar-brand').attr('href');
    var today = moment().format('YYYY-MM-DD');
    $('#RegistrationDate').val(today);
    $('#RegistrationDate').datepicker({
        format: "yyyy-mm-dd",
        todayBtn: "linked",
        orientation: "top left",
        autoclose: true,
        todayHighlight: true,
        toggleActive: true
    });

    $('#DrDOB').val(today);
    $('#DrDOB').datepicker({
        format: "yyyy-mm-dd",
        todayBtn: "linked",
        orientation: "top left",
        autoclose: true,
        todayHighlight: true,
        toggleActive: true
    });



    $("#getItem").click(function () {
        var code = $("#DrCode").val();
        var json = { code: code };
        $.ajax({
            type: "POST",
            url: site_url + "CodeMaintenance/GetAllDrListByCode/",
            //url: '@Url.Action("GetInvestigationListByCode", "CodeMaintenance")',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {
                //alert(data);
                $.each(data, function (key, value) {
                    $('#DrCode').val(value.DrCode);
                    $('#DrName').val(value.DrName);
                    $('#PresentAddress').val(value.PresentAddress);
                    $('#PermanentAddress').val(value.PermanentAddress);
                    $('#Department').val(value.Department);
                    $('#RegistrationDate').val(value.RegistrationDate);
                    $('#DrDOB').val(value.DrDOB);
                    $('#TelephoneNo').val(value.TelephoneNo);
                    $('#Email').val(value.Email);
                    $('#TakeCommission').val(value.TakeCommission);
                    $('#MPOId').val(value.MPOId);
                });
            }
        });
    });


    $('#example').DataTable({
        "scrollY": "200px",
        "scrollCollapse": true,
        "paging": false
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
            Email : {
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
});