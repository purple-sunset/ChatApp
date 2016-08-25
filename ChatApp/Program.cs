using System;
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
            DIContainer.SetModule<ChatWindow, ChatWindow>();            
            var chatWindow = DIContainer.GetModule<ChatWindow>();
            Application.Run(chatWindow);
        }
    }
}
