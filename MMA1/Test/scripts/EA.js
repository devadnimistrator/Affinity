//api/EmployeeAvailability
var reqEAList = function (strEmployeeEmail, strStartDate, successFunc, errFunc) {
    //alert('email ' + strEmployeeEmail);
    $.ajax({
        type: 'GET',
        url: '/api/EmployeeAvailability/byEmployee/' + strEmployeeEmail + '/' + strStartDate,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function (data, status) {
        errFunc(data, status);
    });
}

// GET api/EmployeeAvailability/{Id}
var reqEAById = function (id, successFunc, errFunc) {

    $.ajax({
        type: 'GET',
        url: '/api/EmployeeAvailability/' + id,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function (data, status) {
        errFunc(data, status);
    });
}


// POST api/EmployeeAvailability
var reqEAPOST = function (thebody, successFunc, errFunc) {
    $.ajax({
        type: 'POST',
        url: '/api/EmployeeAvailability',
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


// PUT api/EmployeeAvailability/5
var reqEAPUT = function (id, thebody, successFunc, errFunc) {
    $.ajax({
        type: 'PUT',
        url: '/api/EmployeeAvailability/' + id,
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

var reqEADEL = function (id, successFunc, errFunc) {
    $.ajax({
        type: 'DELETE',
        url: '/api/EmployeeAvailability/' + id,
        headers: { 'Authorization': 'Bearer ' + getAccessToken() }
    }).done(function (data, status) {
        successFunc(data, status);
    }).fail(function (data, status) {
        errFunc(data, status);
    });
}
