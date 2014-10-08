module CloudScale
{
	export class Movie
	{
		id: KnockoutObservable<string> = ko.observable<string>(null);
		name: KnockoutObservable<string> = ko.observable<string>(null);
		poster: KnockoutObservable<string> = ko.observable<string>(null);
		rating: KnockoutObservable<number> = ko.observable<number>(null);

		constructor(id: string, name: string, img: string, rating: number)
		{
			this.id(id);
			this.name(name);
			this.poster(img);
			this.rating(rating);
		}
	}
}