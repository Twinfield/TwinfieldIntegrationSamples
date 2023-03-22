using NSubstitute;
using System.Xml;
using TwinfieldApi.Organization;
using TwinfieldApi.Services;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Tests.Services;
class OrganizationServiceTests : BaseTestData
{
	const string CompanyCode1 = "C001";
	const string CompanyCode2 = "C002";
	const string CompanyName1 = "C001";
	const string CompanyName2 = "C002";
	const string CompanyXml =
		@$"<result>
				<office name='{CompanyName1}' shortname='{CompanyCode1}'>{CompanyCode1}</office>
				<office name='{CompanyName2}' shortname='{CompanyCode2}'>{CompanyCode2}</office>			
			</result>";

	[Test]
	public void Should_return_companies_in_the_organization_using_process_xml_service()
	{
		var processXmlService = Substitute.For<IProcessXmlService>();
		processXmlService.Process(Arg.Any<XmlDocument>(), ClusterUrl, AccessToken, null)
			.Returns(CompanyXml.ToXmlDocument().DocumentElement);

		var organizationService = new OrganizationService(processXmlService);
		var actualCompanySummary = organizationService.GetCompanies(ClusterUrl, AccessToken);
		var expectedCompanySummary = GetCompanySummaries();

		for (var i = 0; i < actualCompanySummary.Count; i++)
		{
			Assert.That(actualCompanySummary[i].Name, Is.EqualTo(expectedCompanySummary[i].Name));
			Assert.That(actualCompanySummary[i].Code, Is.EqualTo(expectedCompanySummary[i].Code));
		}
	}

	static List<CompanySummary> GetCompanySummaries()
	{
		return new List<CompanySummary>()
			{
				new() { Code = CompanyCode1, Name = CompanyName1 },
				new() { Code = CompanyCode2, Name = CompanyName2 }
			};
	}

}
