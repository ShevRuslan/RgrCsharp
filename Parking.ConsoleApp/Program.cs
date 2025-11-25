using Microsoft.Extensions.DependencyInjection;
using Parking.Core.Models;
using Parking.Infrastructure;
using Parking.Infrastructure.Calculators;
using Parking.Infrastructure.Config;
using Parking.Infrastructure.Factory;


Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("=== Расчёт стоимости парковки ===");
Console.WriteLine("Загрузка конфигурации тарифов...");

string tariffPath = Path.Combine(AppContext.BaseDirectory, "tariffs.json");

if (!File.Exists(tariffPath))
{
    Console.WriteLine("Файл tariffs.json не найден! Создаю пример...");
    File.WriteAllText(tariffPath, """
{
    "CarHourlyRate": 100,
    "TruckBaseHourlyRate": 200,
    "TruckVolumeCoefficient": 5,
    "MunicipalHourlyRate": 80,
    "MunicipalDiscountCoefficient": 0.5
}
""");
}

TariffConfig tariff = TariffLoader.LoadFromJson(tariffPath);

// DI контейнер
var services = new ServiceCollection();

var registry = new ParkingCalculatorRegistry();

// Регистрируем калькуляторы
registry.Register(new CarFeeCalculator(tariff));
registry.Register(new TruckFeeCalculator(tariff));
registry.Register(new MunicipalFeeCalculator(tariff));

// Подгружаем плагины
string pluginDir = Path.Combine(AppContext.BaseDirectory, "plugins");
foreach (var plugin in PluginLoader.LoadPlugins(pluginDir))
{
    registry.Register(plugin);
}

services.AddSingleton(registry);

var provider = services.BuildServiceProvider();

Console.WriteLine("Готово. Можно пользоваться!");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Выберите тип транспорта:");
    Console.WriteLine("1 — Легковой автомобиль");
    Console.WriteLine("2 — Грузовик");
    Console.WriteLine("3 — Муниципальный транспорт");
    Console.WriteLine("0 — Выход");
    Console.Write("Ваш выбор: ");

    string? choice = Console.ReadLine();
    if (choice == "0") break;

    Vehicle? vehicle = null;

    switch (choice)
    {
        case "1":
            vehicle = CreateCar();
            break;

        case "2":
            vehicle = CreateTruck();
            break;

        case "3":
            vehicle = CreateMunicipal();
            break;

        default:
            Console.WriteLine("Ошибка: неизвестный тип.");
            continue;
    }

    var reg = provider.GetRequiredService<ParkingCalculatorRegistry>();
    var calc = reg.Resolve(vehicle);

    if (calc == null)
    {
        Console.WriteLine("Нет калькулятора для данного типа транспорта.");
        continue;
    }

    var fee = calc.CalculateFee(vehicle);
    Console.WriteLine($"Стоимость парковки: {fee} рублей");
}

Console.WriteLine("Работа завершена.");

// ---------------- Вспомогательные методы ----------------

static Car CreateCar()
{
    Console.Write("Введите номер автомобиля: ");
    string lp = Console.ReadLine() ?? "AUTO";

    Console.Write("Сколько часов он простоял? ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal hours)) hours = 1;

    var entered = DateTime.Now.AddHours(-(double)hours);
    return new Car(lp, entered);
}

static Municipal CreateMunicipal()
{
    Console.Write("Введите номер муниципального ТС: ");
    string lp = Console.ReadLine() ?? "MUN";

    Console.Write("Сколько часов он простоял? ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal hours)) hours = 1;

    var entered = DateTime.Now.AddHours(-(double)hours);
    return new Municipal(lp, entered);
}

static Truck CreateTruck()
{
    Console.Write("Введите номер грузовика: ");
    string lp = Console.ReadLine() ?? "TRK";

    Console.Write("Сколько часов он простоял? ");
    if (!double.TryParse(Console.ReadLine(), out double hours)) hours = 1.0;

    Console.Write("Длина (м): ");
    double len = double.TryParse(Console.ReadLine(), out var L) ? L : 6;

    Console.Write("Ширина (м): ");
    double wid = double.TryParse(Console.ReadLine(), out var W) ? W : 2.5;

    Console.Write("Высота (м): ");
    double hei = double.TryParse(Console.ReadLine(), out var H) ? H : 2.5;

    var entered = DateTime.Now.AddHours(-hours);
    return new Truck(lp, entered, len, wid, hei);
}
