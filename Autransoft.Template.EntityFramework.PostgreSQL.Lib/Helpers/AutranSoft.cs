using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Autransoft.Template.EntityFramework.PostgreSQL.Lib.Helpers
{
    public static class AutranSoft
    {
        public static DTOs.Autransoft LoadDatabase(IServiceCollection serviceColletion, IConfiguration configuration)
        {
            serviceColletion.Configure<DTOs.Autransoft>(configuration.GetSection("Autransoft"));

            var appSettings = new DTOs.Autransoft();
            new ConfigureFromConfigurationOptions<DTOs.Autransoft>(configuration.GetSection("Autransoft")).Configure(appSettings);

            return appSettings;
        }
    }
}