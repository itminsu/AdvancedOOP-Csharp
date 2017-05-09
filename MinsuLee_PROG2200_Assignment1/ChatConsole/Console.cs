using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLib;

namespace ChatConsole
{
    class Console
    {
        public void Drive(EndPoint obj)
        {
            string sendMsg = null;
            string recievedMsg = null;

            while (true)
            {
                if (System.Console.KeyAvailable)
                { 
                    if (System.Console.ReadKey(true).Key == ConsoleKey.I)
                    {
                        System.Console.Write(">> ");
                        sendMsg = System.Console.ReadLine(); 

                        if (sendMsg.Equals("quit"))
                        { 
                            obj.Close();
                            System.Console.WriteLine();
                            System.Console.WriteLine("You Quit.");
                            break; 

                        }
                        else
                        {
                            obj.SendMessage(sendMsg); //Send a Message.
                        }

                    }

                }

                recievedMsg = obj.GetMessage(); //Gets a Message.
               
                if (!String.IsNullOrEmpty(recievedMsg))
                {
                    System.Console.WriteLine(recievedMsg); // Write a Message.
                  
                }
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Lost Connection."); // Lost connection from the server.

        }
    }
}
