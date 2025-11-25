using Parking.Core.Interfaces;
using Parking.Core.Models;
using Parking.Infrastructure.Config;

namespace Parking.Infrastructure.Calculators
{
    public class TruckFeeCalculator : IParkingFeeCalculator
    {
        private readonly TariffConfig _tariff;


        public TruckFeeCalculator(TariffConfig tariff)
        {
            _tariff = tariff;
        }


        public bool CanCalculate(Vehicle vehicle) => vehicle is Truck;


        public decimal CalculateFee(Vehicle vehicle)
        {
            if (vehicle is not Truck truck)
                throw new ArgumentException("Vehicle is not a truck");


            var hours = (decimal)truck.GetHoursStayed();
            var volumeCoef = (decimal)(Convert.ToDouble(_tariff.TruckVolumeCoefficient) * (double)truck.Volume);
            var rate = _tariff.TruckBaseHourlyRate + volumeCoef;
            return Math.Max(0, Math.Round(hours * rate, 2));
        }
    }
}
