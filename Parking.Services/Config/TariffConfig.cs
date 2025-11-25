namespace Parking.Infrastructure.Config
{
    public record TariffConfig
    (
        decimal CarHourlyRate,
        decimal TruckBaseHourlyRate,
        decimal TruckVolumeCoefficient,
        decimal MunicipalHourlyRate,
        decimal MunicipalDiscountCoefficient
    );
}
