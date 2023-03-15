using TwinfieldBankBookService;

namespace TwinfieldApi.Bankbooks;

public class Bankbook
{
	public string Code { get; set; }
	public string Name { get; set; }
	public string AccountNumber { get; set; }
	public string Iban { get; set; }

	public static Bankbook FromQueryResult(string bankCode, QueryResult queryResult)
	{
		var result = queryResult as GetBankBookResult;
		if (result == null)
			return null;

		return new Bankbook
		{
			Code = bankCode.ToUpper(),
			Name = result.Name,
			AccountNumber = result.BankAccount.Number,
			Iban = result.BankAccount.Iban
		};
	}
}