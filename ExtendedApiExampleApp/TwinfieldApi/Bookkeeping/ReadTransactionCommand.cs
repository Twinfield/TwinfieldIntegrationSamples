using System.Globalization;
using System.Xml;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Bookkeeping;

class ReadTransactionCommand
{
	public string Office { get; set; }
	public string Daybook { get; set; }
	public decimal TransactionNumber { get; set; }

	internal XmlDocument ToXml()
	{
		var command = new XmlDocument();
		var rootElement = command.AppendNewElement("read");
		rootElement.AppendNewElement("type").InnerText = "transaction";
		rootElement.AppendNewElement("office").InnerText = Office;
		rootElement.AppendNewElement("code").InnerText = Daybook;
		rootElement.AppendNewElement("number").InnerText = TransactionNumber.ToString(CultureInfo.InvariantCulture);
		return command;
	}
}