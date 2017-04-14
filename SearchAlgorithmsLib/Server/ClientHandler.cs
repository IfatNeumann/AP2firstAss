using System;
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
        IController con = new Controller();

        public void HandleClient(TcpClient client)
        {
            
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryReader writer = new BinaryReader(stream))
                {
                    string commandLine = reader.ReadString();
                    Console.WriteLine("Got command: {0}", commandLine);
                    string result = con.ExecuteCommand(commandLine, client);
                   
                    writer.ToString(result);
                }
                client.Close();
            }).Start();
        }
    }

}