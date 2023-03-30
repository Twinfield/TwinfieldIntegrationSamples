using System.Xml;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Browse.Query;

public class BrowseDefinition
{
	public string Company { get; set; }
	public string Code { get; set; }
	public string Name { get; set; }
	public QueryColumnList QueryColumns { get; set; }

	internal static BrowseDefinition FromXml(XmlElement element)
	{
		return new BrowseDefinition
		{
			Company = element.SelectInnerText("office"),
			Code = element.SelectInnerText("code"),
			Name = element.SelectInnerText("name"),
			QueryColumns = QueryColumnList.FromXml(element.SelectSingleElement("columns"))
		};
	}
}
