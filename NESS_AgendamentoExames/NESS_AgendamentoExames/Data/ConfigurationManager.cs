using Microsoft.Extensions.Configuration;
using System.IO;

namespace NESS_AgendamentoExames.Data
{
    public static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; private set; }
        
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
