using Parking.Core.Interfaces;
using Parking.Core.Models;
using Parking.Infrastructure.Config;

namespace Parking.Infrastructure.Calculators
{
    public class CarFeeCalculator : IParkingFeeCalculator
    {
        private readonly TariffConfig _tariff;


        public CarFeeCalculator(TariffConfig tariff)
        {
            _tariff = tariff;
        }


        public bool CanCalculate(Vehicle vehicle) => vehicle is not null && vehicle.Type == Parking.Core.Enums.VehicleType.Car;


        public decimal CalculateFee(Vehicle vehicle)
        {
            var hours = (decimal)vehicle.GetHoursStayed();
            return Math.Max(0, Math.Round(hours * _tariff.CarHourlyRate, 2));
        }
    }
}
