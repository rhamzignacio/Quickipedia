angular.module("rail", ["ngFileUpload"])

.controller("railController", function ($scope, $location, $http, growl, Upload) {
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    ReloadProgram = function () {
        $http({
            method: "POST",
            url: "/Rail/GetRailProgram",
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
    //===============RAIL PROGRAM====================
    $scope.initProgram = function () {
        $http({
            method: "POST",
            url: "/Rail/GetRailProgram",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            vm.Links = data.data.links;

            if (vm.FileUploads == null) {
                vm.FileUploads = new Array();
            }

            vm.FileUploads = data.data.attachments;

            vm.ForUpload = new Array();
        });
    }

    UploadFile = function () {
        angular.forEach(vm.ForUpload, function (file) {
            if (file.Status != "X") {
                file.upload = Upload.upload({
                    url: "/Rail/FileUpload",
                    data: { file: file },
                    async: true
                });
            }
        });

    }

    UpdateFile = function (files) {

        $http({
            method: "POST",
            url: "/Rail/UpdateAttachment",
            data: { files: files },
            async: true
        }).then(function (data) {

        });
    }

    SaveLink = function (link) {
        $http({
            method: "POST",
            url: "/Rail/SaveRailProgram",
            data: {
                links: link,
            },
            async: true
        }).then(function (data) {

        });
    }

    $scope.saveRailProgram = function (link, files) {
        UploadFile();

        UpdateFile(files);

        SaveLink(link);

        PopUpMessage("Saved");

        location.reload();
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

    $scope.uploadClick = function () {
        $("#Files").click();
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

        vm.Modal.Status = "U";
    }

    $scope.delete = function (value) {
        value.Status = "X";
    }

    //===============RAIL POLICY=====================
    $scope.initRailPolicy = function () {
        $http({
            method: "POST",
            url: "/Rail/GetRailPolicy",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            vm.RailPolicy = data.data;
        });
    };

    $scope.saveRailPolicy = function (value) {
        $http({
            method: "POST",
            url: "/Rail/AddUpdateRailPolicy",
            data: { railPolicy: value }
        }).then(function(data){
            PopUpMessage(data.data);
        });
    }

    $scope.initRailCorporateCode = function () {
        $http({
            method: "POST",
            url: "/Rail/GetRailCorporateCode",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            vm.RailCorporateCode = data.data;
        });
    };

    $scope.saveRailCorporateCode = function (value) {
        $http({
            method: "POST",
            url: "/Rail/AddUpdateRailCorporateCode",
            data: { railCorporateCode: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }
})