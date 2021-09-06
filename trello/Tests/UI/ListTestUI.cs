using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using trello.Helpers;
using trello.Helpers.models;

namespace trello.Tests.UI
{
    [TestClass]
    public class ListTestUI : BaseTestUI
    {
        public static volatile IList<Board> _currentBoards = new List<Board>();

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var response = BoardClient.CreateBoard(_client);
            _currentBoards.Add(JsonConvert.DeserializeObject<Board>(response.Content));
        }
    }
}
