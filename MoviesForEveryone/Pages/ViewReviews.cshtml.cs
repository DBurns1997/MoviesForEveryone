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
            _reviews = await _context.Reviews.ToListAsync(); //Change this query when theaters are added.           
        }



        public ViewReviewsModel(MoviesForEveryone.Models.MoviesDbContext context)
        {
            _context = context;
        }

        public IList<MoviesForEveryone.Models.Review> _reviews { get; set; }
        private readonly MoviesForEveryone.Models.MoviesDbContext _context;        
    }
}
