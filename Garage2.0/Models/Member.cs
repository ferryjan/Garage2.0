using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNr { get; set; }
        public DateTime RegDate { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}