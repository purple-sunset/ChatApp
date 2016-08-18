﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp
{
    public interface IAsyncSender
    {
        int Port { get; set; }
        IPAddress Address { get; set; }
        IPEndPoint SendEndPoint { get; set; }
        int ByteSended { get; set; }
        void Init(object o);
        void ConnectCallback(IAsyncResult ar);
        void Send(string s);
        void SendCallback(IAsyncResult ar);
        void Close();
    }
}