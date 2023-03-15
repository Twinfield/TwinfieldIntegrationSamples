using TwinfieldApi.Services;
using TwinfieldBankBookService;

namespace TwinfieldApi.Bankbooks;

public class BankbookService
{
	readonly IClientFactory clientFactory;

	public BankbookService()
		: this(new ClientFactory())
	{ }

	public BankbookService(IClientFactory clientFactory)
	{
		this.clientFactory = clientFactory;
	}

	public Bankbook FindBankBook(string code, string clusterUrl, string accessToken, string companyCode)
	{
		var queryResponse = Query(new GetBankBook { Code = code }, clusterUrl, accessToken, companyCode);
		return Bankbook.FromQueryResult(code, queryResponse.Result);
	}

	QueryResponse Query(GetBankBook query, string clusterUrl, string accessToken,
		string companyCode)
	{
		var queryRequest = new QueryRequest
		{
			Query = query,
			Authentication = new AuthenticationHeader
			{
				AccessToken = accessToken,
				CompanyCode = companyCode
			}
		};
		var bankBookClient = clientFactory.CreateBankBookClient(clusterUrl);
		return bankBookClient.Query(queryRequest);
	}
}