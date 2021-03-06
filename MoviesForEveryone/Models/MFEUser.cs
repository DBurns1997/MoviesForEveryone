using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MoviesForEveryone.Models
{
    public class MFEUser : IdentityUser
    {
        public MFEUser()
        {
            positiveKeys = new HashSet<PositiveKeys>();
            negativeKeys = new HashSet<NegativeKeys>();
            opinions = new HashSet<MovieOpinions>();
            settings = new UserSettings();
        }

        public virtual ICollection<PositiveKeys> positiveKeys { get; set; }
        public virtual ICollection<NegativeKeys> negativeKeys { get; set; }
        public virtual UserSettings settings { get; set; }
        public virtual ICollection<MovieOpinions> opinions { get; set;}
    }
}
