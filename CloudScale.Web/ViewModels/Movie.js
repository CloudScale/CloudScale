var CloudScale;
(function (CloudScale) {
    var Movie = (function () {
        function Movie(id, name, img) {
            this.Id = ko.observable(null);
            this.Name = ko.observable(null);
            this.Poster = ko.observable(null);
            this.Id(id);
            this.Name(name);
            this.Poster(img);
        }
        return Movie;
    })();
    CloudScale.Movie = Movie;
})(CloudScale || (CloudScale = {}));
//# sourceMappingURL=Movie.js.map
