﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.CodeFirst_Movies.Migrations
{
    internal class UserDto
    {
        public string Username { get; set; }

        public int? Age { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }
    }
}
