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
})(CloudScale || (CloudScale = {}));
//# sourceMappingURL=vote.js.map
