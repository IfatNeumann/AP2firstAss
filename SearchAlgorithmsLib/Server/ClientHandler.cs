using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    /// <summary>
    /// class that handel the interection between the server and the client 
    /// </summary>
    /// <seealso cref="Server.IClientHandler" />
    public class ClientHandler : IClientHandler
    {
        /// <summary>
        /// controller
        /// </summary>
        private IController con;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        /// <param name="con">The controller.</param>
        public ClientHandler(IController con)
        {
            this.con = con;
        }

        /// <summary>
        /// Handles the client and the interction with the server 
        /// </summary>
        /// <param name="client">The client.</param>
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