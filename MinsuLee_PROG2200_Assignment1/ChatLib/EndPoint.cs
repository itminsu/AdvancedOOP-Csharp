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
    public abstract class EndPoint
    {
        public TcpClient client;
        public NetworkStream networkStream;
        //public StreamReader streamReader = null;
        //public StreamWriter streamWriter = null;

        public Int32 port = 8080;
        public IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        public abstract bool Listen(out string errorMessage);

        //public MessageReceivedEventHandler MessageHandler;

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

        public abstract void Close();
    }
}
