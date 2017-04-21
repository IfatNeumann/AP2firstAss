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
        private bool finish = false;
        private bool closeClient = false;
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
            NetworkStream stream = null;
            BinaryWriter writer = null;
            BinaryReader reader = null;
            Task task = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        // Get result from server
                        string result = reader.ReadString();
                        if(result.Equals(null))
                            Console.WriteLine("connection stopped");
                        Console.WriteLine(result);
                        finish = true;
                    }
                    catch (SocketException)
                    {
                        Console.WriteLine("connection stopped2");
                        break;
                    }
                    Console.Write("k");
                }
                Console.Write("t");
            });
            
            while (true)
            {
                try
                {
                    if (finish.Equals(true))
                    {
                        writer.Dispose();
                        reader.Dispose();
                        client.Close();
                        finish = false;
                        Console.WriteLine("connection stopped3");
                    }
                    // Send data to server
                    string line = Console.ReadLine();
                    if (!client.Connected)
                    {
                        client.Connect(ipep);
                        Console.WriteLine("You are connected");
                        stream = client.GetStream();
                        writer = new BinaryWriter(stream);
                        reader = new BinaryReader(stream);
                        task.Start();
                    }
                    writer.Write(line);
                    string commandKey = line.Split(' ').First();
                    
                }
                catch (SocketException)
                {
                    break;
                }              
            }
        }
    }
}