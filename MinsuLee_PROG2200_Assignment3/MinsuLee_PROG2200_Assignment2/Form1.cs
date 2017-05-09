using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatLib;
using LoggerLib;
using System.Threading;

namespace MinsuLee_PROG2200_Assignment2
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        Client client = new Client();
        Logger logger = new Logger();
        Thread multiThread;

        string fileName;
        string line; // for looger

        bool client_start = false;
        string recievedMsg = null;

        public Form1()
        {
            InitializeComponent();
            connectToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem.Enabled = false;

            //ChatLib.EndPoint.
            client.MessageHandler += new MessageReceivedEventHandler(newMessage);
            fileName = "C:\\Users\\Minsu\\Source\\Repos\\Lee-Minsu-w0293156\\MinsuLee_PROG2200_Assignment2\\" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".log";
        }

        /// <summary>
        /// button for connection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            
            client.Listen(out errorMessage);
            multiThread = new Thread(client.GetMessage);
            multiThread.Name = "Listening Thread";
            multiThread.Start();
            
            // to generate error message
            if (errorMessage != "")
            {
                MessageBox.Show(errorMessage);
            }
            else
            {
                MessageBox.Show("Connected to the Server");
                client_start = true;
            }

            // button able and enable
            connectToolStripMenuItem.Enabled = false;
            disconnectToolStripMenuItem.Enabled = true;

            // getMessage goes here.
            //recievedMsg = client.GetMessage; //Gets a Message.

            //if (!String.IsNullOrEmpty(recievedMsg))
            //{
            //    //MessageBox.Show(recievedMsg); // Write a Message.
            //    DisplayChat.AppendText(recievedMsg);// + Environment.NewLine);
            //    line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + " Server: " + recievedMsg + "\n";
            //    logger.WriteLog(fileName, line);
            //}

            line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + " The client Connected to Server" + "\n";
            logger.WriteLog(fileName, line);

        }

        /// <summary>
        /// button for disconnection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Close();

            // set default
            //SendMessageBox.Text = "";
            //DisplayChat.Text = "";
            SendMessageBox.Clear();
            DisplayChat.Clear();
            MessageBox.Show("Disconnected from the Server");

            line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + " The client Disconnected From Server" + "\n";
            logger.WriteLog(fileName, line);

            // button able and enable
            disconnectToolStripMenuItem.Enabled = false;
            connectToolStripMenuItem.Enabled = true;

        }

        /// <summary>
        /// button for send message from client to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendButton_Click(object sender, EventArgs e)
        {
            string sendMsg = SendMessageBox.Text;

            if (!client_start)
            {
                MessageBox.Show("Connect to Server Before Send Message.");
                SendMessageBox.Text = "";
            }
            else
            {
                if (String.IsNullOrEmpty(sendMsg))
                {
                    MessageBox.Show("Please Input Messages");
                }
                else
                {
                    DisplayChat.AppendText(">> " + sendMsg + Environment.NewLine);
                    line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + " Client: " + sendMsg + "\n";
                    logger.WriteLog(fileName, line);
                    //Sendbutton1WasClicked = true;

                    client.SendMessage(sendMsg); //Send a Message.
                    SendMessageBox.Clear();
                    
                }
            }
        }

        /// <summary>
        /// exit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Close();
            Application.Exit();
        }

        /// <summary>
        /// manage message from server to client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="MessageArgs"></param>
        private void newMessage(object sender, MessageArgs MessageArgs)
        {
            if (SendMessageBox.InvokeRequired)
            {
                SendMessageBox.Invoke(new MethodInvoker(delegate ()
                {
                    SendMessageBox.Text += "Server: " + MessageArgs.message + "\r\n";
                    line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + " Server: " + MessageArgs.message + "\n";
                    logger.WriteLog(fileName, line);
                }));
            }
        }
    }
}
