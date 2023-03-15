using TwinfieldApi.Browse;
using TwinfieldApi.Browse.Data;
using TwinfieldApi.Browse.Query;

namespace ExtendedApiExampleApp;

class BookkeepingDemo
{
	public void Run(string accessToken, string clusterUrl, string companyCode)
	{
		BrowseTransactions(accessToken, clusterUrl, companyCode);
	}

	static void BrowseTransactions(string accessToken, string clusterUrl, string companyCode)
	{
		const string transactionListBrowseCode = "020";

		Console.WriteLine("Browse transactions list");

		var browseService = new BrowseService();
		var browseDefinition = browseService.ReadBrowseDefinition(transactionListBrowseCode, clusterUrl, accessToken, companyCode);
		var browseQuery = CreateBrowseQuery(browseDefinition);

		var transactionList = browseService.Read(browseQuery, clusterUrl, accessToken, companyCode);
		DisplayBrowseResult(transactionList);
	}

	static BrowseQuery CreateBrowseQuery(BrowseDefinition browseDefinition)
	{
		var browseQuery = new BrowseQuery
		{
			Code = browseDefinition.Code,
			QueryColumns = browseDefinition.QueryColumns
		};

		ClearYearPeriodPrompt(browseQuery);
		SetTransactionDatePrompt(browseQuery);
		return browseQuery;
	}

	static void ClearYearPeriodPrompt(BrowseQuery browseQuery)
	{
		var column = browseQuery.QueryColumns.FindAskColumn("fin.trs.head.yearperiod");
		column.From = string.Empty;
		column.To = string.Empty;
	}

	static void SetTransactionDatePrompt(BrowseQuery browseQuery)
	{
		var column = browseQuery.QueryColumns.FindAskColumn("fin.trs.head.date");
		column.From = DateTime.Today.AddMonths(-1).ToString("yyyyMMdd");
		column.To = DateTime.Today.ToString("yyyyMMdd");
	}

	static void DisplayBrowseResult(BrowseResult result)
	{
		Console.WriteLine("Found {0} rows.", result.TotalNumberOfRows);
		Console.WriteLine(result.Columns.Select(c => c.Label).ToCommaSeparatedString());
		foreach (var row in result.Rows)
			Console.WriteLine(row.Cells.ToCommaSeparatedString());
		Console.WriteLine();
	}
}