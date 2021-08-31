using Microsoft.VisualStudio.TestTools.UnitTesting;
using NsTestFrameworkUI.Helpers;
using RestSharp;
using DriverOptions = NsTestFrameworkUI.Helpers.DriverOptions;

namespace trello.Helpers
{
    public class BaseTestUI
    {
        protected static IRestClient _client = new RestClient(Constants.BaseUrl);

        [TestInitialize]
        public void Before()
        {
            Browser.InitializeDriver(new DriverOptions
            {
                IsHeadless = false
            });
            Browser.GoTo(Constants.LoginUrl);
        }

        [TestCleanup]
        public void After()
        {
            Browser.Cleanup();
        }
    }
}
