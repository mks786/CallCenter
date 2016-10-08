angular
    .module('ordersApp')
    .directive('dateInput', dateInput);

function dateInput() {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            ngModelCtrl.$formatters.length = 0;
            ngModelCtrl.$parsers.length = 0;
        }
    };
}