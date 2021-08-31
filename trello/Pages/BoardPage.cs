using NsTestFrameworkUI.Helpers;
using NsTestFrameworkUI.Pages;
using OpenQA.Selenium;

namespace trello.Pages
{
    public class BoardPage
    {
        #region Selectors
        private readonly By _boardNamePane = By.CssSelector(".js-board-editing-target.board-header-btn-text");
        private readonly By _addListPane = By.CssSelector(".open-add-list");
        private readonly By _listNameInput = By.CssSelector(".list-name-input");
        private readonly By _addListButton = By.CssSelector(".mod-list-add-button");
        private readonly By _listsPanes = By.CssSelector("div.list.js-list-content h2");
        #endregion

        public string GetBoardNameFromPane()
        {
            WaitHelpers.WaitUntilElementIsVisible(_boardNamePane);
            var name = _boardNamePane.GetText();
            return name;
        }

        public void AddListToBoard(string title)
        {
            WaitHelpers.WaitUntilElementIsVisible(_addListPane);
            _addListPane.ActionClick();
            WaitHelpers.WaitUntilElementIsVisible(_listNameInput);
            _listNameInput.ActionSendKeys(title);
            _addListButton.ActionClick();
        }

        public string GetTitleOfLastList()
        {
            var lists = _listsPanes.GetElements();
            return lists[lists.Count - 1].Text;
        }
    }
}
