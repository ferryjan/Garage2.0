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
        [Display(Name = "Member ID")]
        public int MemberId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        [Display(Name = "Tel.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Tel. must be numeric")]
        public string PhoneNr { get; set; }

        [Display(Name = "Registeration Date")]
        public DateTime RegDate { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}