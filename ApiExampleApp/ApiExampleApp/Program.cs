using ApiExampleApp;
using System.Net;
using System.ServiceModel;
using System.Xml.Linq;
using static System.Console;

WriteLine("Enter access token:");
var accessToken = ReadLine();
WriteLine("Enter cluster url:");
var clusterUrl = ReadLine();

if (string.IsNullOrWhiteSpace(clusterUrl))
	clusterUrl = "https://api.accounting.twinfield.com";

GetCompanies(accessToken, clusterUrl);

DisplayCustomers();

static void GetCompanies(string accessToken, string clusterUrl)
{
	try
	{
		var processXmlService = new ProcessXmlService(clusterUrl);

		WriteLine("ProcessXml: Displaying a list of offices... ");

		var result = processXmlService.GetCompanies(accessToken);

		var doc = XDocument.Parse(result);
		var companies = doc.Root?.Elements("office").ToArray();
		WriteLine($"Found {companies?.Length} companies");
		if (companies != null)
		{
			foreach (var office in companies)
				WriteLine($"{office.Attribute("name")?.Value} [{office.Value}]");
		}

	}
	catch (FaultException ex)
	{
		WriteLine("Error occurred while processing the xml request.");
		WriteLine(ex.Message);
	}
	catch (WebException ex)
	{
		ex.HandleWebException();
	}
	catch (Exception ex)
	{
		WriteLine("Error occurred while processing the xml request.");
		WriteLine(ex.Message);
	}
	WriteLine("Press any key to continue...\n");
	ReadKey();
}

void DisplayCustomers()
{
	if (string.IsNullOrWhiteSpace(clusterUrl)) return;

	try
	{
		WriteLine("\nEnter company : ");
		var companyCode = ReadLine();

		if (string.IsNullOrWhiteSpace(companyCode))
		{
			WriteLine("Invalid company.");
			return;
		}

		var finderService = new FinderService(clusterUrl);

		WriteLine($"\nDisplaying first 10 dimensions of type 'DEB' with bank account which starts with '1 * ' within company {companyCode}");

		var result = finderService.FindDimensions(accessToken, companyCode);

		if (result.Errors.Any())
			foreach (var message in result.Errors)
				WriteLine(message.Text);
		else
		{
			if (result.Data.Items != null)
				for (var i = 0; i < result.Data.Items.Length; ++i)
					WriteLine("{0}: Customer {1} ({2})", i, result.Data.Items[i][0],
						result.Data.Items[i][1]);
			else
				WriteLine("Nothing found.");
		}
	}
	catch (Exception ex)
	{
		WriteLine("Error occurred while processing the request.");
		WriteLine(ex.Message);
	}

	WriteLine("Press any key to exit...");
	ReadKey();
}
