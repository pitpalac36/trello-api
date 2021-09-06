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
    public class CardTest : BaseTestAPI
    {
        private static volatile IList<Card> _cards = new List<Card>();
        private static string _boardId;
        

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var response = BoardClient.CreateBoard(_client);
            _boardId = JsonConvert.DeserializeObject<Board>(response.Content).Id;

            response = ListClient.CreateList(_client, _boardId, Faker.Name.FullName());
            var listId = JsonConvert.DeserializeObject<Board>(response.Content).Id;

            var card = new Card().MakeFake();
            card.IdList = listId;
            card.IdBoard = _boardId;

            response = CardClient.CreateCard(_client, card);
            var cardId = JsonConvert.DeserializeObject<Card>(response.Content).Id;

            card.Id = cardId;
            _cards.Add(card);
        }

        [TestMethod]
        public void CreateCardTest()
        {
            var card = new Card().MakeFake();
            card.IdList = _cards[0].IdList;
            card.IdBoard = _cards[0].IdBoard;

            var response = CardClient.CreateCard(_client, card);
            var responseCard = JsonConvert.DeserializeObject<Card>(response.Content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responseCard.IdBoard.Should().Be(card.IdBoard);
            responseCard.IdList.Should().Be(card.IdList);
            responseCard.Should().Be(card);

            card.Id = responseCard.Id;
            _cards.Add(card);
        }

        [TestMethod]
        public void GetCardTest()
        {
            var response = CardClient.GetCard(_client, _cards[0].Id);
            var responseCard = JsonConvert.DeserializeObject<Card>(response.Content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responseCard.IdBoard.Should().Be(_cards[0].IdBoard);
            responseCard.IdList.Should().Be(_cards[0].IdList);
            responseCard.Should().Be(_cards[0]);
        }

        [TestMethod]
        public void UpdateCardNameTest()
        {
            var card = _cards[_cards.Count - 1];
            card.Name = Faker.Name.First();

            var response = CardClient.UpdateCardName(_client, card);
            var responseCard = JsonConvert.DeserializeObject<Card>(response.Content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responseCard.Name.Should().Be(card.Name);
        }

        [TestMethod]
        public void UpdateCardStatus()
        {
            var card = _cards[_cards.Count - 1];
            card.Closed = !card.Closed;

            var response = CardClient.UpdateCardStatus(_client, card);
            var responseCard = JsonConvert.DeserializeObject<Card>(response.Content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responseCard.Closed.Should().Be(card.Closed);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            BoardClient.DeleteBoard(_client, _boardId);
        }
    }
}
