using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Garage2._0.Helpers;

namespace Garage2._0.Models
{
    public class StatisticsViewModel
    {
        [Display(Name = "Total number of members")]
        public int NumberOfMembers { get; }

        [Display(Name = "Total number of vehicles")]
        public int NumberOfVehicles { get; }

        [Display(Name = "Total number of wheels")]
        public int NumberOfWheels { get; }

        [UIHint("EnumIntDictionary")]
        [Display(Name = "Number of vehicles per color")]
        public Dictionary<Enum, int> NumberOfVehiclesPerColor { get; }


        [Display(Name = "Number of vehicles per type")]
        public Dictionary<string, int> NumberOfVehiclesPerType { get; }

        [Display(Name = "Earliest check-in time")]
        public DateTime? EarliestCheckInTime { get; }

        [Display(Name = "Most recent check-in time")]
        public DateTime? LatestCheckInTime { get; }

        [UIHint("Currency")]
        [Display(Name = "Total parking price (5 SEK / 15 min)")]
        public int TotalPrice { get; }

        public StatisticsViewModel(List<Vehicle> vehicles) {

            Garage2_0Context db = new Garage2_0Context();
            NumberOfMembers = db.Members.Count();
            NumberOfVehicles = vehicles.Count();
            NumberOfWheels = vehicles.Sum(v => v.NumOfTires);
            NumberOfVehiclesPerColor = new Dictionary<Enum, int>();
            foreach (Colors color in Enum.GetValues(typeof(Colors))) {
                NumberOfVehiclesPerColor.Add(color, vehicles.Where(v => v.Color == color).Count());
            }
            NumberOfVehiclesPerType = new Dictionary<string, int>();
            foreach (var item in db.VehicleTypes)
            {
                NumberOfVehiclesPerType.Add(item.Type, vehicles.Where(v => v.VehicleType.Type == item.Type).Count());
            }
            EarliestCheckInTime = vehicles.OrderBy(v => v.CheckInTime).FirstOrDefault()?.CheckInTime;
            LatestCheckInTime = vehicles.OrderByDescending(v => v.CheckInTime).FirstOrDefault()?.CheckInTime;
            foreach (Vehicle vehicle in vehicles) {
                if (vehicle.TypeId != 3)
                {
                    TotalPrice += VehicleHelpers.CalculateParkingPrice(vehicle.CheckInTime, DateTime.Now);
                }
                else
                {
                    TotalPrice += 2 * VehicleHelpers.CalculateParkingPrice(vehicle.CheckInTime, DateTime.Now);
                }
            }
        }
    }
}
