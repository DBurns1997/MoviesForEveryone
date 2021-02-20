using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesForEveryone.Models
{
    public class Movie
    {
        public string movieTitle { get; set; }
        public List<string> genres { get; set; }
        public List<string> keywords { get; set; }
        public List<string> movieDirectors { get; set; }
        public string overview { get; set; }
        public Movie()
        {
            keywords = new List<string>();
            genres = new List<string>();
            movieDirectors = new List<string>();
        }
        public bool marked { get; set; }
        private int apiKey { get; set; }

        public void setApiKey(int key)
        {
            apiKey = key;
        }
    };
}
