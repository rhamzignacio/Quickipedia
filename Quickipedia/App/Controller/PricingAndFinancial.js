angular.module("pricingAndFinancial",[])

.controller("pricingAndFinancialController", function ($scope, $location, $http, growl) {
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else if (message == "Deleted") {
            growl.success("Successfully Deleted", { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    $scope.BillingCurrency = [
        { value: "PHP", label: "(PHP) Philippine Peso" },
        { value: "USD", label: "(USD) US Dollar" },
        { value: "OTH", label: "Other" }
    ];

    $scope.YesNo = [
        { value: "Y", label: "Yes" },
        { value: "N", label: "No" }
    ];

    $scope.FOPDropdown = [
        { value: "Credit Card (Airplus)", label: "Credit Card (Airplus)" },
        { value: "Credit Card (Amex)", label: "Credit Card (Amex)" },
        { value: "Credit Card (Diners)", label: "Credit Card (Diners)" },
        { value: "Credit Card (JCB)", label: "Credit Card (JCB)" },
        { value: "Credit Card (MasterCard)", label: "Credit Card (MasterCard)" },
        { value: "Credit Card (Visa)", label: "Credit Card (Visa)" },        
        { value: "Invoice(7 days)", label: "Invoice(7 days)" },
        { value: "Invoice(15 days)", label: "Invoice(15 days)" },
        { value: "Invoice(30 days)", label: "Invoice(30 days)" },
        { value: "Invoice(45 days)", label: "Invoice(45 days)" },
        { value: "Invoice(60 days)", label: "Invoice(60 days)" },
        { value: "Invoice(90 days)", label: "Invoice(90 days)" },
        { value: "Others",label: "Others" }
    ];

    $scope.MerchantDropdown = [
        { value: "Merchant Fee (2%)", label: "Merchant Fee (2%)" },
        { value: "Merchant Fee (2.8%)", label: "Merchant Fee (2.8%)" },
        { value: "Merchant Fee (3.0%)", label: "Merchant Fee (3.0%)" },
        { value: "Merchant Fee (3.5%)", label: "Merchant Fee (3.5%)" },
    ];

    $scope.IssuedCardDropDown = [
        { value: "One Corporate Card", label: "One Corporate Card" },
        { value: "Individual Card", label: "Individual Card" },
        { value: "Others", label: "Others"},
    ];

    $scope.CCLiquidationDropDown = [
        { value: "Per Employee", label: "Per Employee" },
        { value: "Individual Card", label: "Individual Card" },
        { value: "Others", label: "Others"}
    ];

    $scope.TaxTypeDropDown = [
        { value: "VAT", label: "VAT" },
        { value: "VAT Inclusive", label: "VAT Inclusive" },
        { value: "Zero Rated", label: "Zero Rated" },
        { value: "Non-VAT", label: "Non-VAT" },
    ];

    $scope.FeesCategory = [
        { value: "Ticket Issuance Fees", label: "Ticket Issuance Fees" },
        { value: "Re-Issuance Fees", label: "Re-Issuance Fees" },
        { value: "Revalidation Fees", label: "Revalidation Fees" },
        { value: "Refund Fees", label: "Refund Fees" },
        { value: "Other Air Fees", label: "Other Air Fees" },
        { value: "Travel Insurance", label: "Travel Insurance" },
        { value: "After Office Hours Fees", label: "After Office Hours Fees" },
        { value: "GDS Hotel, Car, Rail Ferry Only", label: "GDS Hotel, Car, Rail, Ferry Only: Booked after ticket issuance" },
        { value: "Service Voucher", label: "Service Voucher / Payment billback (hotel, car, others)" },
        { value: "Local Transfer", label: "Local Transfer" },
        { value: "Other MICE Service fees", label: "Other MICE Service fees" },
        { value: "Online Booking Tool", label: "Online Booking Tool" },
        { value: "Visa/Passport/Others", label: "Visa / Passport / Others" },
        { value: "Pickup and Delivery", label: "Pickup and Delivery (During office hours)" },
        { value: "Loyalty Programme Booking", label: "Loyalty Programme Booking" },
        { value: "Airport Assistance", label: "Airport Assistance" },
        { value: "Accounting", label: "Accounting" },
        { value: "Account Management", label: "Account Management" }
    ];

    //======FARE REF===========
    $scope.InitFareRef = function () {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/GetFareRef",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage != "") {
                growl.error(data.data.errorMessage, { title: "Error!", ttl: 3000 });
            }
            else {
                vm.FareRef = data.data.fare;
            }
        });
    }

    $scope.SaveFareRef = function (value) {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/SaveFareRef",
            data: { fare: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }

    //=======END OF FARE REF==========
    $scope.CheckIfCreditCard = function (value) {
        if(value == "Credit Card (Airplus)" || value == "Credit Card (Amex)" || value == "Credit Card (Diners)"
            || value == "Credit Card (JCB)" || value == "Credit Card (MasterCard)" || value == "Credit Card (Visa)") {
            return true;
        }
        else {
            return false;
        }
    }

    $scope.ClearModal = function () {
        vm.Modal = {};
    }

    $scope.AssignTime = function (value) {
        vm.Time = value;
    }

    $scope.OpenTableFees = function (value) {
        value.Status = "U";

        vm.Modal = value;
    }

    $scope.DeleteTableFees = function () {
        vm.Modal.Status = "X";

        $http({
            method: "POST",
            url: "/PricingAndFinancial/SaveTableOfFees",
            data: { fees: vm.Modal }
        }).then(function (data) {
            PopUpMessage(data.data);

            if (data.data == "Saved" || data.data == "Updated") {
                $("#deleteModal").modal('hide');
            }
        });
    }

    $scope.SaveTableOfFees = function (value) {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/SaveTableOfFees",
            data: { fees: value }
        }).then(function (data) {
            PopUpMessage(data.data);

            if (data.data == "Saved" || data.data == "Updated") {
                //vm.TableOfFees.push(value)

                $scope.initTableOfFees();

                $("#feesModal").modal('hide');
            }
        });
    }

    $scope.SaveOther = function (value) {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/SaveOthers",
            data: { other: value },
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }

    $scope.initOther = function () {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/GetOthers",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.Other = data.data;
        });
    }

    $scope.Filter = function (value) {
        var list = vm.TableOfFees.filter(x=>x.CategoryID == value);

        if (list.length > 0) {
            return true;
        }
        else {
            return false;
        }
    }

    $scope.initTableOfFees = function () {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/GetTableOfFees",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.TableOfFees = data.data;
        });

        $http({
            method: "POST",
            url: "/PricingAndFinancial/GetCategoryDropDown",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            vm.CategoryDropDown = data.data.dropdown;

            vm.TableCategory = data.data.category;
        });
    }

    $scope.initSchedule = function () {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/GetScheduleOfInvoice",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.ScheduleOfInvoice = data.data;
        });
    }

    $scope.saveSchedule = function (value) {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/SaveScheduleOfInvoice",
            data: { schedule: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }

    $scope.saveInvoiceAttachment = function (value) {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/SaveInvoiceAttachment",
            data: { invoice: value }
        }).then(function (data) {
            PopUpMessage(data.data);
        });
    }

    $scope.initInvoiceAttachment = function(){
        $http({
            method: "POST",
            url: "/PricingAndFinancial/GetInvoiceAttachment",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            vm.InvoiceAttachment = data.data;  
        });
    }

    $scope.initPricingModel = function(){
        $http({
            method: "POST",
            url: "/PricingAndFinancial/GetPricingModel",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            vm.PricingModel = data.data;
        });
    }

    $scope.initFormOfPayment = function(){
        $http({
            method: "POST",
            url: "/PricingAndFinancial/GetFormOfPayment",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.FormOfPayment = data.data;
        });
    }

    $scope.initBillingCollectionFinance = function () {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/GetBillingCollectionFinance",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.BillingCollectionFinances = data.data;
        });
    }

    $scope.initRefundProcess = function () {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/GetRefundProcess",
            arguments: {"Content-Type": "application/json"}
        }).then(function(data){
            vm.RefundProcess = data.data;
        });
    }

    $scope.savePricingModel = function (value, type){
        $http({
            method: "POST",
            url: "/PricingAndFinancial/AddUpdatePricingModel",
            data: {
                pricingModel: value,
                type: type
            }
        }).then(function(data){
            PopUpMessage(data.data);
        });
    }

    $scope.saveFormOfPayment = function (value) {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/AddUpdateFormOfPayment",
            data: { fop: value }
        }).then(function(data){
            PopUpMessage(data.data);
        });
    }

    $scope.saveBillingCollection = function (value) {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/AddUpdateBillingCollectionFinance",
            data: { billingCollections: value }
        }).then(function(data){
            PopUpMessage(data.data);

            $scope.initBillingCollectionFinance();
        });
    }

    $scope.saveRefundProcess = function (value) {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/AddUpdateRefundProcess",
            data: { refundProcess: value }
        }).then(function(data){
            PopUpMessage(data.data);
        });
    }

    $scope.editBilling = function (value) {
        value.Status = "U";

        vm.Modal = value;
    }

    $scope.addBilling = function (value) {
        if (vm.BillingCollectionFinances == null) {
            vm.BillingCollectionFinances = new Array();
        }

        var temp = vm.BillingCollectionFinances.filter(function(o){return o.IDNo == value.IDNo});

        if(temp.length != 0){
            temp.Name = value.Name;
            temp.Position = value.Position;
            temp.Email = value.Email;
            temp.ContactNo = value.ContactNo;
        }
        else{

            var person = {
                IDNo: vm.BillingCoinitTableOfFeesllectionFinances.length + 1,
                ID: null,
                Name: value.Name,
                Position: value.Position,
                Email: value.Email,
                ContactNo: value.ContactNo,
                Status: "N"
            }

            vm.BillingCollectionFinances.push(person);
        }
    }

    $scope.RemoveBilling = function (value) {
        vm.DeleteBilling = value;
    }

    $scope.DeleteBilling = function () {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/DeleteBillingCollectionFinance",
            data: { billing: vm.DeleteBilling }
        }).then(function (data) {
            PopUpMessage(data.data);

            if (data.data == "Deleted") {
                $("#deleteBilling").modal('hide');
            }

            $scope.initBillingCollectionFinance();
        });
    }

    //===========Table of Fees Category===========
    $scope.SaveCategory = function (value) {
        if (value.CategoryName === "") {
            PopUpMessage("Category is required");
        }
        else {
            $http({
                method: "POST",
                url: "/PricingAndFinancial/SaveCategory",
                data: { category: value }
            }).then(function (data) {
                PopUpMessage(data.data);

                if (data.data === "Saved" || data.data === "Updated") {
                    $("#categoryModal").modal('hide');

                    $scope.InitCategory();
                }
            })
        }
    }

    $scope.InitCategory = function () {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/GetTableOfFeesCategory",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.Category = data.data.category;
        });
    }

    $scope.AssignCatDelete = function (value) {
        vm.CategoryDelete = value;
    }

    $scope.AssignCatEdit = function (value) {
        vm.Modal = value;
    }


    $scope.DeleteCategory = function () {
        $http({
            method: "POST",
            url: "/PricingAndFinancial/DeleteCategory",
            data: { category: vm.CategoryDelete }
        }).then(function (data) {
            PopUpMessage(data.data);

            if (data.data === "Deleted") {
                $("#deleteModal").modal('hide');
            }
         })
    }

    $scope.NewCategory = function () {
        vm.Modal.ArrangeBy = '';

        vm.Modal.CategoryName = '';
    }

    $scope.IfNotNull = function (value) {
        for (var i = 0; i < vm.TableOfFees.length; i++) {
            if (vm.TableOfFees[i].CategoryID == value) {
                return true;
            }
        }
    }
});
