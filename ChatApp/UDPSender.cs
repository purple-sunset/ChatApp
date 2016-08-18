using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    class UDPSender
    {
        public IPAddress Address { get; set; }

        public int Port { get; set; }

        public IPEndPoint SendEndPoint { get; set; }

        private UdpClient client;

        public void Init()
        {
            client = new UdpClient(Port);
            SendEndPoint = new IPEndPoint(Address, Port);
            client.Connect(SendEndPoint);
        }

        public bool Send(string s)
        {
            if (client.Client.Connected)
            {
                byte[] data = Encoding.UTF8.GetBytes(s);
                if (client.Send(data, data.Length) > 0)
                    return true;
            }
            return false;
        }

        public void Close()
        {
            if (client!=null)
            {
                client.Close(); 
            }
        }
    }
}
