namespace OAuth2ExampleApp;

class AuthorizeResult
{
	public bool Success { get; private set; }
	public string AccessToken { private set; get; }
	public string User { get; private set; }
	public string Organisation { get; private set; }
	public string ClusterUrl { get; private set; }
	public string Message { get; private set; }
	public string State { get; private set; }


	public static AuthorizeResult Authenticated(string accessToken, string user, string organisation,
		string clusterUrl, string state)
	{
		return new AuthorizeResult
		{
			Success = true,
			AccessToken = accessToken,
			User = user,
			Organisation = organisation,
			ClusterUrl = clusterUrl,
			State = state
		};
	}

	public static AuthorizeResult Error(string message)
	{
		return new AuthorizeResult
		{
			Success = false,
			Message = message
		};
	}
}
