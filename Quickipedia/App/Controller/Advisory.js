angular.module("advisory", [])

.controller("advisoryController", function ($scope, $location, $http, growl) {
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    initAdvisory = function () {
        $http({
            method: "POST",
            url: "/Advisory/GetAvisory",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.Advisory = data.data.advisory;
        });
    }

    $scope.init = function () {
        initAdvisory();
    }

    $scope.saveAdvisory = function (value) {
        $http({
            method: "POST",
            url: "/Advisory/SaveAdvisory",
            data: { advisory: value }
        }).then(function (data) {
            PopUpMessage(data.data);

            initAdvisory();
        });
    }
})