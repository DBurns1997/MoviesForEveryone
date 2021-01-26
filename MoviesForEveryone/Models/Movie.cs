using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesForEveryone.Models
{
    public class Movie
    {
        public string movieTitle { get; set; }
        public List<string> movieTags { get; set; }
        public string movieDirector { get; set; }
    }

    public class MovieDBContext
    {

    }
}
