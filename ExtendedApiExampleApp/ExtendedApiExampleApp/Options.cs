namespace ExtendedApiExampleApp;

class Options
{
	public string CompanyCode { get; set; }
	public string ClusterUrl { get; set; }

	public string AccessToken { get; set; }

	public static Options Parse(string[] args)
	{
		if (args.Length < 3)
			return null;

		if (string.IsNullOrWhiteSpace(args[0]) || string.IsNullOrWhiteSpace(args[1]) || string.IsNullOrWhiteSpace(args[2]))
			return null;

		return new Options
		{
			AccessToken = args[0],
			ClusterUrl = args[1],
			CompanyCode = args[2].ToUpper()
		};
	}
}