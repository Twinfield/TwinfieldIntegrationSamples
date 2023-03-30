using System.ServiceModel;
using ApiExampleApp.TwinfieldProcessXml;

namespace ApiExampleApp;

class ProcessXmlService
{
	readonly ProcessXmlSoapClient processXmlSoapClient;
	const string ProcessXmlServicePath = "/webservices/processxml.asmx";

	public ProcessXmlService(string clusterUrl)
	{
		processXmlSoapClient = new ProcessXmlSoapClient(
			new BasicHttpsBinding(),
			new EndpointAddress(clusterUrl + ProcessXmlServicePath));
	}

	public string GetCompanies(string accessToken) =>
		processXmlSoapClient.ProcessXmlString(new Header { AccessToken = accessToken },
			"<list><type>offices</type></list>");
}
