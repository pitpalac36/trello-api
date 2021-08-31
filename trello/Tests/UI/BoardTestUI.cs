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
    public class BoardTestUI : BaseTestUI
    {
        public volatile static IList<string> _currentBoards = new List<string>();

        /*[ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var response = BoardClient.CreateBoard(_client);
            _currentBoards.Add(JsonConvert.DeserializeObject<Board>(response.Content)); // because i need id
        }*/

        [TestMethod]
        public void CreateBoardTest()
        {
            var name = Faker.Name.FullName();

            MyPages.LoginPage.Login(Constants.credentials.Item1, Constants.credentials.Item2);
            MyPages.HomePage.CreateBoard(name);

            _currentBoards.Add(name);

            MyPages.BoardPage.GetBoardNameFromPane().Should().Be(name);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var response = BoardClient.GetBoards(_client);

            var list = JsonConvert.DeserializeObject<Board[]>(response.Content);
            foreach(var each in list)
            {
                if ( _currentBoards.Any(x => x.Equals(each.Name)))
                {
                    BoardClient.DeleteBoard(_client, each.Id);
                }
            }
        }
    }
}
