(function () {
    'use strict';

    angular.module('sushiApp').directive('cartsummary', cartsummary);

    function cartsummary(cartFactory) {
        return {
            templateUrl: '/scripts/angular/views/cartSummary.html',
            controller: function ($scope) {
                var cartData = cartFactory.getProduct();

                $scope.total = function () {
                    var total = 0;
                    for (var i = 0; i < cartData.length; i++) {
                        total += (cartData[i].price * cartData[i].count);
                    }

                    return total;
                }
            }
        };
    }
})()