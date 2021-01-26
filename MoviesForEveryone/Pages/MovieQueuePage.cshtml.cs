using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MoviesForEveryone.Models;
using System.IO;

namespace MoviesForEveryone.Pages
{
    public class MovieQueuePageModel : PageModel
    {
        public Queue<Movie> movieQueue;

        public void OnGet()
        {
        }

        //[HttpGet]
        //Use API GET requests to populate the movie queue
        public async Task<IActionResult> OnGetPopulateQueue()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://api.themoviedb.org/3/movie/550?api_key=89d7b6827e40162f83ec0bb9bccc5ee6"); //TEST API Request URL, gets "Fight Club"
            if (response != null)
            {
                
                Movie movieToAdd = new Movie();
                string jsonString = await response.Content.ReadAsStringAsync();
                JsonReader reader = new JsonTextReader(new StringReader(jsonString));
                while (reader.Read())
                {   
                    if (reader.Value != null)
                    {
                        Console.WriteLine("Token: {0}, Value{1}", reader.TokenType, reader.Value);
                    }
                }
                JObject newMovie = JsonConvert.DeserializeObject<JObject>(jsonString);
                

                

                //movieQueue.Append(newMovie);                
                
            }
            return RedirectToPage();
        }
    }
}