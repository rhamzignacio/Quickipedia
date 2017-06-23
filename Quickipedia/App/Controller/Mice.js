angular.module("mice", [])

.controller("miceController", function($scope, $location, $http, growl, Upload){
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }
    //=================PROGRAM===================
    $scope.uploadClick = function () {
        $("#Files").click();
    }

    $scope.uploadFiles = function (files, errFiles) {
        $scope.files = files;
        $scope.errFiles = errFiles;

        angular.forEach(files, function (file) {
            if (file.size >= 25600000) {
                growl.error("Maxium file upload is 25MB", { title: "Error!", ttl: 3000 });
            }
            else {
                if (file.Status != "X") {
                    file.upload = Upload.upload({
                        url: "/Mice/FileUpload",
                        data: { file: file },
                        async: true
                    }).then(function (data) {
                        growl.success("Successfully uploaded", { ttl: 2000 });

                        $scope.InitProgram();
                    });
                }
            }
        });
    }

    $scope.AssignFileToDelete = function (value) {
        vm.FileDelete = value;
    }

    $scope.DeleteFile = function () {
        $http({
            method: "POST",
            url: "/Mice/DeleteAttachment",
            data: { file: vm.FileDelete },
            async: true
        }).then(function (data) {
            if (data.data === "") {
                growl.success("Successfully delete", { ttl: 2000 });

                $scope.InitProgram();
            }
            else {
                growl.error(data.data, { title: "Error!", ttl: 3000 });
            }
        });
    }

    $scope.AssignDeleteLink = function (value) {
        vm.ToBeDelete = value;
    }

    $scope.DeleteLink = function () {
        $http({
            method: "POST",
            url: "/Mice/DeleteMiceProgram",
            data: { links: vm.ToBeDelete }
        }).then(function (data) {
            if (data.data === "Saved" || data.data === "Updated") {
                growl.success("Successfully deleted", { ttl: 2000 });

                vm.ToBeDelete.Status = "X";
            }
        });
    }

    $scope.OpenModal = function (value) {
        vm.Modal = value;

        vm.Modal.Status = "U";
    }

    $scope.SaveLink = function (link) {
        $http({
            method: "POST",
            url: "/Mice/SaveMiceProgram",
            data: { links: link }
        }).then(function (data) {
            if (data.data === "Saved" || data.data == "Updated") {
                PopUpMessage(data.data);

                $("#linkModal").modal('hide');

                $scope.InitProgram();
            }
        });
    }

    $scope.ClearModal = function () {
        vm.Modal = {};
    }

    $scope.InitProgram = function () {
        $http({
            method: "POST",
            url: "/Mice/GetMiceProgram",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.Links = data.data.links;

            if (vm.FileUploads == null) {
                vm.FileUploads = new Array();
            }

            vm.FileUploads = data.data.attachments;

            vm.ForUpload = new Array();
        });
    }

    $scope.OpenLink = function (value) {
        vm.Modal = value;
    }

    $scope.Delete = function (value) {
        vm.Status = "X";
    }

    //=================MICE OTHER================
    $scope.initOther = function () {
        $http({
            method: "POST",
            url: "/Mice/GetOther",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.MiceOther = data.data;
        });
    }

    $scope.SaveOther = function (value) {
        $http({
            method: "POST",
            url: "/Mice/SaveOther",
            data: { other: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
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