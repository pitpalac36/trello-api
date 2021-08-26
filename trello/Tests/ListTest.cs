using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using trello.Helpers;
using trello.Helpers.clients;
using trello.Helpers.models;

namespace trello.Tests
{
    [TestClass]
    public class ListTest
    {
        public static Card _card;
        public static List<BoardList> _lists = new List<BoardList>();
        public static string _boardId;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var client = new RestClient(Constants.BaseUrl);

            var response = BoardClient.CreateBoard(client);
            _boardId = JsonConvert.DeserializeObject<Board>(response.Content).Id;

            response = ListClient.CreateList(client, _boardId, Faker.Name.FullName());
            var listId = JsonConvert.DeserializeObject<Board>(response.Content).Id;

            _card = new Card().MakeFake();
            _card.IdList = listId;
            _card.IdBoard = _boardId;

            response = CardClient.CreateCard(client, _card);
            var cardId = JsonConvert.DeserializeObject<Card>(response.Content).Id;

            _card.Id = cardId;

            response = ListClient.GetLists(client, _boardId);
            // lists will contain: default list, todo list, doing list and done list in this order
            _lists.AddRange(JsonConvert.DeserializeObject<BoardList[]>(response.Content));
        }

        [TestMethod]
        public void MoveCardToTODOList()
        {

        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var client = new RestClient(Constants.BaseUrl);
            BoardClient.DeleteBoard(client, _boardId);
        }

    }
}
