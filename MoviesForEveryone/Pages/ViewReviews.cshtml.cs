using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MoviesForEveryone.Pages
{
    public class ViewReviewsModel : PageModel
    {
        public async Task OnGetAsync()
        {
            //TODO: Get current theater
            _reviews = await _context.Reviews.ToListAsync();
            /*_reviews = await _context.Reviews.Where(rev => rev.reviewKey > 0) //Change this query when theaters are implimented
                        .Include(rev => rev.cleanlinessRating)
                        .Include(rev => rev.concessionsRating)
                        .Include(rev => rev.arcadeRating)
                        .Include(rev => rev.experienceRating)
                        .Include(rev => rev.reviewAvgScore)
                        .Include(rev => rev.cleanlinessReview)
                        .Include(rev => rev.concessionsReview)
                        .Include(rev => rev.arcadeReview)
                        .Include(rev => rev.experienceReview).ToListAsync();*/
        }

        public ViewReviewsModel(MoviesForEveryone.Models.MoviesDbContext context)
        {
            _context = context;
        }

        public IList<MoviesForEveryone.Models.Review> _reviews { get; set; }
        private readonly MoviesForEveryone.Models.MoviesDbContext _context;        
    }
}
