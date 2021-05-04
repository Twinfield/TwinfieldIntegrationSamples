using System;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace OAuth2ExampleApp
{
	public partial class AuthorizeDialog : Form
	{
		const string AuthorizationEndPoint = "https://login.twinfield.com/auth/authentication/connect/authorize";
		readonly string clientId;
		readonly string redirectUri;
		string oAuth2State;

		AuthorizeResult result;

		public AuthorizeDialog(string clientId, string redirectUri)
		{
			InitializeComponent();
			this.clientId = clientId;
			this.redirectUri = redirectUri;
			NavigateToAuthorizationPage();
		}

		void NavigateToAuthorizationPage()
		{
			oAuth2State = Guid.NewGuid().ToString();
			webView.Source = BuildAuthorizationUrl(clientId, redirectUri, oAuth2State);
		}

		internal static AuthorizeResult Show(string clientId, string redirectUri)
		{
			using (var dialog = new AuthorizeDialog(clientId, redirectUri))
			{
				dialog.ShowDialog();
				return dialog.result;
			}
		}

		static Uri BuildAuthorizationUrl(string clientId, string redirectUri, string oAuth2State)
		{
			var queryString = HttpUtility.ParseQueryString("");
			queryString["client_id"] = clientId;
			queryString["response_type"] = "token";
			queryString["scope"] = "twf.organisationUser twf.organisation";
			queryString["redirect_uri"] = redirectUri;
			queryString["state"] = oAuth2State;

			// nonce is mandatory for all authorization requests, while it should only be mandatory in case of OpenID Connect.
			queryString["nonce"] = Guid.NewGuid().ToString();
			
			return new Uri($"{AuthorizationEndPoint}?{queryString}");
		}

		void webView_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
		{
			if (!e.Uri.StartsWith(redirectUri))
				return;

			e.Cancel = true;

			var uri = new Uri(e.Uri);
			var fragment = uri.Fragment.Substring(1);
			var parameters = HttpUtility.ParseQueryString(fragment);

			var error = parameters["error"];
			if (!string.IsNullOrEmpty(error))
			{
				result = AuthorizeResult.Error(error);
				Close();
				return;
			}

			var state = parameters["state"];
			if (state != oAuth2State)
			{
				result = AuthorizeResult.Error("State parameter mismatch.");
				Close();
				return;
			}

			var accessToken = parameters["access_token"];
			var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
			var accessTokenHeader = tokenHandler.ReadJwtToken(accessToken);

			result = AuthorizeResult.Authenticated(
				accessToken,
				accessTokenHeader.Claims.Single(c => c.Type == "twf.organisationUserCode").Value,
				accessTokenHeader.Claims.Single(c => c.Type == "twf.organisationCode").Value,
				accessTokenHeader.Claims.Single(c => c.Type == "twf.clusterUrl").Value, 
				state);

			Close();
		}
	}
}
