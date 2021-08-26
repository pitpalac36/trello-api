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

        public static IRestResponse GetCard(IRestClient client, string id)
        {
            var getUrl = string.Format(Constants.GetCard, id, Key, Token);

            return client.Execute(new RestRequest(getUrl, Method.GET));
        }

        public static IRestResponse UpdateCardName(IRestClient client, Card card)
        {
            var updateUrl = string.Format(Constants.UpdateCardName, card.Id, Key, Token, card.Name);

            var request = new RestRequest(updateUrl, Method.PUT);

            return client.Execute(request);
        }

        public static IRestResponse UpdateCardStatus(IRestClient client, Card card)
        {
            var updateUrl = string.Format(Constants.UpdateCardStatus, card.Id, Key, Token, card.Closed.ToString().ToLower());

            var request = new RestRequest(updateUrl, Method.PUT);

            return client.Execute(request);
        }

    }
}
