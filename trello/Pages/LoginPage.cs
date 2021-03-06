using NsTestFrameworkUI.Helpers;
using NsTestFrameworkUI.Pages;
using OpenQA.Selenium;

namespace trello.Pages
{
    public class LoginPage
    {
        #region Selectors
        private readonly By _emailField = By.CssSelector("input#user");
        private readonly By _atlasianLoginButton = By.CssSelector("input#login");
        private readonly By _passwordField = By.CssSelector("input#password");
        private readonly By _finalLoginButton = By.CssSelector("#login-submit");
        #endregion

        public void Login(string username, string password)
        {
            _emailField.ActionSendKeys(username);
            _atlasianLoginButton.WaitUntilElementIsVisible();
            _atlasianLoginButton.ActionClick();
            _finalLoginButton.WaitForElementToBeClickable();
            _passwordField.ActionSendKeys(password);
            _finalLoginButton.ActionClick();
        }
    }
}
