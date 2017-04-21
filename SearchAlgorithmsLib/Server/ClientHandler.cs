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
        private IController con;
        public ClientHandler(IController con)
        {
            this.con = con;
        }

        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    while (true)
                    {
                        try
                        {
                            string commandLine = reader.ReadString();
                            Console.WriteLine("Got command: {0}", commandLine);
                            string result = con.ExecuteCommand(commandLine, client);
                            writer.Write(result);
                            string commandKey = commandLine.Split(' ').First();
                        }

                        catch (SocketException)
                        {
                            break;
                        }
                        catch (System.IO.EndOfStreamException)
                        {
                            break;
                        }
                    }
                }
                //client.Close();
            }).Start();
        }
    }

}