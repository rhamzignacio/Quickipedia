angular.module("hotel",[])

.controller("hotelController", function($scope, $location, $http, growl, Upload){
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    //===================HOTEL POLICY+=========================
    $scope.initHotelPolicy = function () {
        $http({
            method: "POST",
            url: "/Hotel/GetHotelPolicy",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.HotelPolicy = data.data;
        });
    };

    $scope.saveHotelPolicy = function (value) {
        $http({
            method: "POST",
            url: "/Hotel/AddUpdateHotelPolicy",
            data: { hotelPolicy: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }

    //==============HOTEL PROGRAM==================
    $scope.uploadClick = function () {
        $("#Files").click();
    }

    $scope.uploadFiles = function (files, errFiles) {
        $scope.errFiles = errFiles;

        angular.forEach(files, function (file) {
            if (file.size >= 25600000) {
                growl.error("Maximum file upload is 25MB", { title: "Error!", ttl: 3000 });
            }
            else {
                if (file.Status != "X") {
                    file.upload = Upload.upload({
                        url: "/Hotel/FileUpload",
                        data: { file: file },
                        async: true
                    }).then(function(data){
                        growl.success("Successfully uploaded", { ttl: 2000 });

                        $scope.initProgram();
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
            url: "/Hotel/DeleteAttachment",
            data: { files: vm.FileDelete },
            async: true
        }).then(function(data){
            if (data.data == "") {
                growl.success("Successfully deleted", { ttl: 2000 });

                $scope.initProgram();
            }
            else {
                growl.error(data.data, { title: "Error!", ttl: 3000 });
            }
        });

    }

    $scope.OpenTab = function (value) {
        var win = window.open(value, '_blank');

        win.focus();
    }

    $scope.AssignDeleteLink = function (value) {
        vm.ToBeDelete = value;
    }

    $scope.DeleteLink = function () {
        $http({
            method: "POST",
            url: "/Hotel/DeleteHotelProgram",
            data: { links: vm.ToBeDelete }
        }).then(function (data) {
            if (data.data == "Saved" || data.data == "Updated") {
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
            url: "/Hotel/SaveHotelProgram",
            data: { links: link },
        }).then(function(data){
            if (data.data == "Saved" || data.data == "Updated") {
                PopUpMessage(data.data);

                $("#linkModal").modal('hide');

                $scope.initProgram();
            }
        });
    }

    $scope.ClearModal = function () {
        vm.Modal = {};
    }

    $scope.initProgram = function () {
        $http({
            method: "POST",
            url: "/Hotel/GetHotelProgram",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.Links = data.data.links;

            if (vm.FileUploads == null) {
                vm.FileUploads = new Array();
            }

            vm.FileUploads = data.data.attachment;

            vm.ForUpload = new Array();
        });
    }


    $scope.saveHotelProgram = function (link, files) {
        UploadFile();

        UpdateFile(files);

        SaveLink(link);

        PopUpMessage("Saved");

        location.reload();
    }


    $scope.openLink = function (value) {
        vm.Modal = value;
    }

    $scope.delete = function (value) {
        value.Status = "X";
    }

    $scope.OpenInNewTab = function (value) {
        var win = window.open(value, '_blank');

        win.focus();
    }

    //=================HOTEL COPORATE CODE=========================
    $scope.initHotelCorporateCode = function () {
        $http({
            method: "POST",
            url: "/Hotel/GetHotelCorporateCode",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            vm.HotelCorporateCode = data.data;
        });
    };

    $scope.saveHotelCorporateCode = function (value) {
        $http({
            method: "POST",
            url: "/Hotel/AddUpdateHotelCorporateCode",
            data: { corporateCode: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }
});