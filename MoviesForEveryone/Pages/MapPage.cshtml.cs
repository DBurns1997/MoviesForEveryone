using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;

namespace MoviesForEveryone.Pages
{
    public class MapPageModel : PageModel
    {
        public void OnGet()
        {
            showOptionsIndicator = false;
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
                            latitutde = reader.Value.ToString();
                        }
                        if (reader.Value.ToString() == "lon")
                        {
                            reader.Read();
                            longitude = reader.Value.ToString();
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
            return latitutde;
        }

        public string getLong()
        {
            return longitude;
        }

        public bool getOpt()
        {
            return showOptionsIndicator;
        }

        protected string latitutde;
        protected string longitude;
        protected bool showOptionsIndicator;
    }
}
