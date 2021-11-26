using Microsoft.Extensions.Configuration;
using Transporte.BL.Configurations;

namespace Transporte.BL
{
    public class Configuration
    {
        private IConfiguration appsettingsFile { get; }

        public Configuration(IConfiguration configuration)
        {
            appsettingsFile = configuration;
        }

        public ConnectionString connectionString => new ConnectionString(this.appsettingsFile);
    }
}
