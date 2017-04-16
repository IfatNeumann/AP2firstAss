using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;

namespace Server
{
    public class StartMazeCommand :ICommand
    {
        private IModel model;
        public StartMazeCommand(IModel model)
        {
            this.model = model;
        }
        private int numOfUsers = 2;
        private Socket client1;
        private Socket client2;
        private string name;
        private string maze1;
        private string maze2;

        public int NumOfUsers
        {
            get
            {
                return numOfUsers;
            }

            set
            {
                numOfUsers = value;
            }
        }
        public Socket Client1
        {
            get
            {
                return client1;
            }

            set
            {
                client1 = value;
            }
        }
        public Socket Client2
        {
            get
            {
                return client2;
            }

            set
            {
                client2 = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        public string Maze1
        {
            get
            {
                return maze1;
            }

            set
            {
                maze1 = value;
            }
        }
        public string Maze2
        {
            get
            {
                return maze2;
            }

            set
            {
                maze2 = value;
            }
        }


        public string Execute(string[] args, TcpClient client)
        {
            List<string> mazes = model.NamesList();
            Random rand = new Random();

            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = model.GenerateMaze(name, rows, cols);

            int k = maze.Rows + maze.Cols;
            string n = "kk";
            return n;



        }
    }
}
