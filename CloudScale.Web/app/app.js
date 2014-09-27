/// <reference path="../scripts/typings/knockout/knockout.d.ts" />
/// <reference path="models/movie.ts" />
define(["require", "exports"], function(require, exports) {
    ko.components.register('movie-display', { require: 'app/components/movie-display' });
    ko.components.register('login-form', { require: 'app/components/login-form' });
    ko.components.register('register-form', { require: 'app/components/register-form' });
    ko.components.register('access-denied', { require: 'app/components/access-denied' });

    var MainViewModel = (function () {
        function MainViewModel() {
            this.movie = ko.observable(null);
            this.movie(new CloudScale.Movie('test', 'War of the Worlds', '', 5));
        }
        return MainViewModel;
    })();
    exports.MainViewModel = MainViewModel;

    var vm = new MainViewModel();

    ko.applyBindings(vm);
});
//# sourceMappingURL=app.js.map
