using FlickMeter.Data.Entities;
using FlickMeter.Data.Mappers;
using FlickMeter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickMeter.Infrastructure.Extensions;

namespace FlickMeter.Data
{
    public class FlickMeterContext : DbContext, IDbContext
    {
        public FlickMeterContext() : base("flickMeterConnection")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FlickMeterContext, FlickMeterContextMigrationConfiguration>());
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieArtist> MovieArtists { get; set; }
        public DbSet<MovieReview> MovieReviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArtistMapper());
            modelBuilder.Configurations.Add(new MovieMapper());
            modelBuilder.Configurations.Add(new MovieArtistMapper());
            modelBuilder.Configurations.Add(new MovieReviewMapper());
            modelBuilder.Configurations.Add(new ReviewerMapper());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            this.ApplyStateChanges();
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}
