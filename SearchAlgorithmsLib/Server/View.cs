using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Server
{
    public class View: IView
    {
        private int PORT_NUMBER;
        private IController controller;

        public IController Controller
        {
            get
            {
                return controller;
            }

            set
            {
                controller = value;
            }
        }

        public int MY_PORT_NUMBER
        {
            get
            {
                return PORT_NUMBER;
            }

            set
            {
                PORT_NUMBER = value;
            }
        }

        public View()
        {
        }

        public void StartConnection()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, MY_PORT_NUMBER);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);

            newsock.Listen(10);

            while (true)
            {
                Socket client = newsock.Accept();
               //PresenterForView presenter = new PresenterForView(this.controller);
               //Recive receiver = new Recive(client, controller);
               // Task.Factory.StartNew(receiver.Handle);
            }
        }
    }
}
