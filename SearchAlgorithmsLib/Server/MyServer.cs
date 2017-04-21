using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Server
{
    public class MyServer : IView
    {
        private int portNum;
        private IClientHandler ch;
        private TcpListener listener;
        public MyServer(string port, IClientHandler ch)
        {
            this.portNum = int.Parse(port);
            this.ch = ch;
        }

        public void StartConnection()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portNum);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
            task.Wait();
        }
        public void Stop()
        {
            listener.Stop();
        }
    }
}