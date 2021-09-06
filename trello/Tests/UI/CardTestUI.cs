using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using trello.Helpers;
using trello.Helpers.clients;
using trello.Helpers.models;

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

            response = ListClient.CreateList(_client, _currentBoards[0].Id, Faker.Name.FullName());
            var list = JsonConvert.DeserializeObject<BoardList>(response.Content);

            card = new Card();
            card.IdList = list.Id;
            response = CardClient.CreateCard(_client, card);
            card = JsonConvert.DeserializeObject<Card>(response.Content);
        }
    }
}
