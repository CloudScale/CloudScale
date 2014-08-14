/// <reference path="../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../scripts/typings/jquery.pnotify/jquery.pnotify.d.ts" />
/// <reference path="../scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../scripts/typings/bootstrap/bootstrap.d.ts" />

module CloudScale {
    export class Vote {
        MovieId: string;
        Score: number;

        constructor(movieId: string, score: number) {
            this.MovieId = movieId;
            this.Score = score;
        }
    }

    export class RegisterUser {
        userName: string;
        password: string;
        confirmPassword: string;

        constructor(userName: string, password: string, confirm: string) {
            this.userName = userName;
            this.password = password;
            this.confirmPassword = confirm;
        }
    }

    export class Movie {
        Id: KnockoutObservable<string> = ko.observable<string>(null);
        Name: KnockoutObservable<string> = ko.observable<string>(null);
        Poster: KnockoutObservable<string> = ko.observable<string>(null);
        Rating: KnockoutObservable<number> = ko.observable<number>(null);

        constructor(id: string, name: string, img: string, rating: number) {
            this.Id(id);
            this.Name(name);
            this.Poster(img);
            this.Rating(rating);
        }
    }

    export class LoginUser {
        grant_type: string;
        client_id: string;
        userName: string;
        password: string;

        constructor(userName: string, password: string) {
            this.grant_type = 'password';
            this.client_id = 'CloudScale';
            this.userName = userName;
            this.password = password;
        }
    }
}

module CloudScale {
    export class MainViewModel {
        public Movies: KnockoutObservableArray<Movie> = ko.observableArray([]);

        public CurrentMovie: KnockoutObservable<Movie> = ko.observable<Movie>(null);

        public AddString: KnockoutObservable<string> = ko.observable<string>(null);
        public SearchString: KnockoutObservable<string> = ko.observable<string>(null);

        public IsLoading: KnockoutObservable<boolean> = ko.observable<boolean>(false);
        public IsAuth: KnockoutObservable<boolean> = ko.observable<boolean>(false);

        private baseUrl: string;

        constructor(baseApiUrl: string) {
            this.baseUrl = baseApiUrl;

            var token = JSON.parse(localStorage.getItem('token'));

            if (token != null) {
                var access_token = token.access_token;
                var token_type = token.token_type;

                this.IsAuth(true);
            }
        }

        public GetRandomMovie() {
            var self = this;

            var token = JSON.parse(localStorage.getItem('token'));

            if (token == null)
                return;

            self.IsLoading(true);

            $.ajax({
                url: self.baseUrl + '/movies/random',
                type: 'get',
                contentType: 'application/json',
                success: function (m) {
                    if (m == null) {
                    } else {
                        var url = "http://image.tmdb.org/t/p/w154";
                        self.CurrentMovie(new Movie(m.id, m.title, url + m.posterPath, m.userRating));
                    }

                    self.IsLoading(false);
                },
                error: function (response) {
                    new PNotify({ title: 'error getting random movie' });

                    self.IsLoading(false);
                },
                beforeSend: function (xhr) {
                    var token = JSON.parse(localStorage.getItem('token'));

                    if (token != null) {
                        var access_token = token.access_token;
                        var token_type = token.token_type;

                        xhr.setRequestHeader('Authorization', 'Bearer ' + access_token);
                    }
                }
            });
        }

        public Vote(value, event) {
            var self = this;

            var vote = event.target.innerText;

            if (self.CurrentMovie() === null)
                return;

            $.ajax({
                url: self.baseUrl + '/movies/vote',
                type: 'post',
                data: new Vote(self.CurrentMovie().Id(), vote),
                success: function (m) {
                    self.GetRandomMovie();
                    self.IsLoading(false);
                },
                error: function (allData) {
                    self.IsLoading(false);
                },
                beforeSend: function (xhr) {
                    var token = JSON.parse(localStorage.getItem('token'));

                    if (token != null) {
                        var access_token = token.access_token;
                        var token_type = token.token_type;

                        xhr.setRequestHeader('Authorization', 'Bearer ' + access_token);
                    }
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
                },
                beforeSend: function (xhr) {
                    var token = JSON.parse(localStorage.getItem('token'));

                    if (token != null) {
                        var access_token = token.access_token;
                        var token_type = token.token_type;

                        xhr.setRequestHeader('Authorization', 'Bearer ' + access_token);
                    }
                }
            });
        }

        public Login() {
            var self = this;

            self.IsLoading(true);

            $.ajax({
                url: self.baseUrl + '/token',
                type: 'post',
                data: new LoginUser('Shaw', 'secret'),
                success: function (response) {
                    localStorage.setItem('token', JSON.stringify(response));

                    self.IsAuth(true);
                    self.IsLoading(false);

                    self.GetRandomMovie();
                },
                error: function (response) {
                    new PNotify({ title: 'error logging in.', text: 'please seek help.' });

                    self.IsAuth(false);
                    self.IsLoading(false);
                }
            });
        }

        public Logout() {
            var self = this;

            localStorage.setItem('token', null);

            self.IsAuth(false);
        }

        public Register() {
            var self = this;

            self.IsLoading(true);

            $.ajax({
                url: self.baseUrl + '/account/register',
                type: 'post',
                data: new RegisterUser('Shaw', 'secret', 'secret'),
                success: function (response) {
                    new PNotify({ title: 'registration successful', text: 'please log in.' });
                    self.IsLoading(false);
                },
                error: function (response) {
                    var error = response.responseJSON;

                    new PNotify({ title: 'registration failed', text: error.modelState[""][0] });

                    self.IsLoading(false);
                }
            });
        }

        public SearchMovie() {
            var self = this;

            self.IsLoading(true);
            var searchUri = self.baseUrl + '/movies/search/' + encodeURIComponent(this.SearchString());

            $.ajax({
                url: searchUri,
                type: 'get',
                success: function (allData) {
                    var url = "http://image.tmdb.org/t/p/w154";

                    var mappedMovies = $.map(allData, function (item) {
                        return new Movie(item.id, item.originalTitle, url + item.posterPath, 0)
                    });

                    self.Movies(mappedMovies);

                    self.SearchString(null);
                    self.IsLoading(false);
                },
                error: function (vm) {
                    self.SearchString(null);
                    self.IsLoading(false);
                },
                beforeSend: function (xhr) {
                    var token = JSON.parse(localStorage.getItem('token'));

                    if (token != null) {
                        var access_token = token.access_token;
                        var token_type = token.token_type;

                        xhr.setRequestHeader('Authorization', 'Bearer ' + access_token);
                    }
                }
            });
        }

    }
}