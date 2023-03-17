using TwinfieldFinderService;

namespace TwinfieldApi.Services;

class FinderService
{
	readonly IClientFactory clientFactory;

	public FinderService()
		: this(new ClientFactory())
	{ }

	public FinderService( IClientFactory clientFactory)
	{
		this.clientFactory = clientFactory;
	}

	public FinderData Search(Query query, string clusterUrl, string accessToken, string companyCode)
	{
		var client = clientFactory.CreateFinderClient(clusterUrl);
		var header = new Header { AccessToken = accessToken, CompanyCode = companyCode};

		var searchRequest = new SearchRequest()
		{
			field = query.Field,
			firstRow = query.FirstRow,
			Header = header,
			maxRows = query.MaxRows,
			options = query.Options,
			pattern = query.Pattern,
			type = query.Type
		};

		var searchResponse = client.Search(searchRequest);
		AssertNoMessages(searchResponse.SearchResult);

		return searchResponse.data;
	}

	static void AssertNoMessages(MessageOfErrorCodes[] messages)
	{
		if (messages.Any())
			throw new FinderException(messages);
	}

	public class Query
	{
		public string @Type { get; set; }
		public string Pattern { get; set; }
		public int Field { get; set; }
		public int FirstRow { get; set; }
		public int MaxRows { get; set; }
		public string[][] Options { get; set; }

		public Query()
		{
			Field = 0;
			FirstRow = 1;
			MaxRows = 10;
			Options = null;
		}
	}
}

public class FinderException : Exception
{
	public MessageOfErrorCodes[] Messages { get; }

	public FinderException(MessageOfErrorCodes[] messages)
	{
		Messages = messages;
	}

	public override string Message
	{
		get
		{
			var messages = Messages.Select(m => m.Text).ToArray();
			return string.Join(". ", messages);
		}
	}
}
