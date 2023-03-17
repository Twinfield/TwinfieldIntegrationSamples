using System.Xml;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Organization;

class ListOfficesCommand
{
	internal XmlDocument ToXml()
	{
		return @"<list><type>offices</type></list>".ToXmlDocument();
	}
}
