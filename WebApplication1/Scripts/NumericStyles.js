function ShowAllSelected() {
    var item = $('#ModifyCategoryName');
  var  list = $('.checkboxes');

    for (var i = 0; i < list.length; i++) {
        if (list[i].checked == true) {
            item.val = list[0].name;
            document.getElementById("ModifyCategoryName").value = list[i].name;
        //document.getElementById('ModifyCategoryName').textContent(list[i].id + list[i].name);
        }
    }
    //item.val = list[0].name;
};


function DeleteItemModal() {
    var item = $('#deletedItem');
    var list = $('.checkboxes');
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
    //$.post("/Admin/DeleteCategoryItem/", { data: arr }).then(function () {

    //});
    $("#delBtn").click(function () {
        $.ajax({
            type: "POST",
            url: '/Admin/DeleteCategoryItem/',
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: { idSelected: arr },
            success: location.href = '/Admin/Category/'
            
        });
    });
    resultString = resultString.substring(0, resultString.length - 2);
    document.getElementById("deletedItem").innerHTML = "<span><b>" + resultString + "</b></span>";
};
function DeleteDishModal()
{
    var item = $('#deletedItem');
    var list = $('.checkboxes');
    var resultString = "";
    var arr = [];
    var j = 0;
    for (var i = 0; i < list.length; i++) {
        if (list[i].checked == true)
        {
            resultString += list[i].name;
            resultString += ", ";
            arr[j++] = list[i].id;
        }
    }
    resultString = resultString.substring(0, resultString.length - 2);
    document.getElementById("deletedItem").innerHTML = "<span><b>" + resultString + "</b></span>";
    
    $("#delDishBtn").click(function () {
        $.ajax({
            type: "POST",
            url: '/Admin/DeleteDishModal/',
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: { idSelected: arr },
            
            success:
                setTimeout(function() {location.reload()
                }, 500)
                

        });
    });
}
function HideDishModal()
{
    var item = $('#hidedItem');
    var list = $('.checkboxes');
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

    $("#HideDishBtn").click(function () {
        $.ajax({
            type: "POST",
            url: '/Admin/HideDishModal/',
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: { idSelected: arr },
            success:
        setTimeout(function() {location.reload()
        }, 500)

        });
    });
}
function DeleteOrderModal() {
    var item = $('#deletedItem');
    var list = $('.checkboxes');
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
    //$.post("/Admin/DeleteCategoryItem/", { data: arr }).then(function () {

    //});
    $("#delOrderBtn").click(function () {
        $.ajax({
           
            type: "POST",
            url: '/Admin/DeleteOrderModal/',
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: { idSelected: arr },
            success: setTimeout(function () {
                location.reload()
            }, 500)

        });
    });
    resultString = resultString.substring(0, resultString.length - 2);
    document.getElementById("deletedItem").innerHTML = "<span><b>" + resultString + "</b></span>";
};
function ChangeOrderStatus() {
    var item = $('#deletedItem');
    var drpdwn = $('#dropdown').val();
    var list = $('.checkboxes');
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
    
    //$.post("/Admin/DeleteCategoryItem/", { data: arr }).then(function () {

    //});
    $("#changeStatus").click(function () {
        var drpdwnVal = $('#dropdown').val();
        
        $.ajax({
           
            type: "POST",
            url: '/Admin/ChangeOrderStatus/',
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                idSelected: arr,
                drpdwnVal: drpdwnVal
            },
            success: setTimeout(function () {
                location.reload()
            }, 500)

        });
    });
    resultString = resultString.substring(0, resultString.length - 2);
    document.getElementById("ChangedItem").innerHTML = "<span><b>" + resultString + "</b></span>";
};

