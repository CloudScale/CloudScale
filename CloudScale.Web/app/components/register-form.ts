/// <amd-dependency path="text!app/components/register-form.html"/>
/// <reference path="../../scripts/typings/knockout/knockout.d.ts" />
export var template: string = require("text!app/components/register-form.html");

export class viewModel
{
	public userName = ko.observable<String>(null);
	public password = ko.observable<String>(null);
	public confirmPassword = ko.observable<String>(null);

	public execute() {
		console.log('click.');
	}
}