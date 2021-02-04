using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesForEveryone.Models
{
    public class Review
    {
        [Key]
        public int reviewKey { get; set; }
        public float cleanlinessRating { get; set; }
        public float concessionsRating { get; set; }
        public float arcadeRating { get; set; }
        public float experienceRating { get; set; }
        public float reviewAvgScore { get; set; }
        public string cleanlinessReview { get; set;}
        public string concessionsReview { get; set; }
        public string arcadeReview { get; set; }
        public string experienceReview { get; set; }

        public void calcAvg()
        {
            reviewAvgScore = (cleanlinessRating + concessionsRating + experienceRating + arcadeRating) / 4;
        }
    }
}
