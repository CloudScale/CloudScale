/// <reference path="../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../scripts/typings/bootstrap/bootstrap.d.ts" />
/// <reference path="../scripts/typings/jquery.pnotify/jquery.pnotify.d.ts" />
var CloudScale;
(function (CloudScale) {
    var Movie = (function () {
        function Movie(name) {
            this.name = name;
        }
        return Movie;
    })();
    CloudScale.Movie = Movie;

    var MainViewModel = (function () {
        function MainViewModel(baseApiUrl) {
            this.Movies = ko.observableArray([]);
            this.AddString = ko.observable(null);
            this.SearchString = ko.observable(null);
            this.IsLoading = ko.observable(false);
            this.baseUrl = baseApiUrl;
        }
        MainViewModel.prototype.Initialize = function () {
            var self = this;

            self.IsLoading(true);

            $.ajax({
                url: self.baseUrl + '/movies/',
                type: 'get',
                contentType: 'application/json',
                success: function (allData) {
                    var mappedMovies = $.map(allData, function (item) {
                        return new CloudScale.Movie(item.OriginalTitle);
                    });

                    self.Movies(mappedMovies);

                    self.IsLoading(false);
                },
                error: function (vm) {
                    self.IsLoading(false);
                }
            });
        };

        MainViewModel.prototype.AddMovie = function () {
            var self = this;

            self.IsLoading(true);

            $.ajax({
                url: self.baseUrl + '/movies/new/' + encodeURI(this.AddString()),
                type: 'get',
                contentType: 'application/json',
                success: function (vm) {
                    self.AddString(null);
                    self.IsLoading(false);
                },
                error: function (vm) {
                    self.AddString(null);
                    self.IsLoading(false);
                }
            });
        };

        MainViewModel.prototype.SearchMovie = function () {
            var self = this;

            self.IsLoading(true);

            $.ajax({
                url: self.baseUrl + '/movies/search',
                type: 'post',
                data: this.SearchString(),
                contentType: 'application/json',
                success: function (vm) {
                    self.SearchString(null);
                    self.IsLoading(false);
                },
                error: function (vm) {
                    self.SearchString(null);
                    self.IsLoading(false);
                }
            });
        };
        return MainViewModel;
    })();
    CloudScale.MainViewModel = MainViewModel;
})(CloudScale || (CloudScale = {}));
//# sourceMappingURL=MainViewModel.js.map
