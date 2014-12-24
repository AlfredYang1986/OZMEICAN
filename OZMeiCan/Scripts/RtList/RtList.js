
function RtList() {
    this.curSub = "";
    this.curSubID = -1;
    this.ds = null;
    this.rt = null;

    this.country = "Australia";
    this.postalCode = 0;
}

RtList.prototype.createRTContent = function () {
    var self = this;

    $.ajax({
        url: '/Home/getRTContent',
        dataType: 'json',
        contentType: 'application/json, charset=utf-8',
        data: { "cor": this.curSub, "subID": this.curSubID },
        cache: false,
        //beforeSend: function () {
        //    Loading();
        //},
        //complete: function () {
        //    hiddenLoading();
        //},
        success: function (data) {
            self.rt = eval(data);
            self.initRTView();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}

RtList.prototype.initRTView = function () {
    var self = this;

    $('#rtList').children().remove();

    $.each(this.rt, function (index, item) {
        var element = $("<div class='restBtn' style='display: table; width: 100%; height: 60px; border: 1px solid black; cursor: pointer'> \
                            <strong data-ele='title' style='font-size: 1.5em; display: table-cell; vertical-align: middle;'>123</strong> \
                            <p data-ele='dis' style='display: table-cell; vertical-align: middle;'>123</p> \
                            <p data-ele='loc' style='display: none; vertical-align: middle;'>123</p> \
                         </div>").appendTo($('#rtList'));
        element.children('strong[data-ele=title]').first().html(item.name);
        element.children('p[data-ele=dis]').first().html(item.dish);
        element.children('p[data-ele=loc]').first().html(item.loc);

        element.click(function () {
            self.createDishContent(item.name, "test");
            self.initDishView();

            $.each($(".restBtn"), function (id, it) {
                $(it).removeClass("rtBtn_active");
            });

            $(this).addClass("rtBtn_active");
        });
    });

    $(".restBtn").first().click();
}

RtList.prototype.createDishContent = function (name, cor) {
    var self = this;

    $.ajax({
        url: '/Home/getDishContent',
        dataType: 'json',
        contentType: 'application/json, charset=utf-8',
        data: { "name": name, "cor": cor },
        cache: false,
        //beforeSend: function () {
        //    Loading();
        //},
        //complete: function () {
        //    hiddenLoading();
        //},
        success: function (data) {
            self.ds = eval(data);
            self.initDishView(name);
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}

RtList.prototype.dishElementView = function (index, item) {
    var element = $("<div class='dishBtn' style='display: table; width: 100%; height: 60px; border: 1px solid black; cursor: pointer'> \
                            <p class='pull-left' data-ele='remove' style='display: none; vertical-align: middle;'> \
                                <span class='glyphicon glyphicon-remove' aria-hidden='true'></span> \
                            </p> \
                            <p class='pull-left' data-ele='rest' style='font-size: 1.0em; display: none; vertical-align: middle;'>0</p> \
                            <p class='pull-left' data-ele='dishID' style='font-size: 1.0em; display: none; vertical-align: middle;'>0</p> \
                            <p class='pull-left' data-ele='count' style='font-size: 1.0em; display: none; vertical-align: middle;'>0</p> \
                            <p class='pull-left' data-ele='title' style='font-size: 1.5em; display: table-cell; vertical-ali0n: middle;'>123</p> \
                            <p class='pull-right' data-ele='price' style='font-size: 1.5em; display: table-cell; vertical-align: middle;'>123</p> \
                         </div>").appendTo($('#dishList'));
    element.children('p[data-ele=title]').first().html(item.name);
    element.children('p[data-ele=price]').last().html(item.price);
    element.children('p[data-ele=rest]').last().html(item.restName);
    element.children('p[data-ele=dishID]').first().html(item.ID);

    element.click(function () {
        //$(this).addClass("dishBtn_active");

        var tmp = $(this).children('p[data-ele=count]');
        var tmpCount = parseInt(tmp.html()) + 1;
        tmp.html(tmpCount);
        tmp.css({ "display": "table-cell" });

        $(this).children('p[data-ele=remove]').css({ "display": "table-cell" });

        od.addOrderDish(item.restName, item.name, item.price, item.ID);
    });

    element.children('p[data-ele=remove]').click(function (event) {
        var tmp = element.children('p[data-ele=count]');
        var tmpCount = parseInt(tmp.html()) - 1;
        tmp.html(tmpCount);

        if (tmpCount == 0) {
            tmp.css({ "display": "none" });
            element.children('p[data-ele=remove]').css({ "display": "none" });
        }

        od.removeOrderDish(item.restName, item.name, item.price);

        event.stopPropagation();
    });
}

RtList.prototype.initDishView = function (restName) {
    var self = this;
    $('#dishList').children().remove();
    $.each(this.ds, this.dishElementView);
}