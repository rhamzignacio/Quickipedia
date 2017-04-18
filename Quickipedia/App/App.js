var app = angular.module("app", ["angular-growl", "basicInfo", "pricingAndFinancial", "air", "hotel", "car", "rail", "visaDocumentation", "mice", "vip", "login", "clientBooker", "user", "advisory", "profileManagement", "bookingProcess", "ancillaries"])

.controller("mainController", ['$scope','$location','$http','growl',function ($scope, $location, $http, growl) {
    var main = this;

    $scope.init = function () {
        $http({
            method: "POST",
            url: "/Home/GetCurrentUser",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            main.CurrentUser = data.data.obj;

            main.ClientDropdown = data.data.clients;

            main.CurrentClient = data.data.selectedclient;
        });
    };

    $scope.SaveSelectedClient = function (client) {
        $http({
            method: "POST",
            url: "/Home/SelectClient",
            data: { clientcode: main.SelectedClient }
        }).then(function (data) {
            main.CurrentClient = data.data;

            if (data.data.ClientCode != "NewClient") {
                window.location.reload();
            }
            else {
                window.location.href = "/BasicInfo/Index";
            }

        });
    };

    $scope.Logout = function () {
        $http({
            method: "POST",
            url: "/Home/Logout",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data != "") {
                growl.error(data.data, { title: "Error!", ttl: 3000 });
            }
            else {
                window.location.href = "/Home/Login";
            }
        });
    }
}]);