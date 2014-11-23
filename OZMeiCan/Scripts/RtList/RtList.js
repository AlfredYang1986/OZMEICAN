
function RtList() {

}

RtList.prototype.getRTContent = function () {
    var self = this;

    $.ajax({
        url: '/Home/getRTContent',
        dataType: 'json',
        contentType: 'application/json, charset=utf-8',
        data: { "cor": "AlfredTest" },
        cache: false,
        //beforeSend: function () {
        //    Loading();
        //},
        //complete: function () {
        //    hiddenLoading();
        //},
        success: function (data) {
            alert(data)
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}

RtList.prototype.getDishContent = function (name, cor) {
    var self = this;

    $.ajax({
        url: '/Home/getDishContent',
        dataType: 'json',
        contentType: 'application/json, charset=utf-8',
        data: { "name": "TestAlfred", "cor": "AlfredTest" },
        cache: false,
        //beforeSend: function () {
        //    Loading();
        //},
        //complete: function () {
        //    hiddenLoading();
        //},
        success: function (data) {
            alert(data)
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}