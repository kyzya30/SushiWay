function ShowAllSelected() {
    var itemRus = $("#ModifyCategoryNameRus").val();
    var catId = 0;
    var list = $(".checkboxes");
    var nameukr;
    for (var i = 0; i < list.length; i++) {
        if (list[i].checked == true) {
            itemRus = list[i].name;
            catId = list[i].id;
            var element = document.getElementById(list[i].id);
            nameukr = element.dataset.nameukr;
        }
    }
    document.getElementById("ModifyCategoryNameUkr").value = nameukr;
    document.getElementById("ModifyCategoryNameRus").value = itemRus;
    document.getElementById("CategoryId").value = catId;
};

function DeleteItemModal() {
    var list = $(".checkboxes");
    var resultString = "";
    var arr = [];
    var j = 0;
    for (var i = 0; i < list.length; i++) {
        if (list[i].checked == true) {
            resultString += list[i].name;
            resultString += ", ";
            arr[j++] = list[i].id;
        }
    }
    resultString = resultString.substring(0, resultString.length - 2);
    document.getElementById("deletedItem").innerHTML = "<span><b>" + resultString + "</b></span>";
    $("#delBtn").click(function() {
        $.ajax({
            type: "POST",
            url: $(this).data("url"),
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: { idSelected: arr },
            success:
                setTimeout(function() {
                    location.reload();
                }, 500)
        });
    });

};

function DeleteDishModal() {
    var list = $(".checkboxes");
    var resultString = "";
    var arr = [];
    var j = 0;
    for (var i = 0; i < list.length; i++) {
        if (list[i].checked == true) {
            resultString += list[i].name;
            resultString += ", ";
            arr[j++] = list[i].id;
        }
    }
    resultString = resultString.substring(0, resultString.length - 2);
    document.getElementById("deletedItem").innerHTML = "<span><b>" + resultString + "</b></span>";

    $("#delDishBtn").click(function() {
        $.ajax({
            type: "POST",
            url: $(this).data("url"),
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: { idSelected: arr },

            success:
                setTimeout(function() {
                    location.reload();
                }, 500)


        });
    });
}

function HideDishModal() {
    var list = $(".checkboxes");
    var resultString = "";
    var arr = [];
    var j = 0;
    for (var i = 0; i < list.length; i++) {
        if (list[i].checked == true) {
            resultString += list[i].name;
            resultString += ", ";
            arr[j++] = list[i].id;
        }
    }
    resultString = resultString.substring(0, resultString.length - 2);
    document.getElementById("hidedItem").innerHTML = "<span><b>" + resultString + "</b></span>";

    $("#HideDishBtn").click(function() {
        $.ajax({
            type: "POST",
            url: $(this).data("url"),
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: { idSelected: arr },
            success:
                setTimeout(function() {
                    location.reload();
                }, 500)
        });
    });
}

function DeleteOrderModal() {
    var list = $(".checkboxes");
    var resultString = "";
    var arr = [];
    var j = 0;
    for (var i = 0; i < list.length; i++) {
        if (list[i].checked == true) {
            resultString += list[i].name;
            resultString += ", ";
            arr[j++] = list[i].id;
        }
    }
    $("#delOrderBtn").click(function () {
        $.ajax({

            type: "POST",
            url: $(this).data("url"),
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: { idSelected: arr },
            success: setTimeout(function () {
                location.reload();
            }, 500)
        });
    });
    resultString = resultString.substring(0, resultString.length - 2);
    document.getElementById("deletedItem").innerHTML = "<span><b>" + resultString + "</b></span>";
};

function ChangeOrderStatus() {

    var list = $(".checkboxes");
    var resultString = "";
    var arr = [];
    var j = 0;
    for (var i = 0; i < list.length; i++) {
        if (list[i].checked == true) {
            resultString += list[i].name;
            resultString += ", ";
            arr[j++] = list[i].id;
        }
    }
    resultString = resultString.substring(0, resultString.length - 2);
    document.getElementById("ChangedItem").innerHTML = "<span><b>" + resultString + "</b></span>";
    $("#changeStatus").click(function () {
        var drpdwnVal = $("#dropdown").val();
        $.ajax({
            type: "POST",
            url: $(this).data("url"),
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                idSelected: arr,
                drpdwnVal: drpdwnVal
            },
            success: setTimeout(function () {
                location.reload();
            }, 500)
        });
    });
   
};

function DeleteDishModal_insideView() {
    var name = $("#NameRus").val();  
    var arr = [];
    arr= $('#ProductId').val();
    document.getElementById("deletedItem").innerHTML = "<span><b>" + name  +"</b></span>";

    $("#delDishBtn").click(function () {
        $.ajax({
            type: "POST",
            url: $(this).data("url"),
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: { idSelected: arr },

            success:
                setTimeout(function () {
                    location.reload();
                }, 500)
        });
    });
}

function DeleteOrder() {
    var orderId = $('#OrderId').val();
    var arr = [];
    arr = orderId;
    document.getElementById("deletedItem").innerHTML = "<span><b>" + orderId + "</b></span>";
    $("#delOrderBtn").click(function () {
        $.ajax({
            type: "POST",
            url: $(this).data("url"),
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: { idSelected: arr },

            success:
                setTimeout(function () {
                    location.reload();
                }, 500)
        });
    });

}