namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Controllers;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage2_0Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Garage2_0Context context) {
            //context.Vehicles.AddOrUpdate(
            //    v => v.RegNum,
            //    new Vehicle { VehicleType = VehicleTypes.Car, Color = Colors.Blue, RegNum = "ABC123", Model = "Volvo", NumOfTires = 4, CheckInTime = DateTime.Now },
            //    new Vehicle { VehicleType = VehicleTypes.Motorcycle, Color = Colors.Black, RegNum = "XYZ789", Model = "Yamaha", NumOfTires = 2, CheckInTime = DateTime.Now.AddDays(-1) },
            //    new Vehicle { VehicleType = VehicleTypes.Truck, Color = Colors.Blue, RegNum = "AAA111", Model = "Scania", NumOfTires = 8, CheckInTime = DateTime.Now.AddDays(-2) }
            //);
            for (int i = 1; i <= VehiclesController.ParkingCapacity; i++) {
                context.ParkingSpaces.AddOrUpdate(p => p.Number, new ParkingSpace { Number = i });
            }
        }
    }
}
