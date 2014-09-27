define(["require", "exports", "text!app/components/login-form.html"], function(require, exports) {
    /// <amd-dependency path="text!app/components/login-form.html"/>
    /// <reference path="../../scripts/typings/knockout/knockout.d.ts" />
    exports.template = require("text!app/components/login-form.html");

    var viewModel = (function () {
        function viewModel() {
            this.userName = ko.observable("ShawInnes");
            this.password = ko.observable(null);
            this.confirmPassword = ko.observable(null);
        }
        viewModel.prototype.execute = function () {
            console.log('click.');
        };
        return viewModel;
    })();
    exports.viewModel = viewModel;
});
//# sourceMappingURL=login-form.js.map
