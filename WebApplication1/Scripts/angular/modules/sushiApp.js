angular.module("sushiApp", ["ngRoute"])
        .config(function ($routeProvider, $locationProvider) {

            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });

            $routeProvider.when('/cart', {
                templateUrl: "scripts/angular/views/Cart.html"
            });

            $routeProvider.when("/order", {
                templateUrl: "scripts/angular/views/Contact.html"
            });

            $routeProvider.when("/contact", {
                templateUrl: "scripts/angular/views/OrderSearch.html"
            });

            $routeProvider.when("/menu", {
                templateUrl: "scripts/angular/views/menu.html"
            });

            $routeProvider.otherwise({
                templateUrl: "/scripts/angular/views/menu.html"
            });
        })
        .controller("navCtrl", function ($scope, $location) {

            $scope.goToCart = function () {
                $location.path("/cart");
            }

            $scope.goToMenu = function () {
                $location.path("/menu");
            }

            $scope.goToOrder = function () {
                $location.path("/order");
            }

            //$scope.goToContact = function () {
            //    $location.path("/contact");
            //}
            //    .constant("baseUrl", "http://localhost:2403/items/");

            //$scope.refresh = function () {
            //    $http.get(window.baseUrl).success(function (data) {
            //        $scope.items = data;
            //    });
            //}
                $scope.items = [
                    { id: 1, name: "Item 1", price: 10, category :1 },
                    { id: 2, name: "Item 2", price: 12, category: 1 },
                    { id: 3, name: "Item 3", price: 15, category: 1 },
                    { id: 4, name: "Item 12", price: 110, category: 2},
                    { id: 5, name: "Item 23", price: 122, category: 3 },
                    { id: 6, name: "Item 34", price: 152, category: 4 },
                    { id: 7, name: "Item 15", price: 103, category: 4 },
                    { id: 8, name: "Item 26", price: 125, category: 4 },
                    { id: 9, name: "Item 37", price: 151, category: 5}
                ];

                $scope.sortFunc = function (value) {
                    return value.category = 1 ;
                };

            function sushiCtrl($scope, sushiService, cartFactory) {
                sushiService.getSushi().then(function (response) {
                    $scope.sushiList = response.data;
                });
            }

        })
