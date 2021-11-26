using Microsoft.Extensions.Configuration;

namespace Transporte.BL.Configurations
{
    public class ConnectionString
    {
        private IConfiguration appsettingsFile { get; }

        public ConnectionString(IConfiguration configuration)
        {
            appsettingsFile = configuration;
        }

        public string transport => appsettingsFile.GetValue<string>("connectionString:transport");
    }
}
