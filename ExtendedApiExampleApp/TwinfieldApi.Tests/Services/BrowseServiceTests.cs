using NSubstitute;
using System.Xml;
using TwinfieldApi.Browse;
using TwinfieldApi.Browse.Query;
using TwinfieldApi.Services;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Tests
{
	class BrowseServiceTests : BaseTestData
	{
		const string TestBrowseCode = "020";
		const string TestBrowseName = "TestReport";
		const string BrowseDefination =
			@$"<result><office name='{CompanyCode}' shortname='{CompanyCode}'>{CompanyCode}</office><code>{TestBrowseCode}</code>
			<name>{TestBrowseName}</name><shortname>{TestBrowseName}</shortname><visible>true</visible>
			<columns code='020'>
				<column id='1'><field>ColumnA</field><label>A</label><visible>true</visible><ask>true</ask><operator>between</operator><from>$DEFAULT$</from><to>$DEFAULT$</to><finderparam></finderparam></column><column id='2'><field>ColumnB</field><label>B</label><visible>true</visible><ask>true</ask><operator>equal</operator><from></from><to></to><finderparam></finderparam></column>
			</columns></result>";

		[Test]
		public void Should_read_browse_definition()
		{
			var processXmlService = Substitute.For<IProcessXmlService>();
			processXmlService.Process(Arg.Any<XmlDocument>(), ClusterUrl, AccessToken, CompanyCode)
				.Returns(BrowseDefination.ToXmlDocument().DocumentElement);

			var browseService = new BrowseService(processXmlService);
			var result = browseService.ReadBrowseDefinition(TestBrowseCode, ClusterUrl, AccessToken, CompanyCode);

			Assert.That(result.Name, Is.EqualTo(TestBrowseName));
			Assert.That(result.Code, Is.EqualTo(TestBrowseCode));
			Assert.That(result.Office, Is.EqualTo(CompanyCode));
			Assert.That(result.QueryColumns.Count, Is.EqualTo(2));
		}

		[Test]
		public void Should_read_browse_data()
		{
			var processXmlService = Substitute.For<IProcessXmlService>();
			processXmlService.Process(Arg.Any<XmlDocument>(), ClusterUrl, AccessToken, CompanyCode)
				.Returns(BrowseDefination.ToXmlDocument().DocumentElement);

			var browseService = new BrowseService(processXmlService);
			browseService.Read(GetBrowseQuery(), ClusterUrl, AccessToken, CompanyCode);

			processXmlService.Received().Process(Arg.Any<XmlDocument>(), ClusterUrl,
				AccessToken, CompanyCode);
		}

		static BrowseQuery GetBrowseQuery()
		{
			var columnList = new List<QueryColumn>()
			{
				new() { Ask = true, Field = "period", From = string.Empty, Label = "Period", Operator = "between", To = string.Empty, Visible = true }
			};
			return new BrowseQuery()
			{
				Code = TestBrowseCode,
				QueryColumns = new QueryColumnList(columnList)
			};
		}
	}
}
