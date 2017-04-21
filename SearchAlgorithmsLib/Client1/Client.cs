using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Configuration;

namespace Client1
{
    public class Client
    {
        private string line;
        private int port;
        private string ip;
        public Client(string p, string i)
        {
            this.port = int.Parse(p);
            this.ip = i;
        }

        public void Handle()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), this.port);
            TcpClient client = null;
            NetworkStream stream = null;
            BinaryWriter writer = null;
            BinaryReader reader = null;
            Task task;

            Action receiveThread = new Action(() =>
            {
                while (true)
                {
                    try
                    {
                        // Get result from server
                        string result = reader.ReadString();
                        Console.WriteLine(result);
                        string commandKey = line.Split(' ').First();
                        if (commandKey.Equals("generate") || commandKey.Equals("solve") ||
                                        commandKey.Equals("close"))
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
            
            while (true)
            {
                try
                {                  
                    // Send data to server
                    line = Console.ReadLine();
                    if (client==null)
                    {
                        //create new TcpClient
                        client = new TcpClient(); 
                        client.Connect(ipep);
                        Console.WriteLine("You are connected");
                        stream = client.GetStream();
                        writer = new BinaryWriter(stream);
                        reader = new BinaryReader(stream);
                        task = new Task(receiveThread);
                        task.Start();
                    }
                    writer.Write(line);
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