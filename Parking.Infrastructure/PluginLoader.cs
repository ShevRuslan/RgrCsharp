using Parking.Core.Interfaces;
using System.Reflection;

namespace Parking.Infrastructure
{
    public static class PluginLoader
    {
        public static IEnumerable<IParkingFeeCalculator> LoadPlugins(string pluginDirectory)
        {
            if (!Directory.Exists(pluginDirectory))
                yield break;


            foreach (var file in Directory.EnumerateFiles(pluginDirectory, "*.dll"))
            {
                Assembly? asm = null;
                try { asm = Assembly.LoadFrom(file); } catch { continue; }


                if (asm == null) continue;


                foreach (var type in asm.GetTypes())
                {
                    if (type.IsClass && !type.IsAbstract && typeof(IParkingFeeCalculator).IsAssignableFrom(type))
                    {
                        IParkingFeeCalculator? inst = null;
                        try { inst = (IParkingFeeCalculator?)Activator.CreateInstance(type); } catch { }
                        if (inst != null) yield return inst;
                    }
                }
            }
        }
    }
}
