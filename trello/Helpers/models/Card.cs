
namespace trello.Helpers.models
{
    public class Card
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool Closed { get; set; }

        public string IdList { get; set; }
        public string IdBoard { get; set; }

        public Card MakeFake()
        {
            Name = Faker.Name.FullName();
            Desc = Faker.Lorem.Sentence();
            Closed = false;
            return this;
        }

        public override bool Equals(object other)
        {
            return Name == ((Card)other).Name
                && Desc == ((Card)other).Desc
                && Closed == ((Card)other).Closed;
        }
    }
}
