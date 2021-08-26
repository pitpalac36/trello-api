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

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var client = new RestClient(Constants.BaseUrl);

            BoardClient.DeleteBoards(client);

            var response = BoardClient.CreateBoard(client);
            var boardId = JsonConvert.DeserializeObject<Board>(response.Content).Id;

            response = ListClient.CreateList(client, boardId, Faker.Name.FullName());
            var listId = JsonConvert.DeserializeObject<Board>(response.Content).Id;

            _card = new Card().MakeFake();
            _card.IdList = listId;
            _card.IdBoard = boardId;

            response = CardClient.CreateCard(client, _card);
            var cardId = JsonConvert.DeserializeObject<Card>(response.Content).Id;

            _card.Id = cardId;

            response = ListClient.GetLists(client, boardId);
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
            BoardClient.DeleteBoards(client);
        }

    }
}
