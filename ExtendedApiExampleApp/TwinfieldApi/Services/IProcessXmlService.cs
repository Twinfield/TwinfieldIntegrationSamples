using System.Xml;

namespace TwinfieldApi.Services;

public interface IProcessXmlService
{
	XmlElement Process(XmlDocument input, string clusterUrl, string accessToken, string companyCode);
}
