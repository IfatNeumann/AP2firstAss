using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Configuration;


namespace Client1
{
    /// <summary>
    /// the main of the program
    /// </summary>
    public class Progrm
    {
        /// <summary>
        ///the main function
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            string port = ConfigurationManager.AppSettings["port"];
            string ip = ConfigurationManager.AppSettings["ip"];
            Client client = new Client(port,ip);
            client.Handle();
        }
    }
}
