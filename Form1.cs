using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using OAuth2ExampleApp.TwinfieldProcessXml;

namespace OAuth2ExampleApp
{
	public partial class Form1 : Form
	{
		// Below constants reflect information you should configure in the Twinfield developers portal
		// https://developers.twinfield.com
		const string ClientId = "TwinfieldOAuth2ExampleApp";
		const string RedirectUri = "oob://oauth2exampleapp/redirecturi";

		const string AuthorizationEndPoint = "https://login.twinfield.com/auth/authentication/connect/authorize";

		public Form1()
		{
			InitializeComponent();
		}

		async void Form1_Load(object sender, EventArgs e)
		{
			await webView.EnsureCoreWebView2Async();

			webView.NavigateToString(GetHtmlMessage(
				"Press Login to start the journey",
				"Make sure you create a client first at https://developers.twinfield.com and update the code."));
		}

		async void btnLogin_Click(object sender, EventArgs e)
		{
			var queryString = HttpUtility.ParseQueryString("");
			queryString["client_id"] = ClientId;
			queryString["response_type"] = "token";
			queryString["scope"] = "twf.organisationUser twf.organisation";
			queryString["redirect_uri"] = RedirectUri;
			queryString["state"] = Guid.NewGuid().ToString();
			queryString["nonce"] = Guid.NewGuid().ToString();

			var authorizationUrl = $"{AuthorizationEndPoint}?{queryString}";

			textBoxUrl.Text = authorizationUrl;

			await Navigate(authorizationUrl);
		}

		async Task Navigate(string authorizationUrl)
		{
			try
			{
				webView.CoreWebView2.Navigate(authorizationUrl);
			}
			catch (Exception e)
			{
				webView.NavigateToString(GetHtmlMessage($"Error navigating to {authorizationUrl}", e.Message));
			}
		}

		/// <summary>
		/// Catches the navigation to the redirect uri and parses the access token.
		/// </summary>
		void webView_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
		{
			if (!e.Uri.StartsWith(RedirectUri))
				return;

			e.Cancel = true;

			var uri = new Uri(e.Uri);
			var fragment = uri.Fragment.Substring(1);
			var parameters = HttpUtility.ParseQueryString(fragment);

			var error = parameters["error"];
			if (!string.IsNullOrEmpty(error))
			{
				webView.NavigateToString(GetHtmlMessage($"Error", error));
				return;
			}

			var accessToken = parameters["access_token"];
			var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
			var accessTokenHeader = tokenHandler.ReadJwtToken(accessToken);

			var user = accessTokenHeader.Claims.First(c => c.Type == "twf.organisationUserCode").Value;
			var organisation = accessTokenHeader.Claims.First(c => c.Type == "twf.organisationCode").Value;
			var clusterUrl = accessTokenHeader.Claims.First(c => c.Type == "twf.clusterUrl").Value;

			var companies = GetCompanies(accessToken, clusterUrl).ToArray();

			ShowTokenAndResponseInformation(user, organisation, companies);
		}

		void ShowTokenAndResponseInformation(string user, string organisation, string[] companies)
		{
			var htmlContent = new StringBuilder();
			htmlContent.Append("<html><body>");
			htmlContent.Append("<h1>User</h1>");
			htmlContent.Append($"<p>{HttpUtility.HtmlEncode(user)} in organisation {HttpUtility.HtmlEncode(organisation)}.</p>");

			htmlContent.Append("<h1>Companies</h1>");
			htmlContent.Append("<ul>");

			foreach (var company in companies)
				htmlContent.Append($"<li>{HttpUtility.HtmlEncode(company)}</li>");

			htmlContent.Append("</ul>");
			htmlContent.Append("</body></html>");

			webView.NavigateToString(htmlContent.ToString());
		}

		static IEnumerable<string> GetCompanies(string accessToken, string clusterUrl)
		{
			var processXmlService = new ProcessXmlSoapClient(
				new BasicHttpsBinding(),
				new EndpointAddress(clusterUrl + "/webservices/processxml.asmx"));

			var result = processXmlService.ProcessXmlString(
				new Header() { AccessToken = accessToken }, "<list><type>offices</type></list>");

			var doc = new XmlDocument();
			doc.LoadXml(result);

			foreach (XmlElement officeElement in doc.SelectNodes("/offices/office"))
				yield return $"{officeElement.GetAttribute("name")} [{officeElement.InnerText}]";
		}

		string GetHtmlMessage(string title, string body)
		{
			return
				$@"<html>
					<body>
						<h1>{HttpUtility.HtmlEncode(title)}</h1>
						<p>{HttpUtility.HtmlEncode(body)}</p>
					</body>
				</html>";
		}
	}
}
