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
    class UDPReceiver : IReceiver
    {
        public IPAddress Address { get; set; }

        public int Port { get; set; }

        public IPEndPoint ReceiveEndPoint { get; set; }

        public Thread ReceivingThread { get; set; }

        private UdpClient client;
        private ChatWindow cw;
        public void Init(ChatWindow cw)
        {
            client = new UdpClient(Port);
            ReceiveEndPoint = new IPEndPoint(Address, Port);
            this.cw = cw;
            ReceivingThread = new Thread(Receive);
            ReceivingThread.IsBackground = true;
            ReceivingThread.Start();
        }

        public void Receive()
        {
            var endPoint = ReceiveEndPoint;
            while (client.Client.Connected)
            {
                byte[] data = client.Receive(ref endPoint);
                string message = Encoding.UTF8.GetString(data);
                cw.Receive(message);
            }
        }

        public void Close()
        {
            if (client != null)
            {
                client.Close(); 
            }
        }

    }
}
