var guid = function() {
  function s4() {
    return Math.floor((1 + Math.random()) * 0x10000)
      .toString(16)
      .substring(1);
  }
  return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
    s4() + '-' + s4() + s4() + s4();
};

String.prototype.replaceAll = function (str1, str2, ignore) {
    return this.replace(new RegExp(str1.replace(/([\/\,\!\\\^\$\{\}\[\]\(\)\.\*\+\?\|\<\>\-\&])/g, "\\$&"), (ignore ? "gi" : "g")), (typeof (str2) == "string") ? str2.replace(/\$/g, "$$$$") : str2);
};

var getCookie = function(cookieName, encoded) {
   res = "";
   cookies=document.cookie.split(";");
   for(var i=0; i<cookies.length; i++) {
      c = cookies[i];
      v = c.split("=");
      if (v[0].trim() == cookieName) {
          if (encoded)
              return res = window.atob(v[1]);
          else
              return res = v[1];
        }
   }
   return res;
};


var handleAPIError = function(resp) {
    var msg;
    msg = resp.status + ' - ' + resp.statusText;
    if (resp.status >= 500) {

        msg += '\n'+ resp.data.Message + '\n' +
        resp.data.ExceptionMessage;    
    }
     
    alert(msg);
};


var cloneObj = function (obj) {
    var newObj = (obj instanceof Array) ? [] : {};
    for (var i in obj) {
        if (obj[i] && typeof obj[i] == "object") {
            newObj[i] = obj[i].clone();
        }
        else {
            if (obj[i]) newObj[i] = obj[i];
            else newObj[i] = null;
        }
    }
    return newObj;
};

var updateObj = function (objDest, objSrc) {
    //var newObj = (obj instanceof Array) ? [] : {};
    for (var i in objSrc) {
        if (objSrc.hasOwnProperty(i)) {
            if (objSrc[i] && typeof objSrc[i] == "object") {
                var newO = objSrc[i].clone();
                objDest[i] = newO;
            } else {
                if (objSrc[i]) objDest[i] = objSrc[i];
                //else newObj[i] = null;
            }
        }
    }
    return objDest;
};

var setDateTime = function (iDateField, iTimeField, dt) {
    var dd = dt.substr(0, 4) + '-' + dt.substr(4, 2) + '-' + dt.substr(6, 2);
    iDateField.val(dd);
    var tt = dt.substr(9, 2) + ':' + dt.substr(11, 2);
    iTimeField.val(tt);
};

var getDateTime = function (iDateField, iTimeField) {
    var dd = iDateField.val();
    dd = dd.substr(0, 4) + dd.substr(5, 2) + dd.substr(8, 2);
    var tt = iTimeField.val();
    tt = tt.substr(0, 2) + tt.substr(3, 2);
    return dd + 'T' + tt + strTimeZoneSh();
};

var dateAddDays = function (dt, days) {
    var nd = dt;
    nd.setTime(dt.getTime() + days * 86400000);
    return nd;
};


var getUrlParameter = function (name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
};

var getEmpDataLS = function () {
    //debugger;
    var empInfos = localStorage.getItem("empInfos");
    var empInfosObject = [];
    empInfosObject = JSON.parse(empInfos);
    return empInfosObject;
};

var setEmpDataLS = function (empData) {
    var newList = [];
    newList[0] = empData;
    var currentList = getEmpDataLS();
    debugger;
    currentList.push(empData);
    //empInfoData.push(empInfoStr);
    localStorage.setItem("empInfos", JSON.stringify(currentList));
};

var getFromStore = function () {
    //check if store values are null, if null, make store =[]
    var store = [undefined, null].indexOf(localStorage["optArray"]) != -1 ? [] : JSON.parse(localStorage["optArray"]);
    console.log(store);
    //empty container before u put values
    $('#myStorage').html('');
    //check if store is empty
    if (store.length != 0) {
        //loop over store if it aint empty and append the content into myStorage div
        for (var k in store) {
            var str = '<div>' + store[k]["title"] + ' - <a target= "_blank" href="http://www.example.com/' + store[k]["value"] + '">View profile</a></div>';
            console.log(store[k]);
            $('#myStorage').append(str);
        }
    } else {
        //do this if no data is found in localStorage
        $("#myStorage").html("No people found in store");
    }
};

function localStore(key, obj) {
        return localStorage.setItem(key, JSON.stringify(obj));
}

function localGet(key) {
    var s = localStorage.getItem(key);
    if (!s) return null;
    if (s === 'undefined') return null;
    if (s.length === 0) return null;
    return JSON.parse(s);
}

function getList(key) {
    return localGet(key) || [];
}

function addItem2List(value, listKey) {
    var l = getList(listKey);
    if(l.indexOf(value)<0) l.push(value);
    localStore(listKey, l);
    return l;
}

function findDictionaryValue(value, valueKey, list) {
    var res = null;
    list.some(function (item) {
        if (item.hasOwnProperty(valueKey)) {
            if (item[valueKey] == value) {
                res = item;
                return true;
            }
        }
        return false;
    });
    return res;
}

function getDictionaryValue(value, valueKey, listKey) {
    var l = getList(listKey);
    return findDictionaryValue(value, valueKey, l);
}

function addObject2Dictionary(obj, valueKey, listKey) {
    var l = getList(listKey);
    var o = findDictionaryValue(obj[valueKey], valueKey, l);
    if (o) {
        updateObj(o, obj);
        //l[l.indexOf(o)] = obj;
    } else {
        l.push(obj);
    }

    localStore(listKey, l);
    return l;
}

//// Get stored data from LocalStorage
//var getData = function (prefix) {
//    var curData = [];
//    for (var i = 0; i <= localStorage.length - 1; i++) {
//        try {
//            var key = localStorage.key(i);
//            if (key.startsWith(prefix)) {
//                val = localGet(key);
//                curData.push({ Id: key, Name: val.employerName });
//            }
//        } catch (err) {
//        };
//    }
//    return curData;
//}

