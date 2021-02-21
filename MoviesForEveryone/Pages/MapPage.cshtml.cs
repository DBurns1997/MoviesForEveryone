using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;

namespace MoviesForEveryone.Pages
{
    public class MapPageModel : PageModel
    {
        public void OnGet()
        {            
            showOptionsIndicator = false;
            localTheaters = new List<Models.Theater>();
        }

        public async Task<IActionResult> OnGetAcquireLocation()
        {
            //Set our params to default            
            showOptionsIndicator = false;
            localTheaters = new List<Models.Theater>();
            //Set the radius to the user's chosen radius
            userSetRadius = _context.Settings.Where(c => c.userId == 0).FirstOrDefault().radiusSetting; //No users yet, so just get the setting

            float[] coords = new float[2];
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://ip-api.com/json/");
            if (response.IsSuccessStatusCode) //Just make ABSOLUTELY sure the API request was succesful
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                JsonReader reader = new JsonTextReader(new StringReader(jsonString));
                while (reader.Read()) // Get the IP address for the user's location to feed into the google maps API request
                {
                    if (reader.Value != null)
                    {
                        if (reader.Value.ToString() == "lat")
                        {
                            reader.Read();
                            latitude = reader.Value.ToString();
                        }
                        if (reader.Value.ToString() == "lon")
                        {
                            reader.Read();
                            longitude = reader.Value.ToString();
                        }
                    }
                }

                //We can't do this unless the first API call succeeded, so it's within the first SuccessStatusCode checker
                response = await client.GetAsync($"https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=theaters+and+cinemas&inputtype=textquery&fields=photos,formatted_address,name,opening_hours,rating&locationbias=circle:2000{latitude},{longitude}&key=AIzaSyCKVHJorOdRlgIFOEC9gZ4AJ2WAXrlligE");
                if (response.IsSuccessStatusCode)
                {
                    string jsonString2 = await response.Content.ReadAsStringAsync();
                    JsonTextReader reader2 = new JsonTextReader(new StringReader(jsonString2));

                    while (reader2.Read())
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        var theaterToken = JToken.Load(reader2);
                        {
                            foreach (var child in theaterToken["candidates"])
                            {
                                Models.Theater theaterToAdd = new Models.Theater();
                                theaterToAdd.theaterName = child["name"].ToString();
                                if (!_context.Theaters.Where(t => t.theaterName == theaterToAdd.theaterName).Any()) //Add the theater to the website database if it's a new theater
                                {                                    
                                    _context.Theaters.Add(theaterToAdd);
                                    await _context.SaveChangesAsync();
                                }
                                theaterToAdd.Id = _context.Theaters.Where(t => t.theaterName == theaterToAdd.theaterName).FirstOrDefault().Id; //For our local theaters, all we need to know is the theater name and ID to pass to the review pages                                
                                
                                localTheaters.Add(theaterToAdd);
                            }
                        }
                    }
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSetRadiusAsync()
        {
            MoviesForEveryone.Models.UserSettings sett = _context.Settings.Where(c => c.userId == 0).FirstOrDefault();
            sett.radiusSetting = int.Parse(Request.Form["radiusSet"]);  

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
        
        //Check if a theater has already been reviewed at all or not
        public bool CheckReviewed(string _theaterName)
        {
            bool reviewed = false;
            if (_context.Reviews.Where(c => c.TheaterId == (_context.Theaters.Where(t => t.theaterName == _theaterName).FirstOrDefault().Id)).Any()) //If the theater specified by the name has ANY reviews...
            {
                reviewed = true;
            }      

            return reviewed;
        }

        public MapPageModel(Models.MoviesDbContext context)
        {
            _context = context;
        }

        public void OnPostOptions()
        {
            showOptionsIndicator = true;
        }

        public string getLat()
        {
            return latitude;
        }

        public string getLong()
        {
            return longitude;
        }

        public int getRad()
        {
            return userSetRadius;
        }

        public bool getOpt()
        {
            return showOptionsIndicator;
        }

        public List<Models.Theater> getLocalTheaters()
        {
            return localTheaters;
        }

        private string latitude;
        private string longitude;
        private bool showOptionsIndicator;
        private int userSetRadius;
        private List<Models.Theater> localTheaters;
        private Models.MoviesDbContext _context;
    }
}
