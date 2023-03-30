namespace ExtendedApiExampleApp;

class ApiDemo
{
	public void Run(string accessToken, string clusterUrl, string companyCode)
	{
		(new OrganizationDemo()).Run(accessToken, clusterUrl);
		(new DimensionDemo()).Run(accessToken, clusterUrl, companyCode);
		(new BankBookDemo()).Run(accessToken, clusterUrl, companyCode);
		(new BookkeepingDemo()).Run(accessToken, clusterUrl, companyCode);
	}
}
