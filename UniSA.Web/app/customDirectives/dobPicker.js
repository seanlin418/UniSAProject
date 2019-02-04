myApp.directive('dobPicker', function () {

    return {
        restrict: 'E',
        link: function (scope, element, attrs) {
            var params = {
                daySelector: '#dobday', /* Required */
                monthSelector: '#dobmonth', /* Required */
                yearSelector: '#dobyear', /* Required */
                dayDefault: 'Day', /* Optional */
                monthDefault: 'Month', /* Optional */
                yearDefault: 'Year', /* Optional */
                minimumAge: 12, /* Optional */
                maximumAge: 80 /* Optional */
            };
            element.dobPicker(params);
        }
    }
});