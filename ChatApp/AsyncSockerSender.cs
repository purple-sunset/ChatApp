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
    class AsyncSockerSender : IAsyncSender
    {
        public IPAddress Address { get; set; }


        public int Port { get; set; }


        public IPEndPoint SendEndPoint { get; set; }

        public bool IsSended { get; set; }
        public int ByteSended { get; set; }

        private Socket client;

        private AsyncChatWindow cw;

        private ManualResetEvent connectDone =
        new ManualResetEvent(false);

        private ManualResetEvent sendDone =
        new ManualResetEvent(false);
        public void Init(object o)
        {
            this.cw = (AsyncChatWindow)o;
            SendEndPoint = new IPEndPoint(Address, Port);
            Connect();
        }

        private void Connect()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            while (true)
            {
                
                if (!socket.Connected)
                {
                    try
                    {
                        connectDone.Reset();
                        socket.BeginConnect(SendEndPoint, new AsyncCallback(ConnectCallback), socket);
                        connectDone.WaitOne(5000);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    } 
                }
                if ((!cw.IsConnected) && (socket.Connected))
                {
                    socket.Disconnect(true);
                }
            }
            
        }
        public void ConnectCallback(IAsyncResult ar)
        {
            client = (Socket)ar.AsyncState;
            try
            {
                client.EndConnect(ar);
                if(client.Connected)
                {
                    cw.EnableConnect();
                    cw.EnableSend();
                }
                else
                {
                    cw.DisableConnect();
                    cw.DisableSend();
                }
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Send(string s)
        {
            if (client.Connected)
            {
                byte[] data = Encoding.UTF8.GetBytes(s);
                try
                {
                    sendDone.Reset();
                    client.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), client);
                    sendDone.WaitOne(5000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    
                }
                
            }
            
        }

        public void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket remote = (Socket)ar.AsyncState;
                ByteSended = remote.EndSend(ar);
                sendDone.Set();
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
                Send("Disconnect");
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
