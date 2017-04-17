angular.module("visaDocumentation", [])

.controller("visaDocumentationController", function ($scope, $location, $http, growl) {
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    $scope.initVisa = function () {
        $http({
            method: "POST",
            url: "/VisaDocumentation/GetVisaDocumentation",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.VisaDocumentation = data.data;
        });
    }

    $scope.saveVisa = function (value) {
        $http({
            method: "POST",
            url: "/VisaDocumentation/AddUpdateVisa",
            data: { visaDocumentation: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }
});