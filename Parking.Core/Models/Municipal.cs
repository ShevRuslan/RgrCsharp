using Parking.Core.Enums;

namespace Parking.Core.Models
{
    public class Municipal : Vehicle
    {
        public Municipal(string licensePlate, DateTime enteredAt)
            : base(licensePlate, VehicleType.Municipal, enteredAt)
        {
        }
    }
}
