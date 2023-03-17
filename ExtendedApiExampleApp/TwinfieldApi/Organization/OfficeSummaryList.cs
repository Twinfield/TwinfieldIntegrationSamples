using System.Xml;

namespace TwinfieldApi.Organization;

public class OfficeSummaryList
{
	internal static List<OfficeSummary> FromXml(XmlElement officesElement)
	{
		return (from XmlElement officeNode in officesElement.SelectNodes("office")
			select OfficeSummary.FromXml(officeNode)).ToList();
	}
}
