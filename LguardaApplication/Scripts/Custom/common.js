

//var appname = "/LguardaApplication";
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


//var hostname = $(location).attr('hostname');
//var apppath = $(location).attr('apppath');

$('#ddl_Application').change(function () {
    $("#ddl_Service").empty();
    $('#ddl_Service').append('<option value="0">---- Service ----</option>');
    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    $.ajax({
        type: "GET",
        url: protocol + "//" + host + appname + "/Common/DDLServicLoadbyAppId",
        //url: "http://" + host + ":6960/Common/DDLServicLoadbyAppId",              //For debug
        //url: "http://" + host + "/LguardaApp/Common/DDLServicLoadbyAppId",      //For localhost + ip
        //url: "../Common/DDLServicLoadbyAppId",
        datatype: "json",
        data: { app_id: $('#ddl_Application').val() },
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data, function (i, element) {
                $('#ddl_Service').append($('<option></option>').val(element.Value).html(element.Text));
            });
        }
    });
});


$('#ddl_Service').change(function () {
    $("#ddl_Module").empty();
    $('#ddl_Module').append('<option value="0">---- Module ----</option>');
    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    $.ajax({
        type: "GET",
        url: protocol + "//" + host + appname + "/Common/DDLModuleLoadbyServiceId",
        datatype: "json",
        data: { service_id: $('#ddl_Service').val(), app_id: $('#ddl_Application').val() },
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data, function (i, element) {
                $('#ddl_Module').append($('<option></option>').val(element.Value).html(element.Text));
            });
        }
    });
});


$('#ddl_Module').change(function () {
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
        }
    });
});


$('#ddl_User_Classification').change(function () {
    $("#ddl_User_Area").empty();
    $('#ddl_User_Area').append('<option value="0">---Select User Area---</option>');
    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    $.ajax({
        type: "GET",
        url: protocol + "//" + host + appname + "/Common/DDLUserAreaLoadbyUserClassification",
        data: { classification_id: $('#ddl_User_Classification').val() },
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data, function (i, element) {
                $('#ddl_User_Area').append($('<option></option>').val(element.Value).html(element.Text));
            });
        }
    });

    /*
    var User_cls_id = $('#ddl_User_Classification').val();
    if (User_cls_id == 1) {
        $('#temp').hide();
        $('#EmployeeID').show();
    }
    else {
        $('#EmployeeID').hide();
    }
    if (User_cls_id == 2) {
        $('#temp').hide();
        $('#CustomerID').show();
    }
    else {
        $('#CustomerID').hide();
    }
    if (User_cls_id == 3) {
        $('#temp').hide();
        $('#AgentID').show();
    }
    else {
        $('#AgentID').hide();
    }
    if (User_cls_id == 4) {
        $('#temp').hide();
        $('#PolicyID').show();
    }
    else {
        $('#PolicyID').hide();
    }
    if ((User_cls_id != 1) & (User_cls_id != 2) & (User_cls_id != 3) & (User_cls_id != 4)) {
        $('#temp').show();
    }
    */
});

$('#ddl_Work_Hour_Type').change(function () {
    var User_Work_Hour_id = $('#ddl_Work_Hour_Type').val();
    if (User_Work_Hour_id == 1) {
        $('#StartTime').show();
        $('#EndTime').show();
    }
    else {
        $('#StartTime').hide();
        $('#EndTime').hide();
    }
});

/*
$(document).ready(function () {
    var User_cls_id = $('#ddl_User_Classification').val();
    if (User_cls_id == 1) {
        $('#temp').hide();
        $('#EmployeeID').show();
    }
    else {
        $('#EmployeeID').hide();
    }
    if (User_cls_id == 2) {
        $('#temp').hide();
        $('#CustomerID').show();
    }
    else {
        $('#CustomerID').hide();
    }
    if (User_cls_id == 3) {
        $('#temp').hide();
        $('#AgentID').show();
    }
    else {
        $('#AgentID').hide();
    }
    if (User_cls_id == 4) {
        $('#temp').hide();
        $('#PolicyID').show();
    }
    else {
        $('#PolicyID').hide();
    }
    if ((User_cls_id != 1) & (User_cls_id != 2) & (User_cls_id != 3) & (User_cls_id != 4)) {
        $('#temp').show();
    }
}); */


$('#ddl_Auth_type').change(function () {
    var Auth_type = $('#ddl_Auth_type').val();
    if (Auth_type == 2) {
        $('#Two_FA_Type').show();
    }
    else {
        $('#Two_FA_Type').hide();
    }
});


/*Function Setup Start*/
$('#2FA_FLAG').change(function () {
    var TwoFA_FLAG = this.checked;
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

$('#AUTH_FLAG').change(function () {
    var AUTH_FLAG = this.checked;
    if (AUTH_FLAG) {
        $('#Auth_Level').attr("readonly", false);
    }
    else {
        $('#Auth_Level').attr("readonly", true);
        $('#Auth_Level').val("");
    }
});
/*Function Setup End*/





/*Role Define Start*/
//$(document).on('change', 'input', function () {
$('#role_name').change(function (e) {
    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    $.ajax({
        type: "GET",
        url: protocol + "//" + host + appname + "/RoleDefine/Get_RoleDescription",
        data: { pROLE_NAME: $('#role_name').val() },
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data) {
                $('#role_descrip').val(data);
                $('span').empty();
                //$('span').text(' ');  //.empty & .text both works.
            }
            else {
                $("#role_name ").after('<span style="color:red">Role name does not exists.</span>');
                return false;
            }
        }
    });
});

/*Role Define End*/








//For Learning
//$('#btn_User_Setup_Profile_Details').click(function () {
//    document.location = '@Url.Action("MyAction","MyController")';
//});
