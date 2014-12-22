
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
    return this.xls;
}

Orders.prototype.removeOrderDish = function (restName, dishName, price) {
    var elem = this.eunmElementByNames(restName, dishName);
    if (elem != null) {
        --elem.count;
        elem.subTotal -= price;

        if (elem.count == 0) {
            this.xls = this.xls.filter(function (index, item) { item != elem; });
        }
    }
}

Orders.prototype.orderJsonData = function () {
    return JSON.stringify(this.xls);
}