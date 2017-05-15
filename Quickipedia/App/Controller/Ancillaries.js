angular.module("ancillaries", [])

.controller("ancillariesController", function ($scope, $location, $http, growl) {
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    $scope.FeesCategoryDropDown = [
        { value: "Additional Service List", label: "Additional Service List" },
        { value: "Exchange/Ammendent", label: "Exchange/Ammendent" },
        { value: "Loyalty Programme Booking", label: "Loyalty Programme Booking" },
        { value: "Insurance", label: "Insurance" },
        { value: "Documentation", label: "Documentation" },
        { value: "Pickup and Delivery", label: "Pick Up and Delivery (During Office Hour)" },
    ];

    $scope.init = function () {
        $http({
            method: "POST",
            url: "/Ancillaries/GetAncillaries",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.AncillariesFees = data.data.ancillaries;
        });
    }

    $scope.save = function (value) {
        $http({
            method: "POST",
            url: "/Ancillaries/SaveAncillaries",
            data: { ancillaries: value }
        }).then(function (data) {
            PopUpMessage(data.data);

            $scope.init();

            $("#feesModal").modal('hide');
        })
    }

    $scope.OpenFees = function (value) {
        value.Status = "U";

        vm.AnciModal = value;
    }

    $scope.AssignTime = function (value) {
        vm.Time = value;
    }

    $scope.ClearModal = function(){
        vm.AnciModal = {};
    }

    $scope.DeleteFees = function () {
        vm.AnciModal.Status = "X";

        $http({
            method: "POST",
            url: "/Ancillaries/SaveAncillaries",
            data: { ancillaries: vm.AnciModal }
        }).then(function (data) {
            PopUpMessage(data.data);

            $("#deleteModal").modal('hide');
        })
    }
});