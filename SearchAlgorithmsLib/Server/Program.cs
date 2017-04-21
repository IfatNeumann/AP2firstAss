using System.Configuration;

namespace Server
{
    /// <summary>
    /// contains the main function.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// the main function of the program.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            string port = ConfigurationManager.AppSettings["port"];
            IController con = new Controller();
            IClientHandler ch = new ClientHandler(con); // view
            IModel model = new Model(con);
            con.Model = model;
            con.Ch = ch;
            con.setDic();
            MyServer server = new MyServer(port, ch);
            server.StartConnection();
        }
    }
}