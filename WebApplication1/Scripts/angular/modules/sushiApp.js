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

            $scope.goToContact = function () {
                $location.path("/contact");
            }

        })