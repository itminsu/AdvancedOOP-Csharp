using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLib
{
    public class Logger
    {
        public void WriteLog(string FileName, String Msg)
        {
            try
            {
                if (File.Exists(FileName))
                {
                    StreamWriter file = new StreamWriter(FileName, true);
                    file.WriteLine(Msg);
                    file.Close();
                }
                else
                {
                    // Write the string to a file.
                    System.IO.StreamWriter file = new System.IO.StreamWriter(FileName);
                    file.WriteLine(Msg);
                    file.Close();
                }

            }
            catch
            {

            }

        }// end WriteLog

    }// end class

}// end namespace
