var CloudScale;
(function (CloudScale) {
    var RegisterUser = (function () {
        function RegisterUser(userName, password, confirm) {
            this.userName = userName;
            this.password = password;
            this.confirmPassword = confirm;
        }
        return RegisterUser;
    })();
    CloudScale.RegisterUser = RegisterUser;
})(CloudScale || (CloudScale = {}));
//# sourceMappingURL=registeruser.js.map
