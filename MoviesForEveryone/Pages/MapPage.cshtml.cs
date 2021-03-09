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
using Microsoft.AspNetCore.Authorization;

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

            if (city == null)
            {
                city = _context.Settings.Where(u => u.userId == 0).FirstOrDefault().setCity;
            }
            //Set the radius to the user's chosen radius

            userSetRadius = _context.Settings.Where(c => c.userId == 0).FirstOrDefault().radiusSetting; //No users yet, so just get the setting
            
            //Get the boundary coords of the google maps window
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

                //Meters in 200 miles ~= 321868
                //Meters in 30 miles ~= 48280
                //321868 / 21 ~= 15327
                //MAX NUM: 15327
                //Go from 48280 to 321868 in 21 steps of 13028

                //Blame Google's bizarre embedded map sizing constraints for this!

                int placeCallRadius = ((22 - userSetRadius) * 13028) - 48280;

                string[] apiCallCoords = getCoords(latitude, longitude, placeCallRadius);


                //We can't do this unless the first API call succeeded, so it's within the first SuccessStatusCode checker //rectangle:south,west|north,east
                if (city == null)
                {
                    response = await client.GetAsync($"https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=movie+theater&inputtype=textquery&fields=photos,formatted_address,name,opening_hours,rating&locationbias=rectangle:{apiCallCoords[0]},{apiCallCoords[1]}|{apiCallCoords[2]},{apiCallCoords[3]}&key=AIzaSyCKVHJorOdRlgIFOEC9gZ4AJ2WAXrlligE");
                }
                else
                {
                    response = await client.GetAsync($"https://maps.googleapis.com/maps/api/place/textsearch/json?input=movie_theaters_{city}&inputtype=textquery&fields=photos,formatted_address,name,opening_hours,rating&locationbias=&key=AIzaSyCKVHJorOdRlgIFOEC9gZ4AJ2WAXrlligE");
                }

                if (response.IsSuccessStatusCode)
                {
                    string jsonString2 = await response.Content.ReadAsStringAsync();
                    JsonTextReader reader2 = new JsonTextReader(new StringReader(jsonString2));

                    while (reader2.Read())
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        var theaterToken = JToken.Load(reader2);
                        {
                            string apiNodeString;
                            if (city == null)
                            {
                                apiNodeString = "candidates";
                            }
                            else
                            {
                                apiNodeString = "results";
                            }
                            foreach (var child in theaterToken[apiNodeString])
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

            int radius = int.Parse(Request.Form["radiusSet"]);
            sett.radiusSetting = 21 - radius;

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async void OnPostSetSearchCity()
        {
            MoviesForEveryone.Models.UserSettings setting = _context.Settings.Where(u => u.userId == 0).FirstOrDefault(); //RESET WHEN USERS ARE IMPLEMENTED
            city = Request.Form["cityName"].ToString();
            setting.setCity = city;

            await _context.SaveChangesAsync();
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

        public bool CitySet()
        {          
            return ( city != null);
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

        public string getCity()
        {
            return city;
        }

        public List<Models.Theater> getLocalTheaters()
        {
            return localTheaters;
        }

        private string[] getCoords(string _lat, string _long, int _radius)
        {
            double latDub = double.Parse(_lat);
            double longDub = double.Parse(_long);
            //Each degree latitude is about 69 miles, 0.0006213712 is the constant to convert meters to miles
            double degrees = (_radius * 0.0006213712) / 69;

            string[] coords = { Math.Round((latDub - degrees), 5).ToString(), Math.Round((longDub - degrees), 5).ToString(), Math.Round((latDub + degrees), 5).ToString(), Math.Round((longDub + degrees), 5).ToString() };

            return coords;
        }

        private string city;
        private string latitude;
        private string longitude;
        private bool showOptionsIndicator;
        private int userSetRadius;
        private List<Models.Theater> localTheaters;
        private Models.MoviesDbContext _context;
    }
}
