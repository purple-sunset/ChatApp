using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatApp
{
    class UDPSender:ISender
    {
        public IPAddress Address { get; set; }

        public int Port { get; set; }

        public IPEndPoint SendEndPoint { get; set; }

        private UdpClient _client;

        public void Init(object o)
        {
            _client = new UdpClient(Port);
            SendEndPoint = new IPEndPoint(Address, Port);
            _client.Connect(SendEndPoint);
        }

        public void Connect()
        {
            
        }

        public bool Send(string s)
        {
            if (_client.Client.Connected)
            {
                byte[] data = Encoding.UTF8.GetBytes(s);
                if (_client.Send(data, data.Length) > 0)
                    return true;
            }
            return false;
        }

        public void Close()
        {
            if (_client!=null)
            {
                _client.Close(); 
            }
        }
    }
}
