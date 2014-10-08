var CloudScale;
(function (CloudScale) {
    var MainViewModel = (function () {
        function MainViewModel(baseApiUrl) {
            this.movie = ko.observable(null);
            this.isLoading = ko.observable(false);
            console.log('constructor: ' + baseApiUrl);

            this.baseUrl = baseApiUrl;
            this.movie(new CloudScale.Movie('test', 'War of the Worlds', '', 5));
        }
        MainViewModel.prototype.getRandomMovie = function () {
            var _this = this;
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
                success: function (m) {
                    if (m == null) {
                    } else {
                        var imgUrl = "http://image.tmdb.org/t/p/w154";

                        _this.movie(new CloudScale.Movie(m.id, m.title, imgUrl + m.posterPath, m.userRating));
                    }

                    _this.isLoading(false);
                },
                error: function (response) {
                    console.log(response);
                    console.log({ title: 'error getting random movie' });

                    _this.isLoading(false);
                }
            });
        };
        return MainViewModel;
    })();
    CloudScale.MainViewModel = MainViewModel;
})(CloudScale || (CloudScale = {}));
//# sourceMappingURL=mainviewmodel.js.map
