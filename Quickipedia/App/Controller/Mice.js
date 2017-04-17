angular.module("mice", [])

.controller("miceController", function($scope, $location, $http, growl){
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    //=================MICE POLICY====================
    $scope.initMicePolicy = function () {
        $http({
            method: "POST",
            url: "/Mice/GetMicePolicy",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.MicePolicy = data.data;
        });
    };
    
    $scope.saveMicePolicy = function (value) {
        $http({
            method: "POST",
            url: "/Mice/AddUpdateMicePolicy",
            data: { micePolicy: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }

    //==================SMMP FOR MSD=======================
    $scope.initSMMPForMSD = function () {
        $http({
            method: "POST",
            url: "/Mice/GetSMMPForMSD",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.SMMPForMSD = data.data;
        });
    };

    $scope.saveSMMP = function (value) {
        $http({
            method: "POST",
            url: "/Mice/AddUpdateSMMP",
            data: { smmp: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }

    //================GROUP BOOKING POLICY======================
    $scope.initGroupBookingPolicy = function () {
        $http({
            method: "POST",
            url: "/Mice/GetGroupBookingPolicy",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.GroupPolicy = data.data;
        });
    };

    $scope.saveGroupBooking = function (value) {
        $http({
            method: "POST",
            url: "/Mice/AddUpdateGroupBooking",
            data: { groupBooking: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }


    //================MICE PRICING=============================
    $scope.initMicePricing = function () {
        $http({
            method: "POST",
            url: "/Mice/GetPricing",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.MicePricing = data.data;
        });
    };

    $scope.savePricing = function (value) {
        $http({
            method: "POST",
            url: "/Mice/AddUpdatePricing",
            data: { pricing : value }
        }).then(function(data){
            PopUpMessage(data.data);
        });
    }

    //==============TICKETING APPROVAL===============
    $scope.initTicketingApproval = function () {
        $http({
            method: "POST",
            url: "/Mice/GetTicketingApproval",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.TicketingApproval = data.data;
        });
    }

    $scope.saveTicketing = function (value) {
        $http({
            method: "POST",
            url: "/mice/AddUpdateTicketingApproval",
            data: { ticketingApproval: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }
});