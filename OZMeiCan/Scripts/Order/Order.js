
function Orders() {
    /**
     * every element in this array 
     * shall have 5 values 
     * 1. rest name             : restName
     * 2. dish name             : dishName
     * 6. dish name             : dishID
     * 3. how much per dish     : price 
     * 4. how many              : count
     * 5. sub total             : subTotal
     */
    this.xls = new Array();
}

Orders.prototype.eunmElementByNames = function (restName, dishName) {
    var reVal = null;
    $.each(this.xls, function (index, item) {
        if (restName == item.restName && dishName == item.dishName) {
            reVal = item;
        }
    });
    return reVal;
}

Orders.prototype.addOrderDish = function (restName, dishName, price, dishID) {
    var elem = this.eunmElementByNames(restName, dishName);
    if (elem != null) {
        ++elem.count;
        elem.subTotal += price;
    } else {
        var newElem = new Object();
        newElem.restName = restName;
        newElem.dishName = dishName;
        newElem.dishID = dishID;
        newElem.price = price;
        newElem.count = 1;
        newElem.subTotal = price;

        this.xls.push(newElem);
    }
}

Orders.prototype.getCurrentOrders = function () {
    var arr = this.xls;
    this.xls = [];

    for (index = 0; index < arr.length; ++index) {
        if (arr[index].count != 0)
            this.xls.push(arr[index]);
    }

    return this.xls;

    //this.xls = this.xls.filter(function (index, item) { item.count != 0; });
    //return this.xls;
}

Orders.prototype.removeOrderDish = function (restName, dishName, price) {
    var elem = this.eunmElementByNames(restName, dishName);
    if (elem != null) {
        --elem.count;
        elem.subTotal -= price;

        //if (elem.count == 0) {
        //    this.xls = this.xls.filter(function (index, item) { item != elem; });
        //}
    }
}

Orders.prototype.orderJsonData = function () {
    return JSON.stringify(this.xls);
}

Orders.prototype.resetOrderCount = function () {
    var self = this;

    $.each($(".dishBtn"), function (index, item) {
        var count = $(item).children('p[data-ele=count]').first().html();
        var restName = $(item).children('p[data-ele=rest]').first().html();
        var dishName = $(item).children('p[data-ele=title]').first().html();

        var sn = self.eunmElementByNames(restName, dishName);

        if (sn != null) {
            $(item).children('p[data-ele=count]').first().html(sn.count);
            if (parseInt(sn.count) == 0) {
                $(item).children('p[data-ele=count]').css({ "display": "none" });
                $(item).children('p[data-ele=remove]').css({ "display": "none" });
            }
        }
    });
}