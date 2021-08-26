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
            return client.Execute(new RestRequest(finalUrl, Method.POST));
        }

        public static IRestResponse GetLists(IRestClient client, string boardId)
        {
            var getUrl = string.Format(Constants.GetLists, boardId, Key, Token);
            return client.Execute(new RestRequest(getUrl, Method.GET));
        }

        public static IRestResponse MoveCardToList(IRestClient client, string cardId, string listId)
        {
            var updateUrl = string.Format(Constants.UpdateCardList, cardId, Key, Token, listId);
            return client.Execute(new RestRequest(updateUrl, Method.PUT));
        }
    }
}
