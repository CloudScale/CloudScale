define(["require", "exports", "text!app/components/movie-display.html"], function(require, exports) {
    /// <amd-dependency path="text!app/components/movie-display.html"/>
    /// <reference path="../../scripts/typings/knockout/knockout.d.ts" />
    /// <reference path="../models/movie.ts" />
    exports.template = require("text!app/components/movie-display.html");

    var viewModel = (function () {
        function viewModel(params) {
            this.movie = ko.observable(null);
            this.movie(params.value);
        }
        return viewModel;
    })();
    exports.viewModel = viewModel;
});
//# sourceMappingURL=movie-display.js.map
