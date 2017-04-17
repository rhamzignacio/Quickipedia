angular.module("vip", [])

.controller("vipController", function($scope, $location, $http, growl){
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    $scope.initVIPList = function () {
        $http({
            method: "POST",
            url: "/VIP/GetVIPList",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.VIP = data.data;
        });
    };

    $scope.saveVIP = function (value) {
        $http({
            method: "POST",
            url: "/VIP/SaveVIP",
            data: { vip : value }
        }).then(function(data){
            PopUpMessage(data.data.message);

            if (data.data.message == "Saved" || data.data.message == "Updated") {
                value.ID = data.data.ID;

                vm.VIP.push(value);
            }
        });
    }

    $scope.addVIP = function () {
        vm.vipModal = {};
    }

    $scope.editVIP = function (value) {
        vm.vipModal = value;
    }

    $scope.deleteVIP = function () {
        vm.vipModal.Status = "X";

        $http({
            method: "POST",
            url: "/VIP/SaveVIP",
            data: { vip: vm.vipModal }
        }).then(function (data) {
            if (data.data.message == "Deleted") {
                growl.success("Successfully " + "Successfully Deleted", { ttl: 2000 });

                $("#vipDeleteModal").modal('hide');
            }
        });
    }
});