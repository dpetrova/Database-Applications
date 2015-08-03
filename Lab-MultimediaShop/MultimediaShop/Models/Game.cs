using MultimediaShop.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace MultimediaShop.Models
{
    public enum Restriction
    {
        Minor,
        Teen,
        Adult
    }

    public class Game : Item, IItem
    {
        private Restriction ageRestriction;

        public Game(string id, string title, decimal price, IList<Genre> genres, Restriction restriction = Restriction.Minor)
            : base(id, title, price, genres)
        {
            this.AgeRestriction = restriction;
        }

        public Game(string id, string title, decimal price, Genre genre, Restriction restriction = Restriction.Minor)
            : this(id, title, price, new List<Genre> { genre }, restriction)
        {
        }
    
        public Restriction AgeRestriction { get; private set; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendFormat("Age Restriction: {0}", this.AgeRestriction);

            return base.ToString() + result.ToString();
        }
    }
}
