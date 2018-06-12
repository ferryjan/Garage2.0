using Garage2._0.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{

    public enum Colors
    {
        Red,
        Blue,
        White,
        Silver,
        Black,
        Grey,
        Brown,
        Green
    }

    public class Vehicle
    {
        public int Id { get; set; }


        [Required]
        [Display(Name = "Registration Number")]
        [StringLength(6, ErrorMessage = "No more than 6 letters are allowed here!")]
        [RegNumValidator]
        public string RegNum { get; set; }

        [EnumDataType(typeof(Colors))]
        public Colors Color { get; set; }

        [Required]
        [Display(Name = "Check-in Time")]
        public DateTime CheckInTime { get; set; }

        [Range(0, 100, ErrorMessage = "Range: 0 - 100")]
        [Display(Name = "Number of Tires")]
        public int NumOfTires { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "No more than 30 letters are allowed here!")]
        public string Model { get; set; }

        [Display(Name = "Parking Position")]
        public int ParkingSpaceNum { get; set; }

        public int TypeId { get; set; }
        virtual public VehicleType VehicleType { get; set; }

        public int MemberId { get; set; }
        virtual public Member Member { get; set; }
    }
}
