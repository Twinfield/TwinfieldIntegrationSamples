using NSubstitute;
using TwinfieldApi.Services;
using TwinfieldFinderService;
using static NUnit.Framework.Assert;

namespace TwinfieldApi.Tests;

class FinderServiceTests : BaseTestData
{
	readonly string[] Columns = { "A", "B" };
	readonly string[] ColumnValues = { "A1", "B1" };


	[Test]
	public void Should_find_data()
	{
		var clientFactory = Substitute.For<IClientFactory>();


		var finderSoapClient = Substitute.For<IFinderSoapClient>();

		finderSoapClient.Search(Arg.Any<SearchRequest>()).Returns(GetSearchResponse());

		clientFactory.CreateFinderClient(ClusterUrl).Returns(finderSoapClient);

		var finderService = new FinderService(clientFactory);

		var finderData = finderService.Search(GetQuery(), ClusterUrl, AccessToken, CompanyCode);

		IsNotNull(finderData);
		CollectionAssert.AreEqual(finderData.Columns, Columns);
		CollectionAssert.AreEqual(finderData.Items[0], ColumnValues);
	}

	static FinderService.Query GetQuery()
	{
		var options = new string[3][];
		options[0] = new[] { "dimtype", "DEB" };
		options[1] = new[] { "section", "financials" };
		return new FinderService.Query
		{
			Field = 0,
			FirstRow = 1,
			MaxRows = 10,
			Options = null
		};
	}

	SearchResponse GetSearchResponse()
	{
		return new SearchResponse()
		{
			SearchResult = new MessageOfErrorCodes[] { },
			data = new FinderData()
			{
				TotalRows = 1,
				Columns = Columns,
				Items = new[]
				{
						ColumnValues
				}
			}
		};
	}

}

