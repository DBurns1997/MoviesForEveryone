using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Newtonsoft.Json;

namespace MoviesForEveryone.Pages
{
    public class MovieQueuePageModel : PageModel
    {
        public void OnGet()
        {
        }

        //[HttpGet]
        public async Task<IActionResult> OnGetPopulateQueue()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://api.themoviedb.org/3/movie/550?api_key=89d7b6827e40162f83ec0bb9bccc5ee6"); //TEST API Request URL, gets "Fight Club"
            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                object movie = JsonConvert.DeserializeObject<object>(jsonString);
                
            }
            return RedirectToPage();
        }
    }
}