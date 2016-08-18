using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp
{
    public interface IReceiver
    {
        int Port { get; set; }
        IPAddress Address { get; set; }
        IPEndPoint ReceiveEndPoint { get; set; }
        Thread ReceivingThread { get; set; }

        void Init(ChatWindow cw);
        void Receive();
        void Close();
    }
}
