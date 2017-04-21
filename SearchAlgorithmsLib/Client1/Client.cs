using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client1
{
    /// <summary>
    /// handel the client flow
    /// </summary>
    public class Client
    {
        /// <summary>
        /// The line
        /// </summary>
        private string line;

        /// <summary>
        /// The port
        /// </summary>
        private int port;

        /// <summary>
        /// The ip
        /// </summary>
        private string ip;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="p">The string of the port number.</param>
        /// <param name="i">The string of the ip .</param>
        public Client(string p, string i)
        {
            this.port = int.Parse(p);
            this.ip = i;
        }

        /// <summary>
        /// Handle the flow of the client 
        /// </summary>
        public void Handle()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(this.ip), this.port);
            TcpClient client = null;
            NetworkStream stream = null;
            BinaryWriter writer = null;
            BinaryReader reader = null;
            Task task;

            Action receiveThread = new Action(
                () =>
                    {
                        while (true)
                        {
                            try
                            {
                                // Get result from server
                                string result = reader.ReadString();
                                Console.WriteLine(result);
                                string commandKey = this.line.Split(' ').First();

                                // check the commands require  closing the connection 
                                if (commandKey.Equals("generate") || commandKey.Equals("solve")
                                    || (commandKey.Equals("close") && (!this.line.Equals(commandKey))))
                                    /* edge case - if the input is "close" and not "close <name>" - 
                                                                        we don't want to close the connection*/
                                {
                                    writer.Dispose();
                                    reader.Dispose();
                                    client.Close();
                                    Console.WriteLine("connection stopped");
                                    client = null;
                                    break;
                                }
                            }
                            catch (SocketException)
                            {
                                Console.WriteLine("exception - connection stopped");
                                break;
                            }
                        }
                    });
            Console.WriteLine("Welcome! please enter a command:");
            while (true)
            {
                try
                {
                    // Send data to server
                    this.line = Console.ReadLine();
                    if (client == null)
                    {
                        // create new TcpClient
                        client = new TcpClient();
                        client.Connect(ipep);
                        Console.WriteLine("You are connected");
                        stream = client.GetStream();
                        writer = new BinaryWriter(stream);
                        reader = new BinaryReader(stream);
                        task = new Task(receiveThread);
                        task.Start();
                    }

                    writer.Write(this.line);
                }
                catch (SocketException)
                {
                    Console.WriteLine("exception - connection stopped");
                    break;
                }
            }
        }
    }
}