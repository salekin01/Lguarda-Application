//var appname = "/LguardaApplication";
//var appname = "";

//var appname = (function (p) {
//    var s = p.split("/").reverse();
//    s.splice(0, 2);
//    return s.reverse().join("/");
//})(location.pathname)


var appname = (function (p) {
    var port = $(location).attr('port');
    if ($.isNumeric(port)) {
        var s = p.split("/").reverse();
        var length = s.length - 1;
        //s.splice(0, 2);
        s.splice(0, length);
        //alert("port");
        return s.reverse().join("/");
    }
    else {
        var pathname = $(location).attr('pathname');
        var s = p.split("/");
        var app_name1 = s[1];
        var app_name = "/" + app_name1;
        //alert("without port");
        return app_name;
    }
})(location.pathname);


$(document).ready(function () {
    var TwoFA_FLAG = $('#2FA_FLAG').is(':checked');;
    if (TwoFA_FLAG) {
        $('#2FA_HARD').show();
        $('#2FA_SOFT').show();
    }
    else {
        $('#2FA_HARD').hide();
        $('#2FA_SOFT').hide();
        $('#2FA_HARD').attr("checked", false);
        $('#2FA_SOFT').attr("checked", false);
    }
});

$(document).ready(function () {
    var AUTH_FLAG = $('#AUTH_FLAG').is(':checked');;
    if (AUTH_FLAG) {
        $('#Auth_Level').attr("readonly", false);
    }
    else {
        $('#Auth_Level').attr("readonly", true);
        $('#Auth_Level').val("");
    }
});

$(document).ready(function () {
    var pre_selected_serviceid = serviceid;
    var ddlApplication = $('#ddl_Application').val();
    if (ddlApplication.length > 0 && ddlApplication != 0) {
        $("#ddl_Service").empty();
        $('#ddl_Service').append('<option value="0">---- Service ----</option>');
        var host = $(location).attr('host');
        var protocol = window.location.protocol;
        $.ajax({
            type: "GET",
            url: protocol + "//" + host + appname + "/Common/DDLServicLoadbyAppId",
            datatype: "json",
            data: { app_id: $('#ddl_Application').val() },
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.each(data, function (i, element) {
                    $('#ddl_Service').append($('<option></option>').val(element.Value).html(element.Text));
                });
                if (pre_selected_serviceid.length > 0 && pre_selected_serviceid != 0) {
                    $('#ddl_Service').val(pre_selected_serviceid).attr("selected", "selected");
                }
            }
        });
    }
});

$(document).ready(function () {
    var pre_selected_serviceid = serviceid;
    var pre_selected_moduleid = moduleid;
    var pre_selected_appid = appid;
    if (pre_selected_serviceid.length > 0 && pre_selected_serviceid != 0)
    {
        $("#ddl_Module").empty();
        $('#ddl_Module').append('<option value="0">---- Module ----</option>');
        var host = $(location).attr('host');
        var protocol = window.location.protocol;
        $.ajax({
            type: "GET",
            url: protocol + "//" + host + appname + "/Common/DDLModuleLoadbyServiceId",
            datatype: "json",
            data: { service_id: pre_selected_serviceid, app_id: pre_selected_appid },
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.each(data, function (i, element) {
                    $('#ddl_Module').append($('<option></option>').val(element.Value).html(element.Text));
                });
                if (pre_selected_moduleid.length > 0 && pre_selected_moduleid != 0)
                {
                    $('#ddl_Module').val(pre_selected_moduleid).attr("selected", "selected");
                }
            }
        });
    }
});

$(document).ready(function () {
    var pre_selected_moduleid = moduleid;
    var pre_selected_itemtype = itemtype;
    if (pre_selected_moduleid.length > 0 && pre_selected_moduleid != 0)
    {
        $("#ddl_ItemType").empty();
        $('#ddl_ItemType').append('<option value="0">---- Item Type ----</option>');
        var host = $(location).attr('host');
        var protocol = window.location.protocol;
        $.ajax({
            type: "GET",
            url: protocol + "//" + host + appname + "/Common/DDLItemType",
            data: {},
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.each(data, function (i, element) {
                    $('#ddl_ItemType').append($('<option></option>').val(element.Value).html(element.Text));
                });
                if (pre_selected_itemtype.length > 0 && pre_selected_itemtype != 0)
                {
                    $('#ddl_ItemType').val(pre_selected_itemtype).attr("selected", "selected");
                }
            }
        });
    }
});
