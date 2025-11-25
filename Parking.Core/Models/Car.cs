using Parking.Core.Enums;

namespace Parking.Core.Models
{
    public class Car : Vehicle
    {
        public Car(string licensePlate, DateTime enteredAt)
            : base(licensePlate, VehicleType.Car, enteredAt)
        {
        }
    }
}
