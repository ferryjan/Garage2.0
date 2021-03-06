﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Garage2._0.Models
{
    public class ParkingSpace
    {
        public int[] ps;
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
                if (item.TypeId == 1 || item.TypeId == 2)
                {
                    ps[item.ParkingSpaceNum] = 3;
                }
                if (item.TypeId == 3)
                {
                    ps[item.ParkingSpaceNum] = 3;
                    ps[item.ParkingSpaceNum + 1] = 3;
                }
                if (item.TypeId == 4)
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

        public bool HasSpaceForMotorCycle()
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (ps[i] > 0 && ps[i] < 3)
                {
                    return true;
                }
            }
            return false;

        }

        public void RemoveFromParkingSpace(Vehicle vehicle)
        {
            if (vehicle.TypeId == 1 || vehicle.TypeId == 2)
            {
                ps[vehicle.ParkingSpaceNum] = 0;
            }
            if (vehicle.TypeId == 3)
            {
                ps[vehicle.ParkingSpaceNum] = 0;
                ps[vehicle.ParkingSpaceNum+1] = 0;
            }
            if (vehicle.TypeId == 4)
            {
                ps[vehicle.ParkingSpaceNum] = ps[vehicle.ParkingSpaceNum] - 1;
            }
        }

        public int AssignParkingSpace(Vehicle vehicle)
        {
            if (vehicle.TypeId == 1 || vehicle.TypeId == 2)
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

            if (vehicle.TypeId == 4)
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

            if (vehicle.TypeId == 3)
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