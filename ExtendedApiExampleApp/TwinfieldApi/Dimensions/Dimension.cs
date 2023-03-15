using System.Xml;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Dimensions;

public class Dimension
{
	internal static Dimension FromXml(XmlElement element)
	{
		return new Dimension
		{
			Office = element.SelectInnerText("office"),
			Type = element.SelectInnerText("type"),
			Code = element.SelectInnerText("code"),
			Name = element.SelectInnerText("name")
		};
	}

	public string Office { get; set; }
	public string @Type { get; set; }
	public string Code { get; set; }
	public string Name { get; set; }

	internal XmlDocument ToXml()
	{
		var document = new XmlDocument();
		var dimension = document.AppendNewElement("dimension");
		dimension.AppendNewElement("office").InnerText = Office;
		dimension.AppendNewElement("type").InnerText = @Type;
		dimension.AppendNewElement("code").InnerText = Code;
		dimension.AppendNewElement("name").InnerText = Name;
		return document;
	}
}