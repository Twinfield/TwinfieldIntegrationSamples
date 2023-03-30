using System.Net;
using System.ServiceModel;
using TwinfieldApi.Services;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Organization;

public class OrganizationService
{
	readonly IProcessXmlService processXml;

	public OrganizationService()
	{
		processXml = new ProcessXmlService();
	}

	public OrganizationService(IProcessXmlService processXmlService)
	{
		processXml = processXmlService;
	}

	public List<CompanySummary> GetCompanies(string clusterUrl, string accessToken)
	{
		List<CompanySummary> companySummaries = null;
		try
		{
			var command = new ListCompanyCommand();
			var response = processXml.Process(command.ToXml(), clusterUrl, accessToken, null);
			companySummaries = CompanySummaryList.FromXml(response);
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
		return companySummaries;
	}
}
