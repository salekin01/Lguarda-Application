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
    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    $("#grid").jqGrid({
        url: protocol + "//" + host + appname + "/RoleAssign/GetRolesAssignForGrid",
        datatype: 'json',
        mtype: 'Post',
        //postData: { application_id: $('#ddl_Application').val() },
        postData: { },
        colNames: ['Role Id', 'Role Name'],
        colModel: [
            { key: true, name: 'Value', Index: 'Value' },
            { key: true, name: 'Text', Index: 'Text' }],
        rowNum: 10000,
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: "0"
        },
        caption: "Role List",
        hiddengrid: false,
        autowidth: false,
        multiselect: true
    });
});




// for selected row purpose: when user select any check box at grid and want to save.
$(document).ready(function () {
    $('#btnsubmitForCreateAssignUserRole').click(function (evt) {
        if ((!$('#user_name').val().trim().length > 0) || ($('#user_id_span').text().trim().length > 0)) {
            evt.preventDefault();
            return;
        }

        evt.preventDefault();
        var element = this;
        var host = $(location).attr('host');
        var protocol = window.location.protocol;
        $.ajax({
            type: "Get",
            url: protocol + "//" + host + appname + "/RoleAssign/GetSelectedRoleFromGrid",
            data: { selected_roleids: function () { return $("#grid").jqGrid('getGridParam', 'selarrrow') } },
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                submitVal = $('#btnsubmitForCreateAssignUserRole').val();
                $(element).append("<input type='hidden' name='command' value='" + submitVal + "' />");
                $(element).closest("form").submit();
            },
            error: function () {
                alert("Error. Please contact support.");
            }
        });
    });
});


//  grid reload for any condition changed
$(function (ready) {
    $('#ddl_Application').change(function () {
        $("#grid").jqGrid('setGridParam', { postData: { application_id: $('#ddl_Application').val() } }).trigger('reloadGrid');
    });
});


//checking if the User name actually exists or not
$(function (ready) {
    $('#user_id').change(function () {
        var host = $(location).attr('host');
        var protocol = window.location.protocol;
        $.ajax({
            type: "GET",
            url: protocol + "//" + host + appname + "/RoleAssign/GetUserInfoByUserId",
            data: { pUSER_ID: $('#user_id').val() },
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $('#user_name').val(data.USER_NM);
                    $('#user_id').val(data.USER_ID);
                    $('span').empty();
                    if (data.AUTH_STATUS_ID == "U") {
                        $("#user_id").after('<span id="user_id_span", style="color:red">User is unauthorized.</span>');
                        $('#user_name').val("");
                        return false;
                    }
                    else if (data.AUTH_STATUS_ID == "D") {
                        $("#user_id").after('<span id="user_id_span", style="color:red">User is Declined.</span>');
                        $('#user_name').val("");
                        return false;
                    }
                    else {
                        $('span').empty();
                    }
                    //$('span').text(' ');  //.empty & .text both works.
                }
                else {
                    $('span').empty();
                    $("#user_id").after('<span id="user_id_span", style="color:red">User does not exists.</span>');
                    $('#user_name').val("");
                    return false;
                }
            },
            error: function () {
                alert("Error. Please contact support.");
            }
        });
    });
});




/*----------------------------------For Edit-------------------------------------------*/

$(document).ready(function () {
    //var app_id = appid;
    var user_id = userid;
    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    $("#gridEdit").jqGrid({
        url: protocol + "//" + host + appname + "/RoleAssign/GetRolesAssignForGrid",
        //url: "http://" + host + "/LguardaApp/RoleAssign/GetRolesAssignForGrid",
        datatype: 'json',
        mtype: 'Post',
        //postData: { application_id: app_id },
        postData: { pUser_id: user_id, pFormAction : "Edit" },
        colNames: ['Role Id', 'Role Name'],
        colModel: [
            { key: true, name: 'Value', Index: 'Value' },
            { key: true, name: 'Text', Index: 'Text' }],
        caption: "Role List",
        autowidth: false,
        multiselect: true,
        rowNum: 10000,
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: "0"
        },
        loadComplete: function (data) {
            var user_id = userid;
            var host = $(location).attr('host');
            $.ajax({
                type: "Get",
                url: protocol + "//" + host + appname + "/RoleAssign/GetRoleIdsByUserID",
                data: { user_id: user_id },
                datatype: "json",
                cache: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var i, count;
                    for (i = 0, count = data.length; i < count; i += 1) {
                        $("#gridEdit").jqGrid('setSelection', data[i], false);
                    }
                }
            });
        }
    });
});



$(document).ready(function () {
    $('#btnsubmitForEditAssignUserRole').click(function (evt) {
        evt.preventDefault();
        var element = this;

        var host = $(location).attr('host');
        var protocol = window.location.protocol;
        $.ajax({
            type: "Get",
            url: protocol + "//" + host + appname + "/RoleAssign/GetUpdatedRoleIdsFromGrid",
            data: { selected_roleids: function () { return $("#gridEdit").jqGrid('getGridParam', 'selarrrow') } },
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                submitVal = $('#btnsubmitForEditAssignUserRole').val();
                $(element).append("<input type='hidden' name='command' value='" + submitVal + "' />");
                $(element).closest("form").submit();
            },
            error: function () {
                alert("Error. Please contact support.");
            }
        });
    });
});

/*----------------------------------For Edit-------------------------------------------*/





/*
$("#m1").click(function () {
    debugger;
    var s = $("#grid").jqGrid('getGridParam', 'selarrrow');
    alert(s);
    return s;

    var allrowData = $("#grid").getRowData(s);
    var id = allrowData.Value;
    var Text = allrowData.Text;
    var indrowData = id.concat(Text);
    alert(indrowData);
});   */