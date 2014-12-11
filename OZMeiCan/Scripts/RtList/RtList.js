
function RtList() {
    this.curSub = "";
}

RtList.prototype.createRTContent = function () {
    var self = this;

    $.ajax({
        url: '/Home/getRTContent',
        dataType: 'json',
        contentType: 'application/json, charset=utf-8',
        data: { "cor": this.curSub },
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
        var element = $("<div class='dishBtn' style='display: table; width: 100%; height: 60px; border: 1px solid black'> \
                            <strong class='ele-title' style='font-size: 1.5em; display: table-cell; vertical-align: middle;'>123</strong> \
                            <p class='ele-dis' style='display: table-cell; vertical-align: middle;'>123</p> \
                         </div>").appendTo($('#rtList'));
        element.children('strong').first().html(item.name);
        element.children('p').first().html(item.dish);

        element.click(function () {
            self.createDishContent("test", "test");
            self.inDishView();
        });
    });
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
            self.initDishView();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}

RtList.prototype.initDishView = function () {
    var self = this;

    $('#dishList').children().remove();

    $.each(this.ds, function (index, item) {
        var element = $("<div class='dishBtn' style='display: table; width: 100%; height: 60px; border: 1px solid black'> \
                            <p class='ele-title' style='font-size: 1.5em; display: table-cell; vertical-align: middle;'>123</p> \
                         </div>").appendTo($('#dishList'));
        element.children('p').first().html(item.name);

        element.click(function () {
            alert("dish click");
        });
    });
}