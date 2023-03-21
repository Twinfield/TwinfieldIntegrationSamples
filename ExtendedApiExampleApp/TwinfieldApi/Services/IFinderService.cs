using TwinfieldFinderService;

namespace TwinfieldApi.Services;

interface IFinderService
{
	FinderData Search(FinderService.Query query, string clusterUrl, string accessToken,
		string companyCode);
}
