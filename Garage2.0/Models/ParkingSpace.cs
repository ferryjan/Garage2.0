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

        public ParkingSpace(int capacity) {
            db = new Garage2_0Context();
            ps = new int[capacity];
            GetList();
            Capacity = capacity;
        }

        public void GetList() {
            Array.Clear(ps, 0, ps.Length);
            foreach (var item in db.Vehicles) {
                if (item.VehicleType == VehicleTypes.Truck) {
                    ps[item.ParkingSpaceNum] = 3;
                    ps[item.ParkingSpaceNum + 1] = 3;
                } else if (item.VehicleType == VehicleTypes.Motorcycle) {
                    ps[item.ParkingSpaceNum] += 1;
                } else {
                    ps[item.ParkingSpaceNum] = 3;
                }
            }
        }

        public int GetNumOfAvailableSpace() {
            var cap = Capacity;
            foreach (var item in ps) {
                if (item != 0) {
                    cap -= 1;
                }
            }
            return cap;
        }

        public bool HasSpaceForMotorCycle() {
            for (int i = 0; i < Capacity; i++) {
                if (ps[i] < 3) {
                    return true;
                }
            }
            return false;
        }

        public void RemoveFromParkingSpace(Vehicle vehicle) {
            if (vehicle.VehicleType == VehicleTypes.Truck) {
                ps[vehicle.ParkingSpaceNum] = 0;
                ps[vehicle.ParkingSpaceNum + 1] = 0;
            } else if (vehicle.VehicleType == VehicleTypes.Motorcycle) {
                ps[vehicle.ParkingSpaceNum] -= 1;
            } else {
                ps[vehicle.ParkingSpaceNum] = 0;
            }
        }

        public int AssignParkingSpace(Vehicle vehicle) {
            if (vehicle.VehicleType == VehicleTypes.Motorcycle) {
                for (int i = 0; i < Capacity; i++) {
                    if (ps[i] < 3) {
                        ps[i] += 1;
                        return i;
                    }
                }
            } else if (vehicle.VehicleType == VehicleTypes.Truck) {
                for (int i = 0; i < Capacity - 1; i++) {
                    if (ps[i] == 0 && ps[i + 1] == 0) {
                        ps[i] = 3;
                        ps[i + 1] = 3;
                        return i;
                    }
                }
            } else {
                for (int i = 0; i < Capacity; i++) {
                    if (ps[i] == 0) {
                        ps[i] = 3;
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}
