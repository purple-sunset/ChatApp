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
        private Socket _socket;
        private Socket _handler;
        private ChatWindow _cw;
        public void Init(object o)
        {
            ReceiveEndPoint = new IPEndPoint(Address, Port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _cw = (ChatWindow) o;
            try
            {
                _socket.Bind(ReceiveEndPoint);
                _socket.Listen(10);
                ReceivingThread = new Thread(Receive) {IsBackground = true};
                ReceivingThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Accept()
        {
            
        }

        public void Receive(object o)
        {
            while (_socket != null)
            {
                if (_handler == null)
                {
                    _handler = _socket.Accept();
                }
                string message = null;
                while ((bool) _handler?.Connected)
                {
                    var data = new byte[1024];
                    var byteReceived = _handler.Receive(data);
                    message += Encoding.UTF8.GetString(data, 0, byteReceived);
                    if (message.IndexOf("\n", StringComparison.Ordinal) > -1)
                    {
                        message = message.Replace("\n", String.Empty);
                        break;
                    }
                }
                if (message != null)
                    _cw.Receive(message);
                
            }

        }


        public void Close()
        {
            try
            {
                _handler.Close();
                _socket.Close();
                ReceivingThread.Abort();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
