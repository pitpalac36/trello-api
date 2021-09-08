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
        private readonly By _listsPanes = By.CssSelector("h2+textarea:last-of-type");
        private readonly By _menuButton = By.CssSelector(".board-header-btn.mod-show-menu");
        private readonly By _aboutBoardButton = By.CssSelector(".board-menu-navigation-item-link.js-about-this-board");
        private readonly By _boardDescArea = By.CssSelector(".description-fake-text-area");
        private readonly By _boardDescTextArea = By.CssSelector("textarea.field");
        private readonly By _confirmSaveBoardButton = By.CssSelector("input.confirm");
        private readonly By _boardDescLabel = By.CssSelector(".js-desc");
        private readonly By _addCardButton = By.CssSelector(".js-add-a-card");
        private readonly By _cardTitleField = By.CssSelector(".list-card-composer-textarea");
        private readonly By _addCardSubmitButton = By.CssSelector(".confirm.js-add-card");
        private readonly By _firstCardPane = By.CssSelector(".list-card.js-member-droppable.ui-droppable");
        private readonly By _checklistMenu = By.CssSelector(".js-add-checklist-menu");
        private readonly By _checklistNameField = By.CssSelector("input#id-checklist");
        private readonly By _addChecklistButton = By.CssSelector(".js-add-checklist");
        private readonly By _checklistNewItemField = By.CssSelector("textarea.checklist-new-item-text");
        private readonly By _confirmNewChecklistItemButton = By.CssSelector(".confirm.js-add-checklist-item");
        private readonly By _dialogCloseButton = By.CssSelector(".dialog-close-button");
        private readonly By _checklistIcon = By.CssSelector(".icon-checklist");
        private readonly By _checkItemsBadgeText = By.CssSelector(".js-checkitems-badge-text");
        private readonly string _genericItemCheckbox = ".checklist-item:nth-child({0})>.checklist-item-checkbox";
        #endregion

        public string GetBoardNameFromPane()
        {
            _boardNamePane.WaitUntilElementIsVisible();
            var name = _boardNamePane.GetText();
            return name;
        }

        public void AddListToBoard(string title)
        {
            _addListPane.WaitUntilElementIsVisible();
            _addListPane.ActionClick();
            _listNameInput.WaitUntilElementIsVisible();
            _listNameInput.ActionSendKeys(title);
            _addListButton.ActionClick();
        }

        public string GetTitleOfLastList()
        {
            var lists = _listsPanes.GetElements();
            var title = lists[lists.Count - 1].Text;
            return title;
        }

        public void OpenMenu()
        {
            _menuButton.ActionClick();
        }

        public void AddDescription(string desc)
        {
            _aboutBoardButton.ActionClick();
            _boardDescArea.WaitForElementToBeClickable();
            _boardDescArea.ActionClick();
            _boardDescTextArea.ActionSendKeys(desc);
            _confirmSaveBoardButton.ActionClick();
        }

        public string GetDescription()
        {
            return _boardDescLabel.GetText();
        }

        public void AddCard(string name)
        {
            _addCardButton.ActionClick();
            _cardTitleField.ActionSendKeys(name);
            _addCardSubmitButton.ActionClick();
        }

        public void AddChecklist(string name)
        {
            _checklistMenu.WaitForElementToBeClickable();
            _checklistMenu.ActionClick();
            _checklistNameField.ClearField();
            _checklistNameField.ActionSendKeys(name);
            _addChecklistButton.ActionClick();
        }

        public void AddItemsChecklistMenu(string[] items)
        {
            foreach (var each in items)
            {
                _checklistNewItemField.ActionSendKeys(each);
                _confirmNewChecklistItemButton.ActionClick();
            }
        }

        public bool IsChecklistBadgePresent()
        {
            return _checklistIcon.IsElementPresent();
        }

        public string GetChecklistBadgeText()
        {
            return _checkItemsBadgeText.GetText();
        }

        public void ClickOnCard()
        {
            _firstCardPane.ActionClick();
        }

        public void CloseDialog()
        {
            _dialogCloseButton.ActionClick();
            WaitHelpers.WaitForDocumentReadyState();
        }

        public void CheckItem(int indexInChecklist)
        {
            var itemCheckbox = By.CssSelector(string.Format(_genericItemCheckbox, indexInChecklist));
            itemCheckbox.ActionClick();
        }

        public void Wait()
        {
            WaitHelpers.ExplicitWait(1000);
        }
    }
}
