using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickSome.Data
{
    public class FlickSomeContextMigrationConfiguration : DbMigrationsConfiguration<FlickSomeContext>
    {
        public FlickSomeContextMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FlickSomeContext context)
        {
            new FlickSomeDataSeeder(context).Seed();
            base.Seed(context);
        }
    }
}
