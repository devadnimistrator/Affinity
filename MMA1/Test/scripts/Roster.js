//api/Roster/byEmployer/{strEmployerId}/{strStartDate}/{strEndDate}
var reqRosterList = function (strEmployerId, strStartDate, strEndDate, successFunc, errFunc) {

    $.ajax({
        type: 'GET',
        url: '/api/Roster/byEmployer/' + strEmployerId + '/' + strStartDate + '/' + strEndDate,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function(data, status) {
        errFunc(data, status);        
    });
}

// GET api/Roster/{Id}

var reqRosterById = function (id, successFunc, errFunc) {

    $.ajax({
        type: 'GET',
        url: '/api/Roster/' + id,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function (data, status) {
        errFunc(data, status);
    });
}


// POST api/Roster

var reqRosterPOST = function (thebody, successFunc, errFunc) {

    $.ajax({
        type: 'POST',
        url: '/api/Roster',
        headers: { 'Authorization': 'Bearer ' + getAccessToken() },
        data: JSON.stringify(thebody),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function(data, status) {
        errFunc(data, status);
    });
}


// PUT api/Roster/5
var reqRosterPUT = function (id,thebody, successFunc, errFunc) {
    $.ajax({
        type: 'PUT',
        url: '/api/Roster/'+id,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() },
        data: JSON.stringify(thebody),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function (data, status) {
        errFunc(data, status);
    });
}

var reqRosterDEL = function (id, successFunc, errFunc) {
    $.ajax({
        type: 'DELETE',
        url: '/api/Roster/' + id,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function (data, status) {
        errFunc(data, status);
    });
}

var reqClockOnDataByEmployer = function (strEmployerId, strStartDate, strEndDate, successFunc, errFunc) {

    $.ajax({
        type: 'GET',
        url: '/api/ClockData/byEmployer/' + strEmployerId + '/' + strStartDate + '/' + strEndDate,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function (data, status) {
        errFunc(data, status);
    });
}

var reqClockOnDataByRoster = function (strRosterId, successFunc, errFunc) {

    $.ajax({
        type: 'GET',
        url: '/api/ClockData/byRoster/' + strRosterId,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function (data, status) {
        errFunc(data, status);
    });
}