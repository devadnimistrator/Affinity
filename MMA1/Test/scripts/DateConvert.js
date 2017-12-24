
var strTimeZoneSh = function (d) {
    if (!d) d = new Date();
    var offs = d.getTimezoneOffset();
    var oHrs = Math.trunc(Math.abs(offs) / 60);
    var oMins = Math.abs(offs) % 60;
    return (offs <= 0 ? 'Z' : '-') + pad2(oHrs) + pad2(oMins);
}

var strTimeZone = function (d) {
    if (!d) d = new Date();
    var offs = d.getTimezoneOffset();
    var oHrs = Math.trunc(Math.abs(offs) / 60);
    var oMins = Math.abs(offs) % 60;
    return (offs <= 0 ? 'Z' : '-') + pad2(oHrs) + ':' + pad2(oMins);
}

var str2intTimeZoneOffset = function (s) {
    var sign = s.substr(0, 1);
    var hrs = parseInt(s.substr(1, 2));
    var mins = parseInt(s.substr(3, 2));
    return (sign == "Z" ? -1 : 1) * (hrs * 60 + mins);
}

var stringDMin2Date = function (s) {
    if (!s) return null;
    if (s.length == 0) return null;
    var year = parseInt(s.substr(0, 4));
    var month = parseInt(s.substr(4, 2))-1;
    var day = parseInt(s.substr(6, 2));
    var hours = parseInt(s.substr(9, 2));
    var minutes = parseInt(s.substr(11, 2));
    var dt = new Date(year, month, day, hours, minutes);
    return dt;
}


var addMinutes = function(date, minutes) {
    return new Date(date.getTime() + minutes*60000);
}

var stringD2Date = function (s) {
    if (!s) return null;
    if (s == '') return null;
    s = s.trim();
    var dt = null;
    if (/^\d{8}T\d{4}$/.test(s)) {
        var year = parseInt(s.substr(0, 4));
        var month = parseInt(s.substr(4, 2)) - 1;
        var day = parseInt(s.substr(6, 2));
        var hours = parseInt(s.substr(9, 2));
        var minutes = parseInt(s.substr(11, 2));
        dt = new Date(year, month, day, hours, minutes);
       
    }
    else if (/^\d{8}T\d{4}[Z,-]\d{4}$/.test(s)) {
        var year = parseInt(s.substr(0, 4));
        var month = parseInt(s.substr(4, 2)) - 1;
        var day = parseInt(s.substr(6, 2));
        var hours = parseInt(s.substr(9, 2));
        var minutes = parseInt(s.substr(11, 2));
        var tz = s.substr(13);
        var dt = new Date(year, month, day, hours, minutes);
        dt = addMinutes(dt, str2intTimeZoneOffset(tz) - dt.getTimezoneOffset());
    }
    else if (/^\d{8}T\d{6}[Z,-]\d{4}$/.test(s)) {
        var year = parseInt(s.substr(0, 4));
        var month = parseInt(s.substr(4, 2)) - 1;
        var day = parseInt(s.substr(6, 2));
        var hours = parseInt(s.substr(9, 2));
        var minutes = parseInt(s.substr(11, 2));
        var seconds = parseInt(s.substr(13, 2));
        var tz = s.substr(15);
        var dt = new Date(year, month, day, hours, minutes, seconds);
        dt = addMinutes(dt, str2intTimeZoneOffset(tz) - dt.getTimezoneOffset());
    }
    else if (/^\d{8}T\d{9}[Z,-]\d{4}$/.test(s)) {
        var year = parseInt(s.substr(0, 4));
        var month = parseInt(s.substr(4, 2)) - 1;
        var day = parseInt(s.substr(6, 2));
        var hours = parseInt(s.substr(9, 2));
        var minutes = parseInt(s.substr(11, 2));
        var seconds = parseInt(s.substr(13, 2));
        var mseconds = parseInt(s.substr(15, 3));
        var tz = s.substr(18);
        var dt = new Date(year, month, day, hours, minutes, seconds, mseconds);
        dt = addMinutes(dt, str2intTimeZoneOffset(tz) - dt.getTimezoneOffset());
    }
    return dt;
}

var pad2= function(i) {
    if (i <= 9) return "0" + i;
    else return i.toString();
}


var Date2stringDMin = function (dt) {
    if (!dt) return "";
    var year = (1900+dt.getYear()).toString();
    var month = pad2((dt.getMonth() + 1).toString());
    var day = pad2((dt.getDate()).toString());
    var hours = pad2((dt.getHours()).toString());
    var minutes = pad2((dt.getMinutes()).toString());
    return year + month + day + "T" + hours + minutes + strTimeZoneSh();
}