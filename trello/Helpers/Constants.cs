
using System;

namespace trello.Helpers
{
    public class Constants
    {
        public static string BaseUrl = ApiResource.BaseUrl;
        public static string CreateBoard = ApiResource.CreateBoard;
        public static string GetBoards = ApiResource.GetBoards;
        public static string Username = ApiResource.Username;
        public static string DeleteBoard = ApiResource.DeleteBoard;
        public static string UpdateBoard = ApiResource.UpdateBoard;
        public static string CreateList = ApiResource.CreateList;
        public static string CreateCard = ApiResource.CreateCard;
        public static string GetCard = ApiResource.GetCard;
        public static string UpdateCardName = ApiResource.UpdateCardName;
        public static string UpdateCardStatus = ApiResource.UpdateCardStatus;
        public static string GetLists = ApiResource.GetLists;
        public static string UpdateCardList = ApiResource.UpdateCardList;
        public static string GetList = ApiResource.GetList;
        public static string LoginUrl = ApiResource.LoginUrl;
        public static string CreateSimpleBoard = ApiResource.CreateSimpleBoard;

        public static Tuple<string, string> credentials = new Tuple<string, string>(ApiResource.Email, ApiResource.Password);
    }
}
