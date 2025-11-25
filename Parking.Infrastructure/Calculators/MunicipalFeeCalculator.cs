using Parking.Core.Interfaces;
using Parking.Core.Models;
using Parking.Infrastructure.Config;

namespace Parking.Infrastructure.Calculators
{
    public class MunicipalFeeCalculator : IParkingFeeCalculator
    {
        private readonly TariffConfig _tariff;


        public MunicipalFeeCalculator(TariffConfig tariff)
        {
            _tariff = tariff;
        }


        public bool CanCalculate(Vehicle vehicle) => vehicle is not null && vehicle.Type == Parking.Core.Enums.VehicleType.Municipal;


        public decimal CalculateFee(Vehicle vehicle)
        {
            var hours = (decimal)vehicle.GetHoursStayed();
            var baseFee = Math.Round(hours * _tariff.MunicipalHourlyRate, 2);
            var discounted = Math.Round(baseFee * _tariff.MunicipalDiscountCoefficient, 2);
            return Math.Max(0, discounted);
        }
    }
}
