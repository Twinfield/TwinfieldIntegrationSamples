using System.Xml;

namespace TwinfieldApi.Organization;

public class CompanySummary
{
	public string Code { get; set; }
	public string Name { get; set; }

	internal static CompanySummary FromXml(XmlElement companyElement)
	{
		return new CompanySummary
		{
			Code = companyElement.InnerText,
			Name = companyElement.GetAttribute("name")
		};
	}
}
