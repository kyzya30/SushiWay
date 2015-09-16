angular.module("sushiApp", ["ngRoute"])
        .config(function ($routeProvider, $locationProvider) {

            $locationProvider.html5Mode(true);

            $routeProvider.when('/cart', {
                templateUrl: "scripts/angular/views/Cart.html"
            });

            $routeProvider.when("/order", {
                templateUrl: "scripts/angular/views/OrderSearch.html"
            });

            $routeProvider.otherwise({
                templateUrl: "/scripts/angular/views/menu.html"
            });
        })
        .controller("navCtrl", function ($scope, $location) {

            $scope.goToView1 = function () {
                $location.path("/cart");
            }

            $scope.goToView2 = function () {
                $location.path("/order");
            }

        })