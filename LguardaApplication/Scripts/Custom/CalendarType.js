$(document).ready(function () {

    debugger;
    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    $.ajax({
        type: "GET",
        url: protocol + "//" + host + appname + "/Common/GetAllCalendarType",
        datatype: "json",
        //  data: { },
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data, function (i, element) {
                $('#ddl_Calendar').append($('<option></option>').val(element.Value).html(element.Text));
            });
        }
    });
});
