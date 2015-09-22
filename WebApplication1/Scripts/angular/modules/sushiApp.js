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

        $routeProvider.when("/order", {
            templateUrl: "scripts/angular/views/OrderSearch.html"
        });

        $routeProvider.when("/productname", {
            templateUrl: "scripts/angular/views/ProductCategoriesFilter.html"
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
    .controller("navCtrl", ['$scope', '$location', 'sushiService', function ($scope, $location, sushiService) {
        $scope.cartSum = 0;

        $scope.goToCart = function () {
            $location.path("/cart");
        }

        $scope.goFiltr = function (cat) {
            $scope.findCategory = cat.id;
            $location.path("/filter");
        }

        $scope.goToMenu = function () {
            $location.path("/menu");
        }

        $scope.gotoNameFilter = function() {
            $location.path("/productname");
        }

        $scope.goToOrder = function () {
            $location.path("/order");
        }

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
        }

        $scope.addToCart = function (item) {
            item.count = +item.count + 1;

            $scope.cartSum += item.price;
        }

        $scope.findOredrById = "";

        $scope.matchPattern = /^\d+$/;

        $scope.findOrderStatus = function () {

        }

        $scope.makeOrder = function (newOrder) {

        }

        $scope.sendMessage = function (newMessage) {

        }

        $scope.removeFromCart = function (item) {
            if (item.count > 1) {
                item.count--;

            } else {
                item.selected = false;
            }

            $scope.cartSum -= item.price;
        }

        $scope.FindProductName = "";

        $scope.findByNameFilter = function (item) {
            return item.name == $scope.FindProductName;
        }

        $scope.sortProduct = function (item) {
            return item.categoryId == $scope.findCategory;
        }

        $scope.countFilter = function (item) {
            //return item.count > 0;
            return ((item.selected) && (item.categoryId != 3));
        }

        $scope.addProduct = function (item) {
            return item.categoryId == 3;
        }

        //$scope.method =  function() {
        //    myService.sendData(data).then(function() {

        //    });
        //}

    }]).service('sushiService', function ($http) {
        this.getSushi = function () {
            return $http.get("/Home/GetSushi");
        }

        this.getCategory = function () {
            return $http.get("/Home/GetCategory");
        }
    })


