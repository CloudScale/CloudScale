/// <reference path="../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../scripts/typings/bootstrap/bootstrap.d.ts" />
/// <reference path="../app/models/movie.ts" />
var CloudScale;
(function (CloudScale) {
    var Vote = (function () {
        function Vote(movieId, score) {
            this.movieId = movieId;
            this.score = score;
        }
        return Vote;
    })();
    CloudScale.Vote = Vote;

    var RegisterUser = (function () {
        function RegisterUser(userName, password, confirm) {
            this.userName = userName;
            this.password = password;
            this.confirmPassword = confirm;
        }
        return RegisterUser;
    })();
    CloudScale.RegisterUser = RegisterUser;

    var LoginUser = (function () {
        function LoginUser(userName, password) {
            this.grantType = 'password';
            this.clientId = 'CloudScale';
            this.userName = userName;
            this.password = password;
        }
        return LoginUser;
    })();
    CloudScale.LoginUser = LoginUser;
})(CloudScale || (CloudScale = {}));

var CloudScale;
(function (CloudScale) {
    var MainViewModel = (function () {
        function MainViewModel(baseApiUrl) {
            this.movies = ko.observableArray([]);
            this.currentMovie = ko.observable(null);
            this.addString = ko.observable(null);
            this.searchString = ko.observable(null);
            this.userName = ko.observable(null);
            this.password = ko.observable(null);
            this.confirmPassword = ko.observable(null);
            this.isLoading = ko.observable(false);
            this.isAuth = ko.observable(false);
            this.showLogin = ko.observable(false);
            this.showRegister = ko.observable(false);
            this.baseUrl = baseApiUrl;

            var self = this;

            self.isAuth(false);

            var token = JSON.parse(localStorage.getItem('token'));

            if (token != null) {
                this.isAuth(true);
            }
        }
        MainViewModel.prototype.getRandomMovie = function () {
            var self = this;

            var token = JSON.parse(localStorage.getItem('token'));
            if (token == null)
                return;

            self.isLoading(true);

            $.ajax({
                url: self.baseUrl + '/movies/random',
                type: 'get',
                contentType: 'application/json',
                success: function (m) {
                    if (m == null) {
                    } else {
                        var imgUrl = "http://image.tmdb.org/t/p/w154";

                        self.currentMovie(new CloudScale.Movie(m.id, m.title, imgUrl + m.posterPath, m.userRating));
                    }

                    self.isLoading(false);
                },
                error: function (response) {
                    console.log(response);
                    console.log({ title: 'error getting random movie' });

                    self.isLoading(false);
                },
                beforeSend: function (xhr) {
                    var authToken = JSON.parse(localStorage.getItem('token'));

                    if (authToken != null) {
                        var accessToken = authToken.access_token;

                        xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken);
                    }
                }
            });
        };

        MainViewModel.prototype.vote = function (value, event) {
            var self = this;

            var vote = event.target.innerText;

            if (self.currentMovie() === null)
                return;

            $.ajax({
                url: self.baseUrl + '/movies/vote',
                type: 'post',
                data: new CloudScale.Vote(self.currentMovie().id(), vote),
                success: function () {
                    self.getRandomMovie();
                    self.isLoading(false);
                },
                error: function () {
                    self.isLoading(false);
                },
                beforeSend: function (xhr) {
                    var token = JSON.parse(localStorage.getItem('token'));

                    if (token != null) {
                        var accessToken = token.access_token;

                        xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken);
                    }
                }
            });
        };

        MainViewModel.prototype.addMovie = function () {
            var self = this;

            self.isLoading(true);

            $.ajax({
                url: self.baseUrl + '/movies/new',
                type: 'post',
                data: { '': this.addString() },
                success: function () {
                    self.addString(null);
                    self.isLoading(false);
                },
                error: function () {
                    self.addString(null);
                    self.isLoading(false);
                },
                beforeSend: function (xhr) {
                    var token = JSON.parse(localStorage.getItem('token'));

                    if (token != null) {
                        var accessToken = token.access_token;

                        xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken);
                    }
                }
            });
        };

        MainViewModel.prototype.doLogin = function () {
            var self = this;

            self.showLogin(true);
            self.showRegister(false);
        };

        MainViewModel.prototype.login = function () {
            var self = this;

            self.isLoading(true);

            $.ajax({
                url: self.baseUrl + '/token',
                type: 'post',
                data: new CloudScale.LoginUser(self.userName(), self.password()),
                success: function (response) {
                    localStorage.setItem('token', JSON.stringify(response));

                    self.isAuth(true);
                    self.userName(null);
                    self.password(null);
                    self.isLoading(false);

                    self.getRandomMovie();
                },
                error: function (response) {
                    console.log(response);
                    console.log({ title: 'error logging in.', text: 'please seek help.' });

                    self.isAuth(false);
                    self.isLoading(false);
                }
            });

            self.showLogin(false);
        };

        MainViewModel.prototype.logout = function () {
            var self = this;

            localStorage.setItem('token', null);

            self.isAuth(false);
        };

        MainViewModel.prototype.doRegister = function () {
            var self = this;

            self.showRegister(true);
            self.showLogin(false);
        };

        MainViewModel.prototype.register = function () {
            var self = this;

            self.isLoading(true);

            $.ajax({
                url: self.baseUrl + '/account/register',
                type: 'post',
                data: new CloudScale.RegisterUser(self.userName(), self.password(), self.confirmPassword()),
                success: function (response) {
                    console.log(response);
                    console.log({ title: 'registration successful', text: 'please log in.' });
                    this.isLoading(false);
                },
                error: function (response) {
                    var error = response.responseJSON;
                    console.log(error);
                    console.log({ title: 'registration failed', text: error });

                    self.isLoading(false);
                }
            });

            self.showRegister(false);
        };

        MainViewModel.prototype.searchMovie = function () {
            var self = this;

            self.isLoading(true);
            var searchUri = self.baseUrl + '/movies/search/' + encodeURIComponent(this.searchString());

            $.ajax({
                url: searchUri,
                type: 'get',
                success: function (allData) {
                    var url = "http://image.tmdb.org/t/p/w154";

                    var mappedMovies = $.map(allData, function (item) {
                        return new CloudScale.Movie(item.id, item.originalTitle, url + item.posterPath, 0);
                    });

                    self.movies(mappedMovies);

                    self.searchString(null);
                    self.isLoading(false);
                },
                error: function () {
                    self.searchString(null);
                    self.isLoading(false);
                },
                beforeSend: function (xhr) {
                    var token = JSON.parse(localStorage.getItem('token'));

                    if (token != null) {
                        var accessToken = token.access_token;

                        xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken);
                    }
                }
            });
        };
        return MainViewModel;
    })();
    CloudScale.MainViewModel = MainViewModel;
})(CloudScale || (CloudScale = {}));
//# sourceMappingURL=MainViewModel.js.map
