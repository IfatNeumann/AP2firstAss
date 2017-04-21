using System.Configuration;

namespace Client1
{
    /// <summary>
    /// the class that holds the main function
    /// </summary>
    public class Program
    {
        /// <summary>
        /// the main function
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            string port = ConfigurationManager.AppSettings["port"];
            string ip = ConfigurationManager.AppSettings["ip"];
            Client client = new Client(port, ip);
            client.Handle();
        }
    }
}