using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace MoviesForEveryone.Pages
{
    public class MapPageModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetAcquireLocation()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("https://www.googleapis.com/geolocation/v1/geolocate?key=AIzaSyCKVHJorOdRlgIFOEC9gZ4AJ2WAXrlligE");
        


            return RedirectToPage();
        }
    }
}
