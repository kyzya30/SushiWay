function ShowAllSelected() {
    var itemRus = $('#ModifyCategoryNameRus').val();
    var CatId = 0;
  var  list = $('.checkboxes');
  //var item2 = $('#ModifyCategoryNameUkr');
    for (var i = 0; i < list.length; i++) {
        if (list[i].checked == true) {
            itemRus = list[i].name;
            CatId = list[i].id;
            var el = document.getElementById(list[i].id)
             var nameukr = el.dataset.nameukr;
           //var drpdwnVal = $('#dropdown').val();
           // item2.val = list[0].content;//wrong
          
        //document.getElementById('ModifyCategoryName').textContent(list[i].id + list[i].name);
        }
    }
    document.getElementById("ModifyCategoryNameUkr").value = nameukr;
    document.getElementById("ModifyCategoryNameRus").value = itemRus;
    document.getElementById("CategoryId").value = CatId;
    
    //$("#ModifyCategory").click(function (e) {
       
    //    e.preventDefault();
    //    //var NameRus = document.getElementById("ModifyCategoryNameRus");
    //    //e = NameRus.dataset.placeholder;
    //    var categoryNameUkr1 = $('#ModifyCategoryNameUkr').val();
    //    var categoryNameRus1 = $('#ModifyCategoryNameRus').val();
    //    var Priority = $('#Priority').val();
    //    var startedValNameRus1 = itemRus;
    //    var model = {
    //        //startedValNameRus: startedValNameRus1,
    //        //NameRus: categoryNameRus1,
    //        //NameUkr: categoryNameUkr1,
    //        Priority: Priority

    //    };
    //    //var model = [];
    //    //model[0] = startedValNameRus1;
    //    //model[1] = categoryNameRus1;
    //    //model[2] = categoryNameUkr1;
    //    //model[3] = Priority;
    //    console.log(model);
    //    alert(123);
    //    $.ajax({

    //        type: "POST",
    //        url: '/Admin/ModifyCategoryModal/',
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        data: model,
    //        success: setTimeout(function () {
    //            location.reload()
    //        }, 500)

    //    });
    //});
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
            success:
                 setTimeout(function () {
                     location.reload()
                 }, 500)
            
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

