using NsTestFrameworkUI.Helpers;
using NsTestFrameworkUI.Pages;
using OpenQA.Selenium;

namespace trello.Pages
{
    public class BoardPage
    {
        #region Selectors
        private readonly By _boardNamePane = By.CssSelector(".js-board-editing-target.board-header-btn-text");
        #endregion

        public string GetBoardNameFromPane()
        {
            WaitHelpers.WaitUntilElementIsVisible(_boardNamePane);
            var name = _boardNamePane.GetText();
            return name;
        }
    }
}
