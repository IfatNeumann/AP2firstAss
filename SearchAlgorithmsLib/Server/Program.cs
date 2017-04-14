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
            IController con = new Controller();
            IClientHandler ch = new ClientHandler(con);//view
            IModel model = new Model(con);
            con.Model = model;
            con.Ch = ch;
            con.setDic();
            MyServer server = new MyServer(555, ch);
            server.StartConnection();
            //Console.ReadLine();
        }
    }
}
