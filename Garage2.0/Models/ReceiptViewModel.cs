using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class ReceiptViewModel
    {
        [EnumDataType(typeof(VehicleTypes))]
        [Display(Name = "Vehicle Type")]
        public VehicleTypes VehicleType { get; set; }

        [Display(Name = "Registration Number")]
        public string RegNum { get; set; }

        public string Color { get; set; }

        [Display(Name = "Number of Tires")]
        public int NumOfTires { get; set; }

        public string Model { get; set; }

        [Display(Name = "Check-in Time")]
        public DateTime CheckInTime { get; set; }

        [Display(Name = "Check-out Time")]
        public DateTime CheckOutTime { get; set; }

        [Display(Name = "Time Parked")]
        public TimeSpan TimeParked { get; set; }

        public int Price { get; set; }
    }
}
