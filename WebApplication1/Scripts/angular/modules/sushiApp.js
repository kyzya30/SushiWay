angular.module("sushiApp", ["ngRoute"])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

        //$locationProvider.html5Mode({
        //    enabled: true,
        //    requireBase: false
        //});

        $locationProvider.html5Mode(false);

        $routeProvider.when("/cart", {
            templateUrl: "scripts/angular/views/Cart.html"
        });

        $routeProvider.when("/contact", {
            templateUrl: "scripts/angular/views/Contact.html"
        });

        $routeProvider.when("/result", {
            templateUrl: "scripts/angular/views/OrderStatusResult.html"
        });

        $routeProvider.when("/productname", {
            templateUrl: "scripts/angular/views/productname.html"
        });

        $routeProvider.when("/order", {
            templateUrl: "scripts/angular/views/OrderSearch.html"
        });

        $routeProvider.when("/topProducts", {
            templateUrl: "scripts/angular/views/topFilter.html"
        });
        $routeProvider.when("/hotProducts", {
            templateUrl: "scripts/angular/views/hotFilter.html"
        });
        $routeProvider.when("/saleProducts", {
            templateUrl: "scripts/angular/views/saleFilter.html"
        });

        //$routeProvider.when("/productname", {
        //    templateUrl: "scripts/angular/views/ProductCategoriesFilter.html"
        //});

        $routeProvider.when("/Success", {
            templateUrl: "scripts/angular/views/SuccessMakeOrder.html"
        });

        $routeProvider.when("/menu", {
            templateUrl: "scripts/angular/views/menu.html"
        });

        $routeProvider.when("/filter", {
            templateUrl: "scripts/angular/views/ProductCategoriesFilter.html"
        });

        $routeProvider.otherwise({
            templateUrl: "/scripts/angular/views/menu.html"
        });
    }])
    .controller("navCtrl", ['$scope', '$location', 'sushiService', '$http', function ($scope, $location, sushiService, $http) {
        $scope.cartSum = 0;

        $scope.goToCart = function () {
            $location.path("/cart");
        };
        $scope.goFiltr = function (cat) {
            $scope.findCategory = cat.id;
            $location.path("/filter");
        };
        $scope.goToMenu = function () {
            $location.path("/menu");
        };
        $scope.goResult = function () {
            $location.path("/result");
        };
        $scope.gotoNameFilter = function () {
            $scope.coincidenceNumber = 0;
            $location.path("/productname");
        };
        $scope.goToOrder = function () {
            $location.path("/order");
        };

        $scope.topProduct = function () {
            $location.path("/topProducts");
        };
        $scope.hotProduct = function () {
            $location.path("/hotProducts");
        };
        $scope.saleProduct = function () {
            $location.path("/saleProducts");
        };
        sushiService.getSushi().then(function (res) {
            console.log(res);
            $scope.items = res.data;
        });

        sushiService.getCategory().then(function (res1) {
            console.log(res1);
            $scope.category = res1.data;
        });

        $scope.RadioBntChange = function (params) {
            if (params == "allProduct") {
                var bnt = $("#prod1");
                bnt.checked = true;
                $scope.goToMenu();
            }
            else if (params == "topProduct") {
                var bnt1 = $("#prod1");
                bnt1.checked = true;
                $scope.topProduct();
            }
            else if (params == "saleProduct") {
                var bnt2 = $("#prod2");
                bnt2.checked = true;
                $scope.saleProduct();
            }
            else if (params == "hotProduct") {
                var bnt3 = $("#prod3");
                bnt3.checked = true;
                $scope.hotProduct();              
            }

        }

        $scope.isSelected = function (item) {
            if (item.sale == true) {
                $scope.saleCoff = 0.95;
            } else {
                $scope.saleCoff = 1;
            }
            item.count = 1;

            $scope.cartSum += (item.price * $scope.saleCoff);
            item.selected = true;
            $scope.cartIsEmpty = false;
        };
        $scope.clearCart = function () {
            for (var i = 0; i < $scope.items.length ; i++) {
                if ($scope.items[i].count) {
                    $scope.items[i].selected = false;
                    $scope.items[i].count = 0;
                }
            }
            $scope.cartSum = 0;
            $scope.cartIsEmpty = true;
            $location.path("/cart");
        }
        $scope.addToCart = function (item) {
            if (item.sale == true) {
                $scope.saleCoff = 0.95;
            } else {
                $scope.saleCoff = 1;
            }

            item.count = +item.count + 1;

            $scope.cartSum += (item.price * $scope.saleCoff);
            if ($scope.cartSum > 0) {
                $scope.cartIsEmpty = false;
            }
        };
        $scope.deleteItemFromCart = function (item) {
            if (item.sale == true) {
                $scope.saleCoff = 0.95;
            } else {
                $scope.saleCoff = 1;
            }
            item.selected = false;
            $scope.cartSum -= (item.price * $scope.saleCoff) * item.count;
            item.count = 0;
            if ($scope.cartSum < 1) {
                $scope.cartIsEmpty = true;
            }
            $location.path("/cart");
        }
        $scope.cartIsEmpty = true;
        $scope.findOredrById = " ";
        $scope.resOrderSatus = " ";

        $scope.matchPattern = /^\d+$/;
        $scope.saleCoff = 1;


        $scope.makeOrder = function (newOrder) {

        };

        $scope.removeFromCart = function (item) {
            if (item.sale == true) {
                $scope.saleCoff = 0.95;
            } else {
                $scope.saleCoff = 1;
            }
            if (item.count > 1) {
                item.count--;

            } else {
                item.selected = false;
            }

            $scope.cartSum -= item.price * $scope.saleCoff;

            if ($scope.cartSum < 1) {
                $scope.cartSum = 0;
                $scope.cartIsEmpty = true;
            }
        };
        $scope.FindProductName = "";
        $scope.coincidenceNumber = 0;
        $scope.findByNameFilter = function (item) {
            return item.name.toLowerCase().indexOf($scope.FindProductName.toLowerCase()) > -1;

        }

        $scope.sortProduct = function (item) {

            return item.categoryId == $scope.findCategory;
        };
        $scope.menuFiltr = function (item) {
            return item.categoryId != 3;
            //return item.categoryId = $scope.currentCategotyFilter;
        }
        $scope.currentCategotyFilter = 0;

        $scope.noAddproductFilter = function (cat) {
            if (COND) {
                $scope.currentCategotyFilter = cat.id;
            }
            $scope.currentCategotyFilter = cat.id;
            return cat.id != 3;
        }
        $scope.categoryWithOutAddProductFilter = function (cat) {
            return cat.id != 3;
        }

        $scope.countFilter = function (item) {
            //return item.count > 0;
            return ((item.selected) && (item.categoryId != 3));
        };
        $scope.addProduct = function (item) {
            return item.categoryId == 3;
        };
        //$scope.method =  function() {
        //    myService.sendData(data).then(function() {

        //    });
        //}
        $scope.SliceNubmer = function (number) {
            var s = this + '', a = s.split('.');
            a[1] = a[1] || '';
            return parseFloat(a[0] + '.' + a[1].substring(0, x));
        }
        $scope.hotFilter = function (item) {
            return item.hot === true;
        }
        $scope.topFilter = function (item) {
            return item.top === true;
        }
        $scope.saleFilter = function (item) {
            return item.sale === true;
        }


        $scope.SendToServer = function (id) {
            $scope.findOredrById = id;
            $http.get("/Home/GetOrderStatus/" + id).then(function (response) {
                $scope.resOrderSatus = (response.data[0].res);
                $location.path("/result");
            });
            //$http.get("/Home/GetOrderStatus/", { data: id });
        };

        $scope.SendToServerMessage = function (id) {
            var arr = [];
            arr[0] = id.Name;
            arr[1] = id.Email;
            arr[2] = id.Message1;
            $http.post("/Home/AddMessageToDB/", { data: arr }).then(function (response) {
                alert(response.data[0].res);
            });
            //$http.get("/Home/GetOrderStatus/", { data: id });
            //JSON.stringify(id)
        };

        $scope.SentOrderToServer = function (details) {
            if ($scope.cartSum < 1) {
                alert("Купите что то");
                return;
            }
            var orderDet = [];
            orderDet.push(details.name);
            orderDet.push(details.phoneNumber);
            orderDet.push(details.email);
            orderDet.push(details.street);
            orderDet.push(details.house);
            orderDet.push(details.room);
            orderDet.push(details.text);

            var prodId = [];
            var prodPrice = [];
            var prodCount = [];
            var product = new Object();
            for (var i = 0; i < $scope.items.length; i++) {

                if ($scope.items[i].selected == true) {
                    $scope.saleCoff = $scope.items[i].sale == true ? 0.95 : 1;
                    prodId.push($scope.items[i].id);
                    prodCount.push($scope.items[i].count);
                    prodPrice.push($scope.items[i].price * $scope.saleCoff).toFixed(2);
                }
            }
            var mass = [];
            mass[0] = orderDet;
            mass[1] = prodId;
            mass[2] = prodCount;
            mass[3] = prodPrice;


            $http.post("Home/AddOrderToDb", { data: mass }).then(function (response) {
                $scope.myOrderID = (response.data[0].res);
            });
            $location.path("/Success");
        }


    }]).service('sushiService', function ($http) {
        this.getSushi = function () {
            return $http.get("/Home/GetSushi");
        };
        this.getCategory = function () {
            return $http.get("/Home/GetCategory");
        };
    })


