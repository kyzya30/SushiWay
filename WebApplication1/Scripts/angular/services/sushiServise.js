(function () {
    'use strict';

    function sushiService($http) {
        // Return public API
        function getSushi() {
            var request = $http({
                method: "GET",
                url: '/Home/GetSushi'
            });

            return request;
        }

        return ({
            getSushi: getSushi
        });
    }

    angular.module('sushiApp').service("sushiService", sushiService);
})()