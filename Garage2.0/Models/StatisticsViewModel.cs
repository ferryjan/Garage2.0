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
        [Display(Name = "Total number of vehicles")]
        public int NumberOfVehicles { get; }

        [Display(Name = "Total number of wheels")]
        public int NumberOfWheels { get; }

        [UIHint("EnumIntDictionary")]
        [Display(Name = "Number of vehicles per color")]
        public Dictionary<Enum, int> NumberOfVehiclesPerColor { get; }

        [UIHint("EnumIntDictionary")]
        [Display(Name = "Number of vehicles per type")]
        public Dictionary<Enum, int> NumberOfVehiclesPerType { get; }

        [Display(Name = "Earliest check-in time")]
        public DateTime? EarliestCheckInTime { get; }

        [Display(Name = "Most recent check-in time")]
        public DateTime? LatestCheckInTime { get; }

        [UIHint("Currency")]
        [Display(Name = "Total parking price (5 SEK / 15 min)")]
        public int TotalPrice { get; }

        public StatisticsViewModel(List<Vehicle> vehicles) {
            NumberOfVehicles = vehicles.Count();
            NumberOfWheels = vehicles.Sum(v => v.NumOfTires);
            NumberOfVehiclesPerColor = new Dictionary<Enum, int>();
            foreach (Colors color in Enum.GetValues(typeof(Colors))) {
                NumberOfVehiclesPerColor.Add(color, vehicles.Where(v => v.Color == color).Count());
            }
            NumberOfVehiclesPerType = new Dictionary<Enum, int>();
            //foreach (VehicleTypes type in Enum.GetValues(typeof(VehicleTypes))) {
            //    NumberOfVehiclesPerType.Add(type, vehicles.Where(v => v.VehicleType == type).Count());
            //}
            EarliestCheckInTime = vehicles.OrderBy(v => v.CheckInTime).FirstOrDefault()?.CheckInTime;
            LatestCheckInTime = vehicles.OrderByDescending(v => v.CheckInTime).FirstOrDefault()?.CheckInTime;
            foreach (Vehicle vehicle in vehicles) {
                TotalPrice += VehicleHelpers.CalculateParkingPrice(vehicle.CheckInTime, DateTime.Now);
            }
        }
    }
}
