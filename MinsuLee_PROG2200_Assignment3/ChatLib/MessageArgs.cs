using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    public class MessageArgs : EventArgs
    {
        public string message { get; }

        public MessageArgs(string inMessage)
        {
            message = inMessage;
        }
        
    }
}