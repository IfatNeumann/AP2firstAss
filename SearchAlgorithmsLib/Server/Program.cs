using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string port = ConfigurationManager.AppSettings["port"];            
            IController con = new Controller();
            IClientHandler ch = new ClientHandler(con);//view
            IModel model = new Model(con);
            con.Model = model;
            con.Ch = ch;
            con.setDic();
            MyServer server = new MyServer(port, ch);
            server.StartConnection();
            //Console.ReadLine();
        }
    }
}
