using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatApp
{
    class AsyncSockerSender : ISender
    {
        public IPAddress Address { get; set; }

        public int Port { get; set; }

        public IPEndPoint SendEndPoint { get; set; }

        private bool _isSended;
        private Socket _client;
        private ChatWindow _cw;
        private readonly ManualResetEvent _connectDone = new ManualResetEvent(false);
        private readonly ManualResetEvent _sendDone = new ManualResetEvent(false);

        public void Init(object o)
        {
            _cw = (ChatWindow)o;
            SendEndPoint = new IPEndPoint(Address, Port);
            Connect();
        }

        public void Connect()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            while (true)
            {
                if (!socket.Connected)
                {
                    try
                    {
                        _connectDone.Reset();
                        socket.BeginConnect(SendEndPoint, ConnectCallback, socket);
                        _connectDone.WaitOne(5000);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    } 
                }
                if ((!_cw.IsConnected) && (socket.Connected))
                {
                    socket.Disconnect(true);
                }
            }         
        }
        private void ConnectCallback(IAsyncResult ar)
        {
            _client = (Socket)ar.AsyncState;
            try
            {
                _client.EndConnect(ar);
                if(_client.Connected)
                {
                    _cw.EnableConnect();
                    _cw.EnableSend();
                }
                else
                {
                    _cw.DisableConnect();
                    _cw.DisableSend();
                }
                _connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
                    _sendDone.Reset();
                    _client.BeginSend(data, 0, data.Length, 0, SendCallback, _client);
                    _sendDone.WaitOne(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());                    
                }                
            }
            return _isSended;            
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket remote = (Socket)ar.AsyncState;
                var byteSended = remote.EndSend(ar);
                if (byteSended > 0)
                {
                    _isSended = true;
                }
                _sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
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
