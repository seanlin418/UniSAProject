'use strict'

myApp.controller("adminController", [function () {

    var vm = this;

    var tabSelection = {"School": 0, "Students": 1, "Teachers": 2 };
    Object.freeze(tabSelection);

    vm.tabSelection = tabSelection;

    var tabs = [{
        selection: tabSelection.Admin,
        name: "School",
        url: "/app/admin/adminSchoolsTab.html"
    },{
        selection: tabSelection.Students,
        name: "Students",
        url: "/app/admin/tabs/school/adminStudentsTab.html"
    }, {
            selection: tabSelection.Teachers,
        name: "Teachers",
        url: "/app/admin/tabs/school/adminTeachersTab.html"
    }];


    vm.selectedTab;

    vm.onTabClicked = function (selected) {
        if (selected == tabSelection.Students) {
            vm.selectedTab = tabs[1];

        } else if (selected == tabSelection.Teachers) {
            vm.selectedTab = tabs[2];

        } else {
            vm.selectedTemplate = tabs[0];

        };
    };


}]);