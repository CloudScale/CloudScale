/// <reference path="../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../scripts/typings/jquery.pnotify/jquery.pnotify.d.ts" />
/// <reference path="../scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../scripts/typings/bootstrap/bootstrap.d.ts" />
var CloudScale;
(function (CloudScale) {
    var Movie = (function () {
        function Movie(id, name, img) {
            this.Id = ko.observable(null);
            this.Name = ko.observable(null);
            this.Poster = ko.observable(null);
            this.Id(id);
            this.Name(name);
            this.Poster(img);
        }
        return Movie;
    })();
    CloudScale.Movie = Movie;

    var MainViewModel = (function () {
        function MainViewModel(baseApiUrl) {
            this.Movies = ko.observableArray([]);
            this.Movie = ko.observable(null);
            this.AddString = ko.observable(null);
            this.SearchString = ko.observable(null);
            this.IsLoading = ko.observable(false);
            this.baseUrl = baseApiUrl;
        }
        MainViewModel.prototype.GetRandomMovie = function () {
            var self = this;

            self.IsLoading(true);

            $.ajax({
                url: self.baseUrl + '/movies/random',
                type: 'get',
                contentType: 'application/json',
                success: function (m) {
                    var url = "http://image.tmdb.org/t/p/w154";

                    self.Movie(new CloudScale.Movie(m.Id, m.Name, url + m.PosterPath));
                    self.IsLoading(false);
                },
                error: function (allData) {
                    self.IsLoading(false);
                }
            });
        };

        MainViewModel.prototype.AddMovie = function () {
            var self = this;

            self.IsLoading(true);

            $.ajax({
                url: self.baseUrl + '/movies/new',
                type: 'post',
                data: { '': this.AddString() },
                success: function (allData) {
                    self.AddString(null);
                    self.IsLoading(false);
                },
                error: function (allData) {
                    self.AddString(null);
                    self.IsLoading(false);
                }
            });
        };

        MainViewModel.prototype.SearchMovie = function () {
            var self = this;

            self.IsLoading(true);
            var searchUri = self.baseUrl + '/movies/search/' + encodeURIComponent(this.SearchString());

            console.log(searchUri);

            $.ajax({
                url: searchUri,
                type: 'get',
                success: function (allData) {
                    var url = "http://image.tmdb.org/t/p/w154";

                    var mappedMovies = $.map(allData, function (item) {
                        console.log(item);
                        return new CloudScale.Movie(item.Id, item.Name, url + item.PosterPath);
                    });

                    self.Movies(mappedMovies);

                    console.log(mappedMovies);

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
