
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
    $('#ddl_FileUploadType').change(function () {
        var FileUploadTypeID = $('#ddl_FileUploadType').val();
        if (FileUploadTypeID == 6) {
            $('#browse_div').hide();
            $('#ThumbPic_div').show();     
        }
        else {
            $('#browse_div').show();
            $('#ThumbPic_div').hide();
        }
    })
});



$(document).ready(function () {
        var FileUploadTypeID = $('#ddl_FileUploadType').val();
        if (FileUploadTypeID == 6) {
            $('#browse_div').hide();
            $('#ThumbPic_div').show();
        }
        else {
            $('#browse_div').show();
            $('#ThumbPic_div').hide();
        }
});



$(document).ready(function () {
    $('#btnEnroll').click(function () {
        var host = $(location).attr('host');
        var protocol = window.location.protocol;
        $.ajax({
            type: "Get",
            url: protocol + "//" + host + appname + "/UserFileUpload/Enroll",
            //data: rowData,
            data: { },
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
            },
            error: function () {
                alert("Error. Please contact support.");
            }
        });
    });

    $('#btnShow').click(function () {
         loadFile();
    });
    var loadFile = function () {
        var output = document.getElementById('thumbpic_id');
        //output.src = URL.createObjectURL(event.target.files[0]);
        output.src = "C:\\thumbpic\\thumbpic.Bmp";
    }
});