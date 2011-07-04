using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ictlab.Classes
{
    static class DatasetHelper
    {
        public enum CarDataColumn
        {
            EntityID = 1,
            TripId = 2,
            VehicleId = 14,
            Timestamp = 0,
            Latitude = 3,
            Longtitude = 4,
            Height =5,
            Speed = 6,
            EngineRPM = 7,
            Temperature = 8,
            AmbientTemperature = 9,
            EngineLoad = 10,
            FuelEconomy = 11,
            LongTermFuelTrim = 12,
            ShortTermFuelTrip = 13

        }
    }
}