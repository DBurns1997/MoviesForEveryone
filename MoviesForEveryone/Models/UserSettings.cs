using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoviesForEveryone.Models
{
    public class UserSettings
    {
        [Key]
        public  int settingsKey { get; set; }
        public int radiusSetting { get; set; }
        public int userId { get; set; }
        public string setCity { get; set; }
    }
}
