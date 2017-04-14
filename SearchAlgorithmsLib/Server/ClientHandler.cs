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
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string commandLine = reader.ReadLine();
                    Console.WriteLine("Got command: {0}", commandLine);
                    string result = con.ExecuteCommand(commandLine, client);

                    writer.Write(result);
                }
                client.Close();
            }).Start();
        }
    }

}