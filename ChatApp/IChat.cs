using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp
{
    public interface IChat
    {
        int SendPort { get; set; }
        int ReceivePort { get; set; }
        string MyAddress { get; set; }
        string FriendAddress { get; set; }
        Thread ReceivingThread { get; set; }
        void InitSender();
        void InitReceiver(ChatWindow cwd);
        void Send(string s);
        void Receive(object o);
        void Close();
        
    }
}
