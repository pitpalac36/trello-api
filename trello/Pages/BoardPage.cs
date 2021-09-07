﻿using NsTestFrameworkUI.Helpers;
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
        private readonly By _firstCardPane = By.CssSelector(".js-list-card");
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


    }
}
