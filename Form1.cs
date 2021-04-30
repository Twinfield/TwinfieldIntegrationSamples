using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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
				"Make sure you create a client first at https://developers.twinfield.com and updated the code."));
		}

		async void btnLogin_Click(object sender, EventArgs e)
		{
			var targetUri = new StringBuilder(AuthorizationEndPoint);
			targetUri.Append($"?client_id={WebUtility.UrlEncode(ClientId)}");
			targetUri.Append("&response_type=token");
			targetUri.Append("&scope=twf.organisationUser+twf.organisation");
			targetUri.Append($"&redirect_uri={WebUtility.UrlEncode(RedirectUri)}");
			targetUri.Append($"&state={WebUtility.UrlEncode(Guid.NewGuid().ToString())}");
			targetUri.Append($"&nonce={WebUtility.UrlEncode(Guid.NewGuid().ToString())}");

			var authorizationUrl = targetUri.ToString();
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

			var queryString = e.Uri.Substring(e.Uri.IndexOf("#"));
			var beginOfAccessToken = queryString.IndexOf("access_token=") + 13;
			var jwtToken = queryString.Substring(beginOfAccessToken,
				queryString.IndexOf("&", beginOfAccessToken) - beginOfAccessToken);

			var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
			var parsedToken = tokenHandler.ReadJwtToken(jwtToken);

			var user = parsedToken.Claims.First(c => c.Type == "twf.organisationUserCode").Value;
			var organisation = parsedToken.Claims.First(c => c.Type == "twf.organisationCode").Value;
			var clusterUrl = parsedToken.Claims.First(c => c.Type == "twf.clusterUrl").Value;

			var companies = GetCompanies(jwtToken, clusterUrl).ToArray();

			ShowTokenAndResponseInformation(user, organisation, companies);
		}

		void ShowTokenAndResponseInformation(string user, string organisation, string[] companies)
		{
			var htmlContent = new StringBuilder();
			htmlContent.Append("<html><body>");
			htmlContent.Append("<h1>User</h1>");
			htmlContent.Append($"<p>{WebUtility.HtmlEncode(user)} in organisation {WebUtility.HtmlEncode(organisation)}</p>");

			htmlContent.Append("<h1>Companies</h1>");
			htmlContent.Append("<ul>");

			foreach (var company in companies)
				htmlContent.Append($"<li>{WebUtility.HtmlEncode(company)}</li>");

			htmlContent.Append("</ul>");
			htmlContent.Append("</BODY></HTML>");

			webView.NavigateToString(htmlContent.ToString());
		}

		static IEnumerable<string> GetCompanies(string accessToken, string clusterUrl)
		{
			var processXmlService = new ProcessXmlSoapClient(
				new BasicHttpsBinding(),
				new EndpointAddress(clusterUrl + "/webservices/processxml.asmx"));

			var result = processXmlService.ProcessXmlString(
				new Header() {AccessToken = accessToken}, "<list><type>offices</type></list>");

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
						<h1>{WebUtility.HtmlEncode(title)}</h1>
						<p>{WebUtility.HtmlEncode(body)}</p>
					</body>
				</html>";
		}
	}
}
