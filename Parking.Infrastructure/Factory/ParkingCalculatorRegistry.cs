using Parking.Core.Interfaces;
using Parking.Core.Models;

namespace Parking.Infrastructure.Factory
{
    public class ParkingCalculatorRegistry
    {
        private readonly List<IParkingFeeCalculator> _calculators = new();


        public void Register(IParkingFeeCalculator calculator)
        {
            _calculators.Add(calculator);
        }


        public IParkingFeeCalculator? Resolve(Vehicle vehicle)
        {
            return _calculators.FirstOrDefault(c => c.CanCalculate(vehicle));
        }
    }
}
