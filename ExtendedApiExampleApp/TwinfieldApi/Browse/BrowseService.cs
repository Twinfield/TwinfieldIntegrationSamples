using System.Net;
using System.ServiceModel;
using TwinfieldApi.Browse.Data;
using TwinfieldApi.Browse.Query;
using TwinfieldApi.Services;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Browse;

public class BrowseService
{
	readonly IProcessXmlService processXml;

	public BrowseService()
	{
		processXml = new ProcessXmlService()
		{
			Compressed = true
		};
	}

	public BrowseService(IProcessXmlService processXmlService)
	{
		processXml = processXmlService;
	}

	public BrowseDefinition ReadBrowseDefinition(string browseCode, string clusterUrl, string accessToken, string companyCode)
	{
		BrowseDefinition browseDefinition = null;
		var command = new ReadBrowseDefinitionCommand
		{
			Company = companyCode,
			Code = browseCode
		};
		try
		{
			var response = processXml.Process(command.ToXml(), clusterUrl, accessToken, companyCode);
			browseDefinition = BrowseDefinition.FromXml(response);
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
		return browseDefinition;
	}

	public BrowseResult Read(BrowseQuery query, string clusterUrl, string accessToken, string companyCode)
	{
		BrowseResult browseResult = null;
		try
		{
			var response = processXml.Process(query.ToXml(), clusterUrl, accessToken, companyCode);
			browseResult = BrowseResult.FromXml(response);
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

		return browseResult;
	}
}
