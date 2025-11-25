using Parking.Core.Enums;

namespace Parking.Core.Models
{
    internal abstract class Vehicle
    {
        public string LicensePlate { get; init; }
        public VehicleType Type { get; init; }
        public DateTime EnteredAt { get; init; }
        public DateTime? LeftAt { get; set; }


        protected Vehicle(string licensePlate, VehicleType type, DateTime enteredAt)
        {
            LicensePlate = licensePlate;
            Type = type;
            EnteredAt = enteredAt;
        }


        public virtual double GetHoursStayed()
        {
            var end = LeftAt ?? DateTime.Now;
            return Math.Ceiling((end - EnteredAt).TotalHours * 100) / 100.0;
        }
    }
}
