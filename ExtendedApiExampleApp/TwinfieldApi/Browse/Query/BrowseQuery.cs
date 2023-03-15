using System.Xml;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Browse.Query;

public class BrowseQuery
{
	public string Code { get; set; }
	public QueryColumnList QueryColumns { get; set; }
	public bool Optimize { get; set; }

	public BrowseQuery()
	{
		Optimize = true;
	}

	internal XmlDocument ToXml()
	{
		var document = new XmlDocument();
		var columns = document.AppendNewElement("columns");
		columns.SetAttribute("code", Code);
		columns.SetAttribute("optimize", Optimize ? "true" : "false");
		foreach (var column in QueryColumns)
			column.AppendToXml(columns);
		return document;
	}
}

public class QueryColumnList : List<QueryColumn>
{
	internal static QueryColumnList FromXml(XmlElement element)
	{
		return new QueryColumnList(
			from XmlElement lineElement in element.SelectNodes("column")
			select QueryColumn.FromXml(lineElement));
	}

	public QueryColumnList(IEnumerable<QueryColumn> columns)
		: base(columns)
	{
	}

	public QueryColumn FindAskColumn(string field)
	{
		return this.FirstOrDefault(c => c.Field == field && c.Ask);
	}
}

public class QueryColumn
{
	internal static QueryColumn FromXml(XmlElement element)
	{
		return new QueryColumn
		{
			Field = element.SelectInnerText("field"),
			Label = element.SelectInnerText("label"),
			Visible = element.SelectInnerText("visible") == "true",
			Ask = element.SelectInnerText("ask") == "true",
			Operator = element.SelectInnerText("operator"),
			From = element.SelectInnerText("from"),
			To = element.SelectInnerText("to"),
		};
	}

	public string Field { get; set; }
	public string Label { get; set; }
	public bool Visible { get; set; }
	public bool Ask { get; set; }
	public string Operator { get; set; }
	public string From { get; set; }
	public string To { get; set; }

	public void AppendToXml(XmlElement parent)
	{
		var column = parent.AppendNewElement("column");
		column.AppendNewElement("field").InnerText = Field;
		column.AppendNewElement("label").InnerText = Label;
		column.AppendNewElement("visible").InnerText = Visible ? "true" : "false";
		column.AppendNewElement("ask").InnerText = Ask ? "true" : "false";
		column.AppendNewElement("operator").InnerText = Operator;
		column.AppendNewElement("from").InnerText = From;
		column.AppendNewElement("to").InnerText = To;
	}
}