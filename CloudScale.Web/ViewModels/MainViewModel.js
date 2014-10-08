/// <reference path="../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../scripts/typings/bootstrap/bootstrap.d.ts" />
/// <reference path="../app/models/movie.ts" />
/*
module CloudScale {
export class OldMainViewModel {
public movies: KnockoutObservableArray<Movie> = ko.observableArray([]);
public currentMovie: KnockoutObservable<Movie> = ko.observable<Movie>(null);
public addString: KnockoutObservable<string> = ko.observable<string>(null);
public searchString: KnockoutObservable<string> = ko.observable<string>(null);
public userName: KnockoutObservable<string> = ko.observable<string>(null);
public password: KnockoutObservable<string> = ko.observable<string>(null);
public confirmPassword: KnockoutObservable<string> = ko.observable<string>(null);
public isLoading: KnockoutObservable<boolean> = ko.observable<boolean>(false);
public isAuth: KnockoutObservable<boolean> = ko.observable<boolean>(false);
public showLogin: KnockoutObservable<boolean> = ko.observable<boolean>(false);
public showRegister: KnockoutObservable<boolean> = ko.observable<boolean>(false);
private baseUrl: string;
constructor(baseApiUrl: string)
{
this.baseUrl = baseApiUrl;
var self = this;
self.isAuth(false);
var token = JSON.parse(localStorage.getItem('token'));
if (token != null)
{
this.isAuth(true);
}
}
public vote(value, event)
{
var self = this;
var vote = event.target.innerText;
if (self.currentMovie() === null)
return;
$.ajax({
url: self.baseUrl + '/movies/vote',
type: 'post',
data: new Vote(self.currentMovie().id(), vote),
success: function ()
{
self.getRandomMovie();
self.isLoading(false);
},
error: function ()
{
self.isLoading(false);
},
beforeSend: function (xhr)
{
var token = JSON.parse(localStorage.getItem('token'));
if (token != null)
{
var accessToken = token.access_token;
xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken);
}
}
});
}
public addMovie()
{
var self = this;
self.isLoading(true);
$.ajax({
url: self.baseUrl + '/movies/new',
type: 'post',
data: { '': this.addString() },
success: function ()
{
self.addString(null);
self.isLoading(false);
},
error: function ()
{
self.addString(null);
self.isLoading(false);
},
beforeSend: function (xhr)
{
var token = JSON.parse(localStorage.getItem('token'));
if (token != null)
{
var accessToken = token.access_token;
xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken);
}
}
});
}
public doLogin()
{
var self = this;
self.showLogin(true);
self.showRegister(false);
}
public login()
{
var self = this;
self.isLoading(true);
$.ajax({
url: self.baseUrl + '/token',
type: 'post',
data: new LoginUser(self.userName(), self.password()),
success: function (response)
{
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
}
public logout()
{
var self = this;
localStorage.setItem('token', null);
self.isAuth(false);
}
public doRegister()
{
var self = this;
self.showRegister(true);
self.showLogin(false);
}
public register() {
var self = this;
self.isLoading(true);
$.ajax({
url: self.baseUrl + '/account/register',
type: 'post',
data: new RegisterUser(self.userName(), self.password(), self.confirmPassword()),
success: function(response) {
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
}
public searchMovie() {
var self = this;
self.isLoading(true);
var searchUri = self.baseUrl + '/movies/search/' + encodeURIComponent(this.searchString());
$.ajax({
url: searchUri,
type: 'get',
success: function (allData)
{
var url = "http://image.tmdb.org/t/p/w154";
var mappedMovies = $.map(allData, function (item)
{
return new Movie(item.id, item.originalTitle, url + item.posterPath, 0);
});
self.movies(mappedMovies);
self.searchString(null);
self.isLoading(false);
},
error: function ()
{
self.searchString(null);
self.isLoading(false);
},
beforeSend: function (xhr)
{
var token = JSON.parse(localStorage.getItem('token'));
if (token != null)
{
var accessToken = token.access_token;
xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken);
}
}
});
}
}
}
*/
//# sourceMappingURL=MainViewModel.js.map
