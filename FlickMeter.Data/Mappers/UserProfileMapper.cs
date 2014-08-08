using FlickMeter.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickMeter.Data.Mappers
{
    public class UserProfileMapper : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileMapper()
        {
            this.ToTable("UserProfile");

            this.HasKey(u => u.UserId);
            this.Property(u => u.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(u => u.UserId).IsRequired();

            this.Ignore(u => u.State);
        }
    }
}
