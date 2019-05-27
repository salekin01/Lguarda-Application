//var appname = "/LguardaApplication";
//var appname = "";

// for iis application name
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
    //jQuery('#grid').jqGrid('GridUnload');
    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    $("#grid").jqGrid({
        url: protocol + "//" + host + appname + "/RoleDefine/GetFunctionsForGrid",
        datatype: 'local',
        mtype: 'Post',
        postData: { app_id: $('#ddl_Application').val(), service_id: $('#ddl_Service').val(), module_id: $('#ddl_Module').val(), item_type: $('#ddl_ItemType').val() },
        colNames: ['FunctionId', 'FunctionName', 'Create', 'Edit', 'Delete', 'Details', 'Index',
                  /* 'OTP', '2FA', 'Soft.', 'Hard.', 'Authorize', 'AuthLevel', 'R-View', 'R-Print', 'R-Gen.', */ 'ApplicationId'],
        colModel: [
            {
                key: true,
                name: 'FUNCTION_ID',
                Index: 'FUNCTION_ID',
                width: 110,
                align: 'left',
            },
            {
                name: 'FUNCTION_NM',
                Index: 'FUNCTION_NM',
                width: 190,
                align: 'left',
            },
            {
                name: 'MAINT_CRT_FLAG',
                index: 'MAINT_CRT_FLAG',
                width: 60,
                align: 'center',
                formatter: 'checkbox',
                editoptions: { value: '1:0' },
                formatoptions: { disabled: false },
            },
        {
            name: 'MAINT_EDT_FLAG',
            index: 'MAINT_EDT_FLAG',
            width: 60,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_DEL_FLAG',
            index: 'MAINT_DEL_FLAG',
            width: 60,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_DTL_FLAG',
            index: 'MAINT_DTL_FLAG',
            width: 60,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_INDX_FLAG',
            index: 'MAINT_INDX_FLAG',
            width: 60,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        /*
        {
            name: 'MAINT_OTP_FLAG',
            index: 'MAINT_OTP_FLAG',
            width: 38,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_2FA_FLAG',
            index: 'MAINT_2FA_FLAG',
            width: 38,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_2FA_SOFT_FLAG',
            index: 'MAINT_2FA_SOFT_FLAG',
            width: 39,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_2FA_HARD_FLAG',
            index: 'MAINT_2FA_HARD_FLAG',
            width: 40,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_AUTH_FLAG',
            index: 'MAINT_AUTH_FLAG',
            width: 66,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
            hidden: true,
        },
        {
            name: 'AUTH_LEVEL',
            index: 'AUTH_LEVEL',
            width: 70,
            align: 'left',
            //editable: true,
            editoptions: { style: "height:30px;" },
            hidden: true,
        },*/

        /*
        {
            name: 'REPORT_VIEW_FLAG',
            index: 'REPORT_VIEW_FLAG',
            width: 50,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'REPORT_PRINT_FLAG',
            index: 'REPORT_PRINT_FLAG',
            width: 50,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'REPORT_GEN_FLAG',
            index: 'REPORT_GEN_FLAG',
            width: 48,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        }, */
        {
            name: 'APPLICATION_ID',
            index: 'APPLICATION_ID',
            width: 70,
            formatter: function () {
                var app_id = $('#ddl_Application').val();
                return app_id;
            },
        }],

        //{ key: false, name: 'Text', Index: 'Text', editable: true }],
        //pager: $('#pager'),
        //rowNum: 4,
        //rowList: [4, 8],
        //height: '100%',
        caption: "Function List",
        viewrecords: true,
        //cellEdit: true,
        autowidth: false,
        multiselect: true,
        //caption: 'Function List',
        //emptyrecords: 'No records to display', 
        rowNum: 10000,

        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: "0"
        },

        loadComplete: function () {
            $("#grid").jqGrid('sortGrid', 'FUNCTION_ID', false, 'asc');
        }


        /*
        onSelectRow: function (id) {
            jQuery('#grid').jqGrid('editRow', id, true);
        },*/



        /*
        onCellSelect: function (rowid, iCol, cellContent, e) {
            grid.restoreRow(lastSelection);
            grid.editRow(rowid, true, null, null, null, null, null);
            lastSelection = rowid;  
        }*/


        /*
        loadComplete: function (data) {
            debugger;
            //$("#grid").css("visibility", "hidden");
            // var columnNames = $("#grid").jqGrid('getGridParam', 'colNames');
            
            var columnNames = $("#grid").jqGrid('getGridParam', 'colModel');
            if (data.rows.length > 0) {
                for (var i = 0; i < data.rows.length; i++) {
                    var rowData = data.rows[i];
                    for (var j = 3; j < columnNames.length; j++){
                        var colname = columnNames[j].name;
                        var colval = data.rows[i][colname];
                        if (colval == 0) {
                            $(columnNames[j]).attr('formatoptions', { disabled: true });

                            //$row.find("input:checkbox").attr("disabled", "disabled");
                            //var checkbox = $("#grid" + rowData.i);//update this with your own grid name
                            //.attr("disabled", false);
                            //$("#grid" + data.rows[i].id).attr("disabled", true);
                            //var checkbox = $("#grid" + columnNames[j]);
                            //checkbox.attr("disabled", false); 
                        }
                    }                    
                }
            }
            $(this).trigger('reloadGrid');

        } */
    }).hideCol("APPLICATION_ID");
    //jQuery('#grid').trigger("reloadGrid");
    //$("#grid").jqGrid('setColProp', 'AUTH_LEVEL', { editable: true });
});




 
$(document).ready(function () {
    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    var pathname = $(location).attr('pathname');
    $("#gridSelectedFunction").jqGrid({
        url: protocol + "//" + host + appname + "/RoleDefine/GetExistingFunctionsForGridByRoleId",
        datatype: 'json',
        mtype: 'Post',
        postData: { role_id: $('#ROLE_ID').val() },

        colNames: ['FunctionId', 'FunctionName', 'Create', 'Edit', 'Delete', 'Details', 'Index',
                  /* 'R-View', 'R-Print', 'R-Gen.', */ ' ', 'ApplicationId'],
        colModel: [
            {
                key: true,
                name: 'FUNCTION_ID',
                Index: 'FUNCTION_ID',
                width: 90,
                align: 'left',
            },
            {
                name: 'FUNCTION_NM',
                Index: 'FUNCTION_NM',
                width: 170,
                align: 'left',
            },
            {
                name: 'MAINT_CRT_FLAG',
                index: 'MAINT_CRT_FLAG',
                width: 60,
                align: 'center',
                formatter: 'checkbox',
                editoptions: { value: '1:0' },
                formatoptions: { disabled: false },
                //     dataEvents: [
                //{
                //    type: 'change', fn: function (e) {
                //        debugger;
                //        var ccode = $('#MAINT_CRT_FLAG').val();
                //        if (ccode == "true") {
                //            $('#MAINT_CRT_FLAG').attr('disabled', false);
                //        }
                //        else {
                //            $('#MAINT_CRT_FLAG').attr('disabled', true);
                //        }
                //    }
                //}]
            },
        {
            name: 'MAINT_EDT_FLAG',
            index: 'MAINT_EDT_FLAG',
            width: 60,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_DEL_FLAG',
            index: 'MAINT_DEL_FLAG',
            width: 60,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_DTL_FLAG',
            index: 'MAINT_DTL_FLAG',
            width: 60,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_INDX_FLAG',
            index: 'MAINT_INDX_FLAG',
            width: 60,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        /*
        {
            name: 'REPORT_VIEW_FLAG',
            index: 'REPORT_VIEW_FLAG',
            width: 50,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'REPORT_PRINT_FLAG',
            index: 'REPORT_PRINT_FLAG',
            width: 50,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'REPORT_GEN_FLAG',
            index: 'REPORT_GEN_FLAG',
            width: 48,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        }, */
        {
            name: 'action',
            index: 'action',
            width: 70,
            formatter: function () {
                return "<input type='button' id='rowid' value='del' class='btn' style='height:15px;' onClick='deleteRecords.call(this, event)' />";
            },

            /*
            //formatter: function (rowId, cellval, colpos, rwdat, _act) {
            formatter: function (cellvalue, options, rowObject) {
                //var rowFunctionId = colpos.FUNCTION_ID.toString();
                //return "<input type='button' id='rowid' value='del' class='btn' style='height:15px;' onClick='deleteRecords(" + options.rowId + ")' />";
            }, */
        },
        {
            name: 'APPLICATION_ID',
            index: 'APPLICATION_ID',
            width: 70,
            align: 'center',
        }],
        caption: "Selected Function List",
        loadonce: true,
        viewrecords: true,
        autowidth: false,
        rowNum: 10000,
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: "0"
        }
    }).hideCol("APPLICATION_ID");
    if (pathname.indexOf("Details") != -1) //string code has "Details" in it
    {
        $("#gridSelectedFunction").hideCol("action");
    }
});

function deleteRecords(e) {
    var rowId = $(this).closest("tr.jqgrow").attr("id");
    $('#gridSelectedFunction').jqGrid('delRowData', rowId);
}

//function deleteRecords(rowId) {
//    debugger;
//    alert(rowId);
//    $('#gridSelectedFunction').jqGrid('delRowData', rowId);
//}



$(document).ready(function () {
    $('#btnsubmitForSelectedRole').click(function () {
        var i, selRowIds = $('#grid').jqGrid("getGridParam", "selarrrow"), n, rowData = [];
        var exists = false;
        var existingSelRowIds = $("#gridSelectedFunction").jqGrid('getCol', 'FUNCTION_ID');
        for (i = 0, n = selRowIds.length; i < n; i++) {
            exists = false;
            for (j = 0; j < existingSelRowIds.length ; j++) {
                if (existingSelRowIds[j] == selRowIds[i]) {
                    exists = true;
                    break;
                }
            }
            if (!exists) {
                rowData[i] = $('#grid').jqGrid("getRowData", selRowIds[i]);
                $("#gridSelectedFunction").jqGrid('addRowData', i + 1, rowData[i]);
            }
        }
        //gridComplete: {
        //    debugger;
        //    var ids = jQuery("#gridSelectedFunction").jqGrid('getDataIDs');
        //    for (var i = 0; i < ids.length; i++) {
        //        var cl = ids[i];
        //        be = "<input style='height:22px;width:20px;' type='button' value='E' onclick=\"jQuery('#gridSelectedFunction').jqGrid('delRowData','" + cl + "');\"  />";
        //        jQuery("#gridSelectedFunction").jqGrid('setRowData', ids[i], { act: be });
        //    }
        //}

        /*
        loadComplete: {
            debugger;
            if (rowData.length > 0) {

                for (var k = 0; k < rowData.length; k++) {
                    jQuery.each(rowData[k], function (col, val) {
                        if (val == 0) {

                            //var ret = $("#gridSelectedFunction").jqGrid('getRowData', id);


                            //$("#gridSelectedFunction" + data.rows[i].cell[j]).css("visibility", "hidden");
                            //$("#gridSelectedFunction" + rowData[k].col).attr("disabled", true);
                            //$(this).jqGrid('setColProp', col, { editable: false });

                            //$("#gridSelectedFunction").jqGrid('setColProp', col, { disabled: true });
                            //$("#gridSelectedFunction").jqGrid('setColProp', col, { formatoptions: { disabled: true } });

                            var cm = $("#gridSelectedFunction").jqGrid('getColProp', col);

                            //some condition to enable or disable editing
                            cm.editable = false;

                            //always call editRow after changing editable property
                            //$('#gridSelectedFunction').jqGrid('editRow', 010101001, {});

                            //set default editable option
                            //cm.editable = true;
                        }
                        return null;
                    });
                }
            }
        } */
    })
});




$(document).ready(function () {
    $('#btnsubmitForCreateRole').click(function (evt) {
        if (!($('#role_descrip').val().trim().length > 0) || ($('#role_name_span').text().trim().length > 0)) {
            evt.preventDefault();
            return;
        }

        evt.preventDefault();                   // <------------------ stop default behaviour of button
        var element = this;

        var rowData = $("#gridSelectedFunction").jqGrid('getRowData');

        var host = $(location).attr('host');
        var protocol = window.location.protocol;
        $.ajax({
            type: "POST",
            url: protocol + "//" + host + appname + "/RoleDefine/GetSelectedFunctionDetailsFromGrid",
            //data: rowData, //salekin commented 26.09.16
            data: JSON.stringify({ selected_rowData: rowData }),
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                submitVal = $('#btnsubmitForCreateRole').val();                                          // <-------- Here, submitVal = Save
                $(element).append("<input type='hidden' name='command' value='" + submitVal + "' />");   // <-------- pass extra param with form
                $(element).closest("form").submit();                                                     //<--------- submit form
            },
            error: function () {
                alert("Error. Please contact support.");
            }
        });
    });
});




/*
$(document).ready(function () {
    $('#btnsubmitForCreateRole').click(function (evt) {
        if (!($('#role_descrip').val().trim().length > 0) || ($('#role_name_span').text().trim().length > 0)) {
            evt.preventDefault();
            return;
        }

        evt.preventDefault();                   // <------------------ stop default behaviour of button
        var element = this;

        var i, selRowIds = $('#gridSelectedFunction').jqGrid("getGridParam", "selarrrow"), n, rowData = [];



        //for (i = 0, n = selRowIds.length; i < n; i++) {
        //    $("#grid").jqGrid('saveRow', selRowIds[i], true);
        //}

        //var rowno = jQuery("#grid").jqGrid('getGridParam', 'savedRow');
        //$("#grid").jqGrid("setCell", selRowIds[i], "AUTH_LEVEL", "New value");

        for (i = 0, n = selRowIds.length; i < n; i++) {
            rowData[i] = $('#gridSelectedFunction').jqGrid("getRowData", selRowIds[i]);
        }

        var host = $(location).attr('host');
        $.ajax({
            type: "POST",
            url: "http://" + host + appname + "/RoleDefine/GetSelectedFunctionDetailsFromGrid",
            data: rowData,
            data: JSON.stringify({ selected_rowData: rowData }),
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                submitVal = $('#btnsubmitForCreateRole').val();                                          // <-------- Here, submitVal = Save
                $(element).append("<input type='hidden' name='command' value='" + submitVal + "' />");   // <-------- pass extra param with form
                $(element).closest("form").submit();                                                     //<--------- submit form
            },
            error: function () {
                alert("Error. Please contact support.");
            }
        });
    });
});  */





//$('#btnsubmitForCreateRole').click(function () {
//    var host = $(location).attr('host');
//    $.ajax({
//        type: "Get",
//        url: "http://" + host + "/RoleDefine/GetSelectedFunctions",
//        //url: "http://" + host + ":6960/RoleDefine/GetSelectedRole",
//        //url: "http://" + host + "/LguardaApp/RoleDefine/GetSelectedRole",
//        data: { selected_roles: function () { return $("#grid").jqGrid('getGridParam', 'selarrrow') } },
//        datatype: "json",
//        contentType: "application/json; charset=utf-8",
//        success: function (data) {
//        }
//    });
//});


$(function (ready) {
    $('#ddl_ItemType').change(function () {
        $("#grid").jqGrid('setGridParam', { datatype: 'json', postData: { app_id: $('#ddl_Application').val(), service_id: $('#ddl_Service').val(), module_id: $('#ddl_Module').val(), item_type: $('#ddl_ItemType').val() } }).trigger('reloadGrid');
    });
});



//$(document).on('change', 'input', function () {
$(function (ready) {
    $('#role_name').change(function () {
        var host = $(location).attr('host');
        var protocol = window.location.protocol;
        $.ajax({
            type: "GET",
            url: protocol + "//" + host + appname + "/RoleDefine/GetRoleInfoByRoleName",
            data: { pROLE_NAME: $('#role_name').val() },
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $('#role_descrip').val(data.ROLE_DESCRIP);
                    $('#ROLE_ID').val(data.ROLE_ID);
                    $('span').empty();
                    if (data.AUTH_STATUS_ID == "U") {
                        $("#role_name ").after('<span id="role_name_span", style="color:red">Role is unauthorized.</span>');
                        return false;
                    }
                    else {
                        $('span').empty();
                    }
                    //$('span').text(' ');  //.empty & .text both works.
                    //document.getElementById("role_name_span").innerHTML = "";
                }
                else {
                    $('span').empty();
                    $("#role_name ").after('<span id="role_name_span", style="color:red">Role name does not exists.</span>');
                    $('#role_descrip').val("");
                    return false;
                }
            }
        });
    });
});


/*----------------------------------For Edit-------------------------------------------*/
$(document).ready(function () {
    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    var role_id = roleid;
    $("#gridEdit").jqGrid({
        url: protocol + "//" + host + appname + "/RoleDefine/GetSelectedFunctionsForGridByRoleId",
        datatype: 'json',
        mtype: 'Post',

        postData: { app_id: $('#ddl_Application').val(), service_id: $('#ddl_Service').val(), module_id: $('#ddl_Module').val(), item_type: $('#ddl_ItemType').val(), role_id: role_id },
        colNames: ['FunctionId', 'FunctionName', 'Create', 'Edit', 'Delete', 'Details', 'Index',
                  /* 'R-View', 'R-Print', 'R-Gen.', */ 'ApplicationId'],
        colModel: [
            {
                key: true,
                name: 'FUNCTION_ID',
                Index: 'FUNCTION_ID',
                width: 110,
                align: 'left',
            },
            {
                name: 'FUNCTION_NM',
                Index: 'FUNCTION_NM',
                width: 190,
                align: 'left',
            },
            {
                name: 'MAINT_CRT_FLAG',
                index: 'MAINT_CRT_FLAG',
                width: 60,
                align: 'center',
                formatter: 'checkbox',
                editoptions: { value: '1:0' },
                formatoptions: { disabled: false },
            },
        {
            name: 'MAINT_EDT_FLAG',
            index: 'MAINT_EDT_FLAG',
            width: 60,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_DEL_FLAG',
            index: 'MAINT_DEL_FLAG',
            width: 60,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_DTL_FLAG',
            index: 'MAINT_DTL_FLAG',
            width: 60,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'MAINT_INDX_FLAG',
            index: 'MAINT_INDX_FLAG',
            width: 60,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        /*
        {
            name: 'REPORT_VIEW_FLAG',
            index: 'REPORT_VIEW_FLAG',
            width: 50,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'REPORT_PRINT_FLAG',
            index: 'REPORT_PRINT_FLAG',
            width: 50,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },
        {
            name: 'REPORT_GEN_FLAG',
            index: 'REPORT_GEN_FLAG',
            width: 48,
            align: 'center',
            formatter: 'checkbox',
            editoptions: { value: '1:0' },
            formatoptions: { disabled: false },
        },   */
        {
            name: 'APPLICATION_ID',
            index: 'APPLICATION_ID',
            width: 70,
            formatter: function () {
                var app_id = $('#ddl_Application').val();
                return app_id;
            },
        }],
        caption: "Function List",
        viewrecords: true,
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
            var role_id = roleid;
            var host = $(location).attr('host');
            $.ajax({
                type: "Get",
                url: protocol + "//" + host + appname + "/RoleDefine/GetFunctionIdsByRoleID",
                data: { role_id: role_id },
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var i, count;
                    for (i = 0, count = data.length; i < count; i += 1) {
                        $("#gridEdit").jqGrid('setSelection', data[i], false);
                    }
                }
            });
            $("#gridEdit").jqGrid('sortGrid', 'FUNCTION_ID', true, 'asc');
        }
    }).hideCol("APPLICATION_ID");;
});

$(function (ready) {
    $('#ddl_ItemType').change(function () {
        $("#gridEdit").jqGrid('setGridParam', { datatype: 'json', postData: { app_id: $('#ddl_Application').val(), service_id: $('#ddl_Service').val(), module_id: $('#ddl_Module').val(), item_type: $('#ddl_ItemType').val(), role_id: $('#ROLE_ID').val() } }).trigger('reloadGrid');
    });
});


$(document).ready(function () {
    $('#btnsubmitForSelectedRole_gridEdit').click(function () {
        var i, selRowIds = $('#gridEdit').jqGrid("getGridParam", "selarrrow"), n, rowData = [];   //get all seleted rowids
        var exists = false;
        var existingSelRowIds = $("#gridSelectedFunction").jqGrid('getCol', 'FUNCTION_ID');       //get all values to a specific column
        for (i = 0, n = selRowIds.length; i < n; i++) {
            exists = false;
            for (j = 0; j < existingSelRowIds.length ; j++) {
                if (existingSelRowIds[j] == selRowIds[i]) {
                    exists = true;
                    break;
                }
            }
            if (!exists) {
                rowData[i] = $('#gridEdit').jqGrid("getRowData", selRowIds[i]);
                $("#gridSelectedFunction").jqGrid('addRowData', i + 1, rowData[i]);     //add rowdata in grid
            }
        }
    })
});

/*
//$(".SetFunctionsByRoleID").click(function () {
$(document).ready(function () {
    //var s = ($(this).data("id"));
    var host = $(location).attr('host');
    $.ajax({
        type: "Get",
        url: "http://" + host + "/RoleDefine/GetFunctionsByRoleID",
        //url: "http://" + host + ":6960/RoleDefine/GetFunctionsByRoleID",
        //url: "http://" + host + "/LguardaApp/RoleDefine/GetFunctionsByRoleID",
        data: { role_id: $('#ROLE_ID').val() },
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var i, count;
            for (i = 0, count = data.length; i < count; i += 1) {
                $("#grid").jqGrid('setSelection', data[i], false);
            }
        }
    });
});*/




$(document).ready(function () {
    $('#btnsubmitForEditRole').click(function (evt) {
        evt.preventDefault();
        var element = this;

        /*
        var i, selRowIds = $('#gridEdit').jqGrid("getGridParam", "selarrrow"), n, rowData = [];
        for (i = 0, n = selRowIds.length; i < n; i++) {
            rowData[i] = $('#gridEdit').jqGrid("getRowData", selRowIds[i]);
        } */

        var rowData = $("#gridSelectedFunction").jqGrid('getRowData');  //get all rows from grid

        var host = $(location).attr('host');
        var protocol = window.location.protocol;
        $.ajax({
            type: "POST",
            url: protocol + "//" + host + appname + "/RoleDefine/GetUpdatedFunctionDetailsFromGrid",
            //url: "http://" + host + "/LguardaApp/RoleDefine/GetSelectedRole",
            data: rowData,
            data: JSON.stringify({ selected_rowData: rowData }),
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                submitVal = $('#btnsubmitForEditRole').val();
                $(element).append("<input type='hidden' name='command' value='" + submitVal + "' />");   // to pass an extra value to the request controller
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


function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}