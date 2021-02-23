using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoviesForEveryone.Models
{
    public class NegativeKeys
    {
        [Key]
        public int negativeKeyKey { get; set; }
        public string keyword { get; set; }
        public int userID { get; set; }
    }
}
