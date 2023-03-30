using System.Xml;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Dimensions;

class ReadDimensionCommand
{
	public string Company { get; set; }
	public string DimensionType { get; set; }
	public string DimensionCode { get; set; }

	internal XmlDocument ToXml()
	{
		var command = new XmlDocument();
		var readElement = command.AppendNewElement("read");
		readElement.AppendNewElement("type").InnerText = "dimensions";
		readElement.AppendNewElement("office").InnerText = Company;
		readElement.AppendNewElement("dimtype").InnerText = DimensionType;
		readElement.AppendNewElement("code").InnerText = DimensionCode;
		return command;
	}
}
