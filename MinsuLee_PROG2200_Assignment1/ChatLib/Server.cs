using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    public class Server : EndPoint
    {
        private TcpListener server = null;

        /// <summary>
        /// Start the TcpListener(server) and start Listening for Clients.
        /// </summary>
        /// <returns>Bool server connected</returns>
        public override bool Listen(out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                server = new TcpListener(localAddr, port);
                server.Start(); // Start listening for client requests.
                client = server.AcceptTcpClient();
                networkStream = client.GetStream(); // the stream get message from a socket
                //streamReader = new StreamReader(networkStream); //Creates a new StreamReader
                //streamWriter = new StreamWriter(networkStream); //Creates a bew StreamWriter

                return true;

            }
            catch (SocketException e)
            {
                //Console.WriteLine("SocketException: {0}", e);
                errorMessage = string.Format("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }

            return false;

        }

        /// <summary>
        /// Close the stream and listner
        /// </summary>
        public override void Close()
        {
            //streamReader.Close();
            //streamWriter.Close();
            networkStream.Close();
            client.Close();
            server.Stop();
        }
    }
}
