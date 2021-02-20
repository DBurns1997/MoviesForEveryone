using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviesForEveryone.Models;

namespace MoviesForEveryone.Pages
{
    public class ViewReviewsModel : PageModel
    {
        public async Task OnGetAsync(int _theaterId)
        {         
            _reviews = await _context.Reviews.Where(r => r.TheaterId == _theaterId).ToListAsync();         
        }

        public async Task<IActionResult> OnPostHelpfulAsync(int buttonId)
        {
            //Get the review 
            Review rev = _context.Reviews.Where(c => c.reviewKey == buttonId).FirstOrDefault();
            rev.VotedHelpful();                                             
           
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostNotHelpfulAsync(int buttonId)
        {
            Review rev = _context.Reviews.Where(c => c.reviewKey == buttonId).FirstOrDefault();
            rev.VotedNotHelpful();           

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public ViewReviewsModel(MoviesForEveryone.Models.MoviesDbContext context)
        {
            _context = context;
        }

        public IList<MoviesForEveryone.Models.Review> _reviews { get; set; }
        private readonly MoviesDbContext _context;        
    }
}