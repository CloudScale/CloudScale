/// <reference path="../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../scripts/typings/jquery.pnotify/jquery.pnotify.d.ts" />
/// <reference path="../scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../scripts/typings/bootstrap/bootstrap.d.ts" />

module CloudScale {
    export class Movie {
        public Id: KnockoutObservable<string> = ko.observable<string>(null);
        public Name: KnockoutObservable<string> = ko.observable<string>(null);
        public Poster: KnockoutObservable<string> = ko.observable<string>(null);

        constructor(id: string, name: string, img: string) {
            this.Id(id);
            this.Name(name);
            this.Poster(img);
        }
    }

    export class MainViewModel {
        public Movies: KnockoutObservableArray<Movie> = ko.observableArray([]);

        public Movie: KnockoutObservable<Movie> = ko.observable<Movie>(null);

        public AddString: KnockoutObservable<string> = ko.observable<string>(null);
        public SearchString: KnockoutObservable<string> = ko.observable<string>(null);

        public IsLoading: KnockoutObservable<boolean> = ko.observable<boolean>(false);

        private baseUrl: string;

        constructor(baseApiUrl: string) {
            this.baseUrl = baseApiUrl;
        }

        public GetRandomMovie() {
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
        }

        public AddMovie() {
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
        }

        public SearchMovie() {
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
                        return new CloudScale.Movie(item.Id, item.Name, url + item.PosterPath)
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
        }
    }
}