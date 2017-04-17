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

    UploadFile = function () {
        angular.forEach(vm.ForUpload, function (file) {
            if (file.Status != "X") {
                file.upload = Upload.upload({
                    url: "/Hotel/FileUpload",
                    data: { file: file },
                    async: true
                });
            }
            });
    }

    UpdateFile = function (files) {
        $http({
            method: "POST",
            url: "/Hotel/UpdateAttachment",
            data: { files: files },
            async: true
        }).then(function(data){
            
        });

    }

    SaveLink = function (link) {
        $http({
            method: "POST",
            url: "/Hotel/SaveHotelProgram",
            data: { links: link },
            async: true
        }).then(function(data){
            
        });
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

    $scope.uploadFiles = function (files, errFiles) {
        $scope.files = files;
        $scope.errFiles = errFiles;

        if (vm.ForUpload == null) {
            vm.ForUpload = new Array();
        }

        angular.forEach(files, function (file) {
            if (file.size >= 25600000) {
                growl.error("Maximum file upload is 25MB", { title: "Error!", ttl: 3000 });
            }
            else {
                vm.ForUpload.push(file);
            }
        });
    }

    $scope.saveHotelProgram = function (link, files) {
        UploadFile();

        UpdateFile(files);

        SaveLink(link);

        PopUpMessage("Saved");

        location.reload();
    }

    $scope.addLink = function (value) {
        if (vm.Links == null) {
            vm.Links = new Array();
        }

        var ifExist = vm.Links.filter(function (o) { return o.Link == value.Link });

        if (ifExist.length == 0) {
            vm.Links.push(value);
        }
    }

    $scope.openLink = function (value) {
        vm.Modal = value;
    }

    $scope.delete = function (value) {
        value.Status = "X";
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