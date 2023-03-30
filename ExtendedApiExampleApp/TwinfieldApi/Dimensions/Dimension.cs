using System.Xml;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Dimensions;

public class Dimension
{
	public string Company { get; set; }
	public string @Type { get; set; }
	public string Code { get; set; }
	public string Name { get; set; }

	internal static Dimension FromXml(XmlElement element)
	{
		return new Dimension
		{
			Company = element.SelectInnerText("office"),
			Type = element.SelectInnerText("type"),
			Code = element.SelectInnerText("code"),
			Name = element.SelectInnerText("name")
		};
	}
}
