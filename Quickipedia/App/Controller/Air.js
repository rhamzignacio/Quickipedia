angular.module("air", [])

.controller("airController", function ($scope, $location, $http, growl) {
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    GetAirlinesDropDown = function () {
        $http({
            method: "POST",
            url: "/Air/GetAirlines",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            vm.AirlineDropDown = data.data;
        });
    }

    $scope.Remove = function (value) {
        value.Status = "X";
    }

    $scope.QuotationFormatDropDown = [
        { value: "Compleat", label: "Compleat" },
        { value: "Parsing", label: "Parsing" },
        { value: "Other", label: "Other" }
    ];

    $scope.AuthorityToTicketDropDown = [
        { value: "BTR", label: "BTR (Business Travel Request)" },
        { value: "CNFM", label: "Conforme" },
        { value: "eBTA", label: "eBTA (Electornic Business Travel Authority" },
        { value: "EMAIL", label: "Email" },
        { value: "PO", label: "PO (Purchase Order)" },
        { value: "RFT", label: "RFT (Request For Travel)" },
        { value: "TBS", label: "TBS (Travel Booking Sheet)" },
        { value: "TCS", label: "TCS (Ticketing Confrome Sheet)"},
        { value: "TO", label: "TO (Travel Order)" },
        { value: "TOR", label: "TOR (Travel Order Request)" },
        { value: "TA", label: "TA (Travel Authorizer)" },
        { value: "TR", label: "TR (Travel Request)" },
        { value: "TRF", label: "TRF (Travel Request Form)" },
        { value: "TRN", label: "TRN (Traven Request No)" },
    ];

    $scope.initOthers = function () {
        $http({
            method: "POST",
            url: "/Air/GetOthers",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.Others = data.data;
        });
    }

    $scope.initSLA = function () {
        $http({
            method: "POST",
            url: "/Air/GetSLA",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.SLA = data.data;
        });
    };

    $scope.initTripAuthorizer = function () {
        $http({
            method: "POST",
            url: "/Air/GetTripAuthorizerProcess",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.TripAuthorizer = data.data;
        });
    };

    $scope.initTravelPolicy = function () {
        $http({
            method: "POST",
            url: "/Air/GetTravelPolicy",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.TravelPolicy = data.data;
        });
    };

    $scope.saveOthers = function (value) {
        $http({
            method: "POST",
            url: "/Air/SaveOthers",
            data: {other:value}
        }).then(function(data){
            PopUpMessage(data.data);
        });
    }

    $scope.saveSLA = function (value) {
        $http({
            method: "POST",
            url: "/Air/AddUpdateSLA",
            data: { sla: value }
        }).then(function(data){
            PopUpMessage(data.data);
        });
    }

    $scope.saveTripAuthorizer = function (value) {
        $http({
            method: "POST",
            url: "/Air/AddUpdateTripAuthorizerProcess",
            data: { tripAuthorizer: value }
        }).then(function(data){
            PopUpMessage(data.data);
        });
    }

    $scope.saveTravelPolicy = function (value) {
        $http({
            method: "POST",
            url: "/Air/AddUpdateTravelPolicy",
            data: { travelPolicy: value }
        }).then(function(data){
            PopUpMessage(data.data);
        });
    }

    //=================TRAVEL SECURITY=====================
    $scope.initTravelSecurity = function () {
        $http({
            method: "POST",
            url: "/Air/GetTravelSecurity",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.TravelSecurity = data.data;
        });
    };

    $scope.saveTravelSecurity = function (value) {
        $http({
            method: "POST",
            url: "/Air/AddUpdateTravelSecurity",
            data: { travelSecurity: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }

    //=================DISALLOWED AIRLINES=================
    $scope.initDisallowedAirline = function () {
        $http({
            method: "POST",
            url: "/Air/GetDisallowedAirlines",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.DisallowedAirlines = data.data;

            GetAirlinesDropDown();
        });
    };

    $scope.ClearDisModal = function () {
        vm.ModalDis = {};
    }

    $scope.selectDisAirline = function (val) {
        vm.ModalDis = val;

        if (val.ContractStart != null || val.ContractStart != "") {
            vm.ModalDis.ContractStart = new Date(val.ShowContractStart);
        }

        if (val.ContractEnd != null || val.ContractEnd != "") {
            vm.ModalDis.ContractEnd = new Date(val.ShowContractEnd);
        }
    }

    $scope.DeleteDisAirline = function () {
        vm.ModalDis.Status = "X";

        $http({
            method: "POST",
            url: "/Air/AddUpdateDisallowedAirlines",
            data: { airlines: vm.ModalDis }
        }).then(function (data) {
            growl.success("Successfully " + "Deleted", { ttl: 2000 });
        });
    }

    $scope.saveDisallowedAirline = function (value) {
        $http({
            method: "POST",
            url: "/Air/AddUpdateDisallowedAirlines",
            data: { airlines: value }
        }).then(function(data){
            PopUpMessage(data.data);

            if (data.data == "Saved" || data.data == "Updated") {
                $scope.initDisallowedAirline();

                $("#airlineModal").modal('hide');
            }
        });
    }

    $scope.addDisallowedAirline = function (value) {
        if (vm.DisallowedAirlines == null) {
            vm.DisallowedAirlines = new Array();
        }

        var temp = vm.DisallowedAirlines.filter(function (o) { return o.AirlineCode == value });

        if (temp.length == 0) {
            var airline = vm.AirlineDropDown.filter(function (o) { return o.AirlineCode == value });

            vm.DisallowedAirlines.push(airline[0]);

            growl.success("Sucessfully added " + airline[0].AirlineName, { ttl: 2000 });
        }
        else {
            growl.error("Airline already added", { title: "Error!", ttl: 3000 });
        }
    }

    //=================PREFERRED AIRLINES===================
    $scope.initPreferredAirline = function () {
        $http({
            method: "POST",
            url: "/Air/GetPreferredAirlines",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.PreferredAirlines = data.data;

            GetAirlinesDropDown();
        });
    };

    $scope.selectPrefAirline = function (val) {
        vm.ModalPref = val;

        if (val.ContractStart != null || val.ContractStart != "") {
            vm.ModalPref.ContractStart = new Date(val.ShowContractStart)
        }

        if (val.ContractEnd != null || val.ContractEnd != "") {
            vm.ModalPref.ContractEnd = new Date(val.ShowContractEnd);
        }
    }


    $scope.DeletePrefAirline = function () {
        vm.ModalPref.Status = "X";

        $http({
            method: "POST",
            url: "/Air/AddUpdatePreferredAirlines",
            data: { airlines: vm.ModalPref }
        }).then(function (data) {
            growl.success("Successfully " + "Deleted", { ttl: 2000 });
        });
    }

    $scope.savePreferredAirline = function (value) {
        $http({
            method: "POST",
            url: "/Air/AddUpdatePreferredAirlines",
            data: { airlines: value }
        }).then(function(data){
            PopUpMessage(data.data);

            if (data.data == "Saved" || data.data == "Updated") {
                $scope.initPreferredAirline();

                $("#airlineModal").modal('hide');
            }
        });
    }

    $scope.addPreferredAirline = function (value) {
        if (vm.PreferredAirlines == null) {
            vm.PreferredAirlines = new Array();
        }

        var temp = vm.PreferredAirlines.filter(function (o) { return o.AirlineCode == value });

        if (temp.length == 0) {
            var airline = vm.AirlineDropDown.filter(function (o) { return o.AirlineCode == value });

            vm.PreferredAirlines.push(airline[0]);

            growl.success("Sucessfully added " + airline[0].AirlineName, { ttl: 2000 });
        }
        else {
            growl.error("Airline already added", { title: "Error!", ttl: 3000 });
        }
    }
});