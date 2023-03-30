using System.Xml;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Browse.Query;

class ReadBrowseDefinitionCommand
{
	public string Company { get; set; }
	public string Code { get; set; }

	internal XmlDocument ToXml()
	{
		var command = new XmlDocument();
		var readElement = command.AppendNewElement("read");
		readElement.AppendNewElement("type").InnerText = "browse";
		readElement.AppendNewElement("office").InnerText = Company;
		readElement.AppendNewElement("code").InnerText = Code;
		return command;
	}
}
