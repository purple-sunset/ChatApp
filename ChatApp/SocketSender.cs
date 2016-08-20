using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatApp
{
    class SocketSender:ISender
    {
        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public IPEndPoint SendEndPoint { get; set; }

        private Socket _socket;

        public void Init(object o)
        {
            try
            {
                SendEndPoint = new IPEndPoint(Address, Port);
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Connect()
        {
            
        }

        public bool Send(string s)
        {
            try
            {
                if (!_socket.Connected)
                {
                    _socket.Connect(SendEndPoint);
                }
                if(_socket.Connected)
                {
                    byte[] data = Encoding.UTF8.GetBytes(s + "\n");
                    int byteSended = _socket.Send(data);
                    if (byteSended > 0)
                        return true;
                }
                return false;

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.ToString());
            }
            catch (SocketException se)
            {

                Console.WriteLine(se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;
        }

        public void Close()
        {
            try
            {
                _socket.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
