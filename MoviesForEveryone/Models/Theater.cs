using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesForEveryone.Models
{
    public class Theater
    {
        [Key]
        public int Id { get; set; }
        public string theaterName { get; set; }
        public float avgClean { get; set; }        
        public float avgConc { get; set; }
        public float avgArcade { get; set; }
        public float avgViewing { get; set; }
        public float totalAvg { get; set; }
        public void recalcAvg()
        {
            totalAvg = (avgClean + avgConc + avgArcade + avgViewing + totalAvg) / 5;
        }
        public virtual ICollection<Review> textReviews { get; set; }
    }
}
