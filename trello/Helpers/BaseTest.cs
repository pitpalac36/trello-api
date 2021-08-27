using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

[assembly: Parallelize(Workers = 8, Scope = ExecutionScope.ClassLevel)]
namespace trello.Helpers
{
    public class BaseTest
    {
        protected static IRestClient _client = new RestClient(Constants.BaseUrl);
    }
}
