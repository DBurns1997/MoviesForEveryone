using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace MoviesForEveryone.Models
{
    public class MoviesDbContext : IdentityDbContext<MFEUser>
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
