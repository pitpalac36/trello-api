using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using trello.Helpers;
using trello.Helpers.models;

namespace trello
{
    [TestClass]
    public class BoardTest
    {
        public static IList<Board> _currentBoards = new List<Board>();

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var client = new RestClient(Constants.BaseUrl);
            var request = new RestRequest(Constants.GetBoards, Method.GET);

            var response = client.Execute(request);
            var content = JsonConvert.DeserializeObject<Board[]>(response.Content);
            foreach (var each in content)
            {
                var deleteBoard = string.Format(Constants.DeleteBoard, each.Id);
                client.Execute(new RestRequest(deleteBoard, Method.DELETE));
            }
        }

        public static IEnumerable<object[]> Data
        {
            get
            {
                yield return new object[] { new Board { Name = Faker.Name.FullName(), Desc = Faker.Lorem.Sentence(), Closed = false }, System.Net.HttpStatusCode.OK };
                yield return new object[] { new Board { Name = Faker.Name.FullName(), Desc = Faker.Lorem.Sentence(), Closed = false }, System.Net.HttpStatusCode.OK };
                yield return new object[] { new Board { Name = Faker.Name.FullName(), Desc = Faker.Lorem.Sentence(), Closed = false }, System.Net.HttpStatusCode.OK };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(Data), DynamicDataSourceType.Property)]
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

            _currentBoards.Add(board);
        }

        [TestMethod]
        public void GetBoardsTest()
        {
            var client = new RestClient(Constants.BaseUrl);
            var request = new RestRequest(Constants.GetBoards, Method.POST);

            var response = client.Execute(request);
            try
            {
                var content = JsonConvert.DeserializeObject<Board[]>(response.Content);
                content.Length.Should().Be(_currentBoards.Count);
                content.Should().BeEquivalentTo(_currentBoards);
            }
            catch (JsonReaderException)
            {
                // no data to be deleted
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var client = new RestClient(Constants.BaseUrl);
            var request = new RestRequest(Constants.GetBoards, Method.GET);

            var response = client.Execute(request);
            var content = JsonConvert.DeserializeObject<Board[]>(response.Content);
            foreach (var each in content)
            {
                var deleteBoard = string.Format(Constants.DeleteBoard, each.Id);
                client.Execute(new RestRequest(deleteBoard, Method.DELETE));
            }
        }
    }
}
