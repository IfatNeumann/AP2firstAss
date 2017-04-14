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
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to server
                Console.Write("Please enter an action: ");
                string line = Console.ReadLine();
                writer.Write(line);
                // Get result from server
                int result = reader.ReadInt32();
                Console.WriteLine("Result = {0}", result);
            }
            client.Close();

      

            void connectToServer()
            {

                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep);

                    Send sender = new Send(server);
                    Task.Factory.StartNew(sender.Handle);

                    while (true)
                    {
                        byte[] data = new byte[1024];
                        int recv = server.Receive(data);
                        string stringData = Encoding.ASCII.GetString(data, 0, recv);
                        Console.WriteLine(stringData);
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Unable to connect to server." + e.ToString());
                }
            }
        }
    }
}