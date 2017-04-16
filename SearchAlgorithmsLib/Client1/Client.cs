using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client1
{
    public class Client
    {
        private int port;
        private string ip;

        public Client(int p, string i)
        {
            this.port = p;
            this.ip = i;
        }

        public void Handle()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), this.port);
            TcpClient client = new TcpClient();
            client.Connect(ipep);
            Console.WriteLine("You are connected");
            using (NetworkStream stream = client.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                Task task = new Task(() =>
                {
                    while (true)
                    {
                        try
                        {
                            // Send data to server
                            Console.Write("Please enter an action: ");
                            string line = Console.ReadLine();
                            writer.Write(line);
                        }
                        catch (SocketException)
                        {
                            break;
                        }
                    }
                    Console.WriteLine("Server stopped");
                });
                task.Start();
                while (true)
                {
                    // Get result from server
                    string result = reader.ReadString();
                    Console.WriteLine(result);
                }
            }
            client.Close();
        }   
    }
}