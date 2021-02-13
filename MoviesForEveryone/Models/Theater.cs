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
        public double avgClean { get; set; }        
        public double avgConc { get; set; }
        public double avgArcade { get; set; }
        public double avgViewing { get; set; }
        public double totalAvg { get; set; }
        public void recalcAvg()
        {
            totalAvg = (avgClean + avgConc + avgArcade + avgViewing + totalAvg) / 5;
        }
        public virtual ICollection<Review> textReviews { get; set; }
    }
}
