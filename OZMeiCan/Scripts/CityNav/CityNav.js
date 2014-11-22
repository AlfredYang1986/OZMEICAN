
function CityNav(callBack) {
    this.queryData();
    this.cb = callBack;
}

CityNav.prototype.queryData = function () {
    var tmp = this;
    $.ajax({
        url: '/Home/getJsonData',//'@Url.Action("Search", "MWHome")',
        dataType: 'json',
        contentType: 'application/json, charset=utf-8',
        cache: false,
        //beforeSend: function () {
        //    Loading();
        //},
        //complete: function () {
        //    hiddenLoading();
        //},
        success: function (data) {
            tmp.data = eval(data);
            tmp.cb();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}

CityNav.prototype.getProvencesCount = function () {
    return this.data.length;
}

CityNav.prototype.getCitiesCountByProvence = function (name) {
    var reVal = 0;
    $.each(this.data, function (index, item) {
        if (item.name == name) reVal = item.subs.length;
    });
    return reVal;
}

CityNav.prototype.getDistrectCountByProAndCity = function (p, c) {
    var reVal = 0;

    $.each(this.data, function (index, item) {
        if (item.name == p) {
            $.each(item.subs, function (ic, item_c) {
                if (item_c.name == c) reVal = item_c.subs.length;
            });
        }
    });

    return reVal;
}

CityNav.prototype.getProvencesArray = function () {
    var reVal = new Array();
    $.each(this.data, function (index, item) {
        reVal.push(item.name);
    });
    return reVal;
}

CityNav.prototype.getCitiesArrayByProvence = function (name) {
    var reVal = new Array();
    $.each(this.data, function (index, item) {
        if (item.name == name) {
            $.each(item.subs, function (i, it) {
                reVal.push(it.name);
            });
        }
    });
    return reVal;
}

CityNav.prototype.getDistrectArrayByProAndCity = function (p, c) {
    var reVal = new Array();
    $.each(this.data, function (index, item) {
        if (item.name == p) {
            $.each(item.subs, function (i, it) {
                if (it.name == c) {
                    $.each(it.subs, function (id, itc) {
                        reVal.push(itc.name);
                    });
                }
            });
        }
    });
    return reVal;
}