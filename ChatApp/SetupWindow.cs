using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class SetupWindow : Form
    {
        public string MyAddress { get; set; }
        public string FriendAddress { get; set; }
        public string SendPort { get; set; }
        public string ReceivePort { get; set; }
        public SetupWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            MyAddress = textMyAddress.Text.Trim();
            FriendAddress = textFriendAddress.Text.Trim();
            SendPort = textSendPort.Text.Trim();
            ReceivePort = textReceivePort.Text.Trim();
            this.Close();
        }

        private void SetupWindow_Load(object sender, EventArgs e)
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    MyAddress = ip.ToString();
                    textMyAddress.Text = MyAddress;
                }
            }
        }
    }
}
