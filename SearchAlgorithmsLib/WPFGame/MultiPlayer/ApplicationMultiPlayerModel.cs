using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    using System.ComponentModel;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Windows;

    using MazeLib;

    using WPFGame.Properties;

    class ApplicationMultiPlayerModel : IMultiPlayerModel
    {
        private string name;

        private int rows;

        private int cols;

        private string stringMaze;

        private string solution;

        private Maze maze;

        private Point currPoint;

        private Point endPoint;

        private string line;
        public event PropertyChangedEventHandler PropertyChanged;

        public ApplicationMultiPlayerModel()
        {
            this.rows = Settings.Default.MazeRows;
            this.cols = Settings.Default.MazeCols;
        }

        public string MazeName
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name != value)
                {
                    this.name = value;
                }
            }
        }

        public int MazeRows
        {
            get
            {
                return this.rows;
            }

            set
            {
                if (this.rows != value)
                {
                    this.rows = value;
                }
            }
        }

        public int MazeCols
        {
            get
            {
                return this.cols;
            }

            set
            {
                if (this.cols != value)
                {
                    this.cols = value;
                }
            }
        }

        public string StringMaze
        {
            get
            {
                return this.stringMaze;
            }

            set
            {
                this.stringMaze = value;
            }
        }

        public Point CurrPoint
        {
            get
            {
                return this.currPoint;
            }

            set
            {
                this.currPoint = value;
                this.NotifyPropertyChanged("CurrPoint");
            }
        }

        public Point EndPoint
        {
            get
            {
                int iEnd = this.maze.GoalPos.Row;
                int jEnd = this.maze.GoalPos.Col;
                this.endPoint = new Point(iEnd, jEnd);
                return this.endPoint;
            }
        }

        public string Solution
        {
            get
            {
                return this.solution;
            }

            set
            {
                if (this.solution != value)
                {
                    this.solution = value;
                }
            }
        }

        public void StartGame()
        {

        }

        public int KeyPressed(char direction)
        {
            return 1;
        }

        protected void NotifyPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void Game()
        {
            IPEndPoint ipep = new IPEndPoint(
                IPAddress.Parse(Properties.Settings.Default.ServerIP),
                Properties.Settings.Default.ServerPort);
            TcpClient client = null;
            NetworkStream stream = null;
            BinaryWriter writer = null;
            BinaryReader reader = null;
            Task task;

            Action receiveThread = new Action(
                () =>
                    {
                        while (true)
                        {
                            try
                            {
                                // Get result from server
                                string result = reader.ReadString();
                                Console.WriteLine(result);
                                string commandKey = this.line.Split(' ').First();

                                // check the commands require  closing the connection 
                                if (commandKey.Equals("generate") || commandKey.Equals("solve")
                                    || (commandKey.Equals("close") && result.Equals(string.Empty)))
                                    /* if there was an invalid input for the close
                                     * command we want to stay connected */
                                {
                                    writer.Dispose();
                                    reader.Dispose();
                                    client.Close();
                                    Console.WriteLine("connection stopped");
                                    client = null;
                                    break;
                                }
                            }
                            catch (SocketException)
                            {
                                Console.WriteLine("exception - connection stopped");
                                break;
                            }
                        }
                    });
            Console.WriteLine("Welcome! please enter a command:");
            while (true)
            {
                try
                {
                    // Send data to server
                    this.line = Console.ReadLine();
                    if (client == null)
                    {
                        // create new TcpClient
                        client = new TcpClient();
                        client.Connect(ipep);
                        Console.WriteLine("You are connected");
                        stream = client.GetStream();
                        writer = new BinaryWriter(stream);
                        reader = new BinaryReader(stream);
                        task = new Task(receiveThread);
                        task.Start();
                    }

                    writer.Write(this.line);
                }
                catch (SocketException)
                {
                    Console.WriteLine("exception - connection stopped");
                    break;
                }
            }
        }
    }
}
