using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoviesForEveryone.Models
{
    public class MovieOpinions
    {
        [Key]
        public int opinionKey { get; set; }
        public string movieTitle { get; set; }
        public bool liked { get; set; }
        public int userId { get; set; }
    }
}
