using System.Net;
using System.Net.Sockets;

namespace ChatApp
{
    public interface IReceiver
    {
        int Port { get; set; }
        IPAddress Address { get; set; }
        IPEndPoint ReceiveEndPoint { get; set; }
        
        void Init(object o);
        void Accept();
        void Receive(object o);
        void Close();
    }
}
