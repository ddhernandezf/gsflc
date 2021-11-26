using System.Reflection;
using Newtonsoft.Json;

namespace Transporte
{
    public static class Log
    {
        internal static log4net.ILog log { get; } = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void DataToLog(string userName, string className, string methodName, object data)
        {
            log.Info(JsonConvert.SerializeObject(new { User = userName, Class = className, Method = methodName, Data = data }));
        }

        public static void ErrorToLog(string userName, object classObject, MethodBase methodName, string error, object parameters = null)
        {
            if (parameters != null)
                log.Error(JsonConvert.SerializeObject(new { User = userName, Class = classObject.GetType().Name, Method = methodName.Name, Error = error, Parameters = parameters }));
            else
                log.Error(JsonConvert.SerializeObject(new { User = userName, Class = classObject.GetType().Name, Method = methodName.Name, Error = error }));
        }
    }
}
