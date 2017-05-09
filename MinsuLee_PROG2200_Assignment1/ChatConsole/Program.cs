using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLib;


namespace ChatConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console console = new Console();
            string errorMessage = string.Empty; 

            if (args.Length > 0 && args[0] == "-server")
            {
                EndPoint server = new Server(); //Create an Istance of the server
                System.Console.WriteLine("Waiting for client connection...");
                System.Console.WriteLine();
                //if (server.Listen(out errorMessage))
                //{ 
                    while (true) //Keep checking for client to connect
                    {
                        server.Listen(out errorMessage); //Waits for the Client to Connect.
                        System.Console.WriteLine();
                        System.Console.WriteLine("Connected");
                        System.Console.WriteLine();
                        console.Drive(server);
                    }// run as server 

                //}
                //else //State that it couldn't Connect
                //{ 
                //    Console.WriteLine("Unable to connect to Server");
                //    Console.WriteLine(errorMessage);
                //}
            }
            else if (args.Length == 0)// && string.IsNullOrEmpty(args[0]))
            {
                EndPoint client = new Client(); //Create an Instance of Client

                //if (client.Listen(out errorMessage))
                //{
                    while (client.Listen(out errorMessage))
                    {
                        System.Console.WriteLine("Connected to the Server");
                        System.Console.WriteLine();
                        console.Drive(client);
                    }
                //}
                //else
                //{ 
                //    Console.WriteLine("Unable to connect to Client");
                //}

            }// run as cient
            else
            {
                System.Console.WriteLine("Invalid Argument");
                System.Console.WriteLine("ChatConsole.exe -server (runs the chat server)");
                System.Console.WriteLine("ChatConsole.exe         (runs the chat client)");
            }
            
        }//end main method

    }//end of class

}//end of namespace
