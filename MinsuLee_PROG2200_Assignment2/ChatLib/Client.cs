using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LoggerLib;

namespace ChatLib
{
    public class Client //: EndPoint
    {
        public TcpClient client;
        public NetworkStream networkStream;
        //public StreamReader streamReader = null;
        //public StreamWriter streamWriter = null;

        public Int32 port = 8080;
        public IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        
        public MessageReceivedEventHandler MessageHandler;

        string line; // for looger
        private ILoggingService logger;

        public Client(ILoggingService logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Get the user's message and sends it to the other side.
        /// </summary>
        /// <param name="sendMsg"></param>
        public void SendMessage(string sendMsg)
        {
            //streamWriter.WriteLine(sendMsg); // Send Message
            //streamWriter.Flush();
            try
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(sendMsg);
                // Send back a response.
                networkStream.Write(msg, 0, msg.Length);
                networkStream.Flush();
                line = " Client: " + sendMsg + "\n";
                logger.Log(line);
            }
            catch
            {

            }

        }


        /// <summary>
        /// Get the message if it exists
        /// </summary>
        /// <returns>String sent message</returns>
        public void GetMessage()
        {
            string receivedMessage = "";

            while (true)
            {
                try
                {
                    byte[] data = new byte[256];

                    if (networkStream.DataAvailable)
                    {
                        Int32 bytes = networkStream.Read(data, 0, data.Length);
                        receivedMessage = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        //streamReader.ReadLine();

                        //raise event that message was received
                        MessageHandler(this, new MessageArgs(receivedMessage));

                        line = " Server: " + receivedMessage;
                        logger.Log(line);
                        //return receivedMessage;
                    }
                    else
                    {

                    }
                }
                catch
                {

                }
            }
        }
        
        /// <summary>
        /// Start the TcpClient(client) and start finding server.
        /// </summary>
        /// <returns>Bool found listener</returns>
        public bool Listen(out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                client = new TcpClient(localAddr.ToString(), port); //Attempt to connect to client
                networkStream = client.GetStream(); // The stream get message from a socket
                //streamReader = new StreamReader(networkStream, Encoding.UTF8); // Get message
                //streamWriter = new StreamWriter(networkStream, Encoding.UTF8); // Send message
                
                
                line = " The client Connected to Server" + "\n";
                logger.Log(line);

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
        public void Close()
        {
            //if (streamReader != null)
            //    streamReader.Close();
            //if (streamWriter != null)
            //    streamWriter.Close();
            if (networkStream != null)
                networkStream.Close();
            if (client != null)
                client.Close();

            line = " The client Disconnected From Server" + "\n\n";
            logger.Log(line);

        }
    }
}
