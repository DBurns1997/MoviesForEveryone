using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using MoviesForEveryone.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MoviesForEveryone.Pages
{
    [Authorize]
    public class MovieQueuePageModel : PageModel
    {     

        public static Queue<Movie> movieQueue;
        public static bool queueComplete;

        public void OnGet()
        {
            string id = HttpContext.User.Identity.Name;
            positiveKeys = _context.PositiveKeys.Where(c => c.userId == id).ToList();
            negativeKeys = _context.NegativeKeys.Where(c => c.userID == id).ToList();
            CheckQueueWeights();
            if (movieQueue == null)
            {
                queueComplete = false;
            }
            else
            {
                queueComplete = true;
            }
        }

        public MovieQueuePageModel(MoviesDbContext context)
        {
            _context = context;            
        }

        public async Task<IActionResult> OnPostMarkLikedAsync(int buttonId)
        {
            MovieOpinions op = new MovieOpinions();

            op.liked = true;
            op.movieTitle = movieQueue.ElementAt(buttonId).movieTitle;
            movieQueue.ElementAt(buttonId).marked = true;


            foreach (var m in movieQueue)
            {
                if (m.movieTitle == op.movieTitle)
                {
                    foreach (var genre in m.genres)
                    {
                        if (genre.ToString() != "N/A")
                        {
                            PositiveKeys pos = new PositiveKeys();
                            pos.userId = HttpContext.User.Identity.Name; 
                            pos.keyword = genre.ToString();
                            _context.PositiveKeys.Add(pos);
                        }
                    }
                    foreach (var key in m.keywords)
                    {
                        if (key.ToString() != "N/A")
                        {
                            PositiveKeys pos = new PositiveKeys();
                            pos.userId = HttpContext.User.Identity.Name;
                            pos.keyword = key.ToString();                            
                            _context.PositiveKeys.Add(pos);
                        }
                    }
                }
            }

            //op.userId; //We're not setting the userId yet because we don't have a user system!
            _context.Opinions.Add(op);


            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMarkNotLikedAsync(int buttonId)
        {
            MovieOpinions op = new MovieOpinions();

            op.liked = false;
            op.movieTitle = movieQueue.ElementAt(buttonId).movieTitle;
            movieQueue.ElementAt(buttonId).marked = true;


            foreach (var m in movieQueue)
            {
                if (m.movieTitle == op.movieTitle)
                {
                    foreach (var genre in m.genres)
                    {
                        if (genre.ToString() != "N/A")
                        {
                            PositiveKeys pos = new PositiveKeys();
                            pos.userId = HttpContext.User.Identity.Name;
                            pos.keyword = genre.ToString();
                            _context.PositiveKeys.Add(pos);
                        }
                    }
                    foreach (var key in m.keywords)
                    {
                        if (key.ToString() != "N/A")
                        {
                            NegativeKeys neg = new NegativeKeys();
                            neg.userID = HttpContext.User.Identity.Name;
                            neg.keyword = key.ToString();
                            _context.NegativeKeys.Add(neg);
                        }
                    }
                }
            }

            //op.userId; //We're not setting the userId yet because we don't have a user system!
            _context.Opinions.Add(op);

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        //If the user hasn't seen the movie, we don't actually have to do anything except mark the movie as "Marked"
        public void OnPostMarkNotSeen(int buttonId)
        {
            movieQueue.ElementAt(buttonId).marked = true;
            RedirectToPage();
        }

        public static async Task<bool> PopulateQueue()
        {
            movieQueue = new Queue<Movie>();
            Random rnd = new Random();
            int latestID = 790000, //This is the max ID for movies on TMDB 
                movieId;
            Movie movieToAdd;
            using HttpClient client = new HttpClient();

            //Repeat ten times, one for each queue slot
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
                        JsonSerializer jsonSerializer = new JsonSerializer();
                        var movieToken = JToken.Load(reader);


                        //Check and make sure the movie isn't adult, which TMDB has for some VERY strange reason also makes sure the movie is actually titled and has genre tags, we don't want to put up untitled movies or un-genred movies
                        while (!response.IsSuccessStatusCode || movieToken["adult"].ToString() == "true" || movieToken["title"].ToString() == "" || !movieToken["genres"].HasValues)
                        {
                            movieId = rnd.Next(1, latestID);
                            response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key=89d7b6827e40162f83ec0bb9bccc5ee6"); //Gets a random movie from TMDB using movieID
                            jsonString = await response.Content.ReadAsStringAsync();
                            reader = new JsonTextReader(new StringReader(jsonString));
                            jsonSerializer = new JsonSerializer();
                            movieToken = JToken.Load(reader);
                        }

                        //Put the key into the movie node so I can look at it in case strange things happen with the API call
                        movieToAdd.setApiKey(movieId);


                        //Get the movie's genres                
                        foreach (var child in movieToken["genres"])
                        {
                            movieToAdd.genres.Add(child["name"].ToString());
                        }

                        //Get the movie's title
                        movieToAdd.movieTitle = movieToken["title"].ToString();

                        //Get the movie's description, if any.
                        if (!movieToken["overview"].HasValues)
                        {
                            movieToAdd.overview = "No overview provided.";
                        }
                        else movieToAdd.overview = movieToken["overview"].ToString();

                        //Get the keywords movie keywords
                        response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/keywords?api_key=89d7b6827e40162f83ec0bb9bccc5ee6");

                        string jsonKeywords = await response.Content.ReadAsStringAsync();
                        JsonReader keyReader = new JsonTextReader(new StringReader(jsonKeywords));

                        JToken keysToken = JToken.Load(keyReader);
                        if (keysToken["keywords"].HasValues)
                        {                            
                            foreach (var child in keysToken["keywords"])
                            {
                                movieToAdd.keywords.Add(child["name"].ToString());
                            }
                        }
                        else
                        {
                            movieToAdd.keywords.Add("N/A");
                        }

                        //Last API call to get director 
                        response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key=89d7b6827e40162f83ec0bb9bccc5ee6");
                        if (response.IsSuccessStatusCode)
                        {
                            string credString = await response.Content.ReadAsStringAsync();
                            JsonReader credReader = new JsonTextReader(new StringReader(credString));

                            //jsonSerializer = new JsonSerializer();
                            var credsToken = JToken.Load(credReader);
                            if (!credsToken["crew"].HasValues)
                            {
                                movieToAdd.movieDirectors.Add("Unknown");
                            }
                            else
                            {
                                foreach (var child in credsToken["crew"])
                                {
                                    if (child["known_for_department"].ToString() == "Directing")
                                    {
                                        movieToAdd.movieDirectors.Add(child["name"].ToString());
                                    }
                                }
                            }

                        }
                    }

                    movieQueue.Enqueue(movieToAdd);
            }
            return true;
        }

        public async Task<bool> PopulateSingleQueueSlot(int slot)
        {
            int latestID = 790000, //This is the max ID for movies on TMDB 
                movieId;
            Movie movieToAdd;
            Random rnd = new Random();

            using HttpClient client = new HttpClient();
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
                JsonSerializer jsonSerializer = new JsonSerializer();
                var movieToken = JToken.Load(reader);


                //Check and make sure the movie isn't adult, which TMDB has for some VERY strange reason also makes sure the movie is actually titled and has genre tags, we don't want to put up untitled movies or un-genred movies
                while (!response.IsSuccessStatusCode || movieToken["adult"].ToString() == "true" || movieToken["title"].ToString() == "" || !movieToken["genres"].HasValues)
                {
                    movieId = rnd.Next(1, latestID);
                    response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key=89d7b6827e40162f83ec0bb9bccc5ee6"); //Gets a random movie from TMDB using movieID
                    jsonString = await response.Content.ReadAsStringAsync();
                    reader = new JsonTextReader(new StringReader(jsonString));
                    jsonSerializer = new JsonSerializer();
                    movieToken = JToken.Load(reader);
                }

                //Put the key into the movie node so I can look at it in case strange things happen with the API call
                movieToAdd.setApiKey(movieId);


                //Get the movie's genres                
                foreach (var child in movieToken["genres"])
                {
                    movieToAdd.genres.Add(child["name"].ToString());
                }

                //Get the movie's title
                movieToAdd.movieTitle = movieToken["title"].ToString();

                //Get the movie's description, if any.
                if (!movieToken["overview"].HasValues)
                {
                    movieToAdd.overview = "No overview provided.";
                }
                else movieToAdd.overview = movieToken["overview"].ToString();

                //Get the keywords movie keywords
                response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/keywords?api_key=89d7b6827e40162f83ec0bb9bccc5ee6");

                string jsonKeywords = await response.Content.ReadAsStringAsync();
                JsonReader keyReader = new JsonTextReader(new StringReader(jsonKeywords));

                JToken keysToken = JToken.Load(keyReader);
                if (keysToken["keywords"].HasValues)
                {                    
                    foreach (var child in keysToken["keywords"])
                    {
                        movieToAdd.keywords.Add(child["name"].ToString());
                    }
                }
                else
                {
                    movieToAdd.keywords.Add("N/A");
                }

                //Last API call to get director 
                response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key=89d7b6827e40162f83ec0bb9bccc5ee6");
                if (response.IsSuccessStatusCode)
                {
                    string credString = await response.Content.ReadAsStringAsync();
                    JsonReader credReader = new JsonTextReader(new StringReader(credString));

                    //jsonSerializer = new JsonSerializer();
                    var credsToken = JToken.Load(credReader);
                    if (!credsToken["crew"].HasValues)
                    {
                        movieToAdd.movieDirectors.Add("Unknown");
                    }
                    else
                    {
                        foreach (var child in credsToken["crew"])
                        {
                            if (child["known_for_department"].ToString() == "Directing")
                            {
                                movieToAdd.movieDirectors.Add(child["name"].ToString());
                            }
                        }
                    }

                }
            }

            movieQueue.Enqueue(movieToAdd);
            return true;
        }       

        private static int GetMovieWeight(int i)
        {
            int weight = 0;
            Movie movieCheck = movieQueue.ElementAt(i);

            foreach (string key in movieCheck.keywords)
            {
                foreach (NegativeKeys negKey in negativeKeys)
                {
                    if (negKey.keyword == key) weight--;
                }
                foreach (PositiveKeys posKey in positiveKeys)
                {
                    if (posKey.keyword == key) weight++;
                }
            }

            return weight;
        }

        //As the initial queue setting cannot access database data, we need to do that on the page load!
        //Check the key weights of each of the movies in the queue and get a new one if the weight is negative
        private async void CheckQueueWeights()
        {
            for (int i = 0; i < 10; i++)
            {                
                int movieWeight = GetMovieWeight(i);
                if (movieWeight < 0)
                {
                    await PopulateSingleQueueSlot(i);
                }
            }      
            
        }

        private readonly MoviesDbContext _context;
        private static List<PositiveKeys> positiveKeys;
        private static List<NegativeKeys> negativeKeys;

    }
}