using Parking.Core.Enums;

namespace Parking.Core.Models
{
    internal class ParkingSession
    {
        public VehicleType VehicleType { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }

        public double GetTotalHours()
        {
            return (ExitTime - EntryTime).TotalHours;
        }
    }
}
