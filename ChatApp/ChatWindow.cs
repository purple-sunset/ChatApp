using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class ChatWindow : Form
    {
        private readonly ISender _sender;
        private readonly IReceiver _receiver;
        private string myAddress;
        private string friendAddress;
        delegate void SetTextCallback(string text);
        public ChatWindow(ISender sender, IReceiver receiver)
        {
            InitializeComponent();
            this.Load += new EventHandler(Start);
            this._sender = sender;
            this._receiver = receiver;
        }

        public void Start(object sender, EventArgs e)
        {
            this.Hide();
            using (SetupWindow sw = new SetupWindow())
            {
                sw.ShowDialog();
                try
                {
                    _sender.Address = IPAddress.Parse(sw.FriendAddress);
                    _sender.Port = Int32.Parse(sw.SendPort);
                    _receiver.Address = IPAddress.Parse(sw.MyAddress);
                    _receiver.Port = Int32.Parse(sw.ReceivePort);
                    myAddress = sw.MyAddress;
                    friendAddress = sw.FriendAddress;
                    textConv.SelectionAlignment = HorizontalAlignment.Center;
                    textConv.AppendText(myAddress + " đã đăng nhập! \n");
                    this.Show();
                    _sender.Init(this);
                    _receiver.Init(this);
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception");
                    this.Close();
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void textSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send();
            }
        }

        public void Send()
        {
            if (textSend.Text.Trim() != "")
            {
                if (_sender.Send(textSend.Text))
                {
                    var text = textSend.Text + " < " + myAddress;
                    textConv.SelectionAlignment = HorizontalAlignment.Right;
                    textConv.AppendText(text + "\n");
                    textSend.ResetText();
                }
                
            }

        }

        public void Receive(string s)
        {
            var text = friendAddress + " > " + s;
            if (this.textConv.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Receive);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                this.textConv.SelectionAlignment = HorizontalAlignment.Left;
                this.textConv.AppendText(text + "\n");
            }
        }

        private void ChatWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _sender.Close();
            _receiver.Close();
        }
    }
}
