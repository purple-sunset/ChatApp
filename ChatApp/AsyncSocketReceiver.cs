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
    class AsyncSocketReceiver : IAsyncReceiver
    {
        public IPAddress Address { get; set; }


        public int Port { get; set; }

        public IPEndPoint ReceiveEndPoint { get; set; }
        public Thread ReceivingThread { get; set; }
        private Socket server;
        private byte[] data = new byte[1024];
        private AsyncChatWindow cw;
        private ManualResetEvent acceptDone =
        new ManualResetEvent(false);

        private ManualResetEvent receiveDone =
        new ManualResetEvent(false);
        public void Init(object o)
        {
            ReceiveEndPoint = new IPEndPoint(Address, Port);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.cw = (AsyncChatWindow)o;
            try
            {
                server.Bind(ReceiveEndPoint);
                server.Listen(100);
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
                if (!server.Connected)
                {
                    try
                    {
                        acceptDone.Reset();
                        server.BeginAccept(new AsyncCallback(AcceptCallback), server);
                        acceptDone.WaitOne(5000);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                if ((!cw.IsConnected) && (server.Connected))
                {
                    server.Disconnect(true);
                }
            }
        }


        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket oldServer = (Socket)ar.AsyncState;
                Socket client = oldServer.EndAccept(ar);
                acceptDone.Set();
                Receive(client);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public void Receive(Socket client)
        {
            while (client.Connected)
            {
                receiveDone.Reset();
                client.BeginReceive(data, 0, 1024, 0, new AsyncCallback(ReceiveCallback), client);
                receiveDone.WaitOne(1000);
            }
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int byteReceived = client?.EndReceive(ar) ?? 0;
                if (byteReceived > 0)
                {
                    string message = Encoding.UTF8.GetString(data, 0, byteReceived);
                    if (message != null)
                        cw.Receive(message);
                    if (message == "Disconnected")
                    {
                        cw.DisableConnect();
                        cw.DisableSend();
                    }

                    receiveDone.Set();
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
                server.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
