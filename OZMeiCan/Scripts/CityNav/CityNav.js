
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

CityNav.prototype.getBuildingCountByProCityAndDis = function (p, c, d) {
    var reVal = 0;
    $.each(this.data, function (index, item) {
        if (item.name == p) {
            $.each(item.subs, function (ic, item_c) {
                if (item_c.name == c) {
                    $.each(item_c.subs, function (id, item_d) {
                        if (item_d.name == d) reVal = item_d.subs.length;
                    });
                }
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

CityNav.prototype.getBuildingArrayByProCityAndDis = function (p, c, d) {
    var reVal = new Array();
    $.each(this.data, function (index, item) {
        if (item.name == p) {
            $.each(item.subs, function (ic, item_c) {
                if (item_c.name == c) {
                    $.each(item_c.subs, function (id, item_d) {
                        if (item_d.name == d) {
                            $.each(item_d.subs, function (i, item_b) {
                                reVal.push(item_b.name);
                            });
                        }
                    });
                }
            });
        }
    });
    return reVal;
}

CityNav.prototype.listAllBtns = function () {
    var self = this;

    $('#cities').children().remove();

    var pro = $('.navBtn[data-level=1]');
    var city = $('.navBtn[data-level=2]');
    var dis = $('.navBtn[data-level=3]');
    var bdg = $('.navBtn[data-level=4]');

    if (dis.length == 0) {
        if (city.length == 0) {
            if (pro.length == 0) {
                self.createListButton(self.getProvencesArray(), 1);
            } else {
                self.createListButton(self.getCitiesArrayByProvence(pro.html()), 2);
            }
        } else {
            self.createListButton(self.getDistrectArrayByProAndCity(pro.html(), city.html()), 3);
        }
    } else {
        self.createListButton(self.getBuildingArrayByProCityAndDis(pro.html(), city.html(), dis.html()), 4);
    }
}

CityNav.prototype.createNavButton = function (n, v) {
    var self = this;

    var element = $("<button class='navBtn pull-left' style='width: 60px; height: 40px; margin-right: 3px'> \
                   </button>").appendTo($('#cityNav'));
    element.html(n);
    element.attr('data-level', v);
    element.click(function () {

        var tmp = this;
        /**
         * 1. remove all the buttons which level larger than this one
         */
        $.each($('.navBtn'), function (index, item) {
            if (parseInt($(item).attr('data-level'), 10) > v)
                $(item).remove();
        });

        /**
         * 2. remove all the current next levels
         */

        /**
         * 3. add new button for next levels
         */
        self.listAllBtns();
    });
}

CityNav.prototype.createListButton = function (array, level) {
    var self = this;
    
    $.each(array, function (index, item) {
        var element = $("<button class='listBtn' style='width: 100%; height: 40px'></button>").appendTo($('#cities'));
        element.html(item);
        element.attr("data-level", level);

        element.click(function () {
            var tmp = this;

            if (parseInt(element.attr('data-level')) != 4) {
                self.createNavButton(tmp.textContent, level);
                self.listAllBtns();
            }
        });
    });
}