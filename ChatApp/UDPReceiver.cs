using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatApp
{
    class UDPReceiver:IReceiver
    {
        public IPAddress Address { get; set; }

        public int Port { get; set; }

        public IPEndPoint ReceiveEndPoint { get; set; }

        public Thread ReceivingThread { get; set; }

        private UdpClient _client;
        private ChatWindow _cw;
        public void Init(object o)
        {
            _client = new UdpClient(Port);
            ReceiveEndPoint = new IPEndPoint(Address, Port);
            _cw = (ChatWindow) o;
            ReceivingThread = new Thread(Receive);
            ReceivingThread.IsBackground = true;
            ReceivingThread.Start();
        }

        public void Accept()
        {
        }
        
        public void Receive(object o)
        {
            var endPoint = ReceiveEndPoint;
            while (_client.Client.Connected)
            {
                byte[] data = _client.Receive(ref endPoint);
                string message = Encoding.UTF8.GetString(data);
                _cw.Receive(message);
            }
        }

        public void Close()
        {
            _client?.Close();
        }

    }
}
