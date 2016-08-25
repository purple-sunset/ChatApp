using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatApp
{
    class SocketReceiver:IReceiver
    {
        public IPAddress Address { get; set; }

        public int Port { get; set; }

        public IPEndPoint ReceiveEndPoint { get; set; }

        public Thread ReceivingThread { get; set; }
        private Socket _server;
        private readonly byte[] _data = new byte[1024];
        private ChatWindow _cw;
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
                        _server.Accept();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                if ((_cw.IsConnected) && (_server.Connected))
                {
                    ReceivingThread = new Thread(Receive) { IsBackground = true };
                    ReceivingThread.Start();
                }
                if ((!_cw.IsConnected) && (_server.Connected))
                {
                    _server.Disconnect(true);
                    ReceivingThread.Abort();
                }
            }
        }

        public void Receive(object o)
        {
            while (_server.Connected)
            {
                try
                {
                    int byteReceived = _server.Receive(_data);
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
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                } 
            }
        }


        public void Close()
        {
            try
            {
                _server.Close();
                ReceivingThread.Abort();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
