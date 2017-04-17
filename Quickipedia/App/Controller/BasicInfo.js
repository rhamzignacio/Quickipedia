angular.module("basicInfo", [])

.controller("basicInfoController", function ($scope, $location, $http, growl) {

    var vm = this;

    vm.ErrorMessage = [];

    vm.BasicInfo = {};

    $scope.ClientType = [
        { value: "BCD", label: "BCD" },
        { value: "Marine", label: "Marine"},
        { value: "Mice", label: "Mice" },    
        { value: "National", label: "National" }
    ];

    $scope.Remove = function (value) {
        value.Status = "X";
    }

    $scope.addContact = function (contact) {
        if (vm.BasicInfo.ClientContactPersons == null) {
            vm.BasicInfo.ClientContactPersons = new Array();
        }

     
        var temp = vm.BasicInfo.ClientContactPersons.filter(function (o) { return o.Name == contact.Name; });

        if (temp.length == 0) {
            contact.Status = "Y";

            vm.BasicInfo.ClientContactPersons.push(contact);
        }
        else {
            if (contact.Status == "U") {
                growl.success("Edited Client contact person", { title: "Successfully!", ttl: 2000 });
            }
            else if (temp[0].Status == "Y") {
                growl.warning("Client contact person already added", { title: "Warning!", ttl: 3000 });
            }
            else {
                vm.BasicInfo.ClientContactPersons.push(contact);
            }
        }
    }

    $scope.EditContact = function (value) {
        value.Status = "U";

        vm.OfficialContactPerson = {};

        vm.OfficialContactPerson = value;

        vm.OfficialContactPerson.Status = "U";
    }

    $scope.contactModal = function () {
        vm.OfficialContactPerson = {};
    }

    $scope.addDomTC = function (ID) {
        if (vm.BasicInfo.DomesticTCs == null) {
            vm.BasicInfo.DomesticTCs = new Array();
        }

        var temp = vm.Consultant.domTC.filter(function (o) { return o.AgentID == ID; });

        var tc = {
            AgentID: temp[0].AgentID,
            AgentName: temp[0].AgentName,
            Status: "Y"
        }

        var ifDuplicate = vm.BasicInfo.DomesticTCs.filter(function (o) { return o.AgentID == ID; });

        if (ifDuplicate.length == 0) {
            vm.BasicInfo.DomesticTCs.push(tc);
        }
        else {
            growl.warning("Travel Consultant already added", { title: "Warning!", ttl: 3000 });
        }
    }

    $scope.addIntlTC = function (ID) {

        if (vm.BasicInfo.InternationalTCs == null) {
            vm.BasicInfo.InternationalTCs = new Array();
        }

        var tcDropdown = vm.Consultant.intlTC.filter(function (o) { return o.AgentID == ID; });

        var tc = {
            AgentID: tcDropdown[0].AgentID,
            AgentName: tcDropdown[0].AgentName,
            Status: "Y"
        }

        var ifDuplicate = vm.BasicInfo.InternationalTCs.filter(function (o) { return o.AgentID == ID; });

        if (ifDuplicate.length == 0) {
            vm.BasicInfo.InternationalTCs.push(tc);
        }
        else {
            growl.warning("Travel Consultant already added", { title: "Warning!", ttl: 3000 });
        }
    }

    $scope.init = function () {
        $http({
            method: "POST",
            url: "/BasicInfo/GetBasicInfo",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.BasicInfo = data.data;

            if (vm.BasicInfo.ShowContractStartDate != '') {
                vm.BasicInfo.ContractStartDate = new Date(vm.BasicInfo.ShowContractStartDate);
            }

            if (vm.BasicInfo.ShowContractEndDate != '') {
                vm.BasicInfo.ContractEndDate = new Date(vm.BasicInfo.ShowContractEndDate);
            }
        });

        $http({
            method: "POST",
            url: "/BasicInfo/GetTCDropdown",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.Consultant = data.data;
        });
    }

    releoad = function () {
        $http({
            method: "POST",
            url: "/Home/SelectClient",
            data: { clientcode: $("#clientCode").val() }
        }).then(function (data) {
            main.CurrentClient = data.data;

            window.location.reload();

        });
    }

    $scope.save = function (info, type) {
        vm.ErrorMessage = [];

        if (info.ClientName == "" || info.ClientName == null) {
            vm.ErrorMessage.push("Client name is required");
        }

        if (info.ClientCode == "" || info.ClientCode == null) {
            vm.ErrorMessage.push("Client code is required");
        }

        if (info.ClientType == "" || info.ClientType == null) {
            vm.ErrorMessage.push("Client type is required");
        }

        if (info.AccountOfficerManager == "" || info.AccountOfficerManager == null) {
            vm.ErrorMessage.push("Account Officer is required");
        }

        if (vm.ErrorMessage.length == 0) {
            $http({
                method: "POST",
                url: "/BasicInfo/AddUpdateBasicInfo",
                data: {
                    basicInfo: info,
                    type: type
                }
            }).then(function (data) {
                if (data.data == "Saved" || data.data == "Updated") {
                    growl.success("Sucessfully " + data.data, { ttl: 2000 });

                    window.location.reload();
                }
                else {
                    growl.error(data.data, { title: "Error!", ttl: 3000 });
                }
            });
        }
        else {
            for (i = 0; i < vm.ErrorMessage.length; i++) {
                growl.warning(vm.ErrorMessage[i], { title: "Warning!", ttl:3000 });
            }
        }
    }
});