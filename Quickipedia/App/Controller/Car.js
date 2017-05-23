angular.module("car", [])

.controller("carController", function($scope, $location, $http, growl, Upload){
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    //==============CAR PROGRAM===================

    $scope.OpenModal = function (value) {
        vm.Modal = value;

        vm.Modal.Status = "U";
    }

    inLoading = function(){
        document.getElementById("myDiv").style.display = "none";

        document.getElementById("loader").style.display = "block";
    }

    outLoading = function () {
        document.getElementById("loader").style.display = "none";

        document.getElementById("myDiv").style.display = "block";
    }

    $scope.AssignFileToDelete = function (value) {
        vm.FileDelete = value;
    }

    $scope.DeleteFile = function () {
        $http({
            method: "POST",
            url: "/Car/DeleteAttachment",
            data: { file: vm.FileDelete },
            async: true
        }).then(function (data) {
            if (data.data == "") {
                growl.success("Successfully deleted", { ttl: 2000 });

                $scope.initProgram();
            }
            else {
                growl.error(data.data, { title: "Error!", ttl: 3000 });
            }
        });
    }

    $scope.SaveLink = function (link) {
        $http({
            method: "POST",
            url: "/Car/SaveCarProgram",
            data: {
                links: link,
            },
            async: true
        }).then(function (data) {
            if (data.data == "Saved" || data.data == "Updated") {
                PopUpMessage(data.data)

                $("#linkModal").modal('hide');

                $scope.initProgram();
            }
        });
    }

    $scope.initProgram = function () {
        $http({
            method: "POST",
            url: "/Car/GetCarProgram",
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

    $scope.uploadClick = function () {
        $("#Files").click();
    }

    $scope.ClearModal = function () {
        vm.Modal = {};
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
                if (file.Status != "X") {
                    file.upload = Upload.upload({
                        url: "/Car/FileUpload",
                        data: { file: file },
                        async: true
                    }).then(function(data){
                        growl.success("Successfully uploaded", { ttl: 2000 });

                        $scope.initProgram();  
                    });
                }
            }
        });

        growl.success("Successfully uploaded", { ttl: 2000 });

        $scope.initProgram();
    }

    $scope.saveCarProgram = function (link, files) {
        inLoading();

        UploadFile();

        UpdateFile(files);

        SaveLink(link);

        PopUpMessage("Saved");

        location.reload();
    }

    $scope.addLink = function(value){
        if(vm.Links == null){
            vm.Links = new Array();
        }

        var ifExist = vm.Links.filter(function(o) {return o.Link == value.Link});

        if(ifExist.length == 0){
            vm.Links.push(value);
        }
    }

    $scope.openLink = function (value) {
        vm.Modal = value;
    }
    
    $scope.AssignDeleteLink = function (value) {
        vm.ToBeDelete = value;
    }

    $scope.DeleteLink = function () {
        $http({
            method: "POST",
            url: "/Car/DeleteHotelProgram",
            data: { links: vm.ToBeDelete }
        }).then(function (data) {
             if (data.data == "Saved" || data.data == "Updated") {
                growl.success("Successfully deleted", { ttl: 2000 });

                vm.ToBeDelete.Status = "X";
            }
        });
    }

    $scope.initCarPolicy = function () {
        $http({
            method: "POST",
            url: "/Car/GetCarPolicy",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.CarPolicy = data.data;
        });
    };

    $scope.saveCarPolicy = function (value) {
        $http({
            method: "POST",
            url: "/Car/AddUpdateCarPolicy",
            data: { carPolicy : value }
        }).then(function(data){
            PopUpMessage(data.data);
        });
    }

    $scope.initCarCorporateCode = function () {
        $http({
            method: "POST",
            url: "/Car/GetCarCorporateCode",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.CarCorporateCode = data.data;
        });
    };

    $scope.saveCarCorporateCode = function (value) {
        $http({
            method: "POST",
            url: "/Car/AddUpdateCarCorporateCode",
            data: { carCorporateCode : value }
        }).then(function(data){
            PopUpMessage(data.data);
        });
    }
});