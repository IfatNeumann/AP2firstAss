using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// an interface of the commands
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>the output of the command</returns>
        string Execute(string[] args, TcpClient client = null);
    }
}