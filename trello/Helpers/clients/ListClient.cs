using RestSharp;

namespace trello.Helpers.clients
{
    public static class ListClient
    {
        public static IRestResponse CreateList(IRestClient client, string boardId, string listName)
        {
            var finalUrl = string.Format(Constants.CreateList, boardId, listName);
            var request = new RestRequest(finalUrl, Method.POST);
            return client.Execute(request);
        }
    }
}
