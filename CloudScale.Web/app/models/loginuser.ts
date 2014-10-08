module CloudScale
{
	export class LoginUser
	{
		grantType: string;
		clientId: string;
		userName: string;
		password: string;

		constructor(userName: string, password: string)
		{
			this.grantType = 'password';
			this.clientId = 'CloudScale';
			this.userName = userName;
			this.password = password;
		}
	}
}
