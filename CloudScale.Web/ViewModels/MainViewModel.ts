/// <reference path="../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../scripts/typings/bootstrap/bootstrap.d.ts" />
/// <reference path="../scripts/typings/jquery.pnotify/jquery.pnotify.d.ts" />

module CloudScale {
    export class Movie {
        name: string;

        constructor(name: string) {
            this.name = name;
        }
    }

    export class MainViewModel {
        public Movies: KnockoutObservableArray<Movie> = ko.observableArray([]);

        public AddString: KnockoutObservable<string> = ko.observable<string>(null);
        public SearchString: KnockoutObservable<string> = ko.observable<string>(null);

        public IsLoading: KnockoutObservable<boolean> = ko.observable<boolean>(false);

        private baseUrl: string;

        constructor(baseApiUrl: string) {
            this.baseUrl = baseApiUrl;
        }

        public Initialize() {
            var self = this;

            self.IsLoading(true);

            $.ajax({
                url: self.baseUrl + '/movies/',
                type: 'get',
                contentType: 'application/json',
                success: function (allData) {
                    var mappedMovies = $.map(allData, function (item) {
                        return new CloudScale.Movie(item.OriginalTitle)
                    });

                    self.Movies(mappedMovies);

                    self.IsLoading(false);
                },
                error: function (vm) {
                    self.IsLoading(false);
                }
            });
        }

        public AddMovie() {
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
        }

        public SearchMovie() {
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
        }
    }
}