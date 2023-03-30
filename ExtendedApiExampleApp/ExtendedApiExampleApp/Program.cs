using ExtendedApiExampleApp;

var options = Options.Parse(args);

if (options == null)
	ShowUsage();
else
	RunDemo(options);

WaitForUser();

static void ShowUsage() => Console.WriteLine("ExtendedApiExampleApp.exe <accessToken> <clusterUrl> <companyCode>");

static void RunDemo(Options options)
{
	try
	{
		var demo = new ApiDemo();
		demo.Run(options.AccessToken, options.ClusterUrl, options.CompanyCode);
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex.Message);
	}
}

static void WaitForUser()
{
	Console.WriteLine("Press any key to continue");
	Console.ReadKey();
}
