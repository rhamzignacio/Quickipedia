angular.module("user", [])

.controller("userController", function ($scope, $location, $http, growl) {
    var vm = this;


    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    $scope.Type = [
        { value: "I", label: "International" },
        { value: "D", label: "Domestic" },
        { value: "B", label: "Both" },
        { value: "AB", label: "Biller" },
        { value: "N", label: "Non-Biller, Non-TC"}
    ];

    $scope.AccessLevel = [
        { value: "U", label: "User" },
        { value: "A", label: "Administrator"},
    ]

    $scope.InitUser = function () {
        $http({
            method: "POST",
            url: "/User/GetUserList",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            vm.Users = data.data.users;
        });
    }

    $scope.SaveUser = function (value) {
        $http({
            method: "POST",
            url: "/User/SaveUser",
            data: { user: value }
        }).then(function (data) {
            PopUpMessage(data.data);

            $("#userModal").modal('hide');

            vm.Modal = {};
        });
    }

    $scope.OpenNewModal = function () {
        vm.Modal = {};
    }

    $scope.EditModal = function (value) {
        vm.Modal = value;
    }

    $scope.DeleteUser = function (value) {
        if (value.Status == 'Y') {
            value.Status = "X";
            value.ShowStatus = "Inactive";
        }
        else {
            value.Status = "Y";
            value.ShowStatus = "Active";
        }
    }
});