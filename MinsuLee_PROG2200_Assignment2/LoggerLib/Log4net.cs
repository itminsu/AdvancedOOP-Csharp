using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)] 
//set on assemblyInfo.cs

namespace LoggerLib
{
    public class Log4net : ILoggingService
    {
        //get logger
        private static readonly ILog log = LogManager.GetLogger("Form1.cs");

        public void Log(string message)
        {
            log.Info(message);
        }
    }
}

