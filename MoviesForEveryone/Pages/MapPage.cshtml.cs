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
                                localTheaters.Add(theaterToAdd);
                            }
                        }
                    }
                }
            }

            return RedirectToPage();
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

        public bool getOpt()
        {
            return showOptionsIndicator;
        }

        public List<Models.Theater> getLocalTheaters()
        {
            return localTheaters;
        }

        protected string latitude;
        protected string longitude;
        protected bool showOptionsIndicator;
        protected List<Models.Theater> localTheaters;
    }
}
