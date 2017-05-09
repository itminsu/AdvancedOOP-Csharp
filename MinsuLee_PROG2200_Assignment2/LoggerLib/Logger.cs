using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLib
{
    public class Logger : ILoggingService
    {
        public void Log(string message)
        {
            string fileName = "C:\\Users\\Minsu\\Source\\Repos\\Lee-Minsu-w0293156\\MinsuLee_PROG2200_Assignment2\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string time = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss" + " - ");
            try
            {
                if (File.Exists(fileName))
                {
                    StreamWriter file = new StreamWriter(fileName, true);
                    file.WriteLine(time + message);
                    file.Close();
                }
                else
                {
                    StreamWriter file = new StreamWriter(fileName);
                    file.WriteLine(time + message);
                    file.Close();
                }

            }
            catch
            {

            }
        }// end log

        //public void WriteLog(string FileName, String Msg)
        //{
            

        //}// end WriteLog

    }// end class

}// end namespace
