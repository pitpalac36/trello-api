using FluentAssertions;
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
    public class CardTest
    {
        private volatile static IList<Card> _cards = new List<Card>();

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var client = new RestClient(Constants.BaseUrl);

            BoardClient.DeleteBoards(client);

            var response = BoardClient.CreateBoard(client);
            var boardId = JsonConvert.DeserializeObject<Board>(response.Content).Id;

            response = ListClient.CreateList(client, boardId, Faker.Name.FullName());
            var listId = JsonConvert.DeserializeObject<Board>(response.Content).Id;

            var card = new Card().MakeFake();
            card.IdList = listId;
            card.IdBoard = boardId;

            response = CardClient.CreateCard(client, card);
            var cardId = JsonConvert.DeserializeObject<Card>(response.Content).Id;

            card.Id = cardId;
            _cards.Add(card);
        }

        [TestMethod]
        public void CreateCardTest()
        {
            var client = new RestClient(Constants.BaseUrl);

            var card = new Card().MakeFake();
            card.IdList = _cards[0].IdList;
            card.IdBoard = _cards[0].IdBoard;

            var response = CardClient.CreateCard(client, card);
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
            var client = new RestClient(Constants.BaseUrl);

            var response = CardClient.GetCard(client, _cards[0].Id);
            var responseCard = JsonConvert.DeserializeObject<Card>(response.Content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responseCard.IdBoard.Should().Be(_cards[0].IdBoard);
            responseCard.IdList.Should().Be(_cards[0].IdList);
            responseCard.Should().Be(_cards[0]);
        }

        [TestMethod]
        public void UpdateCardNameTest()
        {
            var client = new RestClient(Constants.BaseUrl);
            var card = _cards[_cards.Count - 1];
            card.Name = Faker.Name.First();

            var response = CardClient.UpdateCardName(client, card);
            var responseCard = JsonConvert.DeserializeObject<Card>(response.Content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responseCard.Name.Should().Be(card.Name);
        }

        [TestMethod]
        public void UpdateCardStatus()
        {
            var client = new RestClient(Constants.BaseUrl);
            var card = _cards[_cards.Count - 1];
            card.Closed = !card.Closed;

            var response = CardClient.UpdateCardStatus(client, card);
            var responseCard = JsonConvert.DeserializeObject<Card>(response.Content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responseCard.Closed.Should().Be(card.Closed);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var client = new RestClient(Constants.BaseUrl);
            BoardClient.DeleteBoards(client);
        }
    }
}
