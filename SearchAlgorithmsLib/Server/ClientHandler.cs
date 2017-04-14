﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    public class ClientHandler : IClientHandler
    {

        public void HandleClient(TcpClient client)
        {
            IController con = new Controller();
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    string commandLine = reader.ReadString();
                    Console.WriteLine("Got command: {0}", commandLine);
                    string result = con.ExecuteCommand(commandLine, client);
                    writer.Write(result);
                }
                client.Close();
            }).Start();
        }
    }

}