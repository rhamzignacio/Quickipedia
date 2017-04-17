angular.module("clientBooker", [])

.controller("clientBookerContoller", function ($scope, $location, $http, growl) {
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    $scope.initClientBooker = function () {
        $http({
            method: "POST",
            url: "/ClientBookerApprover/GetClientBooker",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            vm.ClientBookerApprover = data.data;
        });
    }

    $scope.saveClientBooker = function (value) {
        $http({
            method: "POST",
            url: "/ClientBookerApprover/AddUpdateClientBooker",
            data: { booker: value }
        }).then(function(data){
            PopUpMessage(data.data);
        });
    }
});