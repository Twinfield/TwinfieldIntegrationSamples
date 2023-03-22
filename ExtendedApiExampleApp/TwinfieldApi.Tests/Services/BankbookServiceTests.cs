using NSubstitute;
using TwinfieldApi.Bankbooks;
using TwinfieldApi.Services;
using TwinfieldBankBookService;

namespace TwinfieldApi.Tests.Services;

class BankbookServiceTests : BaseTestData
{
	BankbookService bankbookService;
	const string BankbookCode = "TestBNK";
	const string BankbookName = "TestBankbook";
	const string BankAccountNumber = "123456789";

	[Test]
	public void Should_find_bankbook_using_bankbook_service_when_requested()
	{
		var clientFactory = Substitute.For<IClientFactory>();
		var bankBookServiceClient = Substitute.For<IBankBookServiceClient>();
		bankbookService = new BankbookService(clientFactory);
		bankBookServiceClient.Query(Arg.Any<QueryRequest>()).Returns(GetBankBook());
		clientFactory.CreateBankBookClient(ClusterUrl).Returns(bankBookServiceClient);

		var bankBook = bankbookService.FindBankBook(BankbookCode, ClusterUrl, AccessToken, CompanyCode);


		Assert.That(bankBook, Is.Not.Null);
		Assert.That(bankBook.Name, Is.EqualTo(BankbookName));
		Assert.That(bankBook.AccountNumber, Is.EqualTo(BankAccountNumber));
	}

	static QueryResponse GetBankBook()
	{
		return new QueryResponse
		{
			Result = new GetBankBookResult
			{
				Name = BankbookName,
				BankAccount = new BankAccount
				{
					Number = BankAccountNumber
				}
			}
		};
	}
}