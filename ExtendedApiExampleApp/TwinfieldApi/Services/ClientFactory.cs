using System.ServiceModel;
using TwinfieldBankBookService;
using TwinfieldFinderService;
using TwinfieldProcessXmlService;

namespace TwinfieldApi.Services;

public class ClientFactory : IClientFactory
{
		
	const string FinderServicePath = "/webservices/finder.asmx";
	const string BankbookServicePath = "/webservices/BankBookService.svc";
	const string ProcessXmlServicePath = "/webservices/processxml.asmx";

	public IBankBookServiceClient CreateBankBookClient(string baseUrl)
	{
		var endpointUrl = baseUrl + BankbookServicePath;
		return new BankBookServiceClient(GetBinding(endpointUrl), new EndpointAddress(endpointUrl));
	}

	public IFinderSoapClient CreateFinderClient(string baseUrl)
	{
		var endpointUrl = baseUrl + FinderServicePath;
		return new FinderSoapClient(GetBinding(endpointUrl), new EndpointAddress(endpointUrl));
	}

	public IProcessXmlSoapClient CreateProcessXmlClient(string baseUrl)
	{
		var endpointUrl = baseUrl + ProcessXmlServicePath;
		return new ProcessXmlSoapClient(GetBinding(endpointUrl), new EndpointAddress(endpointUrl));
	}

	static BasicHttpBinding GetBinding(string endpointUrl)
	{
		var uri = new Uri(endpointUrl);
		var securityMode = uri.Scheme == "https" ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.None;
		var binding = new BasicHttpBinding(securityMode)
		{
			MaxReceivedMessageSize = 20_000_000,
			SendTimeout = TimeSpan.FromSeconds(900)
		};
		return binding;
	}
}
