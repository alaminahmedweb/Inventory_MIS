/// <reference path="../../angular.min.js" />
/// <reference path="../../angular.intellisense.js" />

var myApp = angular.module("myModule", [])
    .controller("myController", function ($scope, $http) {

        $scope.sortColumn = "Code";
        $scope.reverseSort = false;
        $scope.sortData= function(column) {
            $scope.reverseSort = ($scope.sortColumn == column) ? !$scope.reverseSort : false;
            $scope.sortColumn = column;
        }

        $scope.getSortClass= function(column) {
            if ($scope.sortColumn == column) {
                return $scope.reverseSort ? 'arrow-down' : 'arrow-up';
            }
            return '';
        }

        var successCallBack = function (response) {
            $scope.InvestigationList = response.data;
        }
        var errorCallBack = function (response) {
            $scope.error = response.data;
        }

        $http({
            method: 'GET',
            url: 'GetAllInvestigationList'
        }).then(successCallBack, errorCallBack);
    });

