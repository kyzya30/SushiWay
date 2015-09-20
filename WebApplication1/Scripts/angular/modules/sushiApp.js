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

        $scope.goFiltr = function (i) {
            $scope.category = i;
            $location.path("/productcategoriesfilter");
        }
        $scope.goToMenu = function () {
            $location.path("/menu");
        }

        $scope.goToOrder = function () {
            $location.path("/order");
        }

        sushiService.getSushi().then(function (res) {
            console.log(res);
            $scope.items = res.data;
        });

        $scope.isSelected = function (item) {
            item.count = 1;
            $scope.cartSum += item.price;
            item.selected = true;
        }

        $scope.addToCart = function(item) {
            //$scope.cartCollection = $scope.cartCollection ? $scope.cartCollection.concat(item) : [].concat(item);
            //console.log($scope.cartCollection);          
            item.count = +item.count + 1;       
                
            $scope.cartSum += item.price;
        }

        $scope.removeFromCart = function (item) {
            //$scope.cartCollection = $scope.cartCollection ? $scope.cartCollection.concat(item) : [].concat(item);
            //console.log($scope.cartCollection);
            if (item.count > 1) {
                item.count--;
                
            } else {
                item.selected = false;
            }

            $scope.cartSum -= item.price;
        }

        //$scope.items = [
        //            { id: 1, name: "Item 1", price: 10, category:1 },
        //            { id: 2, name: "Item 2", price: 12, category:1 },
        //            { id: 3, name: "Item 3", price: 15, category:1 },
        //            { id: 4, name: "Item 12", price: 110, category:2 },
        //            { id: 5, name: "Item 23", price: 122, category:3 },
        //            { id: 6, name: "Item 34", price: 152, category:4 },
        //            { id: 7, name: "Item 15", price: 103, category:4 },
        //            { id: 8, name: "Item 26", price: 125, category:4 },
        //            { id: 9, name: "Item 37", price: 151, category:5 }
        //];
        //$scope.method =  function() {
        //    myService.sendData(data).then(function() {

        //    });
        //}

    }]).service('sushiService', function($http) {
        this.getSushi = function() {
            return $http.get("/Home/GetSushi");
        }
        //this.getCategory = function(data) {
        //    return $http.post("/Home/GetCategory",data);
        //}
    })


