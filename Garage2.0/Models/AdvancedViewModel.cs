using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class AdvancedViewModel
    {
        public int rows { get; }
        public int[] array { get; }
        public int capacity { get; }
        public Garage2_0Context db;
        public AdvancedViewModel(ParkingSpace park)
        {
            capacity = park.Capacity;
            rows = capacity / 5;
            array = park.ps;
            db = park.db;

            if(capacity%5 != 0)
            {
                rows++;
            }
        }

        public int GetType(int index)
        {
            var vehicle = db.Vehicles.Where(a => a.ParkingSpaceNum == (index)).FirstOrDefault();
            if (vehicle != null)
            {
                if (vehicle.VehicleType.Type == "Car")
                    return 1;
                if (vehicle.VehicleType.Type == "Van")
                    return 2;
                if (vehicle.VehicleType.Type == "Truck")
                    return 3;
                if (vehicle.VehicleType.Type == "Motorcycle")
                    return 4;
                else
                    return 0;
            }
            else
                return 0;
        }

        public int CheckAdjacentBikes(int space)
        {
            int result = 0;

            var vehicles = db.Vehicles.Where(e => e.ParkingSpaceNum == space);

            foreach (var item in vehicles)
            {
                result++;
            }
            return result;
        }
    }
}