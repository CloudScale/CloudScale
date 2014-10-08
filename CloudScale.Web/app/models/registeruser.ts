module CloudScale
{
	export class RegisterUser
	{
		userName: string;
		password: string;
		confirmPassword: string;

		constructor(userName: string, password: string, confirm: string)
		{
			this.userName = userName;
			this.password = password;
			this.confirmPassword = confirm;
		}
	}
}
