using NsTestFrameworkUI.Helpers;
using NsTestFrameworkUI.Pages;
using OpenQA.Selenium;
using trello.Helpers.models;

namespace trello.Pages
{
    public class HomePage
    {
        #region Selectors
        private readonly By _createMenuButton = By.XPath("//button[@data-test-id='header-create-menu-button']");
        private readonly By _createBoardOption = By.XPath("//button[@data-test-id='header-create-board-button']");
        private readonly By _boardTitleField = By.XPath("//input[@data-test-id='create-board-title-input']");
        private readonly By _createBoardButton = By.XPath("//button[@data-test-id='create-board-submit-button']");
        private readonly By _firstBoardFromWorkspacePane = By.CssSelector(".board-tile-details.is-badged");
        #endregion

        public void CreateBoard(string name)
        {
            _createMenuButton.WaitUntilElementIsVisible();
            _createMenuButton.ActionClick();
            _createBoardOption.WaitForElementToBeClickable();
            _createBoardOption.ActionClick();
            _boardTitleField.WaitUntilElementIsVisible();
            _boardTitleField.ActionSendKeys(name);
            _createBoardButton.ActionClick();
            WaitHelpers.WaitForDocumentReadyState();
        }

        public void NavigateToBoard(Board board)
        {
            _firstBoardFromWorkspacePane.WaitUntilElementIsVisible();
            var boards = _firstBoardFromWorkspacePane.GetElements();
            foreach (var each in boards)
            {
                if (each.Text.Equals(board.Name))
                {
                    each.Click();
                }
            }
        }
    }
}
