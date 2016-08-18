using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp
{
    public interface IAsyncReceiver
    {
        int Port { get; set; }
        IPAddress Address { get; set; }
        IPEndPoint ReceiveEndPoint { get; set; }
        Thread ReceivingThread { get; set; }
        
        void Init(object o);
        void Accept();
        void AcceptCallback(IAsyncResult ar);
        void Receive(Socket client);
        void ReceiveCallback(IAsyncResult ar);
        void Close();
    }
}
