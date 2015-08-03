using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.CodeFirst_Movies
{
    public class Movie
    {
        private ICollection<User> usersThatMovieIsFavourite;
        private ICollection<Rating> ratings;

         public Movie()
        {
           this.usersThatMovieIsFavourite = new HashSet<User>();
           this.ratings = new HashSet<Rating>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }

        public AgeRestriction AgeRestriction { get; set; }

        public virtual ICollection<User> UsersThatMovieIsFavourite
        {
            get { return this.usersThatMovieIsFavourite; }
            set { this.usersThatMovieIsFavourite = value; }
        }

        public virtual ICollection<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }
        
    }
}
