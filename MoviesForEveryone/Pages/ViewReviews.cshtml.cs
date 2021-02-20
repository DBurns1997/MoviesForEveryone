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
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        [BindProperty(SupportsGet = true)]
        public IList<MoviesForEveryone.Models.Review> _reviews { get; set; }

        public async Task OnGetAsync(int theatId)
        {
            id = theatId;          
            _reviews = await _context.Reviews.Where(r => r.TheaterId == theatId).ToListAsync();         
        }

        public async Task<IActionResult> OnPostHelpfulAsync(int buttonId)
        {
            //Get the review 
            Review rev = new Review();
            int firstKey = _context.Reviews.FirstOrDefault().reviewKey;
            rev  = _context.Reviews.Where(c => c.reviewKey == (firstKey + buttonId)).FirstOrDefault();
            rev.VotedHelpful();

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostNotHelpfulAsync(int buttonId)
        {
            Review rev = new Review();
            int firstKey = _context.Reviews.FirstOrDefault().reviewKey;
            rev =  _context.Reviews.Where(c => c.reviewKey == (firstKey + buttonId)).FirstOrDefault();
            rev.VotedNotHelpful();       

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public ViewReviewsModel(MoviesForEveryone.Models.MoviesDbContext context)
        {
            _context = context;
        }       
       
        private readonly MoviesDbContext _context;        
    }
}