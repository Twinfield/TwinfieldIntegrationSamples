using System.Net;
using System.ServiceModel;
using TwinfieldApi.Services;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Bookkeeping;

public class BookkeepingService
{
	readonly ProcessXmlService processXml;

	public BookkeepingService()
	{
		processXml = new ProcessXmlService();
	}

	public Transaction ReadTransaction(string daybook, decimal transactionNumber, string clusterUrl, string accessToken, string companyCode)
	{
		Transaction transaction = null;
		var command = new ReadTransactionCommand
		{
			Office = companyCode,
			Daybook = daybook,
			TransactionNumber = transactionNumber
		};
		try
		{
			var response = processXml.Process(command.ToXml(), clusterUrl, accessToken, companyCode);
			transaction = Transaction.FromXml(response);
		}
		catch (FaultException ex)
		{
			Console.WriteLine("Error occurred while processing the xml request.");
			Console.WriteLine(ex.Message);
		}
		catch (WebException ex)
		{
			ex.HandleWebException();
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error occurred while processing the xml request.");
			Console.WriteLine(ex.Message);
		}

		return transaction;
	}
}