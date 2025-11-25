namespace Parking.Infrastructure.Config
{
    internal record TariffConfig
    (
        decimal CarHourlyRate,
        decimal TruckBaseHourlyRate,
        decimal TruckVolumeCoefficient,
        decimal MunicipalHourlyRate,
        decimal MunicipalDiscountCoefficient
    );
}
