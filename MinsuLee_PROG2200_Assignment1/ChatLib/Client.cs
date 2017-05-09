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
    public class Client : EndPoint
    {
        /// <summary>
        /// Start the TcpClient(client) and start finding server.
        /// </summary>
        /// <returns>Bool found listener</returns>
        public override bool Listen(out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                client = new TcpClient(localAddr.ToString(), port); //Attempt to connect to client
                networkStream = client.GetStream(); // The stream get message from a socket
                //streamReader = new StreamReader(networkStream, Encoding.UTF8); // Get message
                //streamWriter = new StreamWriter(networkStream, Encoding.UTF8); // Send message

                return true;
            }
            catch (ArgumentNullException e)
            {
                errorMessage = String.Format("ArgumentNullException: {0}", e);
                //Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                errorMessage = String.Format("SocketException: {0}", e);
                //Console.WriteLine("SocketException: {0}", e);
            }

            return false;

        }// end Listen()

        /// <summary>
        /// Close the stream and client
        /// </summary>
        public override void Close()
        {
            //if (streamReader != null)
            //    streamReader.Close();
            //if (streamWriter != null)
            //    streamWriter.Close();
            //if (networkStream != null)
            networkStream.Close();
            //if (client != null)
            client.Close();

        }
    }
}
