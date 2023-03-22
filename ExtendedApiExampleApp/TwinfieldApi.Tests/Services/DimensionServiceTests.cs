using System.Xml;
using NSubstitute;
using TwinfieldApi.Dimensions;
using TwinfieldApi.Services;
using TwinfieldApi.Utilities;
using TwinfieldFinderService;

namespace TwinfieldApi.Tests.Services;

class DimensionServiceTests : BaseTestData
{
	readonly string[] Columns = { "Code", "Name" };
	readonly string[] ColumnValues = { "Code1", "Name1" };
	const string DimensionCode = "DimCode1";
	const string DimensionName = "DimName1";

	const string DimensionXml =
		$@"<dimension status='active' result='1' xmlns=''>
			<office name='{CompanyCode}' shortname='{CompanyName}'>{CompanyCode}</office>
			<type name='Debiteuren' shortname='Debiteuren'>DEB</type>
			<code>{DimensionCode}</code>
			<name>{DimensionName}</name>
		</dimension>";


	[Test]
	public void Should_find_dimensions_using_finder_service()
	{
		var processXmlService = Substitute.For<IProcessXmlService>();
		var finderService = Substitute.For<IFinderService>();
		finderService.Search(Arg.Any<FinderService.Query>(), ClusterUrl, AccessToken, CompanyCode)
			.Returns(GetSearchResponse());

		var dimensionService = new DimensionService(processXmlService, finderService);
		var dimensionSummaries = dimensionService
			.FindDimensions("a*", "DEB", 2, ClusterUrl, AccessToken, CompanyCode);

		Assert.That(dimensionSummaries.Count, Is.EqualTo(1));
		Assert.That(dimensionSummaries[0].Name, Is.EqualTo(ColumnValues[1]));
		Assert.That(dimensionSummaries[0].Code, Is.EqualTo(ColumnValues[0]));
	}

	[Test]
	public void Should_read_dimension_using_processXml_service()
	{
		var processXmlService = Substitute.For<IProcessXmlService>();
		var finderService = Substitute.For<IFinderService>();
		processXmlService.Process(Arg.Any<XmlDocument>(), ClusterUrl, AccessToken, CompanyCode)
			.Returns(DimensionXml.ToXmlDocument().DocumentElement);

		var dimensionService = new DimensionService(processXmlService, finderService);
		var dimension = dimensionService
			.ReadDimension("DEB", DimensionCode, ClusterUrl, AccessToken, CompanyCode);

		Assert.That(dimension.Name, Is.EqualTo(DimensionName));
		Assert.That(dimension.Code, Is.EqualTo(DimensionCode));
		Assert.That(dimension.Company, Is.EqualTo(CompanyCode));
	}

	FinderData GetSearchResponse()
	{
		return new FinderData()
		{
			Columns = Columns,
			Items = new[]
			{
				ColumnValues
			},
			TotalRows = 2
		};
	}
}
