using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OAuth2ExampleApp
{
	public partial class MainForm : Form
	{
		public MainForm() => InitializeComponent();

		void btnAuthorize_Click(object sender, EventArgs e)
		{
			var clientId = ConfigurationManager.AppSettings["ClientId"];
			var redirectUri = ConfigurationManager.AppSettings["RedirectUri"];

			var result = AuthorizeDialog.Show(clientId, redirectUri);
			if (result != null && result.Success)
			{

				Log($"Authorized user {result.User} in organization {result.Organisation}.");
				DisplayCompanies(result.AccessToken, result.ClusterUrl);
			}
			else
				Log($"Authorization failed: {result?.Message}");
			
		}

		void DisplayCompanies(string accessToken, string clusterUrl)
		{
			var processXmlService = new ProcessXmlService(clusterUrl);

			var result = processXmlService.GetCompanies(accessToken);

			var doc = XDocument.Parse(result);
			var companies = doc.Root?.Elements("office").ToArray();

			Log($"Access token : {accessToken}");
			AppendNewLine();
			Log($"Cluster url : {clusterUrl}");
			AppendNewLine();
			Log($"Found {companies?.Length} companies");
			AppendNewLine();
			Log($"Displaying first 10 companies");
			AppendNewLine();

			if (companies == null || !companies.Any()) 
				return;

			foreach (var company in companies.Take(10))
				Log($"{company.Attribute("name")?.Value} [{company.Value}]");
		}

		void Log(string text)
		{
			txtLog.AppendText(text);
			txtLog.AppendText(Environment.NewLine);
			txtLog.SelectionStart = Int32.MaxValue;
		}

		void AppendNewLine() => txtLog.AppendText(Environment.NewLine);
	}
}
