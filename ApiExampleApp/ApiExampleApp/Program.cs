using ApiExampleApp.Finder;
using ApiExampleApp.TwinfieldProcessXml;
using System.Net;
using System.ServiceModel;
using System.Xml.Linq;
using static System.Console;
using Header = ApiExampleApp.TwinfieldProcessXml.Header;

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
		var processXmlService = new ProcessXmlSoapClient(
			new BasicHttpsBinding(),
			new EndpointAddress(clusterUrl + "/webservices/processxml.asmx"));

		WriteLine("ProcessXml: Displaying a list of offices... ");

		var result = processXmlService.ProcessXmlString(
			new Header { AccessToken = accessToken },
			"<list><type>offices</type></list>");

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
		HandleWebException(ex);
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

		var endpointUrl = clusterUrl + "/webservices/finder.asmx";

		var finder = new FinderSoapClient(GetServiceBinding(), new EndpointAddress(endpointUrl));

		var options = new string[3][];
		options[0] = new[] { "dimtype", "DEB" };
		options[1] = new[] { "section", "financials" };

		WriteLine($"\nDisplaying first 10 dimensions of type 'DEB' with bank account which starts with '1 * ' within company {companyCode}");

		var errorMessages = finder.Search(
			Header: new ApiExampleApp.Finder.Header() { AccessToken = accessToken, CompanyCode = companyCode }, "DIM",
			pattern: "1*", field: 3, firstRow: 1, maxRows: 10, options: options, out var finderData);


		if (errorMessages.Any())
			foreach (var message in errorMessages)
				WriteLine(message.Text);
		else
		{
			if (finderData.Items != null)
				for (var i = 0; i < finderData.Items.Length; ++i)
					WriteLine("{0}: Customer {1} ({2})", i, finderData.Items[i][0],
						finderData.Items[i][1]);
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

static BasicHttpBinding GetServiceBinding() => new(BasicHttpSecurityMode.Transport) { MaxReceivedMessageSize = int.MaxValue };

static void HandleWebException(WebException webException)
{
	if (webException == null) return;

	WriteLine("Error occurred while processing the xml request.");
	var statusCode = ((HttpWebResponse)webException.Response).StatusCode;
	WriteLine($"Http status code : {statusCode}");

	if (statusCode != HttpStatusCode.Forbidden &&
		 statusCode != HttpStatusCode.Unauthorized) return;
	var statusDescription = ((HttpWebResponse)webException.Response).StatusDescription;

	if (string.IsNullOrWhiteSpace(statusDescription)) return;
	if (statusDescription.Contains(":"))
	{
		var descriptionDetails = statusDescription.Split(':');
		if (descriptionDetails.Length <= 1) return;
		WriteLine($"Error code : {descriptionDetails[0].Trim()}");
		WriteLine($"Error description : {descriptionDetails[1].Trim()}");
	}
	else
		WriteLine($"Error description : {statusDescription}");
}