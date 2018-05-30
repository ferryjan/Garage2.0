using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public virtual int TypeID
        {
            get
            {
                return (int)this.VehicleType;
            }
            set
            {
                VehicleType = (VehicleTypes)value;
            }
        }

        [EnumDataType(typeof(VehicleTypes))]
        [Display(Name = "Vehicle Type")]
        public VehicleTypes VehicleType { get; set; }

        public enum VehicleTypes
        {
            Motocycle,
            Car,
            Truck,
            Van
        }

        [Required]
        [Display(Name = "Registeration Number")]
        [StringLength(6, ErrorMessage = "No more than 6 letters are allowed here!")]
        public string RegNum { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        [Display(Name = "Parking Time")]
        public DateTime ParkTime { get; set; }

        [Range(0, 100, ErrorMessage = "Range: 0 - 100")]
        [Display(Name = "Number of Tires")]
        public int NumOfTires { get; set; }

        [Required]
        [Display(Name = "Model")]
        [StringLength(30, ErrorMessage = "No more than 30 letters are allowed here!")]
        public string Model { get; set; }
    }
}