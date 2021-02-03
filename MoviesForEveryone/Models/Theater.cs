using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesForEveryone.Models
{
    public class Theater
    {
        public float avgClean { get; set; }        
        public float avgConc { get; set; }
        public float avgArcade { get; set; }
        public float avgViewing { get; set; }
        public List<string> textReviews { get; set; }
    }
}
