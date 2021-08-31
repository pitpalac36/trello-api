using NsTestFrameworkUI.Helpers;
using NsTestFrameworkUI.Pages;
using OpenQA.Selenium;

namespace trello.Pages
{
    public class HomePage
    {
        #region Selectors
        private readonly By _createMenuButton = By.XPath("//button[@data-test-id='header-create-menu-button']");
        private readonly By _createBoardOption = By.XPath("//button[@data-test-id='header-create-board-button']");
        private readonly By _boardTitleField = By.XPath("//input[@data-test-id='create-board-title-input']");
        private readonly By _createBoardButton = By.XPath("//button[@data-test-id='create-board-submit-button']");
        private readonly By _firstBoardFromWorkspacePane = By.CssSelector(".boards-page-section-header-name + div > div + div > ul > li > a");
        #endregion

        public void CreateBoard(string name)
        {
            WaitHelpers.WaitUntilElementIsVisible(_createMenuButton);
            _createMenuButton.ActionClick();
            WaitHelpers.WaitForElementToBeClickable(_createBoardOption);
            _createBoardOption.ActionClick();
            WaitHelpers.WaitUntilElementIsVisible(_boardTitleField);
            _boardTitleField.ActionSendKeys(name);
            _createBoardButton.ActionClick();
            WaitHelpers.WaitForDocumentReadyState();
        }

        public void NavigateToBoard()
        {
            WaitHelpers.WaitUntilElementIsVisible(_firstBoardFromWorkspacePane);
            _firstBoardFromWorkspacePane.ActionClick();
        }
    }
}
