using TwinfieldApi.Dimensions;

namespace ExtendedApiExampleApp;

class DimensionDemo
{
	const string CustomerDimensionType = "DEB";

	readonly DimensionService dimensionService;
	DimensionSummary customerSummary;
	Dimension customer;

	public DimensionDemo()
	{
		dimensionService = new DimensionService();
	}

	public void Run(string accessToken, string clusterUrl, string companyCode)
	{
		if (!FindCustomersWithNameThatStartsWithA(accessToken, clusterUrl, companyCode))
			return;

		if (!ReadCustomerDetails( accessToken,  clusterUrl,  companyCode))
			return;

		Console.WriteLine();
	}

	bool FindCustomersWithNameThatStartsWithA(string accessToken, string clusterUrl, string companyCode)
	{
		Console.WriteLine("Searching for customers with a name that starts with an A");

		const int searchField = 2; // Search in name field
		var customers = dimensionService.FindDimensions("a*", CustomerDimensionType, searchField, clusterUrl,accessToken, companyCode);
			
		DisplayCustomerSummaries(customers);

		// Save first customer for later demo
		customerSummary = customers.First();
		return true;
	}

	bool ReadCustomerDetails(string accessToken, string clusterUrl, string companyCode)
	{
		Console.WriteLine("Read first customer details");

		customer = dimensionService.ReadDimension(CustomerDimensionType, customerSummary.Code, clusterUrl, accessToken, companyCode);

		if (customerSummary == null)
		{
			Console.WriteLine("Customer {0} not found.", customer.Code);
			return false;
		}
			
		DisplayCustomerDetails(customer);
		return true;
	}

	static void DisplayCustomerSummaries(IEnumerable<DimensionSummary> dimensions)
	{
		foreach (var dimension in dimensions)
			Console.WriteLine("{0,-16} {1}", dimension.Code, dimension.Name);
		Console.WriteLine();
	}

	static void DisplayCustomerDetails(Dimension dimension)
	{
		Console.WriteLine("Customer details:");
		Console.WriteLine("office = {0}", dimension.Office);
		Console.WriteLine("type = {0}", dimension.Type);
		Console.WriteLine("code = {0}", dimension.Code);
		Console.WriteLine("name = {0}", dimension.Name);
		Console.WriteLine();
	}

}
