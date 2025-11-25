using Parking.Core.Models;

namespace Parking.Core.Interfaces
{
    public interface IParkingFeeCalculator
    {
        bool CanCalculate(Vehicle vehicle);
        decimal CalculateFee(Vehicle vehicle);
    }
}
