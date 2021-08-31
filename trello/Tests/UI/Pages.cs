using NsTestFrameworkUI.Helpers;
using NsTestFrameworkUI.Pages;
using trello.Pages;

namespace trello.UI
{
    public static class Pages
    {
        public static HomePage HomePage => PageHelpers.InitPage(new HomePage());
        public static LoginPage LoginPage => PageHelpers.InitPage(new LoginPage());
        public static BoardPage BoardPage => PageHelpers.InitPage(new BoardPage());

        public static string GetUrl()
        {
            return Browser.WebDriver.Url;
        }
    }
}
