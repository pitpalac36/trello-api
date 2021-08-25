using RestSharp;

namespace trello.Helpers.clients
{
    public static class ListClient
    {
        private static readonly string Key = ApiResource.Key;
        private static readonly string Token = ApiResource.Token;

        public static IRestResponse CreateList(IRestClient client, string boardId, string listName)
        {
            var finalUrl = string.Format(Constants.CreateList, boardId, Key, Token, listName);
            var request = new RestRequest(finalUrl, Method.POST);
            return client.Execute(request);
        }
    }
}
