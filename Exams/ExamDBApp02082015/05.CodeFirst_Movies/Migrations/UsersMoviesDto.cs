using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _05.CodeFirst_Movies.Migrations
{
    internal class UsersMoviesDto
    {
        public string Username { get; set; }

        [JsonProperty("favouriteMovies")]
        public virtual string[] Isbn { get; set; }
    }
}
