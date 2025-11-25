using Parking.Core.Enums;
using Parking.Core.Models;

namespace Parking.Core.Services
{
    internal class ParkingCalculator
    {
        // Базовые тарифы, можно менять при необходимости
        private readonly Dictionary<VehicleType, double> _hourRates = new()
        {
            { VehicleType.Car, 150.0 },
            { VehicleType.Truck, 300.0 },
        };

        // Допустим, после 24 часов идёт фиксированный суточный тариф
        private const double DailyRateMultiplier = 0.8; // 80% от суммы почасовой тарификации

        public double Calculate(ParkingSession session)
        {
            double hours = session.GetTotalHours();
            if (hours <= 0) return 0;

            double baseRate = _hourRates[session.VehicleType];

            if (hours <= 24)
                return hours * baseRate;

            double firstDay = 24 * baseRate;
            double nextHours = hours - 24;

            double nextDayRate = 24 * baseRate * DailyRateMultiplier;
            int fullDays = (int)(nextHours / 24);

            double remainingHours = nextHours - fullDays * 24;
            double remainingCost = remainingHours * baseRate;

            return firstDay + fullDays * nextDayRate + remainingCost;
        }
    }
}
