using Parking.Infrastructure.Config;
using System.Text.Json;

namespace Parking.Infrastructure
{
    public static class TariffLoader
    {
        public static TariffConfig LoadFromJson(string path)
        {
            var json = File.ReadAllText(path);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;


            var carRate = root.GetProperty("CarHourlyRate").GetDecimal();
            var truckBase = root.GetProperty("TruckBaseHourlyRate").GetDecimal();
            var truckCoef = root.GetProperty("TruckVolumeCoefficient").GetDecimal();
            var munRate = root.GetProperty("MunicipalHourlyRate").GetDecimal();
            var munDisc = root.GetProperty("MunicipalDiscountCoefficient").GetDecimal();


            return new TariffConfig(carRate, truckBase, truckCoef, munRate, munDisc);
        }
    }
}
