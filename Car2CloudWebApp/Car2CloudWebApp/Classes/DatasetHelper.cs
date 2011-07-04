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
            EntityID = 0,
            TripId = 1,
            VehicleId = 2,
            Timestamp = 3,
            Latitude = 4,
            Longtitude = 5,
            Height =6,
            Speed = 7,
            EngineRPM = 8,
            Temperature = 9,
            AmbientTemperature = 10,
            EngineLoad = 11,
            FuelEconomy = 12,
            LongTermFuelTrim = 13,
            ShortTermFuelTrip = 14

        }
    }
}