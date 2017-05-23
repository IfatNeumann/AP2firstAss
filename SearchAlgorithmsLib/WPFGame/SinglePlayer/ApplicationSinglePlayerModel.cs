namespace WPFGame
{
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

    using MazeLib;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using WPFGame.Properties;

    public class ApplicationSinglePlayerModel : ISinglePlayerModel
    {
        private string name;

        private int rows;

        private int cols;

        private string stringMaze;

        private string solution;

        private Maze maze;

        private Point currPoint;

        private Point endPoint;

        public event PropertyChangedEventHandler PropertyChanged;

        public ApplicationSinglePlayerModel()
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

        public int KeyPressed(char direction)
        {
            int iLocation = (int)this.CurrPoint.X, jLocation = (int)this.CurrPoint.Y;
            if ((this.EndPoint.X == iLocation) && (this.EndPoint.Y == jLocation))
            {
                return 1;
            }
            else
            {
                switch (direction)
                {
                    case 'l':
                        {
                            if (jLocation - 1 >= 0 && this.maze[iLocation, jLocation - 1] == CellType.Free)
                            {
                                this.CurrPoint = new Point(iLocation, jLocation - 1);
                            }

                            break;
                        }

                    case 'r':
                        {
                            if (jLocation + 1 < this.MazeCols && this.maze[iLocation, jLocation + 1] == CellType.Free)
                            {
                                this.CurrPoint = new Point(iLocation, jLocation + 1);
                            }

                            break;
                        }

                    case 'u':
                        {
                            if (iLocation - 1 >= 0 && this.maze[iLocation - 1, jLocation] == CellType.Free)
                            {
                                this.CurrPoint = new Point(iLocation - 1, jLocation);
                            }

                            break;
                        }

                    case 'd':
                        {
                            if (iLocation + 1 < this.MazeRows && this.maze[iLocation + 1, jLocation] == CellType.Free)
                            {
                                this.CurrPoint = new Point(iLocation + 1, jLocation);
                            }

                            break;
                        }
                }
                return 0;
            }
        }

        public void StartGame()
        {
            IPEndPoint ipep = new IPEndPoint(
                IPAddress.Parse(Properties.Settings.Default.ServerIP),
                Properties.Settings.Default.ServerPort);

            // create new TcpClient
            TcpClient client = new TcpClient();
             client.Connect(ipep);
            NetworkStream stream = client.GetStream();

            // Write to server
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write("generate " + this.name + " " + this.MazeRows.ToString() + " " + this.MazeCols.ToString());
            BinaryReader reader = new BinaryReader(stream);

            // Get result from server
            this.StringMaze = reader.ReadString();

            this.maze = Maze.FromJSON(this.StringMaze);
            int x = this.maze.InitialPos.Row;
            int y = this.maze.InitialPos.Col;
            Point curr = new Point(x, y);

            this.CurrPoint = curr;

            // solution
            writer.Write("solve " + this.name + " 1");
            JObject jSolution = JObject.Parse(reader.ReadString());
            this.Solution = jSolution["Solution"].ToString();

            // close connection
            writer.Dispose();
            reader.Dispose();
            client.Close();
        }

        protected void NotifyPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void SolveMaze()
        {
            Task t = new Task(
                () =>
                    {
                        // 0 - left, 1- right, 2- up, 3- down
                        int length = this.Solution.Length, index = length - 1;
                        while (index >= 0)
                        {
                            switch (this.Solution[index])
                            {
                                case '0':
                                    {
                                        this.KeyPressed('l');
                                        break;
                                    }

                                case '1':
                                    {
                                        this.KeyPressed('r');
                                        break;
                                    }

                                case '2':
                                    {
                                        this.KeyPressed('u');
                                        break;
                                    }

                                case '3':
                                    {
                                        this.KeyPressed('d');
                                        break;
                                    }
                            }

                            System.Threading.Thread.Sleep(500);
                            index--;
                        }
                    });
            t.Start();
        }

        public void InitStartPos()
        {
            this.maze = Maze.FromJSON(this.StringMaze);
            int x = this.maze.InitialPos.Row;
            int y = this.maze.InitialPos.Col;
            this.CurrPoint = new Point(x, y);
        }
    }
}