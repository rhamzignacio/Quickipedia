angular.module("bookingProcess", [])

.controller("bookingProcessController", function ($scope, $location, $http, growl) {
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    $scope.initINTL = function () {
        $http({
            method: "POST",
            url: "/BookingProcess/GetInternational",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.INTL = data.data;
        });
    }

    $scope.saveInternational = function (value) {
        $http({
            method: "POST",
            url: "/BookingProcess/SaveInternational",
            data: { international: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }

    $scope.initDOM = function () {
        $http({
            method: "POST",
            url: "/BookingProcess/GetDomestic",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.DOM = data.data;
        });
    }

    $scope.saveDomestic = function (value) {
        $http({
            method: "POST",
            url: "/BookingProcess/SaveDomestic",
            data: { domestic: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }
});