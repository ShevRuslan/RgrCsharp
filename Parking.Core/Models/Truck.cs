namespace Parking.Core.Models
{
    internal class Truck : Vehicle
    {
        public double LengthMeters { get; init; }
        public double WidthMeters { get; init; }
        public double HeightMeters { get; init; }


        public Truck(string licensePlate, DateTime enteredAt, double length, double width, double height)
        : base(licensePlate, Enums.VehicleType.Truck, enteredAt)
        {
            LengthMeters = length;
            WidthMeters = width;
            HeightMeters = height;
        }


        public double Volume => LengthMeters * WidthMeters * HeightMeters;
    }
}
