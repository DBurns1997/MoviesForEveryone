using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MoviesForEveryone.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext()
        {
        }

        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
        }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<MovieOpinions> Opinions { get; set; }
        public DbSet<PositiveKeys> PositiveKeys { get; set; }
        public DbSet<NegativeKeys> NegativeKeys {get; set;}
        public DbSet<UserSettings> Settings { get; set; }
    }
}
