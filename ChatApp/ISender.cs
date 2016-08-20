using System.Net;

namespace ChatApp
{
    public interface ISender
    {
        int Port { get; set; }
        IPAddress Address { get; set; }
        IPEndPoint SendEndPoint { get; set; }

        void Init(object o);
        void Connect();
        bool Send(string s);
        void Close();
    }
}
