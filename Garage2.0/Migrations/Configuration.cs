namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage2._0.Models.Garage2_0Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Garage2_0Context context) {
            context.Vehicles.AddOrUpdate(
                v => v.RegNum,
                new Vehicle { VehicleType = VehicleTypes.Car, Color = "Blue", RegNum = "ABC123", Model = "Volvo", NumOfTires = 4, CheckInTime = DateTime.Now },
                new Vehicle { VehicleType = VehicleTypes.Motorcycle, Color = "Black", RegNum = "XYZ789", Model = "Yamaha", NumOfTires = 2, CheckInTime = DateTime.Now.AddDays(-1) },
                new Vehicle { VehicleType = VehicleTypes.Truck, Color = "White", RegNum = "AAA111", Model = "Scania", NumOfTires = 8, CheckInTime = DateTime.Now.AddDays(-2) }
            );
        }
    }
}
