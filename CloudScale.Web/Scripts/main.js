require.config({
    baseUrl: "/Scripts/app",
    paths: {
        jquery: "../lib/jquery-2.1.1",
        jqueryValidate: "../lib/jquery.validate",
        jqueryValidateUnobtrusive: "../lib/jquery.validate.unobtrusive",
        bootstrap: "../lib/bootstrap",
        moment: "../lib/moment"
    },
    shim: {
        jqueryValidate: ["jquery"],
        jqueryValidateUnobtrusive: ["jquery", "jqueryValidate"]
    }
});

require(["kickoff"], function (kickoff) {
    kickoff.init();
});
//# sourceMappingURL=main.js.map
