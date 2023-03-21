using System.ServiceModel;

namespace TwinfieldApi.Tests
{
	class BaseTestData
	{
		public const string AccessToken = "DummyAccessToken";
		public const string CompanyCode = "TestCompanyCode";
		public const string ClusterUrl = "https://dummy.test.com/v1";
		public const string EndPointUrl = "dummyEndpointUrl";

		public static BasicHttpBinding GetBinding(string endpointUrl)
		{
			var uri = new Uri(endpointUrl);
			var securityMode = uri.Scheme == "https" ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.None;
			var binding = new BasicHttpBinding(securityMode)
			{
				MaxReceivedMessageSize = 20_000_000,
				SendTimeout = TimeSpan.FromSeconds(900)
			};
			return binding;
		}
	}
}
