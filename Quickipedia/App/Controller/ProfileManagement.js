angular.module("profileManagement", [])

.controller("profileManagementController", function ($scope, $location, $http, growl) {
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    $scope.BookingType = [
        { value: "TRADITIONAL", label: "Traditional" },
        { value: "OBT", label: "Online Booking Tool" },
        { value: "OTHER", label: "Other"}
    ]

    $scope.ProfileTypeDropDown = [
        { value: "MANUAL", label: "Manual" },
        { value: "TSPM", label: "TSPM" },
        { value: "CONCUR", label: "Concur" },
        { value: "GETTHERE", label: "Get There" },
        { value: "OTHER", label: "Other" }
    ];

    $scope.initProfile = function () {
        $http({
            method: "POST",
            url: "/ProfileManagement/GetProfileManagement",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.Profile = data.data;
        });
    }

    $scope.saveProfile = function (value) {
        $http({
            method: "POST",
            url: "/ProfileManagement/SaveProfileMangement",
            data: { profile: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }
});