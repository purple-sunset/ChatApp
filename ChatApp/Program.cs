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
            DIContainer.SetModule<ISender, AsyncSockerSender>();
            DIContainer.SetModule<IReceiver, AsyncSocketReceiver>();
            DIContainer.SetModule<ChatWindow, ChatWindow>();            
            var chatWindow = DIContainer.GetModule<ChatWindow>();
            Application.Run(chatWindow);
        }
    }
}
