using System.Xml;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Browse.Data;

public class BrowseResult
{
	internal static BrowseResult FromXml(XmlElement element)
	{
		return new BrowseResult
		{
			FirstRow = int.Parse(element.GetAttribute("first")),
			LastRow = int.Parse(element.GetAttribute("last")),
			TotalNumberOfRows = int.Parse(element.GetAttribute("total")),
			Columns = ColumnList.FromXml(element),
			Rows = RowList.FromXml(element),
		};
	}

	public int FirstRow { get; set; }
	public int LastRow { get; set; }
	public int TotalNumberOfRows { get; set; }
	public List<Column> Columns { get; set; }
	public List<Row> Rows { get; set; }
}

public class ColumnList
{
	internal static List<Column> FromXml(XmlElement element)
	{
		var columns = new List<Column>();
		var header = element.SelectSingleElement("th");
		if (header != null)
		{
			columns.AddRange(
				(from XmlElement row in header.SelectNodes("td")
					select Column.FromXml(row)));
		}
		return columns;
	}

	public List<Column> Columns { get; set; }
}

public class Column
{
	internal static Column FromXml(XmlElement element)
	{
		return new Column
		{
			Label = element.GetAttribute("label"),
			Name = element.InnerText,
		};
	}

	public string Label { get; set; }
	public string Name { get; set; }
}

public class RowList
{
	internal static List<Row> FromXml(XmlElement element)
	{
		return (
			from XmlElement row in element.SelectNodes("tr")
			select Row.FromXml(row)).ToList();
	}
}

public class Row
{
	internal static Row FromXml(XmlElement element)
	{
		return new Row
		{
			Cells = CellList.FromXml(element)
		};
	}

	public List<string> Cells { get; set; }
}

public class CellList
{
	internal static List<string> FromXml(XmlElement element)
	{
		return (
			from XmlElement cell in element.SelectNodes("td")
			select cell.InnerText).ToList();
	}
}
