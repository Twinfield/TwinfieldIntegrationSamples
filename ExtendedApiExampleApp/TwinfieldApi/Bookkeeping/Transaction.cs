using System.Xml;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Bookkeeping;

public class Transaction
{
	internal static Transaction FromXml(XmlElement element)
	{
		return new Transaction
		{
			Office = element.SelectInnerText("header/office"),
			Daybook = element.SelectInnerText("header/code"),
			Number = decimal.Parse(element.SelectInnerText("header/number")),
			Currency = element.SelectInnerText("header/currency"),
			Lines = TransactionLines.FromXml(element.SelectSingleElement("lines"))
		};
	}

	public string Office { get; set; }
	public string Daybook { get; set; }
	public decimal Number { get; set; }
	public string Currency { get; set; }
	public List<TransactionLine> Lines { get; set; }
}

public class TransactionLines
{
	internal static List<TransactionLine> FromXml(XmlElement element)
	{
		return (
			from XmlElement lineElement in element.SelectNodes("line")
			select TransactionLine.FromXml(lineElement)).ToList();
	}
}

public class TransactionLine
{
	internal static TransactionLine FromXml(XmlElement element)
	{
		return new TransactionLine
		{
			Amount = decimal.Parse(element.SelectInnerText("value")),
			DebitCredit = element.SelectInnerText("debitcredit"),
			Dimensions = DimensionList.FromXml(element)
		};
	}

	public List<string> Dimensions { get; set; }
	public decimal Amount { get; set; }
	public string DebitCredit { get; set; }
}

public class DimensionList
{
	const int MaxDimensionLevel = 6;

	internal static List<string> FromXml(XmlElement element)
	{
		var dimensions = new List<string>();
		for (var level = 1; level < MaxDimensionLevel; level++)
		{
			var dimensionElement = element.SelectSingleElement("dim" + level);
			if (dimensionElement != null)
				dimensions.Add(dimensionElement.InnerText);
		}
		return dimensions;
	}
}
