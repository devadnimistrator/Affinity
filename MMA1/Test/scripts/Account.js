var login = function (loginData) {

    $.ajax({
        type: 'POST',
        url: '/Token',
        data: loginData
    }).done(function (data) {
        sessionStorage.setItem("loginData", JSON.stringify(loginData));
        $('#loginForm input').val(null);
        // Cache the access token in session storage.
        sessionStorage.setItem("access_token", data.access_token);
        successMesssage("<strong>Logged in</strong>  as " + loginData.username);
    }).fail(function (data) { errorMessage("<strong>Error!</strong> " + data.responseText); });


}

var getAccessToken = function() {
    return sessionStorage.getItem("access_token");
}

var getLoginData = function () {
    return JSON.parse(sessionStorage.getItem("loginData"));
}


var logout = function () {

    $.ajax({
        type: 'POST',
        url: '/api/Account/Logout',
        headers: { 'Authorization': 'Bearer '+ getAccessToken() }
    }).done(function (data) {
        sessionStorage.removeItem("loginData");
        sessionStorage.removeItem("access_token");
        warningMesssage("<strong>Logged Out.</strong> " );
    }).fail(function (data) { errorMessage("<strong>Error!</strong> " + data.responseText); });

}

var register = function (email, pass1, pass2) {

    $.ajax({
        type: 'POST',
        url: '/api/Account/Register',
        data: {
            'Email': email,
            'Password': pass1,
            'ConfirmPassword': pass2
        }
    }).done(function (data) {
        successMesssage("<strong>Registration complete.</strong> ");
    }).fail(function (data) { errorMessage("<strong>Error!</strong> " + data.responseText); });

}

var isLogged = function() {
    return (sessionStorage.getItem("loginData") ? true : false);
}