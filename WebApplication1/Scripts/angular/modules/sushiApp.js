angular.module("sushiApp", ["ngRoute"])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });

        $routeProvider.when("/cart", {
            templateUrl: "scripts/angular/views/Cart.html"
        });

        $routeProvider.when("/contact", {
            templateUrl: "scripts/angular/views/Contact.html"
        });

        $routeProvider.when("/result", {
            templateUrl: "scripts/angular/views/OrderStatusResult.html"
        });

        $routeProvider.when("/order", {
            templateUrl: "scripts/angular/views/OrderSearch.html"
        });

        $routeProvider.when("/productname", {
            templateUrl: "scripts/angular/views/ProductCategoriesFilter.html"
        });

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
    .controller("navCtrl", ['$scope', '$location', 'sushiService','$http', function ($scope, $location, sushiService, $http) {
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
        $scope.gotoNameFilter = function() {
            $location.path("/productname");
        };
        $scope.goToOrder = function () {
            $location.path("/order");
        };
        sushiService.getSushi().then(function (res) {
            console.log(res);
            $scope.items = res.data;
        });

        sushiService.getCategory().then(function (res1) {
            console.log(res1);
            $scope.category = res1.data;
        });

        $scope.isSelected = function (item) {
            item.count = 1;
            $scope.cartSum += item.price;
            item.selected = true;
        };
        $scope.addToCart = function (item) {
            item.count = +item.count + 1;

            $scope.cartSum += item.price;
        };
        $scope.findOredrById = " ";
        $scope.resOrderSatus = " ";

        $scope.matchPattern = /^\d+$/;

        
        $scope.makeOrder = function (newOrder) {

        };
       
        $scope.removeFromCart = function (item) {
            if (item.count > 1) {
                item.count--;

            } else {
                item.selected = false;
            }

            $scope.cartSum -= item.price;
        };
        $scope.FindProductName = "";

        $scope.findByNameFilter = function (item) {
            return item.name == $scope.FindProductName;
        };
        $scope.sortProduct = function (item) {
            
            return item.categoryId == $scope.findCategory;
        };
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

        $scope.SendToServer = function (id) {
            $scope.findOredrById = id;
            $http.get("/Home/GetOrderStatus/" + id).then(function(response) {
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
            var prodCount=[];
            var product = new Object();
            for (var i = 0; i < $scope.items.length; i++) {
                if ($scope.items[i].selected == true) {

                    prodId.push($scope.items[i].id);
                    prodCount.push($scope.items[i].count);
                    prodPrice.push($scope.items[i].price);
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


