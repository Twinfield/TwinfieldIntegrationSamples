using System.Xml;

namespace TwinfieldApi.Organization;

public class CompanySummaryList
{
	internal static List<CompanySummary> FromXml(XmlElement companiesElement)
	{
		return (from XmlElement companyNode in companiesElement.SelectNodes("office")
			select CompanySummary.FromXml(companyNode)).ToList();
	}
}
