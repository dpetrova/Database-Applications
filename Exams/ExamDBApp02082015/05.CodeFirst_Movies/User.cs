using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _05.CodeFirst_Movies
{
    public class User
    {
        private ICollection<Movie> favouriteMovies;
        private ICollection<Rating> ratings;

        public User()
        {
           //this.country = new Country();
           this.favouriteMovies = new HashSet<Movie>();
           this.ratings = new HashSet<Rating>();
        }

        [Key]
        public int Id { get; set; }

        //[Required]
        //[MinLength(5)]
        public string Username { get; set; }
        
        //[DataType(DataType.EmailAddress, ErrorMessage = "Not valid email")]
        public string Email { get; set; }

        
        public Nullable<int> Age { get; set; }

        public int? CountryId { get; set; }
        
        public virtual Country Country { get; set; }
        

        public virtual ICollection<Movie> FavouriteMovies
        {
            get { return this.favouriteMovies; }
            set { this.favouriteMovies = value; }
        }

        public virtual ICollection<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }
    }
}
