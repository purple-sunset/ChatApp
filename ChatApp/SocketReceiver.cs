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
    class SocketReceiver : IReceiver
    {
        public IPAddress Address { get; set; }

        public int Port { get; set; }

        public IPEndPoint ReceiveEndPoint { get; set; }

        public Thread ReceivingThread { get; set; }
        private Socket socket;
        private Socket handler;
        private ChatWindow cw;
        public void Init(ChatWindow cw)
        {
            ReceiveEndPoint = new IPEndPoint(Address, Port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.cw = cw;
            try
            {
                socket.Bind(ReceiveEndPoint);
                socket.Listen(10);
                ReceivingThread = new Thread(Receive);
                ReceivingThread.IsBackground = true;
                ReceivingThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Receive()
        {
            while (socket != null)
            {
                if (handler == null)
                {
                    handler = socket.Accept();
                }
                string message = null;
                while (handler?.Connected ?? false)
                {
                    var data = new byte[1024];
                    var byteReceived = handler.Receive(data);
                    message += Encoding.UTF8.GetString(data, 0, byteReceived);
                    if (message.IndexOf("\n") > -1)
                    {
                        message = message.Replace("\n", String.Empty);
                        break;
                    }
                }
                if (message != null)
                    cw.Receive(message);
                
            }

        }


        public void Close()
        {
            try
            {
                handler.Close();
                socket.Close();
                ReceivingThread.Abort();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
