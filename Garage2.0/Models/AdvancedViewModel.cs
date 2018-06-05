using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class AdvancedViewModel
    {
        public VehicleTypes?[] VehicleTypes { get; }
        public int Columns => 5;
        public int Rows { get; }
        private IEnumerable<ParkingSpace> _ParkingSpaces { get; }

        public AdvancedViewModel(IEnumerable<ParkingSpace> parkingSpaces) {
            _ParkingSpaces = parkingSpaces;
            int capacity = _ParkingSpaces.Count();
            VehicleTypes = new VehicleTypes?[capacity + 1];
            foreach (ParkingSpace ps in _ParkingSpaces) {
                VehicleTypes[ps.Number] = ps.Vehicles.FirstOrDefault()?.VehicleType;
            }
            Rows = (int)Math.Ceiling((decimal)capacity / Columns);
        }

        public int CheckAdjacentBikes(int parkingSpaceNumber) {
            return _ParkingSpaces.Single(p => p.Number == parkingSpaceNumber).Vehicles.Count;
        }
    }
}
