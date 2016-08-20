using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatApp
{
    class AsyncSocketReceiver : IReceiver
    {
        public IPAddress Address { get; set; }

        public int Port { get; set; }

        public IPEndPoint ReceiveEndPoint { get; set; }

        private Socket _server;
        private readonly byte[] _data = new byte[1024];
        private ChatWindow _cw;
        private readonly ManualResetEvent _acceptDone = new ManualResetEvent(false);
        private readonly ManualResetEvent _receiveDone = new ManualResetEvent(false);

        public void Init(object o)
        {
            ReceiveEndPoint = new IPEndPoint(Address, Port);
            _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _cw = (ChatWindow)o;
            try
            {
                _server.Bind(ReceiveEndPoint);
                _server.Listen(100);
                Accept();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Accept()
        {
            while (true)
            {
                if (!_server.Connected)
                {
                    try
                    {
                        _acceptDone.Reset();
                        _server.BeginAccept(AcceptCallback, _server);
                        _acceptDone.WaitOne(5000);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                if ((!_cw.IsConnected) && (_server.Connected))
                {
                    _server.Disconnect(true);
                }
            }
        }


        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket oldServer = (Socket)ar.AsyncState;
                Socket client = oldServer.EndAccept(ar);
                _acceptDone.Set();
                Receive(client);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public void Receive(object o)
        {
            Socket client = (Socket) o;
            while (client.Connected)
            {
                _receiveDone.Reset();
                client.BeginReceive(_data, 0, 1024, 0, ReceiveCallback, client);
                _receiveDone.WaitOne(1000);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int byteReceived = client?.EndReceive(ar) ?? 0;
                if (byteReceived > 0)
                {
                    string message = Encoding.UTF8.GetString(_data, 0, byteReceived);
                    if (message.Length > 0)
                        _cw.Receive(message);
                    if (message == "Disconnected")
                    {
                        _cw.DisableConnect();
                        _cw.DisableSend();
                    }
                    _receiveDone.Set();
                }
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
                _server.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
