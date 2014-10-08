module CloudScale
{
	export class MainViewModel
	{
		public movie: KnockoutObservable<CloudScale.Movie> = ko.observable<CloudScale.Movie>(null);
		public isLoading: KnockoutObservable<boolean> = ko.observable<boolean>(false);

		private baseUrl: string;

		constructor(baseApiUrl: string) {
			console.log('constructor: ' + baseApiUrl);

			this.baseUrl = baseApiUrl;
			this.movie(new CloudScale.Movie('test', 'War of the Worlds', '', 5));
		}

		public getRandomMovie()
		{
			var self = this;

/*
			var token = JSON.parse(localStorage.getItem('token'));
			if (token == null) {
				console.log("not logged in");
				return;
			}
*/

			self.isLoading(true);

			$.ajax({
				url: self.baseUrl + '/movies/random',
				type: 'get',
				contentType: 'application/json',
				success: m => {
					if (m == null)
					{

					} else
					{
						var imgUrl = "http://image.tmdb.org/t/p/w154";

						this.movie(new Movie(m.id, m.title, imgUrl + m.posterPath, m.userRating));
					}

					this.isLoading(false);
				},
				error: response => {
					console.log(response);
					console.log({ title: 'error getting random movie' });

					this.isLoading(false);
				},
/*
				beforeSend: xhr => {
					var authToken = JSON.parse(localStorage.getItem('token'));

					if (authToken != null)
					{
						var accessToken = authToken.access_token;

						xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken);
					}
				}
*/
			});
		}
	}
}