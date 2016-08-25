using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class ChatWindow : Form
    {
        private readonly ISender _sender;
        private readonly IReceiver _receiver;
        private string _myAddress;
        private string _friendAddress;
        public bool IsConnected { get; set; }
        delegate void SetTextCallback(string text);
        delegate void SetButtonCallback();
        delegate void SetConnectCallback();
        public ChatWindow(ISender sender, IReceiver receiver)
        {
            InitializeComponent();
            Load += Start;
            _sender = sender;
            _receiver = receiver;
        }

        public void Start(object sender, EventArgs e)
        {
            Hide();
            using (SetupWindow sw = new SetupWindow())
            {
                sw.ShowDialog();
                try
                {
                    _sender.Address = IPAddress.Parse(sw.FriendAddress);
                    _sender.Port = Int32.Parse(sw.SendPort);
                    _receiver.Address = IPAddress.Parse(sw.MyAddress);
                    _receiver.Port = Int32.Parse(sw.ReceivePort);
                    _myAddress = sw.MyAddress;
                    _friendAddress = sw.FriendAddress;
                    textConv.SelectionAlignment = HorizontalAlignment.Center;
                    textConv.AppendText(_myAddress + " đã đăng nhập! \n");
                    Show();
                    ThreadPool.QueueUserWorkItem(_sender.Init, this);
                    ThreadPool.QueueUserWorkItem(_receiver.Init, this);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Close();
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
                    var text = textSend.Text + " < " + _myAddress;
                    textConv.SelectionAlignment = HorizontalAlignment.Right;
                    textConv.AppendText(text + "\n");
                    textSend.ResetText();
                }
            }
        }

        public void Receive(string s)
        {
            var text = _friendAddress + " > " + s;
            if (textConv.InvokeRequired)
            {
                SetTextCallback d = Receive;
                Invoke(d, s);
            }
            else
            {
                textConv.SelectionAlignment = HorizontalAlignment.Left;
                textConv.AppendText(text + "\n");
            }
        }

        private void ChatWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _sender.Close();
            _receiver.Close();
        }

        public void EnableSend()
        {
            if (btnSend.InvokeRequired)
            {
                SetButtonCallback d = EnableSend;
                Invoke(d);
            }
            else
            {
                btnSend.Enabled = true;
            }

        }

        public void DisableSend()
        {
            if (btnSend.InvokeRequired)
            {
                SetButtonCallback d = DisableSend;
                Invoke(d);
            }
            else
            {
                btnSend.Enabled = false;
            }

        }

        public void EnableConnect()
        {
            if (btnSend.InvokeRequired)
            {
                SetConnectCallback d = EnableConnect;
                Invoke(d);
            }
            else
            {
                IsConnected = true;
            }
        }

        public void DisableConnect()
        {
            if (btnSend.InvokeRequired)
            {
                SetConnectCallback d = DisableConnect;
                Invoke(d);
            }
            else
            {
                IsConnected = false;
            }
        }
    }
}
