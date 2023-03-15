namespace TwinfieldApi.Services;

public interface IClientFactory
{
	IBankBookServiceClient CreateBankBookClient(string baseUrl);
	IFinderSoapClient CreateFinderClient(string baseUrl);
	IProcessXmlSoapClient CreateProcessXmlClient(string baseUrl);
}