(function () {
    "use strict";

    angular.module('CelebrityApp')
        .controller('CelebritiesCtrl',
        ['$scope','CelebritiesService',
            function ($scope, CelebritiesService) {

                /* Help method */
                Object.toparams = function ObjecttoParams(obj) {
                    var p = [];
                    for (var key in obj) {
                        p.push(key + '=' + encodeURIComponent(obj[key]));
                    }
                    return p.join('&');
                };

                // declare controller function that
                // delegates to service
                this.GetAll = function () {
                    CelebritiesService.GetAll()
                        .then(successCallback, errorCallback);

                    function successCallback(response) {
                        $scope.Celebrities = response.data;
                    }
                    function errorCallback(error) {
                        console.log(error);
                    }
                }

                this.GetAll();

                this.Remove = function (id) {
                    CelebritiesService.Remove(id)
                        .then(successCallback, errorCallback);

                    function successCallback(response) {
                        location.reload();
                        $scope.message = "... Deleted!"
                    }
                    function errorCallback(error) {
                        console.log(error);
                    }
                }

                this.Add = function (celebrity) {
                    var newCelebrity = {
                        "Id": 0,
                        "Name": $scope.celebrity.name,
                        "Age": $scope.celebrity.age,
                        "Country": $scope.celebrity.country
                    };

                    CelebritiesService.Add(Object.toparams(newCelebrity))
                        .then(successCallback, errorCallback);

                    function successCallback(response) {
                        $scope.celebrity = null;
                        $scope.message = "... Added!"
                        location.reload();
                    }
                    function errorCallback(error) {
                        console.log(error);
                    }
                }

                this.Select = function (celebrity) {
                    $scope.celebrity = celebrity;
                }

                this.Update = function () {
                    var id = $scope.celebrity.id;
                    var updatedCelebrity = $scope.celebrity;

                    CelebritiesService.Update(id, Object.toparams(updatedCelebrity))
                        .then(successCallback, errorCallback);

                    function successCallback(response) {
                        $scope.celebrity = null;
                        $scope.message = "... Updated!"
                        location.reload();
                    }
                    function errorCallback(error) {
                        console.log(error);
                    }
                }

            }]);
}());