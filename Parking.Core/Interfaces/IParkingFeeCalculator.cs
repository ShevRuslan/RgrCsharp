using Parking.Core.Models;

namespace Parking.Core.Interfaces
{
    internal interface IParkingFeeCalculator
    {
        bool CanCalculate(Vehicle vehicle);
        decimal CalculateFee(Vehicle vehicle);
    }
}
