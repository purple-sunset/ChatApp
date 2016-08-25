using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatApp
{
    class SocketSender:ISender
    {
        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public IPEndPoint SendEndPoint { get; set; }

        private Socket _client;
        private ChatWindow _cw;
        private bool _isSended;

        public void Init(object o)
        {
            _cw = (ChatWindow)o;
            SendEndPoint = new IPEndPoint(Address, Port);
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Connect();
        }

        public void Connect()
        {
            while (true)
            {
                if (!_client.Connected)
                {
                    try
                    {
                        _client.Connect(SendEndPoint);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                if (_client.Connected)
                {
                    _cw.EnableConnect();
                    _cw.EnableSend();
                }
                else
                {
                    _cw.DisableConnect();
                    _cw.DisableSend();
                }
                if ((!_cw.IsConnected) && (_client.Connected))
                {
                    _client.Disconnect(true);
                }
                
            }
        }

        public bool Send(string s)
        {
            _isSended = false;
            if (_client.Connected)
            {
                byte[] data = Encoding.UTF8.GetBytes(s);
                try
                {
                    var byteSended = _client.Send(data);
                    if (byteSended > 0)
                        _isSended = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return _isSended;
        }

        public void Close()
        {
            try
            {
                Send("Disconnected");
                _client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
