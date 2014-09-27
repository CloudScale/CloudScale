var CloudScale;
(function (CloudScale) {
    var Movie = (function () {
        function Movie(id, name, img, rating) {
            this.id = ko.observable(null);
            this.name = ko.observable(null);
            this.poster = ko.observable(null);
            this.rating = ko.observable(null);
            this.id(id);
            this.name(name);
            this.poster(img);
            this.rating(rating);
        }
        return Movie;
    })();
    CloudScale.Movie = Movie;
})(CloudScale || (CloudScale = {}));
//# sourceMappingURL=movie.js.map
