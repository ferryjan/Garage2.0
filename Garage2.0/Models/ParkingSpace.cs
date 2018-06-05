using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Garage2._0.Models
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }

        public ParkingSpace() {
            Vehicles = new HashSet<Vehicle>();
        }
    }
}
