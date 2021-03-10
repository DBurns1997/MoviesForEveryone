using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoviesForEveryone.Models
{
    public class PositiveKeys
    {
        [Key]
        public int positiveKeyKey { get; set; }
        public string keyword { get; set; }
        public string userId { get; set; }
    }
}
