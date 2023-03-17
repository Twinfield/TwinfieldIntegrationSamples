using System.Diagnostics;
using System.Net;
using System.ServiceModel;
using System.Xml;

namespace TwinfieldApi.Utilities;

public static class Extensions
{
	public static XmlDocument ToXmlDocument(this string input)
	{
		var document = new XmlDocument();
		document.LoadXml(input);
		return document;
	}

	public static XmlElement AppendNewElement(this XmlDocument document, string name)
	{
		return (XmlElement)document.AppendChild(document.CreateElement(name));
	}

	public static XmlElement AppendNewElement(this XmlElement element, string name)
	{
		Debug.Assert(element.OwnerDocument != null, "element.OwnerDocument != null");
		return (XmlElement)element.AppendChild(element.OwnerDocument.CreateElement(name));
	}

	public static XmlElement SelectSingleElement(this XmlElement element, string xpath)
	{
		return (XmlElement)element.SelectSingleNode(xpath);
	}

	public static string SelectInnerText(this XmlNode node, string xpath)
	{
		var target = node.SelectSingleNode(xpath);
		return target == null ? string.Empty : target.InnerText;
	}

	public static bool IsSuccess(this XmlDocument document)
	{
		return document != null && document.DocumentElement.IsSuccess();
	}

	public static bool IsSuccess(this XmlElement element)
	{
		return element != null && element.GetAttribute("result") == "1";
	}

	public static List<string> GetMessages(this XmlElement element)
	{
		if (element == null)
			return null;

		var messageAttributes = element.SelectNodes(".//@msg");
		return (from XmlAttribute m in messageAttributes select m.InnerText).ToList();
	}

	public static void HandleWebException(this WebException webException)
	{
		if (webException == null) return;

		Console.WriteLine("Error occurred while processing the xml request.");
		var statusCode = ((HttpWebResponse)webException.Response).StatusCode;
		Console.WriteLine($"Http status code : {statusCode}");

		if (statusCode != HttpStatusCode.Forbidden &&
		    statusCode != HttpStatusCode.Unauthorized) return;
		var statusDescription = ((HttpWebResponse)webException.Response).StatusDescription;

		if (string.IsNullOrEmpty(statusDescription)) return;
		if (statusDescription.Contains(":"))
		{
			var descriptionDetails = statusDescription.Split(':');
			if (descriptionDetails.Length <= 1) return;
			Console.WriteLine($"Error code : {descriptionDetails[0].Trim()}");
			Console.WriteLine($"Error description : {descriptionDetails[1].Trim()}");
		}
		else
			Console.WriteLine($"Error description : {statusDescription}");
	}

	public static void HandleSoapException(this FaultException soapException)
	{
		if (soapException == null) return;

		Console.WriteLine("Error occurred while processing the xml request.");
		if (soapException.Data is XmlElement detail)
		{
			var el = (XmlElement)detail.SelectSingleNode("message");
			if (el != null) Console.WriteLine($"Message : {el.InnerText}");
			el = (XmlElement)detail.SelectSingleNode("code");
			if (el != null) Console.WriteLine($"Code : {el.InnerText}");
			el = (XmlElement)detail.SelectSingleNode("source");
			if (el != null) Console.WriteLine($"Source : {el.InnerText}");
			Console.WriteLine(detail.OuterXml);
			return;

		}

		Console.WriteLine($"Message : {soapException.Message}");
		Console.WriteLine($"Code : {soapException.Code}");
	}
}
