using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class VehicleType
    {
        [Key]
        public int TypeId { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}