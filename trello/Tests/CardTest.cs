using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using trello.Helpers;
using trello.Helpers.clients;
using trello.Helpers.models;

namespace trello.Tests
{
    [TestClass]
    public class CardTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var client = new RestClient(Constants.BaseUrl);
            BoardClient.DeleteBoards(client);
            var response = BoardClient.CreateBoard(client);
            var board = JsonConvert.DeserializeObject<Board>(response.Content);
            ListClient.CreateList(client, board.Id, Faker.Name.FullName());
            //CardClient.CreateCard(...);
        }

        // TODO: CardClient class
        //       Tests for Create, Get, Update, Delete Card
    }
}
