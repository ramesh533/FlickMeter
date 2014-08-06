using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickMeter.Data
{
    public class FlickMeterContextMigrationConfiguration : DbMigrationsConfiguration<FlickMeterContext>
    {
        public FlickMeterContextMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FlickMeterContext context)
        {
            new FlickMeterDataSeeder(context).Seed();
            base.Seed(context);
        }
    }
}
