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

/*var appname = (function (p) {
    var s = p.split("/").reverse();
    s.splice(0, 2);
    return s.reverse().join("/");
})(location.pathname)*/

//$(document).ready(function () {
//    var host = $(location).attr('host');    
//    $("#grid").jqGrid({
//        //shrinkToFit: false,
//        //forceFit: false,
//        height: 200,
//        scollable: true,
//        url: "http://" + host + appname + "/NFT_Authorization/IndexWithFunctionID",
//        datatype: 'json',
//        mtype: 'post',
//        postData: { functionID: $('#ddl_Function').val() },

//        colNames: [
//            'LOG ID', 'FUNCTION ID', 'TABLE NAME'
//            , 'AUTH STATUS ID', 'AUTH LEVEL MAX', 'AUTH LEVEL PENDING'
//            , 'MAKE BY', 'MAKE DT', 'REASON DECLINE'
//        ],
//        colModel: [
//            {
//                key: true,
//                name: 'LOG_ID',
//                Index: 'LOG_ID',
//                //width: 10,
//                width:60,
//                align: 'left',
//                editable: false,
//                hidden: true
//            },
//            {
//                name: 'FUNCTION_ID',
//                Index: 'FUNCTION_ID',
//                width: 90,
//                align: 'center',
//                editable: false
//            },
//            {
//                name: 'TABLE_NAME',
//                Index: 'TABLE_NAME',
//                width: 200,
//                align: 'left',
//                editable: false,
//                hidden: true
//            },
//            {
//                name: 'AUTH_STATUS_ID',
//                Index: 'AUTH_STATUS_ID',
//                width: 150,
//                align: 'center',
//                editable: false,
//                hidden: true
//            },
//            {
//                name: 'AUTH_LEVEL_MAX',
//                Index: 'AUTH_LEVEL_MAX',
//                width: 120,
//                align: 'center',
//                editable: false
//            },
//            {
//                name: 'AUTH_LEVEL_PENDING',
//                Index: 'AUTH_LEVEL_PENDING',
//                width: 130,
//                align: 'center',
//                editable: false
//            },
//           {
//                name: 'MAKE_BY',
//                Index: 'MAKE_BY',
//                width: 80,
//                align: 'center',
//                editable: false
//            },
//            {
//                name: 'MAKE_DT',
//                Index: 'MAKE_DT',
//                width: 80,
//                align: 'left',
//                editable: false,

//                sorttype: 'date',
//                formatter: "date",
//                formatoptions:
//                    { newformat: "d-m-Y H:i:s" }
//            },
//            {
//                name: 'REASON_DECLINE',
//                Index: 'REASON_DECLINE',
//                width: 100,
//                align: 'left',
//                editable: false,
//                hidden: true
//            }
//        ],

//        caption: "Authorization List",        
//        multiselect: false,
//        maxHeight: 50,       
//        viewrecords: true,       
//        //emptyrecords: 'No data found.', //not working
//        emptyDataText: 'No data found.',

//        jsonReader: {
//            repeatitems: false,
//            id: "FUNCTION_ID", // 
//            root: function (obj) { return obj; },
//            page: function (obj) { return 1; },
//            total: function (obj) { return 1; },
//            records: function (obj) { return obj.length; }
//        }
//    });

//    $("#grid").jqGrid('setGridParam', { ondblClickRow: function(rowid, iRow, iCol, e) {
//        //ShowDetails(rowid, iRow, iCol);
//        ShowLogVal(rowid, iRow, iCol);
//        }
//    });

//    /*********************History Grid*******************************/
//    $("#gridHistory").jqGrid({
//        url: "http://" + host + appname + "/NFT_Authorization/History",
//        datatype: 'json',
//        mtype: 'post',
//        postData: { pLOG_ID : "" },

//        colNames: [
//            'LOG ID', 'LOG_DETAILS ID', 'AUTH_OR_DEC_BY'
//            , 'AUTH_OR_DEC_DT', 'AUTH_LEVEL', 'AUTH_STATUS_ID'
//        ],
//        colModel: [
//            {
//                name: 'LOG_ID',
//                Index: 'LOG_ID',
//                width: 10,
//                align: 'left',
//                editable: false,
//                hidden: true
//            },
//            {
//                name: 'LOG_DETAILS_ID',
//                Index: 'LOG_DETAILS_ID',
//                width: 80,
//                align: 'left',
//                editable: false,
//                hidden: true
//            },
//            {
//                name: 'AUTH_OR_DEC_BY',
//                Index: 'AUTH_OR_DEC_BY',
//                width: 200,
//                align: 'center',
//                editable: false
//            },
//            {
//                name: 'AUTH_OR_DEC_DT',
//                Index: 'AUTH_OR_DEC_DT',
//                width: 200,
//                align: 'center',
//                editable: false,

//                sorttype: 'date',
//                formatter: "date",
//                formatoptions:
//                    { newformat: "d-m-Y H:i:s" }
//            },
//            {
//                name: 'AUTH_LEVEL',
//                Index: 'AUTH_LEVEL',
//                width: 200,
//                align: 'center',
//                editable: false
//            },
//            {
//                name: 'AUTH_STATUS_ID',
//                Index: 'AUTH_STATUS_ID',
//                width: 200,
//                align: 'center',
//                editable: false
//            }
//        ],

//        viewrecords: true,
//        emptyrecords: 'No data found.',

//        caption: "Authorization History",
//        autowidth: false,
//        maxHeight: 50,

//        jsonReader: {
//            repeatitems: false,
//            id: "LOG_ID", // 
//            root: function (obj) { return obj; },
//            page: function (obj) { return 1; },
//            total: function (obj) { return 1; },
//            records: function (obj) { return obj.length; }
//        },

//        gridComplete: function () {
//            var recs = parseInt($("#gridHistory").getGridParam("records"), 10);
//            if (isNaN(recs) || recs == 0) {
//                $("#gridWrapper").hide();
//            }
//            else {
//                $('#gridWrapper').show();
//            }
//        }
//    });
//});

$(document).ready(function () {
    var host = $(location).attr('host');
    var protocol = window.location.protocol;

    $("#grid").jqGrid({
        //shrinkToFit: false,
        //forceFit: false,
        height: 200,
        scollable: true,
        url: protocol + "//" + host + appname + "/Authorization/IndexWithFunctionID",
        datatype: 'json',
        mtype: 'post',
        postData: { functionID: $('#ddl_Function').val() },

        colNames: [
            'LOG ID', 'FUNCTION ID', 'TABLE NAME'
            , 'AUTH STATUS ID', 'AUTH LEVEL MAX', 'AUTH LEVEL PENDING'
            , 'MAKE BY', 'MAKE DT', 'REASON DECLINE', 'TABLE_PK_COL_NM', 'TABLE_PK_COL_VAL'
        ],
        colModel: [
            {
                key: true,
                name: 'LOG_ID',
                Index: 'LOG_ID',
                //width: 10,
                width: 60,
                align: 'left',
                editable: false,
                hidden: true
            },
            {
                name: 'FUNCTION_ID',
                Index: 'FUNCTION_ID',
                width: 150,
                align: 'center',
                editable: false

            },
            {
                name: 'TABLE_NAME',
                Index: 'TABLE_NAME',
                width: 200,
                align: 'left',
                editable: false,
                hidden: true
            },
            {
                name: 'AUTH_STATUS_ID',
                Index: 'AUTH_STATUS_ID',
                width: 200,
                align: 'center',
                editable: false,
                hidden: true
            },
            {
                name: 'AUTH_LEVEL_MAX',
                Index: 'AUTH_LEVEL_MAX',
                width: 150,
                align: 'center',
                editable: false
            },
            {
                name: 'AUTH_LEVEL_PENDING',
                Index: 'AUTH_LEVEL_PENDING',
                width: 150,
                align: 'center',
                editable: false
            },
           {
               name: 'MAKE_BY',
               Index: 'MAKE_BY',
               width: 200,
               align: 'center',
               editable: false
           },
            {
                name: 'MAKE_DT',
                Index: 'MAKE_DT',
                width: 200,
                align: 'left',
                editable: false,

                sorttype: 'date',
                formatter: "date",
                formatoptions:
                    { newformat: "d-m-Y H:i:s" }
            },
            {
                name: 'REASON_DECLINE',
                Index: 'REASON_DECLINE',
                width: 200,
                align: 'left',
                editable: false,
                hidden: true
            },
             {
                 name: 'TABLE_PK_COL_NM',
                 Index: 'TABLE_PK_COL_NM',
                 width: 200,
                 align: 'left',
                 editable: false,
                 hidden: true
             },
              {
                  name: 'TABLE_PK_COL_VAL',
                  Index: 'TABLE_PK_COL_VAL',
                  width: 100,
                  align: 'left',
                  editable: false,
                  hidden: true
              }
        ],

        caption: "Authorization List",
        multiselect: false,
        maxHeight: 50,
        viewrecords: true,
        //emptyrecords: 'No data found.', //not working
        emptyDataText: 'No data found.',

        jsonReader: {
            repeatitems: false,
            id: "FUNCTION_ID", // 
            root: function (obj) { return obj; },
            page: function (obj) { return 1; },
            total: function (obj) { return 1; },
            records: function (obj) { return obj.length; }
        }
    });

    $("#grid").jqGrid('setGridParam', {
        ondblClickRow: function (rowid, iRow, iCol, e) {
            //ShowDetails(rowid, iRow, iCol);
            ShowLogVal(rowid, iRow, iCol);
        }
    });

    /*********************Log Value**********************************/

    //$("#LogVal").jqGrid({
    //    //debugger,
    //    //jsonReader: {
    //    //    cell: "",
    //    //    id: "0"
    //    //},
    //    url: "http://" + host + appname + "/FTAuthorization/LogValDetails",
    //    datatype: 'jsonstring',
    //    mtype: 'POST',
    //    datastr: colD,
    //    colNames: colN,
    //    colModel: colM,
    //    pager: jQuery('#pager'),
    //    rowNum: 5,
    //    rowList: [5, 10, 20, 50],
    //    viewrecords: true
    //});
    /*********************History Grid*******************************/
    $("#gridHistory").jqGrid({
        url: protocol + "//" + host + appname + "/Authorization/History",
        datatype: 'json',
        mtype: 'post',
        postData: { pLOG_ID: "" },

        colNames: [
            'LOG ID', 'LOG DETAILS ID', 'AUTH OR DEC BY'
            , 'AUTH OR DEC DT', 'AUTH LEVEL', 'AUTH STATUS ID'
        ],
        colModel: [
            {
                name: 'LOG_ID',
                Index: 'LOG_ID',
                width: 10,
                align: 'left',
                editable: false,
                hidden: true
            },
            {
                name: 'LOG_DETAILS_ID',
                Index: 'LOG_DETAILS_ID',
                width: 80,
                align: 'left',
                editable: false,
                hidden: true
            },
            {
                name: 'AUTH_OR_DEC_BY',
                Index: 'AUTH_OR_DEC_BY',
                width: 200,
                align: 'center',
                editable: false
            },
            {
                name: 'AUTH_OR_DEC_DT',
                Index: 'AUTH_OR_DEC_DT',
                width: 220,
                align: 'center',
                editable: false,

                sorttype: 'date',
                formatter: "date",
                formatoptions:
                    { newformat: "d-m-Y H:i:s" }
            },
            {
                name: 'AUTH_LEVEL',
                Index: 'AUTH_LEVEL',
                width: 200,
                align: 'center',
                editable: false
            },
            {
                name: 'AUTH_STATUS_ID',
                Index: 'AUTH_STATUS_ID',
                width: 200,
                align: 'center',
                editable: false
            }
        ],

        viewrecords: true,
        emptyrecords: 'No data found.',

        caption: "Authorization History",
        autowidth: false,
        maxHeight: 50,

        jsonReader: {
            repeatitems: false,
            id: "LOG_ID", // 
            root: function (obj) { return obj; },
            page: function (obj) { return 1; },
            total: function (obj) { return 1; },
            records: function (obj) { return obj.length; }
        },

        gridComplete: function () {
            var recs = parseInt($("#gridHistory").getGridParam("records"), 10);
            if (isNaN(recs) || recs == 0) {
                $("#gridWrapper").hide();
            }
            else {
                $('#gridWrapper').show();
            }
        }
    });
});
$('#ddl_Function').change(function () {
    $("#grid").jqGrid('setGridParam', { postData: { functionID: $('#ddl_Function').val() } }).trigger('reloadGrid');
});


function Authorize() {
    var reason = $("#REASON_DECLINE").val();

    if (reason == "") {
        $("#msgLabel").text("Authorization reason mandatory");
        return;
    }

    var host = $(location).attr('host');
    var protocol = window.location.protocol;

    var rowId = $("#grid").jqGrid('getGridParam', 'selrow');
    var rowData = jQuery("#grid").getRowData(rowId);
    rowData.REASON_DECLINE = reason;

    $.ajax({
        type: "POST",
        url: protocol + "//" + host + appname + "/Authorization/Edit",

        data: JSON.stringify({ pLG_AA_NFT_AUTH_LOG_MAP: rowData }), // stringify
        traditional: true, // important for passing arrays

        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //RefreshPage();
            //$("#msgLabel").text("Success");
        }
    });
}

function Decline() {
    var reason = $("#REASON_DECLINE").val();

    if (reason == "") {
        $("#msgLabel").text("Decline reason mandatory");
        return;
    }

    var host = $(location).attr('host');
    var protocol = window.location.protocol;

    var rowId = $("#grid").jqGrid('getGridParam', 'selrow');
    var rowData = jQuery("#grid").getRowData(rowId);

    rowData.REASON_DECLINE = reason;

    $.ajax({
        type: "POST",
        url: protocol + "//" + host + appname + "/Authorization/EditDecline",

        data: JSON.stringify({ pLG_AA_NFT_AUTH_LOG_MAP: rowData }),
        traditional: true,

        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            RefreshPage();
            location.reload();
            //$("#msgLabel").text("Declined"); //salekin commented
        }
    });
}

function ShowDetails(rowid, iRow, iCol) {

    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    var rowData = jQuery("#grid").getRowData(rowid);
    var logID = rowData['LOG_ID'];

    $.ajax({
        type: "GET",
        url: protocol + "//" + host + appname + "/Authorization/Details?logID=" + logID,
        data: "",
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        success: function (data) {

            var p1 = data.status;
            var p2 = data.statusText;
            var p2 = data.responseText;
            window.location.href = "Details?logID=" + logID;
        },
        error: function (data) {

            var p1 = data.status;
            var p2 = data.statusText;
            var p2 = data.responseText;
            window.location.href = "Details?logID=" + logID;
        }
    });
}

function ShowLogVal(rowid, iRow, iCol) {

    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    var rowData = jQuery("#grid").getRowData(rowid);
    var logID = rowData['LOG_ID'];

    $.ajax({
        type: "GET",
        url: protocol + "//" + host + appname + "/Authorization/LogValDetails?logID=" + logID,
        data: "",
        cache: false,

        success: function (data) {

            window.location.href = "LogValDetails?logID=" + logID;
        }
        //error: function (data) {
        //    debugger;
        //    window.location.href = "LogValDetails?logID=" + logID;
        //}
    });
}

function ShowHistory() {

    var host = $(location).attr('host');
    var protocol = window.location.protocol;
    var rowId = $("#grid").jqGrid('getGridParam', 'selrow');
    var rowData = jQuery("#grid").getRowData(rowId);
    var colData = rowData['LOG_ID'];

    $.ajax({
        type: "POST",
        url: protocol + "//" + host + appname + "/Authorization/History",
        data: { "pLOG_ID": colData },
        success: function (data) {
            $("#gridHistory").jqGrid('setGridParam', { postData: { "pLOG_ID": colData } }).trigger('reloadGrid');
        },
        error: function (data) {
            alert(data);
        }
    });
}

function RefreshPage() {

    GetFunctionsDDL();
    $('#ddl_Function').prop('selectedIndex', 0);

    $("#grid").jqGrid('setGridParam', { postData: { functionID: $('#ddl_Function').val() } }).trigger('reloadGrid');

    $("#REASON_DECLINE").val("");

    $("#gridWrapper").hide();

    $("#msgLabel").text("");
}

function GetFunctionsDDL() {
    var host = $(location).attr('host');
    var protocol = window.location.protocol;

    $.ajax({
        type: "GET",
        url: protocol + "//" + host + appname + "/Authorization/GetFunctions",
        data: "",
        success: function (data) {
            var dropdown = $('#ddl_Function');
            dropdown.empty();
            dropdown.append(
                        $('<option>', {
                            value: '',
                            text: '-------- Select --------'
                        }, '</option>'));

            $.each(data, function (index, item) {
                dropdown.append(
                    $('<option>', {
                        value: item.Value,
                        text: item.Text
                    }, '</option>'));
            });
        },
        error: function (data) {
            alert(data);
        }
    });
}





/*  //customize jqgrid size considering window size
var DataGrid = $('#grid');
DataGrid.jqGrid('setGridWidth', parseInt($(window).width()) - 20);
$(window).resize(function () { DataGrid.jqGrid('setGridWidth', parseInt($(window).width()) - 20); });

var DataGrid2 = $('#gridHistory');
DataGrid2.jqGrid('setGridWidth', parseInt($(window).width()) - 20);
$(window).resize(function () { DataGrid2.jqGrid('setGridWidth', parseInt($(window).width()) - 20); });*/