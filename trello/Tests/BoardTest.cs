using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using trello.Helpers;
using trello.Helpers.models;

namespace trello
{
    [TestClass]
    public class BoardTest
    {
        public volatile static IList<Board> _currentBoards = new List<Board>();

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var client = new RestClient(Constants.BaseUrl);

            Client.DeleteBoards(client);

            var response = Client.CreateBoard(client);
            _currentBoards.Add(JsonConvert.DeserializeObject<Board>(response.Content)); // because i need id
        }

        public static IEnumerable<object[]> Boards
        {
            get
            {
                yield return new object[] { new Board { Name = Faker.Name.FullName(), Desc = Faker.Lorem.Sentence(), Closed = false }, System.Net.HttpStatusCode.OK };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(Boards), DynamicDataSourceType.Property)]
        public void CreateBoardTest(Board board, System.Net.HttpStatusCode status)
        {
            var client = new RestClient(Constants.BaseUrl);
            var finalUrl = string.Format(Constants.CreateBoard, board.Name, board.Desc, board.Closed);
            var request = new RestRequest(finalUrl, Method.POST);
            request.AddJsonBody(board);

            var response = client.Execute(request);
            var content = JsonConvert.DeserializeObject<Board>(response.Content);

            response.StatusCode.Should().Be(status);
            content.Should().Be(board);

            _currentBoards.Add(content);
        }

        [TestMethod]
        public void CreateListOnBoardTest()
        {
            var client = new RestClient(Constants.BaseUrl);
            var name = Faker.Name.FullName();
            var finalUrl = string.Format(Constants.CreateList, _currentBoards[0].Id, name);
            var request = new RestRequest(finalUrl, Method.POST);

            var response = client.Execute(request);
            var content = JsonConvert.DeserializeObject<BoardList>(response.Content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            content.IdBoard.Should().BeEquivalentTo(_currentBoards[0].Id);
            content.Name.Should().BeEquivalentTo(name);
        }

        [TestMethod]
        public void GetBoardsTest()
        {
            var client = new RestClient(Constants.BaseUrl);
            var request = new RestRequest(Constants.GetBoards, Method.GET);

            var response = client.Execute(request);
            var content = JsonConvert.DeserializeObject<Board[]>(response.Content);

            content.Length.Should().Be(_currentBoards.Count);
            content.Should().BeEquivalentTo(_currentBoards);
        }

        [TestMethod]
        public void UpdateBoardTest()
        {
            var client = new RestClient(Constants.BaseUrl);
            var dto = new UpdateBoardDTO { Id = _currentBoards[0].Id, Name = Faker.Name.FullName() };
            var finalUrl = string.Format(Constants.UpdateBoard, dto.Id, dto.Name);

            var request = new RestRequest(finalUrl, Method.PUT);
            request.AddJsonBody(dto);

            var response = client.Execute(request);
            var content = JsonConvert.DeserializeObject<Board>(response.Content);
            content.Name.Should().Be(dto.Name);
            _currentBoards[0].Name = dto.Name;
        }

        [TestMethod]
        public void DeleteBoardByIdTest()
        {
            var client = new RestClient(Constants.BaseUrl);
            var finalUrl = string.Format(Constants.DeleteBoard, _currentBoards[0].Id);

            var request = new RestRequest(finalUrl, Method.DELETE);

            var response = client.Execute(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            _currentBoards.Remove(_currentBoards[0]);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var client = new RestClient(Constants.BaseUrl);
            Client.DeleteBoards(client);
        }
    }
}
