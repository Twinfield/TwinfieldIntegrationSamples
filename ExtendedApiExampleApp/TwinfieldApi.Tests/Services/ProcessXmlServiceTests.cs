using NSubstitute;
using TwinfieldApi.Services;
using TwinfieldApi.Utilities;
using TwinfieldProcessXmlService;

namespace TwinfieldApi.Tests.Services;

class ProcessXmlServiceTests : BaseTestData
{
	ProcessXmlService processXmlService;
	const string requestXml = "<list><type>offices</type></list>";
	const string responseXml = @"<offices result='1'>
					<office name='Company 1' shortname='Template NL 00123'>C001</office>
					<office name='Company 2' shortname='Template NL 00123'>C002</office>
				</offices>";

	public void Should_process_xml_document()
	{
		var clientFactory = Substitute.For<IClientFactory>();
		var processXmlSoapClient = Substitute.For<IProcessXmlSoapClient>();
		processXmlService = new ProcessXmlService(clientFactory) { Compressed = false };
		processXmlSoapClient.ProcessXmlDocument(Arg.Any<ProcessXmlDocumentRequest>())
			.Returns(GetProcessXmlDocumentResponse());

		clientFactory.CreateProcessXmlClient(ClusterUrl).Returns(processXmlSoapClient);

		var actualResponse = processXmlService.Process(requestXml.ToXmlDocument(), ClusterUrl, AccessToken,
			CompanyCode);

		var expectedResponse = responseXml.ToXmlDocument();
		Assert.That(actualResponse.OuterXml, Is.EqualTo(expectedResponse.OuterXml));
	}

	public virtual ProcessXmlDocumentResponse GetProcessXmlDocumentResponse()
	{
		return new ProcessXmlDocumentResponse()
		{
			ProcessXmlDocumentResult = responseXml.ToXmlDocument()
		};
	}
}
