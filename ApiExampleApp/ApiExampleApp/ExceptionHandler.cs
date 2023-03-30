using System.Net;
using static System.Console;

namespace ApiExampleApp;

static class ExceptionHandler
{
	public static void HandleWebException(this WebException webException)
	{
		if (webException == null) return;

		WriteLine("Error occurred while processing the xml request.");
		var statusCode = ((HttpWebResponse)webException.Response)?.StatusCode;
		WriteLine($"Http status code : {statusCode}");

		if (statusCode != HttpStatusCode.Forbidden &&
		    statusCode != HttpStatusCode.Unauthorized) return;
		var statusDescription = ((HttpWebResponse)webException.Response)?.StatusDescription;

		if (string.IsNullOrWhiteSpace(statusDescription)) return;
		if (statusDescription.Contains(":"))
		{
			var descriptionDetails = statusDescription.Split(':');
			if (descriptionDetails.Length <= 1) return;
			WriteLine($"Error code : {descriptionDetails[0].Trim()}");
			WriteLine($"Error description : {descriptionDetails[1].Trim()}");
		}
		else
			WriteLine($"Error description : {statusDescription}");
	}
}
