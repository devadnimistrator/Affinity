//api/CheckList/byEmployer/{strEmployerId}/{strStartDate}/{strEndDate}
var reqCheckListList = function (strEmployerId, strStartDate, strEndDate, successFunc, errFunc) {

    $.ajax({
        type: 'GET',
        url: '/api/CheckList/byEmployer/' + strEmployerId + '/' + strStartDate + '/' + strEndDate,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function(data, status) {
        errFunc(data, status);        
    });
}

// GET api/CheckList/{Id}
var reqCheckListById = function (id, successFunc, errFunc) {

    debugger;
    $.ajax({
        type: 'GET',
        url: '/api/CheckList/' + id,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function (data, status) {
        errFunc(data, status);
    });
}


// POST api/CheckList
var reqCheckListPOST = function (thebody, successFunc, errFunc) {

    $.ajax({
        type: 'POST',
        url: '/api/CheckList',
        headers: { 'Authorization': 'Bearer ' + getAccessToken() },
        data: JSON.stringify(thebody),
        //data: thebody,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function(data, status) {
        errFunc(data, status);
    });
}


// PUT api/CheckList/5
//var reqCheckListPUT = function (id, thebody, successFunc, errFunc) {
var reqCheckListPUT = function (thebody, successFunc, errFunc) {
    debugger;
    $.ajax({
        type: 'PUT',
        //url: '/api/CheckList/' + id,
        url: '/api/CheckList',
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

// DELETE api/CheckList/5
var reqCheckListDEL = function (id, successFunc, errFunc) {
    $.ajax({
        type: 'DELETE',
        url: '/api/CheckList/' + id,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function (data, status) {
        errFunc(data, status);
    });
}

