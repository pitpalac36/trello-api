using Newtonsoft.Json;
using RestSharp;
using trello.Helpers.models;

namespace trello.Helpers
{
    public static class Client
    {
        public static void DeleteBoards(IRestClient client)
        {
            var request = new RestRequest(Constants.GetBoards, Method.GET);

            var response = client.Execute(request);

            var content = JsonConvert.DeserializeObject<Board[]>(response.Content);
            foreach (var each in content)
            {
                var deleteBoard = string.Format(Constants.DeleteBoard, each.Id);
                client.Execute(new RestRequest(deleteBoard, Method.DELETE));
            }
        }

        public static IRestResponse CreateBoard(IRestClient client)
        {
            var toBeAdded = new Board().MakeFake();
            var createUrl = string.Format(Constants.CreateBoard, toBeAdded.Name, toBeAdded.Desc, toBeAdded.Closed);

            var request = new RestRequest(createUrl, Method.POST);
            request.AddJsonBody(toBeAdded);

            return client.Execute(request);
        }


        public static IRestResponse GetBoards(IRestClient client)
        {
            var request = new RestRequest(Constants.GetBoards, Method.GET);

            return client.Execute(request);
        }


    }
}
