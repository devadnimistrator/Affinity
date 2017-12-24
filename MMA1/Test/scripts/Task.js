//api/Task/byEmployer/{strEmployerId}/{strStartDate}/{strEndDate}
var reqTaskList = function (strEmployerId, strStartDate, strEndDate, successFunc, errFunc) {

    $.ajax({
        type: 'GET',
        url: '/api/Task/byEmployer/' + strEmployerId + '/' + strStartDate + '/' + strEndDate,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function(data, status) {
        errFunc(data, status);        
    });
}

// GET api/Task/{Id}
var reqTaskById = function (id, successFunc, errFunc) {

    $.ajax({
        type: 'GET',
        url: '/api/Task/' + id,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function (data, status) {
        errFunc(data, status);
    });
}


// POST api/Task
var reqTaskPOST = function (thebody, successFunc, errFunc) {

    $.ajax({
        type: 'POST',
        url: '/api/Task',
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


// PUT api/Task/5
var reqTaskPUT = function (id, thebody, successFunc, errFunc) {
    $.ajax({
        type: 'PUT',
        url: '/api/Task/' + id,
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

// DELETE api/Task/5
var reqTaskDEL = function (id, successFunc, errFunc) {
    $.ajax({
        type: 'DELETE',
        url: '/api/Task/' + id,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function (data, status) {
        errFunc(data, status);
    });
}

