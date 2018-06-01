using Garage2._0.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class ParkingReceipt
    {
        public int Id { get; set; }

        [Display(Name = "Registration Number")]
        public string RegNum { get; set; }

        [Display(Name = "Check-in Time")]
        public DateTime ParkTime { get; set; }

        [Display(Name = "Check-out Time")]
        public DateTime CheckOut { get; set; }

        [Display(Name = "Time Parked")]
        public string TimeParked { get; set; }

        [Display(Name = "Parking Price (5 SEK/ 15 Min)")]
        public string Price { get; set; }
    }
}
