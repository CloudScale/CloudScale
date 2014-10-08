module CloudScale {
	export class Vote {
		movieId: string;
		score: number;

		constructor(movieId: string, score: number) {
			this.movieId = movieId;
			this.score = score;
		}
	}
}