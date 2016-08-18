using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class AsyncChatWindow : Form
    {
        private readonly IAsyncSender _sender;
        private readonly IAsyncReceiver _receiver;
        private string myAddress;
        private string friendAddress;
        public bool IsConnected { get; set; }
        delegate void SetTextCallback(string text);
        delegate void SetButtonCallback();
        delegate void SetConnectCallback();
        public AsyncChatWindow(IAsyncSender sender, IAsyncReceiver receiver)
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
                    ThreadPool.QueueUserWorkItem(_sender.Init, this);
                    ThreadPool.QueueUserWorkItem(_receiver.Init, this);
                    
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
                _sender.Send(textSend.Text);
                if (_sender.ByteSended > 0)
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

        public void EnableSend()
        {
            if (this.btnSend.InvokeRequired)
            {
                SetButtonCallback d = new SetButtonCallback(EnableSend);
                this.Invoke(d);
            }
            else
            {
                this.btnSend.Enabled = true;
            }
            
        }

        public void DisableSend()
        {
            if (this.btnSend.InvokeRequired)
            {
                SetButtonCallback d = new SetButtonCallback(DisableSend);
                this.Invoke(d);
            }
            else
            {
                this.btnSend.Enabled = false;
            }

        }

        public void EnableConnect()
        {
            if (this.btnSend.InvokeRequired)
            {
                SetConnectCallback d = new SetConnectCallback(EnableConnect);
                this.Invoke(d);
            }
            else
            {
                this.IsConnected = true;
            }
        }

        public void DisableConnect()
        {
            if (this.btnSend.InvokeRequired)
            {
                SetConnectCallback d = new SetConnectCallback(DisableConnect);
                this.Invoke(d);
            }
            else
            {
                this.IsConnected = false;
            }
        }
    }
}
