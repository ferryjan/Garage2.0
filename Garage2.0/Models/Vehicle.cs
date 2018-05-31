using Garage2._0.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public enum VehicleTypes
    {
        Motorcycle,
        Car,
        Truck,
        Van
    }

    public class Vehicle
    {
        public int Id { get; set; }

        [EnumDataType(typeof(VehicleTypes))]
        [Display(Name = "Vehicle Type")]
        public VehicleTypes VehicleType { get; set; }

        [Required]
        [Display(Name = "Registration Number")]
        [StringLength(6, ErrorMessage = "No more than 6 letters are allowed here!")]
        [RegNumValidator]
        public string RegNum { get; set; }

        [Required]
        public string Color { get; set; }

        private DateTime? parkTime;
        [Required]
        [Display(Name = "Check-in Time")]
        public DateTime ParkTime
        {
            get { return parkTime ?? DateTime.Now; }
            set { parkTime = value; }
        }

        [Range(0, 100, ErrorMessage = "Range: 0 - 100")]
        [Display(Name = "Number of Tires")]
        public int NumOfTires { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "No more than 30 letters are allowed here!")]
        public string Model { get; set; }

        public DateTime Timestamp { get; set; }

        public Vehicle() {
            Timestamp = DateTime.Now;
        }
    }
}
