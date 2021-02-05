using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviesForEveryone.Models;

namespace MoviesForEveryone.Pages
{
    public class ReviewPageModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _review = new Review(); 
            _review.cleanlinessRating = float.Parse(Request.Form["cleanRate"]);
            _review.cleanlinessReview = Request.Form["cleanText"].ToString();
            _review.concessionsRating = float.Parse(Request.Form["concRate"]);
            _review.concessionsReview = Request.Form["concText"].ToString(); 
            _review.arcadeRating = float.Parse(Request.Form["arcadeRate"]);
            _review.arcadeReview = Request.Form["arcadeText"].ToString() ;
            _review.experienceRating = float.Parse(Request.Form["expRate"]);
            _review.experienceReview = Request.Form["expText"].ToString();
            _review.calcAvg();
            _review.numberHelpfulVotes = 0;
            _review.totalHelpRates = 0;
            _review.helpfulRatingPercent = 0;

            _context.Reviews.Add(_review);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public ReviewPageModel(MoviesForEveryone.Models.MoviesDbContext context)
        {
            _context = context;
        }

        private readonly MoviesForEveryone.Models.MoviesDbContext _context;        
        Review _review;
    }
}
