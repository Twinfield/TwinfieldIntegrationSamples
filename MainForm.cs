using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using System.Xml.Linq;
using OAuth2ExampleApp.TwinfieldProcessXml;

namespace OAuth2ExampleApp
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		void btnAuthorize_Click(object sender, EventArgs e)
		{
			var clientId = ConfigurationManager.AppSettings["ClientId"];
			var redirectUri = ConfigurationManager.AppSettings["RedirectUri"];

			var result = AuthorizeDialog.Show(clientId, redirectUri);
			if (result != null)
			{
				if (result.Success)
				{
					Log($"Authorized user {result.User} in organisation {result.Organisation}.");
					GetCompanies(result.AccessToken, result.ClusterUrl);
				}
				else
				{
					Log($"Authorization failed: {result.Message}");
				}
			}
		}

		void GetCompanies(string accessToken, string clusterUrl)
		{
			var processXmlService = new ProcessXmlSoapClient(
				new BasicHttpsBinding(),
				new EndpointAddress(clusterUrl + "/webservices/processxml.asmx"));

			var result = processXmlService.ProcessXmlString(
				new Header { AccessToken = accessToken },
				"<list><type>offices</type></list>");

			var doc = XDocument.Parse(result);
			var offices = doc.Root?.Elements("office").ToArray();
			Log($"Found {offices?.Length} companies");
			if (offices != null)
			{
				foreach (var office in offices)
					Log($"{office.Attribute("name")?.Value} [{office.Value}]");
			}
		}

		void Log(string text)
		{
			txtLog.AppendText(text);
			txtLog.AppendText(Environment.NewLine);
			txtLog.SelectionStart = Int32.MaxValue;
		}
	}
}
