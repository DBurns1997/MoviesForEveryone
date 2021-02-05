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
        public float helpfulRatingPercent { get; set; } //The percentage of people that rated the review as "Helpful"
        public int numberHelpfulVotes { get; set; } //The actual number of people who rated the reviews as "helpful"
        public int totalHelpRates { get; set; } //The total number of people who rated the review's helpfulness
        public void calcAvg()
        {
            reviewAvgScore = (cleanlinessRating + concessionsRating + experienceRating + arcadeRating) / 4;
        }
        public void VotedHelpful()
        {
            totalHelpRates++;
            numberHelpfulVotes++;
            UpdateHelpfulRating();
        }

        public void VotedNotHelpful()
        {
            totalHelpRates++;           
            UpdateHelpfulRating();
        }
        public void UpdateHelpfulRating()
        {
           helpfulRatingPercent = (numberHelpfulVotes / totalHelpRates) * 100;
        }
    }
}
