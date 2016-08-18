using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DIContainer.SetModule<ISender, SocketSender>();
            DIContainer.SetModule<IReceiver, SocketReceiver>();
            DIContainer.SetModule<IAsyncSender, AsyncSockerSender>();
            DIContainer.SetModule<IAsyncReceiver, AsyncSocketReceiver>();
            DIContainer.SetModule<AsyncChatWindow, AsyncChatWindow>();
            DIContainer.SetModule<ChatWindow, ChatWindow>();
            var chatWindow = DIContainer.GetModule<AsyncChatWindow>();
            Application.Run(chatWindow);
        }
    }
}
