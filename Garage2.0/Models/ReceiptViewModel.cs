using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Garage2._0.Helpers;

namespace Garage2._0.Models
{
    public class ReceiptViewModel
    {
        [EnumDataType(typeof(VehicleTypes))]
        [Display(Name = "Vehicle Type")]
        public VehicleTypes VehicleType { get; set; }

        [Display(Name = "Registration Number")]
        public string RegNum { get; set; }

        public Colors Color { get; set; }

        [Display(Name = "Number of Tires")]
        public int NumOfTires { get; set; }

        public string Model { get; set; }

        [Display(Name = "Check-in Time")]
        public DateTime CheckInTime { get; set; }

        [Display(Name = "Check-out Time")]
        public DateTime CheckOutTime { get; set; }

        [UIHint("TimeSpan")]
        [Display(Name = "Time Parked")]
        public TimeSpan TimeParked { get; set; }

        [UIHint("Currency")]
        [Display(Name = "Parking Price (5 SEK / 15 min)")]
        public int Price { get; set; }

        public ReceiptViewModel(Vehicle vehicle) {
            VehicleType = vehicle.VehicleType;
            RegNum = vehicle.RegNum;
            Color = vehicle.Color;
            NumOfTires = vehicle.NumOfTires;
            Model = vehicle.Model;
            CheckInTime = vehicle.CheckInTime;
            CheckOutTime = DateTime.Now;
            TimeParked = CheckOutTime - CheckInTime;
            Price = VehicleHelpers.CalculateParkingPrice(TimeParked) * vehicle.ParkingSpaces.Count;
        }
    }
}
