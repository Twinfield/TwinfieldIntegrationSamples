using TwinfieldApi.Services;
using TwinfieldBankBookService;
using TwinfieldFinderService;
using TwinfieldProcessXmlService;

namespace TwinfieldApi.Tests.Services;

class ClientFactoryTests
{
	const string DummyUri = "https://dummy.test.com/v1";
		
	[Test]
	public void Should_create_bankbook_client()
	{
		var clientFactory = new ClientFactory();

		var client = clientFactory.CreateBankBookClient(DummyUri);
			
		Assert.That(client, Is.Not.Null);
		Assert.That(client, Is.TypeOf<BankBookServiceClient>());
	}

	[Test]
	public void Should_create_processXml_client()
	{
		var clientFactory = new ClientFactory();

		var client = clientFactory.CreateProcessXmlClient(DummyUri);

		Assert.That(client, Is.Not.Null);
		Assert.That(client, Is.TypeOf<ProcessXmlSoapClient>());
	}

	[Test]
	public void Should_create_finder_client()
	{
		var clientFactory = new ClientFactory();

		var client = clientFactory.CreateFinderClient(DummyUri);

		Assert.That(client, Is.Not.Null);
		Assert.That(client, Is.TypeOf<FinderSoapClient>());
	}
}
