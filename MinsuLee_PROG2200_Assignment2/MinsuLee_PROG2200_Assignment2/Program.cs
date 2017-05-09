using ChatLib;
using LoggerLib;
using Microsoft.Practices.Unity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinsuLee_PROG2200_Assignment2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1(new Client(new Logger())));

            ////Ioc Container
            //UnityContainer container = new UnityContainer();
            ////container.RegisterType<ILoggingService, Logger>();
            //container.RegisterType<ILoggingService, Log4net>();
            //Application.Run(container.Resolve<Form1>());

            //ninject
            IKernel kernel = new StandardKernel();
            //kernel.Bind<ILoggingService>().To<Logger>();
            kernel.Bind<ILoggingService>().To<Log4net>();
            Application.Run(kernel.Get<Form1>());

        }
    }
}
