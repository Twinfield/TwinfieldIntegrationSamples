using System.Xml;

namespace TwinfieldApi.Organization;

public class OfficeSummary
{
	public string Code { get; set; }
	public string Name { get; set; }

	internal static OfficeSummary FromXml(XmlElement officeElement)
	{
		return new OfficeSummary
		{
			Code = officeElement.InnerText,
			Name = officeElement.GetAttribute("name")
		};
	}
}
