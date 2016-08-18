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
    class UDPChat:IChat
    {
        public int SendPort { get; set; }
        public int ReceivePort { get; set; }
        public string MyAddress { get; set; }
        public string FriendAddress { get; set; }
        public Thread ReceivingThread { get; set; }
        private UdpClient sendingClient;
        private UdpClient receivingClient;
        public UDPChat() { }
        public void InitSender()
        {
            sendingClient = new UdpClient();
            sendingClient.Connect(IPAddress.Parse(FriendAddress), SendPort);
        }

        public void InitReceiver(ChatWindow cwd)
        {
            receivingClient = new UdpClient(ReceivePort);
            ReceivingThread = new Thread(new ParameterizedThreadStart(Receive));
            ReceivingThread.IsBackground = true;
            ReceivingThread.Start(cwd);
        }

        public void Send(string s)
        {
            if (sendingClient != null)
            {
                byte[] data = Encoding.UTF8.GetBytes(s);
                sendingClient.Send(data, data.Length); 
            }

        }

        public void Receive(object o)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, ReceivePort);
            var cw = (ChatWindow) o;
            while (receivingClient != null)
            {
                byte[] data = receivingClient.Receive(ref endPoint);
                string message = Encoding.UTF8.GetString(data);
                cw.Receive(message);
            }
        }

        public void Close()
        {
            try
            {
                ReceivingThread.Abort();
                sendingClient.Close();
                receivingClient.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
        }
    }
}
