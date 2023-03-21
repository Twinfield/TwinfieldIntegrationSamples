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

	public List<OfficeSummary> GetOffices(string clusterUrl, string accessToken)
	{
		List<OfficeSummary> officeSummaries = null;
		try
		{
			var command = new ListOfficesCommand();
			var response = processXml.Process(command.ToXml(), clusterUrl, accessToken, null);
			officeSummaries = OfficeSummaryList.FromXml(response);
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
		return officeSummaries;
	}
}
