using System.Net;
using System.ServiceModel;
using TwinfieldApi.Services;
using TwinfieldApi.Utilities;
using TwinfieldFinderService;

namespace TwinfieldApi.Dimensions;

public class DimensionService
{
	readonly IProcessXmlService processXml;
	readonly IFinderService finderService;

	public DimensionService()
		: this(new ClientFactory())
	{ }

	public DimensionService(IClientFactory clientFactory)
	{
		processXml = new ProcessXmlService(clientFactory);
		finderService = new FinderService(clientFactory);
	}

	public List<DimensionSummary> FindDimensions(string pattern, string dimensionType, int field, string clusterUrl, string accessToken, string companyCode)
	{
		var query = new FinderService.Query
		{
			Type = "DIM",
			Pattern = pattern,
			Field = field,
			MaxRows = 10,
			Options = new[] { new[] { "dimtype", dimensionType } }
		};
		var searchResult = finderService.Search(query, clusterUrl, accessToken, companyCode);

		return SearchResultToDimensionSummaries(searchResult);
	}

	static List<DimensionSummary> SearchResultToDimensionSummaries(FinderData searchResult)
	{
		if (searchResult.Items == null)
			return new List<DimensionSummary>();

		return searchResult.Items.Select(item =>
			new DimensionSummary { Code = item[0], Name = item[1] }).ToList();
	}

	public Dimension ReadDimension(string dimensionType, string dimensionCode, string clusterUrl, string accessToken, string companyCode)
	{
		Dimension dimension = null;
		try
		{
			var command = new ReadDimensionCommand
			{
				Office = companyCode,
				DimensionType = dimensionType,
				DimensionCode = dimensionCode
			};
			var response = processXml.Process(command.ToXml(), clusterUrl, accessToken, companyCode);
			dimension = Dimension.FromXml(response);
		}
		catch (FaultException ex)
		{
			Console.WriteLine("Error occurred while processing the xml request.");
			Console.WriteLine(ex.Message);
		}
		catch (WebException ex)
		{
			ex.HandleWebException();
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error occurred while processing the xml request.");
			Console.WriteLine(ex.Message);
		}
		return dimension;
	}
}
