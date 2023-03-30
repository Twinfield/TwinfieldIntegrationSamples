using NSubstitute;
using NSubstitute.ReturnsExtensions;
using TwinfieldApi.Services;
using TwinfieldApi.Utilities;
using TwinfieldProcessXmlService;

namespace TwinfieldApi.Tests.Services;

class ProcessXmlServiceTests : BaseTestData
{
	ProcessXmlService processXmlService;
	const string requestXml = "<list><type>offices</type></list>";

	[Test]
	public void Should_throw_process_xml_exception_when_service_do_not_return_any_xml()
	{
		var clientFactory = Substitute.For<IClientFactory>();
		var processXmlSoapClient = Substitute.For<IProcessXmlSoapClient>();
		processXmlService = new ProcessXmlService(clientFactory) { Compressed = false };
		processXmlSoapClient.ProcessXmlDocument(Arg.Any<ProcessXmlDocumentRequest>())
			.ReturnsNull();

		clientFactory.CreateProcessXmlClient(ClusterUrl).Returns(processXmlSoapClient);

		Assert.Throws<ProcessXmlException>(() => processXmlService.Process(requestXml.ToXmlDocument(),
			ClusterUrl, AccessToken, CompanyCode));
	}
}
