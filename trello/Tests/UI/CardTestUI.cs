using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using trello.Helpers;
using trello.Helpers.clients;
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

        [TestMethod]
        public void AddCardWithChecklistTest()
        {
            MyPages.LoginPage.Login(Constants.credentials.Item1, Constants.credentials.Item2);
            MyPages.HomePage.NavigateToBoard(_currentBoards.First(x => x.Name.Equals(_currentBoards[0].Name)));

            var name = Faker.Name.First();
            MyPages.BoardPage.AddCard(name);

            MyPages.BoardPage.AddChecklist();
            MyPages.BoardPage.AddItemsChecklistMenu();
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
