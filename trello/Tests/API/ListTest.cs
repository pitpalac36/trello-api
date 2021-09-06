using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using trello.Helpers;
using trello.Helpers.clients;
using trello.Helpers.models;

namespace trello.Tests
{
    [TestClass]
    public class ListTest : BaseTestAPI
    {
        public static Card _card;
        public static volatile List<BoardList> _lists = new List<BoardList>();
        public static string _boardId;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var response = BoardClient.CreateBoard(_client);
            _boardId = JsonConvert.DeserializeObject<Board>(response.Content).Id;

            response = ListClient.CreateList(_client, _boardId, Faker.Name.FullName());
            var listId = JsonConvert.DeserializeObject<Board>(response.Content).Id;

            _card = new Card().MakeFake();
            _card.IdList = listId;
            _card.IdBoard = _boardId;

            response = CardClient.CreateCard(_client, _card);
            var cardId = JsonConvert.DeserializeObject<Card>(response.Content).Id;

            _card.Id = cardId;

            response = ListClient.GetLists(_client, _boardId);
            // lists will contain: default list, todo list, doing list and done list in this order
            _lists.AddRange(JsonConvert.DeserializeObject<BoardList[]>(response.Content));
        }

        [DataTestMethod]
        [DataRow(1, "To Do")]
        [DataRow(2, "Doing")]
        [DataRow(3, "Done")]
        public void MoveCardToList(int listIndex, string listName)
        {
            var response = ListClient.MoveCardToList(_client, _card.Id, _lists[listIndex].Id);
            var card = JsonConvert.DeserializeObject<Card>(response.Content);

            response = ListClient.GetList(_client, _lists[listIndex].Id);
            var list = JsonConvert.DeserializeObject<BoardList>(response.Content);

            card.IdList.Should().Be(_lists[listIndex].Id);
            list.Name.Should().Be(listName);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            BoardClient.DeleteBoard(_client, _boardId);
        }

    }
}
