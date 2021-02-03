using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Newtonsoft.Json;
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
       
        //Use API GET requests to populate the movie queue
        public async Task<IActionResult> OnGetPopulateQueue()
        {
            movieQueue = new Queue<Movie>();
            Random rnd = new Random();
            int latestID = 790000, //This is the max ID for movies on TMDB 
                movieId; 
            Movie movieToAdd = new Movie();
            using HttpClient client = new HttpClient();
 

            //Repeat ten times, one for each queue slot
            //TODO: Add conditional to test is_adult
            for (int i = 0; i < 10; i++)
            {     
                movieId = rnd.Next(1, latestID);
                HttpResponseMessage response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key=89d7b6827e40162f83ec0bb9bccc5ee6"); //Gets a random movie from TMDB using movieID
                while (!response.IsSuccessStatusCode) //If the API request fails, generate another ID and make another request
                {
                    movieId = rnd.Next(1, latestID);
                    response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key=89d7b6827e40162f83ec0bb9bccc5ee6"); //Gets a random movie from TMDB using movieID
                }
                if (response.IsSuccessStatusCode) //Just make ABSOLUTELY sure the API request was succesful
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    JsonReader reader = new JsonTextReader(new StringReader(jsonString));
                    while (reader.Read())
                    {
                        if (reader.Value != null)
                        {
                            //Parse the JSON data we want 
                            switch (reader.Value.ToString())
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
                                    reader.Read(); //Move to the next token
                                    reader.Read();
                                    reader.Read();
                                    while (reader.Value.ToString() != "homepage") //'Homepage' is the next section in each TMDB entry
                                    {
                                        reader.Read();
                                        reader.Read();
                                        if (reader.Value != null && reader.Value.ToString() == "name")
                                        {
                                            reader.Read();
                                            movieToAdd.genres.Add(reader.Value.ToString());
                                            reader.Read();
                                            reader.Read();
                                            reader.Read();
                                        }
                                        else 
                                        {                                             
                                            reader.Read();
                                            reader.Read();
                                        }
                                    }
                                    if (movieToAdd.genres.Count == 0 ) movieToAdd.genres.Add("N/A");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    //Seperate API call to get the keywords for the film
                    response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/keywords?api_key=89d7b6827e40162f83ec0bb9bccc5ee6");
                    if (response.IsSuccessStatusCode)
                    {
                        bool readEnd = false;
                        string jsonKeywords = await response.Content.ReadAsStringAsync();
                        JsonReader keyReader = new JsonTextReader(new StringReader(jsonKeywords));
                        while (keyReader.Read() && keyReader.TokenType != JsonToken.EndArray)
                        {
                            if (keyReader.Value != null && keyReader.Value.ToString() == "name")
                            {
                                keyReader.Read();
                                movieToAdd.keywords.Add(keyReader.Value.ToString());
                            }
                        }
                        if (movieToAdd.keywords.Count == 0) movieToAdd.keywords.Add("N/A");
                    }

                    //Last API call to get director 
                    response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key=89d7b6827e40162f83ec0bb9bccc5ee6"); 
                    if (response.IsSuccessStatusCode)
                    {
                        string credString = await response.Content.ReadAsStringAsync();
                        JsonReader credReader = new JsonTextReader(new StringReader(credString));
                        while (credReader.Read() && credReader.TokenType != JsonToken.EndArray)
                        {
                            if (credReader.Value != null && credReader.Value.ToString() == "crew")
                            {
                                bool dirFound = false;
                                while (credReader.Read() && dirFound == false)
                                {
                                    if (credReader.Value.ToString() == "name")
                                    {
                                        credReader.Read();
                                        string possibleName = credReader.Value.ToString();
                                        movieToAdd.movieDirector = possibleName;
                                        while(credReader.Read())
                                        {
                                            if (credReader.Value.ToString() == "Director")
                                            {
                                                dirFound = true;
                                            }
                                        }                                        
                                    }
                                }
                            }
                        }
                        if (movieToAdd.movieDirector == null) movieToAdd.movieDirector = "Unknown";                     
                    }
                    movieQueue.Enqueue(movieToAdd);
                }
            }
            return RedirectToPage();
        }
    }
}