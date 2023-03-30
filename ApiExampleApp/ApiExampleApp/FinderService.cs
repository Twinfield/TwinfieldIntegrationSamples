using ApiExampleApp.Finder;
using System.ServiceModel;

namespace ApiExampleApp;

class FinderService
{
	readonly FinderSoapClient finderSoapClient;
	const string FinderServicePath = "/webservices/finder.asmx";

	public FinderService(string clusterUrl)
	{
		var endpointUrl = clusterUrl + FinderServicePath;
		finderSoapClient =
			new FinderSoapClient(GetServiceBinding(), new EndpointAddress(endpointUrl));
	}

	public FinderDataModel FindDimensions(string accessToken, string companyCode)
	{
		var options = new string[3][];
		options[0] = new[] { "dimtype", "DEB" };
		options[1] = new[] { "section", "financials" };

		var errorMessages = finderSoapClient.Search(
			Header: new Header() { AccessToken = accessToken, CompanyCode = companyCode }, "DIM",
			pattern: "1*", field: 3, firstRow: 1, maxRows: 10, options: options, out var finderData);

		return new FinderDataModel { Errors = errorMessages, Data = finderData };
	}

	static BasicHttpBinding GetServiceBinding() => new(BasicHttpSecurityMode.Transport) { MaxReceivedMessageSize = int.MaxValue };
}
