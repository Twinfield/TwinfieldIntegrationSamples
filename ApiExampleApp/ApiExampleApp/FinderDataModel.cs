using ApiExampleApp.Finder;

namespace ApiExampleApp;

class FinderDataModel
{
	public FinderData Data { get; set; }
	public MessageOfErrorCodes[] Errors { get; set; }
}
