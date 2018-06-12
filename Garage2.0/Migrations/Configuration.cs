namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage2._0.Models.Garage2_0Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Garage2_0Context context) { 
            //context.VehicleTypes.AddOrUpdate(
            //    new VehicleType { Type= "Car"},
            //    new VehicleType { Type = "Van" },
            //    new VehicleType { Type = "Truck" },
            //    new VehicleType { Type = "Motorcycle" }
            //    );
        }
    }
}
