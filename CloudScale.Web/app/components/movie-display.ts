/// <amd-dependency path="text!app/components/movie-display.html"/>
/// <reference path="../../scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../models/movie.ts" />
export var template: string = require("text!app/components/movie-display.html");

export class viewModel
{
	public movie: KnockoutObservable<CloudScale.Movie> = ko.observable<CloudScale.Movie>(null);

	constructor(params: any) {
		this.movie(params.value);
	}
}
