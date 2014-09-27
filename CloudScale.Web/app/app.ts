/// <reference path="../scripts/typings/knockout/knockout.d.ts" />
/// <reference path="models/movie.ts" />

ko.components.register('movie-display', { require: 'app/components/movie-display' });
ko.components.register('login-form', { require: 'app/components/login-form' });
ko.components.register('register-form', { require: 'app/components/register-form' });
ko.components.register('access-denied', { require: 'app/components/access-denied' });

export class MainViewModel
{
	public movie: KnockoutObservable<CloudScale.Movie> = ko.observable<CloudScale.Movie>(null);

	constructor()
	{
		this.movie(new CloudScale.Movie('test', 'War of the Worlds', '', 5));
	}
}

var vm = new MainViewModel();

ko.applyBindings(vm);


