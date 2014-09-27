/// <amd-dependency path="text!app/components/login-form.html"/>
/// <reference path="../../scripts/typings/knockout/knockout.d.ts" />
export var template: string = require("text!app/components/login-form.html");

export class viewModel
{
	public userName = ko.observable<String>("ShawInnes");
	public password = ko.observable<String>(null);
	public confirmPassword = ko.observable<String>(null);

	public execute() {
		console.log('click.');
	}
}