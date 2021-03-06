using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using trello.Helpers;
using trello.Helpers.models;
using MyPages = trello.UI.Pages;

namespace trello.Tests.UI
{
    [TestClass]
    public class CardTestUI : BaseTestUI
    {
        public static volatile IList<Board> _currentBoards = new List<Board>();
        public static volatile Card card;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var response = BoardClient.CreateBoard(_client);
            _currentBoards.Add(JsonConvert.DeserializeObject<Board>(response.Content));
        }

        [DataTestMethod]
        [DataRow(3, "1", "2", "3")]
        public void AddCardWithChecklistTest(int checklistSize, params string[] items)
        {
            MyPages.LoginPage.Login(Constants.credentials.Item1, Constants.credentials.Item2);
            MyPages.HomePage.NavigateToBoard(_currentBoards.First(x => x.Name.Equals(_currentBoards[0].Name)));

            var cardName = Faker.Name.First();
            var checklistName = Faker.Name.First();
            MyPages.BoardPage.AddCard(cardName);

            MyPages.BoardPage.ClickOnCard();
            MyPages.BoardPage.AddChecklist(checklistName);
            MyPages.BoardPage.AddItemsChecklistMenu(items);
            MyPages.BoardPage.CloseDialog();

            MyPages.BoardPage.IsChecklistBadgePresent().Should().BeTrue();
            MyPages.BoardPage.GetChecklistBadgeText().Should().Be(string.Format("0/{0}", items.Count()));

            for (var i = 1; i <= items.Count(); i++)
            {
                MyPages.BoardPage.ClickOnCard();
                MyPages.BoardPage.CheckItem(i);
                MyPages.BoardPage.CloseDialog();
                MyPages.BoardPage.Wait();

                MyPages.BoardPage.GetChecklistBadgeText().Should().Be(string.Format("{0}/{1}", i, items.Count()));
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var response = BoardClient.GetBoards(_client);

            var list = JsonConvert.DeserializeObject<Board[]>(response.Content);
            foreach (var each in list)
            {
                if (_currentBoards.Any(x => x.Name.Equals(each.Name)))
                {
                    BoardClient.DeleteBoard(_client, each.Id);
                }
            }
            BoardClient.DeleteBoard(_client, _currentBoards[0].Id);
        }
    }
}
