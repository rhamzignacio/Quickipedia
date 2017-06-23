angular.module("profileManagement", [])

.controller("profileManagementController", function ($scope, $location, $http, growl, Upload) {
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    //PROFILE TEMPLATE
    $scope.UploadClick = function () {
        $("#Files").click();
    }

    $scope.UploadFiles = function (files, errFiles) {
        $scope.files = files;
        $scope.errFiles = errFiles;

        angular.forEach(files, function (file) {
            if (file.size >= 25600000) {
                growl.error("Maxium file upload is 25MB", { title: "Error!", ttl: 3000 });
            }
            else {
                if (file.Status != "X") {
                    file.upload = Upload.upload({
                        url: "/ProfileManagement/FileUpload",
                        data: { file: file },
                        async: true
                    }).then(function(data){
                        growl.success("Successfully uploaded", { ttl: 2000 });

                        $scope.InitProfileTemplate();
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
            url: "/ProfileManagement/DeleteAttachment",
            data: { file: vm.FileDelete }
        }).then(function (data) {
            if (data.data === "") {
                growl.success("Successfully delete", { ttl: 2000 });

                $scope.InitProfileTemplate();
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
            url: "/ProfileManagement/DeleteTemplateLink",
            data: { link: vm.ToBeDelete }
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

    $scope.SaveLink = function (value) {
        $http({
            method: "POST",
            url: "/ProfileManagement/SaveTemplateLink",
            data: { link: value }
        }).then(function (data) {
            if (data.data === "Saved" || data.data == "Updated") {
                PopUpMessage(data.data);

                $("#linkModal").modal('hide');

                $scope.InitProfileTemplate();
            }
        });
    }

    $scope.ClearModal = function () {
        vm.Modal = {};
    }

    $scope.InitProfileTemplate = function () {
        $http({
            method: "POST",
            url: "/ProfileManagement/GetProfileTemplate",
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
    //END OF PROFILE TEMPLATE

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