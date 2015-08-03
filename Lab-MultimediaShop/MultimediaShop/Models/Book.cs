using MultimediaShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultimediaShop.Models
{
    public class Book : Item, IItem
    {
        private string author;

        public Book(string id, string title, decimal price, string autor, IList<Genre> genres)
            : base(id, title, price, genres)
        {
            this.Author = autor;
        }

        public Book(string id, string title, decimal price, string autor, Genre genre)
            : this(id, title, price, autor, new List<Genre> { genre })
        {
        }

        public string Author
        {
            get { return this.author; }
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 3)
                {
                    throw new ArgumentException("Author cannot be empty or less than 3 symbols");
                }
                this.author = value;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendFormat("Author: {0}", this.Author);

            return base.ToString() + result.ToString();
        }
    }
}
