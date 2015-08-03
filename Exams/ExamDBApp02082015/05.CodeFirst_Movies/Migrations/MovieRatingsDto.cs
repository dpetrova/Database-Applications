using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _05.CodeFirst_Movies.Migrations
{
    internal class MovieRatingsDto
    {
        [JsonProperty("user")]
        public string Username { get; set; }

        [JsonProperty("movie")]
        public string Isbn { get; set; }

        public int Rating { get; set; }
    }
}
