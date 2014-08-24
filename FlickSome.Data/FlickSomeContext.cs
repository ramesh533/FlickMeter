using FlickSome.Data.Entities;
using FlickSome.Data.Mappers;
using FlickSome.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickSome.Infrastructure.Extensions;

namespace FlickSome.Data
{
    public class FlickSomeContext : DbContext, IDbContext
    {
        public FlickSomeContext() : base("flickSomeConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FlickSomeContext, FlickSomeContextMigrationConfiguration>());
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieArtist> MovieArtists { get; set; }
        public DbSet<MovieReview> MovieReviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public bool IgnoreObjectState = false;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArtistMapper());
            modelBuilder.Configurations.Add(new MovieMapper());
            modelBuilder.Configurations.Add(new MovieArtistMapper());
            modelBuilder.Configurations.Add(new MovieReviewMapper());
            modelBuilder.Configurations.Add(new ReviewerMapper());
            modelBuilder.Configurations.Add(new UserProfileMapper());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            if (!IgnoreObjectState)
            {
                this.ApplyStateChanges();
            }
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}
