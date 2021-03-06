﻿
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

CityNav.prototype.getCitiesArrayByProvence_2 = function (name) {
    var reVal = new Array();
    $.each(this.data, function (index, item) {
        if (item.name == name) {
            reVal = item.subs;
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
                //self.createListButton(self.getCitiesArrayByProvence(pro.html()), 2);
                self.createListButton(self.getCitiesArrayByProvence_2(pro.html()), 2);
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

    var element = $("<button class='navBtn pull-left' style='width: 60px; height: 40px; margin-left: -1px; margin-right: 3px; background-color: white; border: 1px solid #62BBFF'> \
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
        var element = null;
        if (typeof (item) == 'string') {
            element = $("<button class='listBtn' style='width: 100%; height: 40px; background-color: white; border: 1px solid #62BBFF'></button>").appendTo($('#cities'));
            element.html(item);
            element.attr("data-level", level);
            element.attr("data-subID", -1);
        } else {
            element = $("<button class='listBtn' style='width: 100%; height: 40px; background-color: white; border: 1px solid #62BBFF'></button>").appendTo($('#cities'));
            element.html(item.name);
            element.attr("data-level", level);
            element.attr("data-subID", item.subID);
        }

        element.click(function () {
            var tmp = this;

            //if (parseInt(element.attr('data-level')) != 4) {
            if (parseInt(element.attr('data-level')) != 2) {
                self.createNavButton(tmp.textContent, level);
                self.listAllBtns();
            } else {
                if (typeof(item) == 'string') {
                    rt.curSub = item;
                    rt.curSubID = -1;
                    rt.postalCode = -1;
                } else {
                    rt.curSub = item.name;
                    rt.curSubID = item.subID;
                    rt.postalCode = item.postalCode;
                }
                $('.changePage').first().click();

            //    /**
            //     * 5. 跳到地图
            //     */
            //    var w = $('#cityNav').width();
            //    var hNav = $('#cityNav').height();
            //    var hCon = $('#cities').height();
            //    $('#cityNav').hide();
            //    $('#cities').hide();
            //    $('#cityMap').show();
            //    $('#cityMap').css({ "width": w + "px", "height": hNav + hCon + "px" });

            //    //var map = new BMap.Map("cityMap");    // 创建Map实例
            //    //map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);  // 初始化地图,设置中心点坐标和地图级别
            //    //map.addControl(new BMap.MapTypeControl());   //添加地图类型控件
            //    //map.setCurrentCity("北京");          // 设置地图显示的城市 此项是必须设置的
            //    //map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放

            //    var mapProp = {
            //        center: new google.maps.LatLng(51.508742, -0.120850),
            //        zoom: 7,
            //        panControl: true,
            //        zoomControl: true,
            //        mapTypeControl: true,
            //        scaleControl: true,
            //        streetViewControl: true,
            //        overviewMapControl: true,
            //        rotateControl: true,
            //        mapTypeId: google.maps.MapTypeId.ROADMAP
            //    };
            //    var map = new google.maps.Map(document.getElementById("cityMap")
                //      , mapProp);
                }
        });
    });
}