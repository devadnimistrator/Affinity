var successMesssage = function (msg) {
    console.log(msg);
    var d = $('#Status');
    d.html(msg.replaceAll('\r\n', '<br />').replace("\\r\\n", '<br />').replace("\n", '<br />'));
    d.removeClass();
    d.addClass("alert alert-success");
    d.show();
}

var infoMesssage = function (msg) {
    console.log(msg);
    var d = $('#Status');
    d.html(msg.replaceAll('\r\n', '<br />').replace("\\r\\n", '<br />').replace("\n", '<br />'));
    d.removeClass();
    d.addClass("alert alert-info");
    d.show();
}


var errorMessage = function (msg) {
    console.log(msg);
    var d = $('#Status');
    d.html(msg.replaceAll('\r\n', '<br />').replace("\\r\\n", '<br />').replace("\n", '<br />'));
    d.removeClass();
    d.addClass("alert alert-danger");
    d.show();
}

var warningMesssage = function (msg) {
    console.log(msg);
    var d = $('#Status');
    d.html(msg.replaceAll('\r\n', '<br />').replace("\\r\\n", '<br />').replace("\n", '<br />'));
    d.removeClass();
    d.addClass("alert alert-warning");
    d.show();
}

var hideMessage = function() {
    $('#Status').hide();
}