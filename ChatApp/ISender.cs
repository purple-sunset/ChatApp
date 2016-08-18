using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp
{
    public interface ISender
    {
        int Port { get; set; }
        IPAddress Address { get; set; }
        IPEndPoint SendEndPoint { get; set; }
        void Init();
        bool Send(string s);
        void Close();
    }
}
