using MultimediaShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultimediaShop.Models
{
    public class Movie : Item, IItem
    {
        private int length;

        public Movie(string id, string title, decimal price, int length, IList<Genre> genres)
            : base(id, title, price, genres)
        {
            this.Length = length;
        }

        public Movie(string id, string title, decimal price, int length, Genre genre)
            : this(id, title, price, length, new List<Genre> { genre })
        {
        }
    
        public int Length
        {
            get { return this.Length; }
            private set { this.length = value; }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendFormat("Length: {0}", this.Length);

            return base.ToString() + result.ToString();
        }
    }
}
