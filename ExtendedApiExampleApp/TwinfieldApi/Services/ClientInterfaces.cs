using TwinfieldBankBookService;
using TwinfieldFinderService;
using TwinfieldProcessXmlService;

namespace TwinfieldApi.Services;

public interface IBankBookServiceClient
{
	ProcessResponse Process(CommandRequest commandRequest);
	QueryResponse Query(QueryRequest request);
}

public interface IFinderSoapClient
{
	SearchResponse Search(SearchRequest options);
}

public interface IProcessXmlSoapClient
{
	ProcessXmlStringResponse ProcessXmlString(ProcessXmlStringRequest xmlRequest);
	ProcessXmlDocumentResponse ProcessXmlDocument(ProcessXmlDocumentRequest xmlRequest);
	ProcessXmlCompressedResponse ProcessXmlCompressed(ProcessXmlCompressedRequest xmlRequest);
}
