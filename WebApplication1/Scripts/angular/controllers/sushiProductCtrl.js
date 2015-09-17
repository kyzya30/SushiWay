(function () {
    'use strict';

    angular.module('sushiApp').controller('sushiGlobalCtrl', sushiGlobalCtrl);

    
    function sushiGlobalCtrl($scope, sushiService, cartFactory) {
            sushiService.getSushi().then(function(response) {
                $scope.sushiList = response.data;
            });
    }
})()