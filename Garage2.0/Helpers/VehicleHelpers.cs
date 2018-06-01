using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2._0.Helpers
{
    internal static class VehicleHelpers
    {
        internal static int ParkingPricePer15Min = 5;

        internal static int CalculateParkingPrice(DateTime checkInTime, DateTime checkOutTime) {
            return CalculateParkingPrice(checkOutTime - checkInTime);
        }

        internal static int CalculateParkingPrice(TimeSpan timeParked) {
            return (int)Math.Ceiling(timeParked.TotalMinutes / 15) * ParkingPricePer15Min;
        }
    }
}