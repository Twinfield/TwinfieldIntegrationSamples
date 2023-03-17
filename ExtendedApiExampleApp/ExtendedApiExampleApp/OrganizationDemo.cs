using TwinfieldApi.Organization;

namespace ExtendedApiExampleApp;

class OrganizationDemo
{
	readonly OrganizationService organizationService;

	public OrganizationDemo()
	{
		organizationService = new OrganizationService();
	}

	public void Run(string accessToken, string clusterUrl)
	{
		Console.WriteLine("Displaying first 10 companies");

		var companyList = organizationService.GetOffices(clusterUrl, accessToken);
		Console.WriteLine("Found {0} companies:", companyList.Count);

		Console.WriteLine("{0,-16} {1}", "Code", "Name");
		foreach (var company in companyList.Take(10))
			Console.WriteLine("{0,-16} {1}", company.Code, company.Name);
		Console.WriteLine();
	}
}
