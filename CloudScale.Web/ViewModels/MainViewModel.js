/// <reference path="../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../scripts/typings/jquery.pnotify/jquery.pnotify.d.ts" />
/// <reference path="../scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../scripts/typings/bootstrap/bootstrap.d.ts" />
var CloudScale;
(function (CloudScale) {
    var Vote = (function () {
        function Vote(movieId, score) {
            this.MovieId = movieId;
            this.Score = score;
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

    var Movie = (function () {
        function Movie(id, name, img, rating) {
            this.Id = ko.observable(null);
            this.Name = ko.observable(null);
            this.Poster = ko.observable(null);
            this.Rating = ko.observable(null);
            this.Id(id);
            this.Name(name);
            this.Poster(img);
            this.Rating(rating);
        }
        return Movie;
    })();
    CloudScale.Movie = Movie;

    var LoginUser = (function () {
        function LoginUser(userName, password) {
            this.grant_type = 'password';
            this.client_id = 'CloudScale';
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
            this.Movies = ko.observableArray([]);
            this.CurrentMovie = ko.observable(null);
            this.AddString = ko.observable(null);
            this.SearchString = ko.observable(null);
            this.UserName = ko.observable(null);
            this.Password = ko.observable(null);
            this.ConfirmPassword = ko.observable(null);
            this.IsLoading = ko.observable(false);
            this.IsAuth = ko.observable(false);
            this.ShowLogin = ko.observable(false);
            this.ShowRegister = ko.observable(false);
            this.baseUrl = baseApiUrl;

            var self = this;

            self.IsAuth(false);

            var token = JSON.parse(localStorage.getItem('token'));

            if (token != null) {
                var access_token = token.access_token;
                var token_type = token.token_type;

                this.IsAuth(true);
            }
        }
        MainViewModel.prototype.GetRandomMovie = function () {
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
                        var imgUrl = "http://image.tmdb.org/t/p/w154";

                        self.CurrentMovie(new CloudScale.Movie(m.id, m.title, imgUrl + m.posterPath, m.userRating));
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
        };

        MainViewModel.prototype.Vote = function (value, event) {
            var self = this;

            var vote = event.target.innerText;

            if (self.CurrentMovie() === null)
                return;

            $.ajax({
                url: self.baseUrl + '/movies/vote',
                type: 'post',
                data: new CloudScale.Vote(self.CurrentMovie().Id(), vote),
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
        };

        MainViewModel.prototype.DoLogin = function () {
            var self = this;

            self.ShowLogin(true);
            self.ShowRegister(false);
        };

        MainViewModel.prototype.Login = function () {
            var self = this;

            self.IsLoading(true);

            $.ajax({
                url: self.baseUrl + '/token',
                type: 'post',
                data: new CloudScale.LoginUser(self.UserName(), self.Password()),
                success: function (response) {
                    localStorage.setItem('token', JSON.stringify(response));

                    self.IsAuth(true);
                    self.UserName(null);
                    self.Password(null);
                    self.IsLoading(false);

                    self.GetRandomMovie();
                },
                error: function (response) {
                    new PNotify({ title: 'error logging in.', text: 'please seek help.' });

                    self.IsAuth(false);
                    self.IsLoading(false);
                }
            });

            self.ShowLogin(false);
        };

        MainViewModel.prototype.Logout = function () {
            var self = this;

            localStorage.setItem('token', null);

            self.IsAuth(false);
        };

        MainViewModel.prototype.DoRegister = function () {
            var self = this;

            self.ShowRegister(true);
            self.ShowLogin(false);
        };

        MainViewModel.prototype.Register = function () {
            var self = this;

            self.IsLoading(true);

            $.ajax({
                url: self.baseUrl + '/account/register',
                type: 'post',
                data: new CloudScale.RegisterUser(self.UserName(), self.Password(), self.ConfirmPassword()),
                success: function (response) {
                    new PNotify({ title: 'registration successful', text: 'please log in.' });
                    self.IsLoading(false);
                },
                error: function (response) {
                    var error = response.responseJSON;
                    console.log(error);
                    new PNotify({ title: 'registration failed', text: error });

                    self.IsLoading(false);
                }
            });

            self.ShowRegister(false);
        };

        MainViewModel.prototype.SearchMovie = function () {
            var self = this;

            self.IsLoading(true);
            var searchUri = self.baseUrl + '/movies/search/' + encodeURIComponent(this.SearchString());

            $.ajax({
                url: searchUri,
                type: 'get',
                success: function (allData) {
                    var url = "http://image.tmdb.org/t/p/w154";

                    var mappedMovies = $.map(allData, function (item) {
                        return new CloudScale.Movie(item.id, item.originalTitle, url + item.posterPath, 0);
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
        };
        return MainViewModel;
    })();
    CloudScale.MainViewModel = MainViewModel;
})(CloudScale || (CloudScale = {}));
//# sourceMappingURL=MainViewModel.js.map
