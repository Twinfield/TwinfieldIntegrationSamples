using System.Xml;
using TwinfieldApi.Utilities;
using TwinfieldProcessXmlService;

namespace TwinfieldApi.Services;

public class ProcessXmlService
{
	readonly IClientFactory clientFactory;

	public ProcessXmlService()
		: this(new ClientFactory())
	{ }

	public ProcessXmlService(IClientFactory clientFactory)
	{
		this.clientFactory = clientFactory;
	}

	public bool Compressed { get; set; }

	public XmlElement Process(XmlDocument input, string clusterUrl, string accessToken, string companyCode)
	{
		var client = clientFactory.CreateProcessXmlClient(clusterUrl);
		var header = new Header { AccessToken = accessToken, CompanyCode = companyCode };
		var result = Process(client, header, input);
		if (!result.IsSuccess())
			throw new ProcessXmlException(result);
		return result;
	}

	XmlElement Process(IProcessXmlSoapClient client, Header header, XmlDocument input)
	{
		return Compressed
			? ProcessCompressed(client, header, input)
			: ProcessUncompressed(client, header, input);
	}

	static XmlElement ProcessUncompressed(IProcessXmlSoapClient client, Header header, XmlDocument input)
	{
		var request = new ProcessXmlDocumentRequest
		{
			Header = header,
			xmlRequest = input
		};
		var response = client.ProcessXmlDocument(request);

		return response?.ProcessXmlDocumentResult as XmlElement;
	}

	static XmlElement ProcessCompressed(IProcessXmlSoapClient client, Header header, XmlDocument input)
	{
		var compressedInput = Zlib.CompressXml(input);
		var request = new ProcessXmlCompressedRequest
		{
			Header = header,
			xmlRequest = compressedInput
		};
		var compressedResponse = client.ProcessXmlCompressed(request);
		var document = Zlib.DecompressXml(compressedResponse.ProcessXmlCompressedResult);

		return document?.DocumentElement;
	}
}

internal class ProcessXmlException : Exception
{
	readonly XmlElement element;

	public ProcessXmlException(XmlElement element)
	{
		this.element = element;
	}

	public ProcessXmlException(XmlDocument document)
	{
		if (document != null)
			element = document.DocumentElement;
	}

	public override string Message
	{
		get
		{
			if (element == null)
				return "Process XML didn't return any XML.";
			var messages = element.GetMessages();
			if (!messages.Any())
				return "Process XML returned XML that contains an unknown error.";
			return string.Join(" ", messages.ToArray());
		}
	}
}