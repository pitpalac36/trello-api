using RestSharp;
using trello.Helpers.models;

namespace trello.Helpers.clients
{
    public static class CardClient
    {
        private static readonly string Key = ApiResource.Key;
        private static readonly string Token = ApiResource.Token;

        public static IRestResponse CreateCard(IRestClient client, Card card)
        {
            var createUrl = string.Format(Constants.CreateCard, Key, Token, card.IdList, card.Name, card.Desc);

            var request = new RestRequest(createUrl, Method.POST);

            return client.Execute(request);
        }

    }
}
