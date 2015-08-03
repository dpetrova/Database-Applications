using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.Models
{
    public class Book
    {
        //fields for navigation properties only
        private ICollection<Category> categories;
        private Author author;
        private ICollection<Book> relatedBooks;

        //constructor include navigation properties only
        public Book()
        {
            this.categories = new HashSet<Category>();
            this.author = new Author();
            this.relatedBooks = new HashSet<Book>();
        }

        //properties coresponding of each table column
        //[Key]
        public int Id { get; set; }

        //[StringLength(50, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 50 character in length.")]
        public string Title { get; set; }

        //[MaxLength(1000)]
        public string Description { get; set; }

        public Edition Edition { get; set; }

        //[Required]
        public decimal Price { get; set; }

        public int Copies { get; set; }

        public DateTime ReleaseDate { get; set; }

        public AgeRestriction AgeRestriction { get; set; }

        //navigation properties for relations
        public virtual ICollection<Category> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }

        public virtual Author Author
        {
            get { return this.author; }
            set { this.author = value; }
        }

        public virtual ICollection<Book> RelatedBooks
        {
            get { return this.relatedBooks; }
            set { this.relatedBooks = value; }
        }
    }
}
