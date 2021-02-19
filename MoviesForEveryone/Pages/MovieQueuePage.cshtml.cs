using System;
using System.Collections.Generic;
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
        public static Queue<Movie> movieQueue;

        public void OnGet()
        {

        }
        public async static Task<bool> PopulateQueue() 
        {
            movieQueue = new Queue<Movie>();
            Random rnd = new Random();
            int latestID = 790000, //This is the max ID for movies on TMDB 
                movieId;
            Movie movieToAdd;
            using HttpClient client = new HttpClient();


            //Repeat ten times, one for each queue slot
            //TODO: Add conditional to test is_adult
            for (int i = 0; i < 10; i++)
            {
                movieToAdd = new Movie();
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
                        JsonSerializer jsonSerializer = new JsonSerializer();
                        var movieToken = JToken.Load(reader);
                        if (!movieToken["genres"].HasValues)
                        {
                            movieToAdd.genres.Add("N/A");
                        }
                        else
                        {
                            foreach (var child in movieToken["genres"])
                            {
                                reader.Read();
                                if (reader.Value != null)
                                {
                                    if (reader.Value.ToString() == "name")
                                    {
                                        reader.Read();
                                        movieToAdd.genres.Add(reader.Value.ToString());
                                    }
                                }
                            }
                        }

                        movieToAdd.movieTitle = movieToken["title"].ToString();

                        movieToAdd.overview = movieToken["overview"].ToString();


                    }

                    //Seperate API call to get the keywords for the film
                    response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/keywords?api_key=89d7b6827e40162f83ec0bb9bccc5ee6");
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonKeywords = await response.Content.ReadAsStringAsync();
                        JsonReader keyReader = new JsonTextReader(new StringReader(jsonKeywords));
                        while (keyReader.Read())
                        {
                            JsonSerializer jsonSerializer = new JsonSerializer();
                            var keysToken = JToken.Load(keyReader);
                            if (!keysToken["keywords"].HasValues)
                            {
                                movieToAdd.keywords.Add("N/A");
                            }
                            else
                            {
                                foreach (var child in keysToken["keywords"])
                                {
                                    reader.Read();
                                    if (reader.Value != null)
                                    {
                                        if (reader.Value.ToString() == "name")
                                        {
                                            movieToAdd.keywords.Add(reader.Value.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //Last API call to get director 
                    response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key=89d7b6827e40162f83ec0bb9bccc5ee6");
                    if (response.IsSuccessStatusCode)
                    {
                        string credString = await response.Content.ReadAsStringAsync();
                        JsonReader credReader = new JsonTextReader(new StringReader(credString));
                        while (credReader.Read())
                        {
                            JsonSerializer jsonSerializer = new JsonSerializer();
                            var credsToken = JToken.Load(credReader);
                            if (!credsToken["crew"].HasValues)
                            {
                                movieToAdd.movieDirectors.Add("Unknown");
                            }
                            else
                            {
                                bool director = false;
                                foreach (var child in credsToken["crew"])
                                {
                                    credReader.Read();
                                    if (credReader.Value != null && credReader.Value.ToString() == "known_for_department")
                                    {
                                        credReader.Read();
                                        if (credReader.Value.ToString() == "Directing")
                                        {
                                            director = true;
                                        }
                                    }

                                    if (credReader.Value != null && director == true)
                                    {
                                        if (credReader.Value.ToString() == "name")
                                        {
                                            credReader.Read();
                                            movieToAdd.movieDirectors.Add(credReader.Value.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                    movieQueue.Enqueue(movieToAdd);
                }
            }
            return true;
        }
    }
}