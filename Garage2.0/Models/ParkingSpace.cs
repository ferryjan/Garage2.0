using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class ParkingSpace
    {
        private int[] ps;
        public int Capacity { get; set; }
        public Garage2_0Context db;

        public ParkingSpace(int capacity)
        {
            db = new Garage2_0Context();
            ps = new int[capacity];
            GetList();
            Capacity = capacity;
        }

        public void GetList()
        {
            Array.Clear(ps, 0, ps.Length);
            foreach (var item in db.Vehicles)
            {
                if (item.VehicleType.ToString() == "Car" || item.VehicleType.ToString() == "Van")
                {
                    ps[item.ParkingSpaceNum] = 3;
                }
                if (item.VehicleType.ToString() == "Truck")
                {
                    ps[item.ParkingSpaceNum] = 3;
                    ps[item.ParkingSpaceNum + 1] = 3;
                }
                if (item.VehicleType.ToString() == "Motorcycle")
                {
                    ps[item.ParkingSpaceNum] += 1;
                }
            }
        }

        public int GetNumOfAvailableSpace()
        {
            var cap = Capacity;
            foreach (var item in ps)
            {
                if (item != 0)
                {
                    cap -= 1;
                }
            }
            return cap;
        }

        public void RemoveFromParkingSpace(Vehicle vehicle)
        {
            if (vehicle.VehicleType.ToString() == "Car" || vehicle.VehicleType.ToString() == "Van")
            {
                ps[vehicle.ParkingSpaceNum] = 0;
            }
            if (vehicle.VehicleType.ToString() == "Truck")
            {
                ps[vehicle.ParkingSpaceNum] = 0;
                ps[vehicle.ParkingSpaceNum+1] = 0;
            }
            if (vehicle.VehicleType.ToString() == "Motorcycle")
            {
                ps[vehicle.ParkingSpaceNum] = ps[vehicle.ParkingSpaceNum] - 1;
            }
        }

        public int AssignParkingSpace(Vehicle vehicle)
        {
            if (vehicle.VehicleType.ToString() == "Car" || vehicle.VehicleType.ToString() == "Van")
            {
                for (int i = 0; i < Capacity; i++)
                {
                    if (ps[i] == 0)
                    {
                        ps[i] = 3;
                        return i;
                    }
                }
            }

            if (vehicle.VehicleType.ToString() == "Motorcycle")
            {
                for (int i = 0; i < Capacity; i++)
                {
                    if (ps[i] > 0 && ps[i] < 3)
                    {
                        ps[i] += 1;
                        return i;
                    }
                }
                for (int i = 0; i < Capacity; i++)
                {
                    if (ps[i] == 0)
                    {
                        ps[i] += 1;
                        return i;
                    }
                }
            }

            if (vehicle.VehicleType.ToString() == "Truck")
            {
                for (int i = 0; i < Capacity - 1; i++)
                {
                    if (ps[i] == 0)
                    {
                        if (ps[i + 1] == 0)
                        {
                            ps[i] = 3;
                            ps[i + 1] = 3;
                            return i;
                        }
                    }
                }
            }
            return -1;
        }
    }
}