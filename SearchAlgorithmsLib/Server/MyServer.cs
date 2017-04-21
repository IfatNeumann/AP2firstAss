using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// the server.
    /// </summary>
    /// <seealso cref="Server.IView" />
    public class MyServer : IView
    {
        /// <summary>
        /// The port number
        /// </summary>
        private int portNum;

        /// <summary>
        /// The client handler.
        /// </summary>
        private IClientHandler ch;

        /// <summary>
        /// The Tcp listener
        /// </summary>
        private TcpListener listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyServer"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="ch">The ch.</param>
        public MyServer(string port, IClientHandler ch)
        {
            this.portNum = int.Parse(port);
            this.ch = ch;
        }

        /// <summary>
        /// Starts the connection.
        /// </summary>
        public void StartConnection()
        {
            string ip = ConfigurationManager.AppSettings["ip"].ToString();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), this.portNum);
            this.listener = new TcpListener(ep);
            this.listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(
                () =>
                    {
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

        /// <summary>
        /// Stops this listener.
        /// </summary>
        public void Stop()
        {
            this.listener.Stop();
        }
    }
}