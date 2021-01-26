using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesForEveryone.Models
{
    public class Movie
    {
        public string original_title { get; set; }
        public List<string> genres { get; set; }
        public string movieDirector { get; set; }
        public string overview { get; set; }
    }

    public class MovieDBContext
    {

    }
}
