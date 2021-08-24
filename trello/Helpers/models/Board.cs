
namespace trello.Helpers.models
{
    public class Board
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool Closed { get; set; }

        public override bool Equals(object other)
        {
            return Name == ((Board)other).Name
                && Desc == ((Board)other).Desc
                && Closed == ((Board)other).Closed;
        }
    }
}
