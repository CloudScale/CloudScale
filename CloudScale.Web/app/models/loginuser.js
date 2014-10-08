var CloudScale;
(function (CloudScale) {
    var LoginUser = (function () {
        function LoginUser(userName, password) {
            this.grantType = 'password';
            this.clientId = 'CloudScale';
            this.userName = userName;
            this.password = password;
        }
        return LoginUser;
    })();
    CloudScale.LoginUser = LoginUser;
})(CloudScale || (CloudScale = {}));
//# sourceMappingURL=loginuser.js.map
