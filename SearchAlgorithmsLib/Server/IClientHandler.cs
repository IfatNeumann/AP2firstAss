﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client1;
using System.Net.Sockets;

namespace Server
{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
