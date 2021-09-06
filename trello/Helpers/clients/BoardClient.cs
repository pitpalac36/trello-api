using Newtonsoft.Json;
using RestSharp;
using trello.Helpers.models;

namespace trello.Helpers
{
    public static class BoardClient
    {
        private static readonly string Key = ApiResource.Key;
        private static readonly string Token = ApiResource.Token;

        public static void DeleteBoards(IRestClient client)
        {
            var response = GetBoards(client);
            var content = JsonConvert.DeserializeObject<Board[]>(response.Content);
            foreach (var each in content)
            {
                var deleteBoard = string.Format(Constants.DeleteBoard, each.Id, Key, Token);

                client.Execute(new RestRequest(deleteBoard, Method.DELETE));
            }
        }

        public static IRestResponse DeleteBoard(IRestClient client, string id)
        {
            var finalUrl = string.Format(Constants.DeleteBoard, id, Key, Token);
            return client.Execute(new RestRequest(finalUrl, Method.DELETE));
        }

        public static IRestResponse CreateBoard(IRestClient client)
        {
            var toBeAdded = new Board().MakeFake();
            var createUrl = string.Format(Constants.CreateBoard, toBeAdded.Name, toBeAdded.Desc, toBeAdded.Closed, Key, Token);

            var request = new RestRequest(createUrl, Method.POST);
            request.AddJsonBody(toBeAdded);

            return client.Execute(request);
        }

        public static IRestResponse CreateSimpleBoard(IRestClient client)
        {
            var toBeAdded = new Board().MakeFake();
            var createUrl = string.Format(Constants.CreateSimpleBoard, toBeAdded.Name, Key, Token);

            var request = new RestRequest(createUrl, Method.POST);
            request.AddJsonBody(toBeAdded);

            return client.Execute(request);
        }

        public static IRestResponse CreateBoard(IRestClient client, Board board)
        {
            var createUrl = string.Format(Constants.CreateBoard, board.Name, board.Desc, board.Closed, Key, Token);

            var request = new RestRequest(createUrl, Method.POST);
            request.AddJsonBody(board);

            return client.Execute(request);
        }

        public static IRestResponse GetBoards(IRestClient client)
        {
            var getUrl = string.Format(Constants.GetBoards, Constants.Username, Key, Token);
            return client.Execute(new RestRequest(getUrl, Method.GET));
        }

        public static IRestResponse UpdateBoard(IRestClient client, UpdateBoard dto)
        {
            var finalUrl = string.Format(Constants.UpdateBoard, dto.Id, Key, Token, dto.Name);

            var request = new RestRequest(finalUrl, Method.PUT);
            request.AddJsonBody(dto);

            return client.Execute(request);
        }
    }
}
