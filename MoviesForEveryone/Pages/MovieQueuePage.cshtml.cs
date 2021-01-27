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
            Random rnd = new Random();
            int latestID = 790000, //This is the max ID for movies on TMDB 
                movieId = rnd.Next(1, latestID); 
            Movie movieToAdd = new Movie();
            using var client = new HttpClient();
            var response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key=89d7b6827e40162f83ec0bb9bccc5ee6"); //Gets a random movie from TMDB using movieID
            if (response != null)
            {            
                string jsonString = await response.Content.ReadAsStringAsync();
                JsonReader reader = new JsonTextReader(new StringReader(jsonString));
                while (reader.Read())
                {
                    if (reader.Value != null)
                    {
                        //Parse the JSON data we want 
                        switch(reader.Value.ToString())
                        {
                            case "title":
                                reader.Read(); //Red the next token to get the actual title
                                movieToAdd.movieTitle = reader.Value.ToString();   
                                break;
                            case "overview":
                                reader.Read();
                                movieToAdd.overview = reader.Value.ToString();
                                break;
                            case "genres":
                                while (reader.Value == null || reader.Value.ToString() != "name" ) reader.Read();
                                reader.Read();
                                movieToAdd.genre = reader.Value.ToString();
                                break;
                            default:
                                break;
                        }                                             
                    }
                }

                //Seperate API call to get the keywords for the film
                response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/keywords?api_key=89d7b6827e40162f83ec0bb9bccc5ee6");
                if (response != null)
                {
                    string jsonKeywords = await response.Content.ReadAsStringAsync();
                    JsonReader keyReader = new JsonTextReader(new StringReader(jsonKeywords));
                    while (keyReader.Read())
                    {
                        if (keyReader.Value != null)
                        {
                            while (keyReader.Value == null || keyReader.Value.ToString() != "name") keyReader.Read();
                            keyReader.Read();
                            movieToAdd.keywords.Add(keyReader.Value.ToString());

                        }
                    }
                }

                //Last API call to get director 
                response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key=89d7b6827e40162f83ec0bb9bccc5ee6"); //TEST API Request URL, gets "Oldboy"
                if (response != null)
                {
                    string credString = await response.Content.ReadAsStringAsync();
                    JsonReader credReader = new JsonTextReader(new StringReader(credString));
                    while (credReader.Read())
                    {
                        if (credReader.Value != null && credReader.Value.ToString() == "crew" )
                        {
                            bool dirFound = false;
                            while (credReader.Read() && dirFound == false)
                            {                                
                                if (credReader.Value != null && credReader.Value.ToString() == "Directing")
                                {
                                    credReader.Read();
                                    credReader.Read();
                                    movieToAdd.movieDirector = credReader.Value.ToString();
                                    dirFound = true;
                                }
                            }
                        }
                    }
                    //JObject newMovie = JsonConvert.DeserializeObject<JObject>(jsonString);
                }



                    movieQueue.Append(movieToAdd);                

                }
            return RedirectToPage();
        }
    }
}