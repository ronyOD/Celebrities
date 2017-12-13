(function () {
    "use strict";

    angular.module('CelebrityApp')
        .service('CelebritiesService',
        ['$http', function ($http) {

            this.GetAll = function () {
                return $http.get('/api/celebrities');
            }

            this.Add = function (newCelebrity) {

                return $http({
                    url: 'api/celebrities/0',
                    method: 'POST',
                    data: newCelebrity,
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                });
            }

            this.Remove = function (id) {
                return $http.delete("/api/celebrities/" + id);
            }

            this.Update = function (id, updatedCelebrity) {

                return $http({
                    url: 'api/celebrities/' + id,
                    method: 'PUT',
                    data: updatedCelebrity,
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                });
            }

        }]);
}());