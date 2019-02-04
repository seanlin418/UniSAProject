myApp.directive("knob", function () {
    return {
        scope: {
            min: "@",
            max: "@",
            width: "@"
        }, 
        link: function (scope, element, ) {
            element.knob({
                min: scope.min,
                max: scope.max,
                width: scope.width
            })
        }
    };
});